using System;
using TodoApp.Core.Entities;

namespace TodoApp.Core.Events
{
    public class TodoCompletedEvent
    {
        public Guid TodoId { get; }
        public string Title { get; }
        public DateTime CompletedDate { get; }

        public TodoCompletedEvent(Todo todo)
        {
            TodoId = todo.Id;
            Title = todo.Title;
            CompletedDate = DateTime.UtcNow;
        }
    }
} 