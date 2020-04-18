using System.ComponentModel.DataAnnotations;
using Todo.Data.Entities;
using Todo.Resources;

namespace Todo.Models.TodoItems
{
    public class TodoItemCreateFields
    {
        public int TodoListId { get; set; }
        public string Title { get; set; }
        public string TodoListTitle { get; set; }
        
        [Display(Name = nameof(TodoItemLabels.ToDoItemResponsible), ResourceType = typeof(TodoItemLabels))]
        public string ResponsiblePartyId { get; set; }
        
        public Importance Importance { get; set; } = Importance.Medium;

        [Display(ResourceType = typeof(TodoItemLabels), Name = nameof(TodoItemLabels.ToDoItemRank))]
        public int Rank { get; set; } = default;

        public TodoItemCreateFields() { }

        public TodoItemCreateFields(int todoListId, string todoListTitle, string responsiblePartyId)
        {
            TodoListId = todoListId;
            TodoListTitle = todoListTitle;
            ResponsiblePartyId = responsiblePartyId;
        }
    }
}