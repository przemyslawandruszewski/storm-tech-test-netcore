using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
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
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(void))]
        public async Task<IActionResult> PatchRank(int todoItemId, [FromBody] TodoItemRankPatchEdit todoItemRank)
        {
            var todoItem = _dbContext.SingleTodoItem(todoItemId);

            todoItem.Rank = todoItemRank.Rank;

            await _dbContext.SaveChangesAsync();

            return this.Ok();
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType((int) HttpStatusCode.Created, Type = typeof(TodoItemSummaryViewmodel))]
        public async Task<IActionResult> Create([FromBody] TodoItemCreateFields fields)
        {
            var item = new TodoItem(fields.TodoListId, fields.ResponsiblePartyId, fields.Title, fields.Importance,
                fields.Rank);

            await _dbContext.AddAsync(item);
            await _dbContext.SaveChangesAsync();

            item = _dbContext.SingleTodoItemWithResponsibleParty(item.TodoItemId);
            
            var itemDto = TodoItemSummaryViewmodelFactory.Create(item);

            return Created(Url.Action("Get", new {todoItemId = item.TodoItemId}), itemDto);
        }

        [HttpGet]
        [Route("{todoItemId}")]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(TodoItemSummaryViewmodel))]
        public async Task<IActionResult> Get(int todoItemId)
        {
            var todoItem = await _dbContext.TodoItems.FindAsync(todoItemId);

            var todoItemDto = TodoItemSummaryViewmodelFactory.Create(todoItem);

            return Ok(todoItemDto);
        }
    }
}