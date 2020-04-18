using System.Linq;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Data.Entities;

namespace Todo.Services
{
    public static class ApplicationDbContextConvenience
    {
        public static IQueryable<TodoList> RelevantTodoLists(this ApplicationDbContext dbContext, string userId)
        {
            return dbContext.TodoLists.Include(tl => tl.Owner)
                .Include(tl => tl.Items)
                .Where(tl => tl.Owner.Id == userId);
        }

        public static TodoList SingleTodoList(this ApplicationDbContext dbContext, int todoListId, bool skipDone)
        {
            var todoList = dbContext.TodoLists.Include(tl => tl.Owner).Single(tl => tl.TodoListId == todoListId);
            
            var todoItemsQuery = dbContext.TodoItems
                .Include(ti => ti.ResponsibleParty)
                .Where(tl => tl.TodoListId == todoListId);
            
            if (skipDone)
            {
                todoItemsQuery = todoItemsQuery.Where(x => x.IsDone == false);
            }

            todoList.Items = todoItemsQuery
                .ToList();

            return todoList;
        }

        public static TodoItem SingleTodoItem(this ApplicationDbContext dbContext, int todoItemId)
        {
            return dbContext.TodoItems.Include(ti => ti.TodoList).Single(ti => ti.TodoItemId == todoItemId);
        }
    }
}