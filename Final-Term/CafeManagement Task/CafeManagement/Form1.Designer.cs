using System.Windows.Forms;

namespace CafeManagement
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.GroupBox grpGender;
        private System.Windows.Forms.RadioButton rbMale;
        private System.Windows.Forms.RadioButton rbFemale;
        private System.Windows.Forms.Label lblMembership;
        private System.Windows.Forms.ComboBox cmbMembership;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.ListBox lstMenu;
        private System.Windows.Forms.DataGridView dgvOrder;
        private System.Windows.Forms.Button btnPlaceOrder;
        private System.Windows.Forms.Button btnSearch;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblCustomerName = new Label();
            txtName = new TextBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            grpGender = new GroupBox();
            rbFemale = new RadioButton();
            rbMale = new RadioButton();
            lblMembership = new Label();
            cmbMembership = new ComboBox();
            lblMenu = new Label();
            lstMenu = new ListBox();
            dgvOrder = new DataGridView();
            btnPlaceOrder = new Button();
            btnSearch = new Button();
            grpGender.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrder).BeginInit();
            SuspendLayout();
            // 
            // lblCustomerName
            // 
            lblCustomerName.AutoSize = true;
            lblCustomerName.Location = new Point(12, 15);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(119, 20);
            lblCustomerName.TabIndex = 0;
            lblCustomerName.Text = "Customer Name:";
            // 
            // txtName
            // 
            txtName.Location = new Point(117, 12);
            txtName.Name = "txtName";
            txtName.Size = new Size(240, 27);
            txtName.TabIndex = 1;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(12, 50);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(111, 20);
            lblPhone.TabIndex = 2;
            lblPhone.Text = "Phone Number:";
            lblPhone.Click += lblPhone_Click;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(117, 47);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(240, 27);
            txtPhone.TabIndex = 3;
            // 
            // grpGender
            // 
            grpGender.Controls.Add(rbFemale);
            grpGender.Controls.Add(rbMale);
            grpGender.Location = new Point(12, 82);
            grpGender.Name = "grpGender";
            grpGender.Size = new Size(200, 50);
            grpGender.TabIndex = 4;
            grpGender.TabStop = false;
            grpGender.Text = "Gender";
            // 
            // rbFemale
            // 
            rbFemale.AutoSize = true;
            rbFemale.Location = new Point(102, 22);
            rbFemale.Name = "rbFemale";
            rbFemale.Size = new Size(78, 24);
            rbFemale.TabIndex = 1;
            rbFemale.Text = "Female";
            rbFemale.UseVisualStyleBackColor = true;
            // 
            // rbMale
            // 
            rbMale.AutoSize = true;
            rbMale.Checked = true;
            rbMale.Location = new Point(16, 22);
            rbMale.Name = "rbMale";
            rbMale.Size = new Size(63, 24);
            rbMale.TabIndex = 0;
            rbMale.TabStop = true;
            rbMale.Text = "Male";
            rbMale.UseVisualStyleBackColor = true;
            // 
            // lblMembership
            // 
            lblMembership.AutoSize = true;
            lblMembership.Location = new Point(12, 145);
            lblMembership.Name = "lblMembership";
            lblMembership.Size = new Size(130, 20);
            lblMembership.TabIndex = 6;
            lblMembership.Text = "Membership Type:";
            // 
            // cmbMembership
            // 
            cmbMembership.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMembership.FormattingEnabled = true;
            cmbMembership.Items.AddRange(new object[] { "Regular", "Premium" });
            cmbMembership.Location = new Point(120, 142);
            cmbMembership.Name = "cmbMembership";
            cmbMembership.Size = new Size(121, 28);
            cmbMembership.TabIndex = 7;
            // 
            // lblMenu
            // 
            lblMenu.AutoSize = true;
            lblMenu.Location = new Point(12, 185);
            lblMenu.Name = "lblMenu";
            lblMenu.Size = new Size(49, 20);
            lblMenu.TabIndex = 8;
            lblMenu.Text = "Menu:";
            // 
            // lstMenu
            // 
            lstMenu.FormattingEnabled = true;
            lstMenu.Items.AddRange(new object[] { "Coffee", "Burger", "French Fry", "Juice", "Sandwitch", "Pasta", "Water" });
            lstMenu.Location = new Point(12, 203);
            lstMenu.Name = "lstMenu";
            lstMenu.Size = new Size(245, 164);
            lstMenu.TabIndex = 9;
            lstMenu.DoubleClick += lstMenu_DoubleClick;
            // 
            // dgvOrder
            // 
            dgvOrder.AllowUserToAddRows = false;
            dgvOrder.AllowUserToDeleteRows = false;
            dgvOrder.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrder.Location = new Point(280, 12);
            dgvOrder.Name = "dgvOrder";
            dgvOrder.RowHeadersWidth = 51;
            dgvOrder.Size = new Size(480, 360);
            dgvOrder.TabIndex = 10;
            dgvOrder.CellContentClick += dgvOrder_CellContentClick;
            // 
            // btnPlaceOrder
            // 
            btnPlaceOrder.Location = new Point(12, 385);
            btnPlaceOrder.Name = "btnPlaceOrder";
            btnPlaceOrder.Size = new Size(120, 30);
            btnPlaceOrder.TabIndex = 11;
            btnPlaceOrder.Text = "Place Order";
            btnPlaceOrder.UseVisualStyleBackColor = true;
            btnPlaceOrder.Click += btnPlaceOrder_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(147, 385);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(110, 30);
            btnSearch.TabIndex = 12;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // Form1
            // 
            ClientSize = new Size(774, 427);
            Controls.Add(btnSearch);
            Controls.Add(btnPlaceOrder);
            Controls.Add(dgvOrder);
            Controls.Add(lstMenu);
            Controls.Add(lblMenu);
            Controls.Add(cmbMembership);
            Controls.Add(lblMembership);
            Controls.Add(grpGender);
            Controls.Add(txtPhone);
            Controls.Add(lblPhone);
            Controls.Add(txtName);
            Controls.Add(lblCustomerName);
            Name = "Form1";
            Text = "Cafe Management";
            Load += Form1_Load;
            grpGender.ResumeLayout(false);
            grpGender.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrder).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
    }
}
