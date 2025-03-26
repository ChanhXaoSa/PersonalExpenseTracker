using CsvHelper;
using ExpenseManager.Models;
using ExpenseManager.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseManager.Views
{
    public partial class ExpenseUpdateView : Form, IExpenseView
    {
        private ExpensePresenter? presenter;
        private readonly ListView? lvExpenses;
        private List<Expense>? currentExpenses;

        public ExpenseUpdateView(ExpensePresenter? _)
        {
            try
            {
                InitializeComponent();

                Size = new Size(800, 500);
                Text = "Sửa/Xóa Chi Tiêu";

                lvExpenses = new ListView
                {
                    Location = new Point(20, 20),
                    Size = new Size(740, 300),
                    View = View.Details,
                    FullRowSelect = true,
                    GridLines = true,
                    BackColor = Color.White,
                };

                lvExpenses.Columns.Add("ID", 50);
                lvExpenses.Columns.Add("Mô tả", 200);
                lvExpenses.Columns.Add("Số tiền", 100);
                lvExpenses.Columns.Add("Thời gian", 150);
                lvExpenses.Columns.Add("Danh mục", 100);
                lvExpenses.Columns.Add("Người dùng", 100);

                var btnEdit = new Button { Text = "Sửa", Location = new Point(20, 340), Width = 100 };
                var btnDelete = new Button { Text = "Xóa", Location = new Point(130, 340), Width = 100 };
                var btnExport = new Button { Text = "Xuất Excel", Location = new Point(240, 340), Width = 100 };

                btnEdit.Click += BtnEdit_Click;
                btnDelete.Click += BtnDelete_Click;
                btnExport.Click += BtnExport_Click;

                Controls.AddRange([lvExpenses, btnEdit, btnDelete, btnExport]);

                Debug.WriteLine("ExpenseUpdateView: Constructor hoàn tất");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi trong constructor: {ex.Message}");
                MessageBox.Show($"Lỗi khởi tạo ExpenseUpdateView: {ex.Message}");
            }
        }

        public string Description => string.Empty;
        public decimal Amount => 0;
        public DateTime Date => DateTime.Now;
        public string Category => string.Empty;

        public void ShowMessage(string message) => MessageBox.Show(message);

        public void SetPresenter(ExpensePresenter? newPresenter)
        {
            Debug.WriteLine("ExpenseUpdateView: Đang gán presenter mới");
            this.presenter = newPresenter;
            if (this.presenter == null)
            {
                Debug.WriteLine("ExpenseUpdateView: Presenter vẫn là null sau khi gán!");
            }
            else
            {
                Debug.WriteLine("ExpenseUpdateView: Presenter đã được gán, gọi UpdateView");
                try
                {
                    this.presenter.UpdateView();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Lỗi khi gọi UpdateView trong SetPresenter: {ex.Message}");
                    MessageBox.Show($"Lỗi khi gọi UpdateView: {ex.Message}");
                }
            }
        }

        public void UpdateExpenseList(List<Expense> expenses)
        {
            try
            {
                Debug.WriteLine("UpdateExpenseList: Bắt đầu thực thi (ExpenseUpdateView)");
                if (expenses == null || lvExpenses == null) return;

                currentExpenses = expenses;

                if (lvExpenses.InvokeRequired)
                {
                    lvExpenses.Invoke(new Action(() => UpdateListView(expenses)));
                }
                else
                {
                    UpdateListView(expenses);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi trong UpdateExpenseList: {ex.Message}");
                MessageBox.Show($"Lỗi khi cập nhật danh sách chi tiêu: {ex.Message}");
            }
        }

        private void UpdateListView(List<Expense> expenses)
        {
            lvExpenses?.Items.Clear();
            foreach (var expense in expenses)
            {
                var item = new ListViewItem([
            expense.Id.ToString(),
            expense.Description,
            expense.Amount.ToString("C"),
            expense.Date.ToString("hh:mm, dd/MM/yyyy"),
            expense.Category,
            expense.UserId
        ]);
                lvExpenses?.Items.Add(item);
            }
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            if(lvExpenses == null || lvExpenses.Items.Count == 0)
            {
                ShowMessage("Không có dữ liệu để sửa");
                return;
            }

            if (lvExpenses.SelectedItems.Count == 0)
            {
                ShowMessage("Chọn một chi tiêu để sửa!");
                return;
            }

            if (presenter == null)
            {
                ShowMessage("Presenter vẫn là null sau khi gán!");
                return;
            }

            var selectedItem = lvExpenses.SelectedItems[0];
            var selectedExpense = new Expense
            {
                Id = Guid.Parse(selectedItem.SubItems[0].Text),
                Description = selectedItem.SubItems[1].Text,
                Amount = decimal.Parse(selectedItem.SubItems[2].Text, System.Globalization.NumberStyles.Currency),
                Date = DateTime.ParseExact(selectedItem.SubItems[3].Text, "hh:mm, dd/MM/yyyy", null),
                Category = selectedItem.SubItems[4].Text,
                UserId = selectedItem.SubItems[5].Text
            };

            var editForm = new ExpenseCreateView(presenter, selectedExpense);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                presenter.EditExpense(new Expense
                {
                    Id = selectedExpense.Id,
                    Description = editForm.Description,
                    Amount = editForm.Amount,
                    Date = editForm.Date,
                    Category = editForm.Category,
                    UserId = selectedExpense.UserId
                });
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if(lvExpenses == null || lvExpenses.Items.Count == 0)
            {
                ShowMessage("Không có dữ liệu để xoá");
                return;
            }

            if (lvExpenses.SelectedItems.Count == 0)
            {
                ShowMessage("Chọn một chi tiêu để xóa!");
                return;
            }

            if (presenter == null)
            {
                ShowMessage("Presenter vẫn là null sau khi gán!");
                return;
            }

            if (MessageBox.Show("Xác nhận xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var selectedItem = lvExpenses.SelectedItems[0];
                var expenseId = Guid.Parse(selectedItem.SubItems[0].Text);
                presenter.DeleteExpense(expenseId);
            }
        }

        private void BtnExport_Click(object? sender, EventArgs e)
        {
            try
            {
                if (currentExpenses == null || currentExpenses.Count == 0)
                {
                    ShowMessage("Không có dữ liệu để xuất!");
                    return;
                }

                using (var writer = new StreamWriter("expenses.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(currentExpenses);
                }
                ShowMessage("Đã xuất thành công!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi khi xuất file CSV: {ex.Message}");
                ShowMessage($"Lỗi khi xuất file: {ex.Message}");
            }
        }

        public void UpdateTotal(decimal total)
        {
            //throw new NotImplementedException();
        }

        public void UpdateChart(Dictionary<string, decimal> expensesByCategory)
        {
            //throw new NotImplementedException();
        }
    }
}
