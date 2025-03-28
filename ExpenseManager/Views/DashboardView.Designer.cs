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
            comboChartPictureBox = new PictureBox();
            panel2 = new Panel();
            chartPictureBox = new PictureBox();
            panel4 = new Panel();
            panel5 = new Panel();
            lb5Lastest = new ListBox();
            panel6 = new Panel();
            lvDashboardExpenses = new ListView();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)comboChartPictureBox).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartPictureBox).BeginInit();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(lblTotal);
            panel1.Controls.Add(lblUsername);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(142, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(710, 43);
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
            lblUsername.Location = new Point(398, 11);
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
            tableLayoutPanel1.Location = new Point(142, 43);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(710, 325);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Controls.Add(comboChartPictureBox);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(358, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(349, 319);
            panel3.TabIndex = 1;
            // 
            // comboChartPictureBox
            // 
            comboChartPictureBox.Dock = DockStyle.Fill;
            comboChartPictureBox.Location = new Point(0, 0);
            comboChartPictureBox.Name = "comboChartPictureBox";
            comboChartPictureBox.Size = new Size(349, 319);
            comboChartPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            comboChartPictureBox.TabIndex = 0;
            comboChartPictureBox.TabStop = false;
            comboChartPictureBox.Resize += ComboChartPictureBox_Resize;
            // 
            // panel2
            // 
            panel2.Controls.Add(chartPictureBox);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(349, 319);
            panel2.TabIndex = 0;
            // 
            // chartPictureBox
            // 
            chartPictureBox.Dock = DockStyle.Fill;
            chartPictureBox.Location = new Point(0, 0);
            chartPictureBox.Name = "chartPictureBox";
            chartPictureBox.Size = new Size(349, 319);
            chartPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            chartPictureBox.TabIndex = 0;
            chartPictureBox.TabStop = false;
            chartPictureBox.Resize += ChartPictureBox_Resize;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Left;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(142, 1027);
            panel4.TabIndex = 2;
            // 
            // panel5
            // 
            panel5.Controls.Add(lb5Lastest);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(142, 368);
            panel5.Name = "panel5";
            panel5.Size = new Size(710, 168);
            panel5.TabIndex = 3;
            // 
            // lb5Lastest
            // 
            lb5Lastest.Dock = DockStyle.Fill;
            lb5Lastest.FormattingEnabled = true;
            lb5Lastest.Location = new Point(0, 0);
            lb5Lastest.Name = "lb5Lastest";
            lb5Lastest.Size = new Size(710, 168);
            lb5Lastest.TabIndex = 0;
            // 
            // panel6
            // 
            panel6.Controls.Add(lvDashboardExpenses);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(142, 536);
            panel6.Name = "panel6";
            panel6.Size = new Size(710, 491);
            panel6.TabIndex = 4;
            // 
            // lvDashboardExpenses
            // 
            lvDashboardExpenses.Dock = DockStyle.Fill;
            lvDashboardExpenses.Location = new Point(0, 0);
            lvDashboardExpenses.Name = "lvDashboardExpenses";
            lvDashboardExpenses.Size = new Size(710, 491);
            lvDashboardExpenses.TabIndex = 1;
            lvDashboardExpenses.UseCompatibleStateImageBehavior = false;
            lvDashboardExpenses.View = View.Details;
            // 
            // DashboardView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(852, 1027);
            Controls.Add(panel6);
            Controls.Add(panel5);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel1);
            Controls.Add(panel4);
            Name = "DashboardView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DashboardView";
            WindowState = FormWindowState.Maximized;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)comboChartPictureBox).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartPictureBox).EndInit();
            panel5.ResumeLayout(false);
            panel6.ResumeLayout(false);
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
        private Panel panel4;
        private Panel panel5;
        private Panel panel6;
        private ListView lvDashboardExpenses;
        private PictureBox comboChartPictureBox;
        private ListBox lb5Lastest;
    }
}