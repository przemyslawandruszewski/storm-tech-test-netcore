using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models.TodoItems;
using Todo.Services;

namespace Todo.Controllers.Api
{
    [Authorize]
    [Route("/api/to-do-item")]
    public class TodoItemApiController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public TodoItemApiController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPatch]
        [Route("patch-rank/{todoItemId}")]
        public async Task<IActionResult> PatchRank(int todoItemId, [FromBody] TodoItemRankPatchEdit todoItemRank)
        {
            var todoItem = _dbContext.SingleTodoItem(todoItemId);

            todoItem.Rank = todoItemRank.Rank;
            
            await _dbContext.SaveChangesAsync();
            
            return this.Ok();
        }
    }
}