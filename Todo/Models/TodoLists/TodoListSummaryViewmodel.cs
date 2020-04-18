namespace Todo.Models.TodoLists
{
    public class TodoListSummaryViewmodel
    {
        public int TodoListId { get; }
        public string Title { get; }
        public int NumberOfNotDoneItems { get; }

        public TodoListSummaryViewmodel(int todoListId, string title, int numberOfNotDoneItems)
        {
            TodoListId = todoListId;
            Title = title;
            NumberOfNotDoneItems = numberOfNotDoneItems;
        }
    }
}