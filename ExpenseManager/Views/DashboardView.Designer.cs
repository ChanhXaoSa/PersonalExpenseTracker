namespace ExpenseManager.Views
{
    partial class DashboardView
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
            lblTotal = new Label();
            lblUsername = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel3 = new Panel();
            panel2 = new Panel();
            chartPictureBox = new PictureBox();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartPictureBox).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(lblTotal);
            panel1.Controls.Add(lblUsername);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(852, 43);
            panel1.TabIndex = 0;
            // 
            // lblTotal
            // 
            lblTotal.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(9, 11);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(123, 20);
            lblTotal.TabIndex = 1;
            lblTotal.Text = "Tổng chi tiêu : 0đ";
            // 
            // lblUsername
            // 
            lblUsername.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lblUsername.Location = new Point(540, 11);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(300, 20);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Hello, User";
            lblUsername.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(panel3, 1, 0);
            tableLayoutPanel1.Controls.Add(panel2, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 43);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(852, 325);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(429, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(420, 319);
            panel3.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.Controls.Add(chartPictureBox);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(420, 319);
            panel2.TabIndex = 0;
            // 
            // chartPictureBox
            // 
            chartPictureBox.Dock = DockStyle.Fill;
            chartPictureBox.Location = new Point(0, 0);
            chartPictureBox.Name = "chartPictureBox";
            chartPictureBox.Size = new Size(420, 319);
            chartPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            chartPictureBox.TabIndex = 0;
            chartPictureBox.TabStop = false;
            chartPictureBox.Resize += ChartPictureBox_Resize;
            // 
            // DashboardView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(852, 729);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel1);
            Name = "DashboardView";
            Text = "DashboardView";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label lblTotal;
        private Label lblUsername;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel3;
        private Panel panel2;
        private PictureBox chartPictureBox;
    }
}