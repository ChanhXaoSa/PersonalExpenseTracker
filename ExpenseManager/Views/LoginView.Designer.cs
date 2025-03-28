namespace ExpenseManager.Views
{
    partial class LoginView
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
            tableLayoutPanel1 = new TableLayoutPanel();
            panel5 = new Panel();
            txtPassword = new TextBox();
            lblPassword = new Label();
            panel4 = new Panel();
            txtUsername = new TextBox();
            lblUsername = new Label();
            panel1 = new Panel();
            pbLogo = new PictureBox();
            panel2 = new Panel();
            linkLabel1 = new LinkLabel();
            chkRememberMe = new CheckBox();
            panel3 = new Panel();
            btnLogin = new Button();
            tableLayoutPanel1.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panel5, 0, 1);
            tableLayoutPanel1.Controls.Add(panel4, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 248);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(406, 144);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel5
            // 
            panel5.Controls.Add(txtPassword);
            panel5.Controls.Add(lblPassword);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(3, 75);
            panel5.Name = "panel5";
            panel5.Size = new Size(400, 66);
            panel5.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Cursor = Cursors.IBeam;
            txtPassword.Location = new Point(46, 32);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(309, 27);
            txtPassword.TabIndex = 3;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 163);
            lblPassword.ForeColor = SystemColors.ControlDarkDark;
            lblPassword.Location = new Point(42, 9);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(69, 20);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Mật khẩu";
            // 
            // panel4
            // 
            panel4.Controls.Add(txtUsername);
            panel4.Controls.Add(lblUsername);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(3, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(400, 66);
            panel4.TabIndex = 0;
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUsername.Cursor = Cursors.IBeam;
            txtUsername.Location = new Point(40, 33);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(315, 27);
            txtUsername.TabIndex = 1;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 163);
            lblUsername.ForeColor = SystemColors.ControlDarkDark;
            lblUsername.Location = new Point(36, 10);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(179, 20);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Tên đăng nhập hoặc email";
            // 
            // panel1
            // 
            panel1.Controls.Add(pbLogo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(406, 248);
            panel1.TabIndex = 1;
            // 
            // pbLogo
            // 
            pbLogo.Anchor = AnchorStyles.Top;
            pbLogo.Location = new Point(52, 12);
            pbLogo.Name = "pbLogo";
            pbLogo.Size = new Size(278, 206);
            pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLogo.TabIndex = 0;
            pbLogo.TabStop = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(linkLabel1);
            panel2.Controls.Add(chkRememberMe);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 392);
            panel2.Name = "panel2";
            panel2.Size = new Size(406, 44);
            panel2.TabIndex = 2;
            // 
            // linkLabel1
            // 
            linkLabel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(242, 11);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(116, 20);
            linkLabel1.TabIndex = 1;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Quên mật khẩu?";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // chkRememberMe
            // 
            chkRememberMe.AutoSize = true;
            chkRememberMe.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 163);
            chkRememberMe.ForeColor = SystemColors.ControlDarkDark;
            chkRememberMe.Location = new Point(52, 10);
            chkRememberMe.Name = "chkRememberMe";
            chkRememberMe.Size = new Size(123, 24);
            chkRememberMe.TabIndex = 0;
            chkRememberMe.Text = "Nhớ mật khẩu";
            chkRememberMe.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnLogin);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 441);
            panel3.Name = "panel3";
            panel3.Size = new Size(406, 87);
            panel3.TabIndex = 3;
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnLogin.BackColor = Color.LimeGreen;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(52, 16);
            btnLogin.Margin = new Padding(0);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(306, 42);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = false;
            // 
            // LoginView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(406, 528);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel1);
            Name = "LoginView";
            Text = "LoginView";
            tableLayoutPanel1.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel5;
        private Panel panel4;
        private TextBox txtUsername;
        private Label lblUsername;
        private PictureBox pbLogo;
        private CheckBox chkRememberMe;
        private TextBox txtPassword;
        private Label lblPassword;
        private LinkLabel linkLabel1;
        private Button btnLogin;
    }
}