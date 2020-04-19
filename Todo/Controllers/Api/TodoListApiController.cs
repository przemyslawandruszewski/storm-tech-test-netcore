using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoItems;
using Todo.Services;

namespace Todo.Controllers.Api
{
    [Authorize]
    [Route("/api/to-do-list")]
    public class TodoListApiController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public TodoListApiController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("{todoListId}")]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(IList<TodoItemSummaryViewmodel>))]
        public async Task<IActionResult> GetAll(int todoListId)
        {
            var todoItems = await _dbContext.SingleTodoItemsWithResponsibleParty(todoListId);

            var todoItemDtos = todoItems.Select(TodoItemSummaryViewmodelFactory.Create);

            return Ok(todoItemDtos);
        }
    }
}