using ToDos;

namespace TestMyProjects;

public class TaskServiceTest
{
    public ITaskService service;

    public TaskServiceTest()
    {
        service = new TaskService();
        service.AddToDo(new ToDo(1, "Test 1", "Test 1 Description", DateTime.Now, false));
        service.AddToDo(new ToDo(2, "Test 2", "Test 2 Description", DateTime.Now, false));
        service.AddToDo(new ToDo(3, "Test 3", "Test 3 Description", DateTime.Now, false));
    }

    [Fact]
    public void ShouldGetToDo()
    {
        var actual = service.GetToDo(1);

        Assert.NotNull(actual);
        Assert.True(actual.Id == 1);
    }

    [Fact]
    public void ShouldGetToDos()
    {
       var actual = service.GetToDos();

       Assert.True(actual.Count == 3);
    }
}
