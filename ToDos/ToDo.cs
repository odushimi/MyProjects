using System.Reflection.Metadata;

namespace ToDos;

public record ToDo(int Id, string Title, string Description, DateTime DueDate, bool IsCompleted);