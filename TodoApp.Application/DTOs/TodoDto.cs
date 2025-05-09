using System;
using TodoApp.Core.Entities;

namespace TodoApp.Application.DTOs
{
    public class TodoDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get; set; }
        public TodoPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

    public class CreateTodoDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
    }

    public class UpdateTodoDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get; set; }
        public TodoPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
    }
} 