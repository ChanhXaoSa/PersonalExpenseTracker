using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models
{
    public class ExpenseModel
    {
        private readonly string dbPath = "Data Source=expenses.db";
        private readonly string connectionString;

        public ExpenseModel()
        {
            connectionString = $"Data Source={dbPath};Version=3;";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
            }

            using var connection = new SQLiteConnection(connectionString);
            connection.Open();
            string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Expenses (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Description TEXT NOT NULL,
                        Amount REAL NOT NULL,
                        Date TEXT NOT NULL,
                        Category TEXT NOT NULL
                    )";
            using var command = new SQLiteCommand(createTableQuery, connection);
            command.ExecuteNonQuery();
        }

        public void AddExpense(Expense expense)
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();
            string insertQuery = @"
                    INSERT INTO Expenses (Description, Amount, Date, Category)
                    VALUES (@Description, @Amount, @Date, @Category)";
            using var command = new SQLiteCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@Description", expense.Description);
            command.Parameters.AddWithValue("@Amount", expense.Amount);
            command.Parameters.AddWithValue("@Date", expense.Date.ToString("dd-MM-yyyy"));
            command.Parameters.AddWithValue("@Category", expense.Category);
            command.ExecuteNonQuery();
            expense.Id = (int)connection.LastInsertRowId;
        }

        public List<Expense> GetAllExpenses()
        {
            var expenses = new List<Expense>();
            using (SQLiteConnection connection = new(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM Expenses";
                using var command = new SQLiteCommand(selectQuery, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var expense = new Expense
                    {
                        Id = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        Amount = reader.GetDecimal(2),
                        Date = DateTime.ParseExact(reader.GetString(3), "dd-MM-yyyy", null),
                        Category = reader.GetString(4)
                    };
                    expenses.Add(expense);
                }
            }
            return expenses;
        }

        public decimal GetTotalExpense()
        {
            decimal total = 0;
            using (SQLiteConnection connection = new(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT SUM(Amount) FROM Expenses";
                using var command = new SQLiteCommand(selectQuery, connection);
                var result = command.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    total = Convert.ToDecimal(result);
                }
            }
            return total;
        }

        public void DeleteExpense(int id)
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();
            string deleteQuery = "DELETE FROM Expenses WHERE Id = @Id";
            using var command = new SQLiteCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }

        public void UpdateExpense(Expense expense)
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();
            string updateQuery = @"
                    UPDATE Expenses
                    SET Description = @Description, Amount = @Amount, Date = @Date, Category = @Category
                    WHERE Id = @Id";
            using var command = new SQLiteCommand(updateQuery, connection);
            command.Parameters.AddWithValue("@Description", expense.Description);
            command.Parameters.AddWithValue("@Amount", expense.Amount);
            command.Parameters.AddWithValue("@Date", expense.Date.ToString("dd-MM-yyyy"));
            command.Parameters.AddWithValue("@Category", expense.Category);
            command.Parameters.AddWithValue("@Id", expense.Id);
            command.ExecuteNonQuery();
        }

        public Dictionary<string, decimal> GetExpensesByCategory()
        {
            var expensesByCategory = new Dictionary<string, decimal>();
            using (SQLiteConnection connection = new(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT Category, SUM(Amount) AS TotalAmount FROM Expenses GROUP BY Category";
                using var command = new SQLiteCommand(selectQuery, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string category = reader.GetString(0);
                    decimal amount = reader.IsDBNull(1) ? 0m : reader.GetDecimal(1);
                    expensesByCategory[category] = amount;
                }
            }
            return expensesByCategory;
        }
    }
}
