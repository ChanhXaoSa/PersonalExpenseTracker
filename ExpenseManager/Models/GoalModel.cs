using ExpenseManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models
{
    public class GoalModel
    {
        private readonly ExpenseDbContext dbContext;

        public GoalModel()
        {
            dbContext = new ExpenseDbContext();
            dbContext.Database.EnsureCreated();
        }

        public List<Goal> GetAllGoals()
        {
            return [.. dbContext.Goals.Where(g => g.IsDeleted == false)];
        }

        public List<Goal> GetAllGoalsByUser(string userId)
        {
            return [.. dbContext.Goals.Where(g => g.UserId == userId && g.IsDeleted == false)];
        }

        public void AddGoal(Goal goal)
        {
            dbContext.Goals.Add(goal);
            dbContext.SaveChanges();
        }

        public void UpdateGoal(Goal goal)
        {
            var existingGoal = dbContext.Goals.FirstOrDefault(g => g.Id == goal.Id);
            if(existingGoal != null)
            {
                existingGoal.Name = goal.Name;
                existingGoal.TargetAmount = goal.TargetAmount;
                existingGoal.CurrentAmount = goal.CurrentAmount;
                existingGoal.Deadline = goal.Deadline;
                dbContext.SaveChanges();
            }
        }

        public void DeleteGoal(Guid id)
        {
            var goal = dbContext.Goals.FirstOrDefault(g => g.Id == id);
            if(goal != null)
            {
                dbContext.Goals.Remove(goal);
                dbContext.SaveChanges();
            }
        }
    }
}
