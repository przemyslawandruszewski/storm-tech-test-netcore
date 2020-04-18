using System.ComponentModel.DataAnnotations;
using Todo.Resources;

namespace Todo.Models.TodoLists
{
    public class ToDoListDetailFilterViewModel
    {
        public ToDoListDetailFilterViewModel(bool skipDone, int toDoListId)
        {
            SkipDone = skipDone;
            ToDoListId = toDoListId;
        }

        [Display(ResourceType = typeof(TodoItemLabels), Name = nameof(TodoItemLabels.ToDoItemSkipDone))]
        public bool SkipDone { get; }

        public int ToDoListId { get; }
    }
}