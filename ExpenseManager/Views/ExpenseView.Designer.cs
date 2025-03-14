namespace ExpenseManager
{
    partial class ExpenseView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtDescription = new TextBox();
            txtAmount = new TextBox();
            cmbCategory = new ComboBox();
            dtpDate = new DateTimePicker();
            btnAdd = new Button();
            dgvExpenses = new DataGridView();
            lblTotal = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvExpenses).BeginInit();
            SuspendLayout();
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(31, 172);
            txtDescription.Name = "txtDescription";
            txtDescription.PlaceholderText = "Nội dung";
            txtDescription.Size = new Size(125, 27);
            txtDescription.TabIndex = 0;
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(31, 217);
            txtAmount.Name = "txtAmount";
            txtAmount.PlaceholderText = "Số tiền";
            txtAmount.Size = new Size(125, 27);
            txtAmount.TabIndex = 1;
            // 
            // cmbCategory
            // 
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(31, 121);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(151, 28);
            cmbCategory.TabIndex = 2;
            // 
            // dtpDate
            // 
            dtpDate.Location = new Point(31, 72);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(250, 27);
            dtpDate.TabIndex = 3;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(31, 270);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // dgvExpenses
            // 
            dgvExpenses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvExpenses.Location = new Point(390, 72);
            dgvExpenses.Name = "dgvExpenses";
            dgvExpenses.RowHeadersWidth = 51;
            dgvExpenses.Size = new Size(300, 188);
            dgvExpenses.TabIndex = 5;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(390, 279);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(0, 20);
            lblTotal.TabIndex = 6;
            // 
            // ExpenseView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblTotal);
            Controls.Add(dgvExpenses);
            Controls.Add(btnAdd);
            Controls.Add(dtpDate);
            Controls.Add(cmbCategory);
            Controls.Add(txtAmount);
            Controls.Add(txtDescription);
            Name = "ExpenseView";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvExpenses).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtDescription;
        private TextBox txtAmount;
        private ComboBox cmbCategory;
        private DateTimePicker dtpDate;
        private Button btnAdd;
        private DataGridView dgvExpenses;
        private Label lblTotal;
    }
}
