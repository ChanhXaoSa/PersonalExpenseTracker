using ExpenseManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models
{
    public class CategoryModel
    {
        private readonly ExpenseDbContext dbContext;

        public CategoryModel()
        {
            dbContext = new ExpenseDbContext();
            dbContext.Database.EnsureCreated();
        }

        public List<Category> GetAllCategories()
        {
            return [.. dbContext.Categories.Where(c => c.IsDeleted == false)];
        }

        public List<Category> GetAllCategoriesByUser(string userId)
        {
            return [.. dbContext.Categories.Where(c => c.UserId == userId && c.IsDeleted == false)];
        }

        public void AddCategory(Category category, string userId)
        {
            category.UserId = userId;
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
        }

        public void UpdateCategory(Category category, string userId)
        {
            var existingCategory = dbContext.Categories.FirstOrDefault(c => c.Id == category.Id  && c.UserId == userId);
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                existingCategory.Description = category.Description;
                dbContext.SaveChanges();
            }
        }

        public void DeleteCategory(Guid id)
        {
            var category = dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if(category != null)
            {
                dbContext.Categories.Remove(category);
                dbContext.SaveChanges();
            }
        }
    }
}
