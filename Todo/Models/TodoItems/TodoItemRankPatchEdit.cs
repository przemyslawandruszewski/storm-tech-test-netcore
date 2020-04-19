using System.ComponentModel.DataAnnotations;

namespace Todo.Models.TodoItems
{
    public class TodoItemRankPatchEdit
    {
        [Required]
        public int Rank { get; set; }
    }
}