using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace CafeManagement
{
    public partial class Form1 : Form
    {
        private readonly string conString =
  @"Data Source=.\SQLEXPRESS;Initial Catalog=CafeManagement;Integrated Security=True;TrustServerCertificate=True";
        private readonly BindingList<OrderItem> bindingItems = new BindingList<OrderItem>();
        private readonly Order currentOrder = new Order();

        private readonly Dictionary<string, decimal> priceList = new Dictionary<string, decimal>
        {
            { "Coffee", 3.50m },
            { "Burger", 6.25m },
            { "French Fry", 2.50m },
            { "Juice", 2.75m },
            { "Sandwitch", 5.00m },
            { "Pasta", 7.00m },
            { "Water", 1.00m }
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvOrder.AutoGenerateColumns = false;
            dgvOrder.Columns.Clear();

            var colItem = new DataGridViewTextBoxColumn { HeaderText = "Item", DataPropertyName = "ItemName", ReadOnly = true };
            var colQty = new DataGridViewTextBoxColumn { HeaderText = "Qty", DataPropertyName = "Quantity" };
            var colPrice = new DataGridViewTextBoxColumn { HeaderText = "Price", DataPropertyName = "Price", ReadOnly = true };
            var colTotal = new DataGridViewTextBoxColumn { HeaderText = "Total", DataPropertyName = "Total", ReadOnly = true };
            var colDelete = new DataGridViewButtonColumn { HeaderText = "Action", Text = "Delete", UseColumnTextForButtonValue = true };

            dgvOrder.Columns.AddRange(new DataGridViewColumn[] { colItem, colQty, colPrice, colTotal, colDelete });
            dgvOrder.DataSource = bindingItems;

            if (cmbMembership.Items.Count > 0) cmbMembership.SelectedIndex = 0;
        }

        private void lstMenu_DoubleClick(object sender, EventArgs e)
        {
            if (lstMenu.SelectedItem == null) return;
            var name = lstMenu.SelectedItem.ToString();
            priceList.TryGetValue(name, out var price);
            var item = new OrderItem { ItemName = name, Quantity = 1, Price = price };
            currentOrder.Add(item);
            bindingItems.Add(item);
        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == dgvOrder.Columns.Count - 1)
            {
                currentOrder.RemoveAt(e.RowIndex);
                bindingItems.RemoveAt(e.RowIndex);
            }
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            var phone = txtPhone.Text.Trim();
            var gender = rbMale.Checked ? "Male" : "Female";
            var membership = cmbMembership.SelectedItem?.ToString() ?? "Regular";

            if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Enter customer name."); return; }
            if (bindingItems.Count == 0) { MessageBox.Show("No items selected."); return; }

            using var conn = new SqlConnection(conString);
            
            conn.Open();

            using (var cmdCheck = new SqlCommand("SELECT COUNT(*) FROM Customers WHERE Name = @Name", conn))
            {
                cmdCheck.Parameters.AddWithValue("@Name", name);
                var exists = (int)cmdCheck.ExecuteScalar();
                if (exists > 0) { MessageBox.Show("Customer already Ordered"); return; }
            }

            using var transaction = conn.BeginTransaction();
            try
            {
                int customerId;
                using (var cmdCust = new SqlCommand("INSERT INTO Customers (Name, Phone, Gender, Membership) OUTPUT INSERTED.Id VALUES (@Name, @Phone, @Gender, @Membership)", conn, transaction))
                {
                    cmdCust.Parameters.AddWithValue("@Name", name);
                    cmdCust.Parameters.AddWithValue("@Phone", phone);
                    cmdCust.Parameters.AddWithValue("@Gender", gender);
                    cmdCust.Parameters.AddWithValue("@Membership", membership);
                    customerId = (int)cmdCust.ExecuteScalar();
                }

                int orderId;
                using (var cmdOrder = new SqlCommand("INSERT INTO Orders (CustomerId, OrderDate) OUTPUT INSERTED.Id VALUES (@CustomerId, @OrderDate)", conn, transaction))
                {
                    cmdOrder.Parameters.AddWithValue("@CustomerId", customerId);
                    cmdOrder.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                    orderId = (int)cmdOrder.ExecuteScalar();
                }

                using (var cmdItem = new SqlCommand("INSERT INTO OrderItems (OrderId, ItemName, Quantity, Price) VALUES (@OrderId, @ItemName, @Quantity, @Price)", conn, transaction))
                {
                    cmdItem.Parameters.Add(new SqlParameter("@OrderId", SqlDbType.Int));
                    cmdItem.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 200));
                    cmdItem.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int));
                    cmdItem.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal) { Precision = 18, Scale = 2 });

                    foreach (var it in currentOrder.Items)
                    {
                        cmdItem.Parameters["@OrderId"].Value = orderId;
                        cmdItem.Parameters["@ItemName"].Value = it.ItemName;
                        cmdItem.Parameters["@Quantity"].Value = it.Quantity;
                        cmdItem.Parameters["@Price"].Value = it.Price;
                        cmdItem.ExecuteNonQuery();
                    }
                }

                transaction.Commit();

                var msg = $"Order placed successfully.\r\nCustomer: {name}\r\nPhone: {phone}\r\nGender: {gender}\r\nMembership: {membership}\r\nItems:\r\n";
                decimal grand = 0;
                foreach (var it in currentOrder.Items) { msg += $"{it.ItemName} x{it.Quantity} @ {it.Price:C} = {it.Total:C}\r\n"; grand += it.Total; }
                msg += $"Total: {grand:C}";
                MessageBox.Show(msg);

                while (currentOrder.Count > 0) currentOrder.RemoveAt(0);
                bindingItems.Clear();
            }
            catch (Exception ex)
            {
                try { transaction.Rollback(); } catch { }
                MessageBox.Show("Error placing order: " + ex.Message);
            }
            finally { conn.Close(); }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Enter name to search."); return; }

            using var conn = new SqlConnection(conString);
            conn.Open();

            using (var cmd = new SqlCommand("SELECT TOP 1 Id, Name, Phone, Gender, Membership FROM Customers WHERE Name = @Name", conn))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                using var rdr = cmd.ExecuteReader();
                if (!rdr.Read()) { MessageBox.Show("Customer not found."); return; }

                var id = rdr.GetInt32(0);
                var phone = rdr.IsDBNull(2) ? "" : rdr.GetString(2);
                var gender = rdr.IsDBNull(3) ? "" : rdr.GetString(3);
                var membership = rdr.IsDBNull(4) ? "" : rdr.GetString(4);
                rdr.Close();

                using var cmdOrder = new SqlCommand("SELECT TOP 1 Id, OrderDate FROM Orders WHERE CustomerId = @Cid ORDER BY OrderDate DESC", conn);
                cmdOrder.Parameters.AddWithValue("@Cid", id);
                using var rdrOrder = cmdOrder.ExecuteReader();
                if (!rdrOrder.Read()) { MessageBox.Show($"Customer: {name}\r\nPhone: {phone}\r\nGender: {gender}\r\nMembership: {membership}\r\nNo orders found."); return; }
                var orderId = rdrOrder.GetInt32(0);
                var orderDate = rdrOrder.GetDateTime(1);
                rdrOrder.Close();

                using var cmdItems = new SqlCommand("SELECT ItemName, Quantity, Price FROM OrderItems WHERE OrderId = @Oid", conn);
                cmdItems.Parameters.AddWithValue("@Oid", orderId);
                using var rdrItems = cmdItems.ExecuteReader();
                var msg = $"Customer: {name}\r\nPhone: {phone}\r\nGender: {gender}\r\nMembership: {membership}\r\nLast Order ({orderDate}):\r\n";
                decimal grand = 0;
                while (rdrItems.Read()) { var itName = rdrItems.GetString(0); var qty = rdrItems.GetInt32(1); var pr = rdrItems.GetDecimal(2); msg += $"{itName} x{qty} @ {pr:C} = {(qty * pr):C}\r\n"; grand += (qty * pr); }
                msg += $"Total: {grand:C}";
                MessageBox.Show(msg);
            }
        }

        private void lblPhone_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Enter customer name."); return; }
            if (bindingItems.Count == 0) { MessageBox.Show("No items to add."); return; }

            using var conn = new SqlConnection(conString);
            conn.Open();

            // Check whether customer exists
            using (var cmdCheck = new SqlCommand("SELECT Id FROM Customers WHERE Name = @Name", conn))
            {
                cmdCheck.Parameters.AddWithValue("@Name", name);
                var idObj = cmdCheck.ExecuteScalar();
                if (idObj == null)
                {
                    MessageBox.Show("Customer not found. Use Place Order to create a new customer.");
                    return;
                }

                var customerId = Convert.ToInt32(idObj);

                using var transaction = conn.BeginTransaction();
                try
                {
                    int orderId;
                    using (var cmdOrder = new SqlCommand("INSERT INTO Orders (CustomerId, OrderDate) OUTPUT INSERTED.Id VALUES (@CustomerId, @OrderDate)", conn, transaction))
                    {
                        cmdOrder.Parameters.AddWithValue("@CustomerId", customerId);
                        cmdOrder.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                        orderId = (int)cmdOrder.ExecuteScalar();
                    }

                    using (var cmdItem = new SqlCommand("INSERT INTO OrderItems (OrderId, ItemName, Quantity, Price) VALUES (@OrderId, @ItemName, @Quantity, @Price)", conn, transaction))
                    {
                        cmdItem.Parameters.Add(new SqlParameter("@OrderId", SqlDbType.Int));
                        cmdItem.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 200));
                        cmdItem.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int));
                        cmdItem.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal) { Precision = 18, Scale = 2 });

                        foreach (var it in currentOrder.Items)
                        {
                            cmdItem.Parameters["@OrderId"].Value = orderId;
                            cmdItem.Parameters["@ItemName"].Value = it.ItemName;
                            cmdItem.Parameters["@Quantity"].Value = it.Quantity;
                            cmdItem.Parameters["@Price"].Value = it.Price;
                            cmdItem.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();

                    var msg = $"Updated customer: {name}\r\nNew Order ({DateTime.Now}):\r\n";
                    decimal grand = 0;
                    foreach (var it in currentOrder.Items)
                    {
                        msg += $"{it.ItemName} x{it.Quantity} @ {it.Price:C} = {it.Total:C}\r\n";
                        grand += it.Total;
                    }
                    msg += $"Total: {grand:C}";
                    MessageBox.Show(msg);

                    while (currentOrder.Count > 0) currentOrder.RemoveAt(0);
                    bindingItems.Clear();
                }
                catch (Exception ex)
                {
                    try { transaction.Rollback(); } catch { }
                    MessageBox.Show("Error updating order: " + ex.Message);
                }
                finally { conn.Close(); }
            }
        }
    }
}
