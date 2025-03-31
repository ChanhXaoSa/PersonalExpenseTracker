namespace ExpenseManager.Views
{
    partial class RegisterView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            pbLogo = new PictureBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel5 = new Panel();
            txtUsername = new TextBox();
            lblPassword = new Label();
            panel4 = new Panel();
            txtEmail = new TextBox();
            lblUsername = new Label();
            panel2 = new Panel();
            txtPassword = new TextBox();
            label2 = new Label();
            panel6 = new Panel();
            txtConfirmPassword = new TextBox();
            label3 = new Label();
            panel3 = new Panel();
            label1 = new Label();
            llbLogin = new LinkLabel();
            btnRegister = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            panel6.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(pbLogo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(432, 248);
            panel1.TabIndex = 2;
            // 
            // pbLogo
            // 
            pbLogo.Anchor = AnchorStyles.Top;
            pbLogo.Location = new Point(69, 12);
            pbLogo.Name = "pbLogo";
            pbLogo.Size = new Size(278, 206);
            pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLogo.TabIndex = 0;
            pbLogo.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panel5, 0, 1);
            tableLayoutPanel1.Controls.Add(panel4, 0, 0);
            tableLayoutPanel1.Controls.Add(panel2, 0, 2);
            tableLayoutPanel1.Controls.Add(panel6, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 248);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Size = new Size(432, 278);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // panel5
            // 
            panel5.Controls.Add(txtUsername);
            panel5.Controls.Add(lblPassword);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(3, 72);
            panel5.Name = "panel5";
            panel5.Size = new Size(426, 63);
            panel5.TabIndex = 1;
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUsername.Cursor = Cursors.IBeam;
            txtUsername.Location = new Point(30, 33);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(370, 27);
            txtUsername.TabIndex = 3;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 163);
            lblPassword.ForeColor = SystemColors.ControlDarkDark;
            lblPassword.Location = new Point(30, 10);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(104, 20);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Tên đăng nhập";
            // 
            // panel4
            // 
            panel4.Controls.Add(txtEmail);
            panel4.Controls.Add(lblUsername);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(3, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(426, 63);
            panel4.TabIndex = 0;
            // 
            // txtEmail
            // 
            txtEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtEmail.Cursor = Cursors.IBeam;
            txtEmail.Location = new Point(30, 33);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(370, 27);
            txtEmail.TabIndex = 1;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 163);
            lblUsername.ForeColor = SystemColors.ControlDarkDark;
            lblUsername.Location = new Point(30, 10);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(45, 20);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Email";
            // 
            // panel2
            // 
            panel2.Controls.Add(txtPassword);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 141);
            panel2.Name = "panel2";
            panel2.Size = new Size(426, 63);
            panel2.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Cursor = Cursors.IBeam;
            txtPassword.Location = new Point(30, 29);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(370, 27);
            txtPassword.TabIndex = 3;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 163);
            label2.ForeColor = SystemColors.ControlDarkDark;
            label2.Location = new Point(26, 6);
            label2.Name = "label2";
            label2.Size = new Size(69, 20);
            label2.TabIndex = 2;
            label2.Text = "Mật khẩu";
            // 
            // panel6
            // 
            panel6.Controls.Add(txtConfirmPassword);
            panel6.Controls.Add(label3);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(3, 210);
            panel6.Name = "panel6";
            panel6.Size = new Size(426, 65);
            panel6.TabIndex = 3;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtConfirmPassword.Cursor = Cursors.IBeam;
            txtConfirmPassword.Location = new Point(30, 30);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(370, 27);
            txtConfirmPassword.TabIndex = 3;
            txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 163);
            label3.ForeColor = SystemColors.ControlDarkDark;
            label3.Location = new Point(26, 7);
            label3.Name = "label3";
            label3.Size = new Size(129, 20);
            label3.TabIndex = 2;
            label3.Text = "Nhập lại mật khẩu";
            // 
            // panel3
            // 
            panel3.Controls.Add(label1);
            panel3.Controls.Add(llbLogin);
            panel3.Controls.Add(btnRegister);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 532);
            panel3.Name = "panel3";
            panel3.Size = new Size(432, 104);
            panel3.TabIndex = 4;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom;
            label1.AutoSize = true;
            label1.Location = new Point(119, 75);
            label1.Name = "label1";
            label1.Size = new Size(124, 20);
            label1.TabIndex = 2;
            label1.Text = "Đã có tài khoản ?";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // llbLogin
            // 
            llbLogin.Anchor = AnchorStyles.Bottom;
            llbLogin.AutoSize = true;
            llbLogin.Location = new Point(239, 75);
            llbLogin.Name = "llbLogin";
            llbLogin.Size = new Size(82, 20);
            llbLogin.TabIndex = 1;
            llbLogin.TabStop = true;
            llbLogin.Text = "Đăng nhập";
            llbLogin.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnRegister
            // 
            btnRegister.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnRegister.BackColor = Color.LimeGreen;
            btnRegister.Cursor = Cursors.Hand;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnRegister.ForeColor = Color.White;
            btnRegister.Location = new Point(33, 16);
            btnRegister.Margin = new Padding(0);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(370, 42);
            btnRegister.TabIndex = 0;
            btnRegister.Text = "Đăng ký";
            btnRegister.UseVisualStyleBackColor = false;
            // 
            // RegisterView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(432, 636);
            Controls.Add(panel3);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel1);
            Name = "RegisterView";
            Text = "RegisterView";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pbLogo;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel5;
        private TextBox txtUsername;
        private Label lblPassword;
        private Panel panel4;
        private TextBox txtEmail;
        private Label lblUsername;
        private Panel panel3;
        private Label label1;
        private LinkLabel llbLogin;
        private Button btnRegister;
        private Panel panel2;
        private TextBox txtPassword;
        private Label label2;
        private Panel panel6;
        private TextBox txtConfirmPassword;
        private Label label3;
    }
}