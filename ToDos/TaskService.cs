namespace ToDos;

public interface ITaskService
{
 public ToDo AddToDo(ToDo task);
 public ToDo? GetToDo(int id);
 public void DeleteToDo(int id);
 public List<ToDo> GetToDos();
}

public class TaskService : ITaskService
{
    private readonly List<ToDo> _todos = [];
    public ToDo AddToDo(ToDo task)
    {
        _todos.Add(task);
        return task;
    }

    public void DeleteToDo(int id)
    {
        _todos.RemoveAll(i => i.Id == id);
    }

    public ToDo? GetToDo(int id)
    {
        return _todos.SingleOrDefault<ToDo>(i => i.Id == id);
    }

    public List<ToDo> GetToDos()
    {
        return _todos;
    }
}
