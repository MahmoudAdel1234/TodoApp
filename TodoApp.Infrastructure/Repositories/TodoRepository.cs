using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;
using TodoApp.Infrastructure.Data;

namespace TodoApp.Infrastructure.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Todo> GetByIdAsync(Guid id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public async Task<IEnumerable<Todo>> GetAllAsync(
            TodoStatus? status = null,
            TodoPriority? priority = null,
            string? startDate = null,
            string? endDate = null,
            string sortBy = "CreatedDate",
            bool sortDescending = true)
        {
            var query = _context.Todos.AsQueryable();

            // Apply filters
            if (status.HasValue)
                query = query.Where(t => t.Status == status.Value);

            if (priority.HasValue)
                query = query.Where(t => t.Priority == priority.Value);



            if (!string.IsNullOrEmpty(startDate))
            {
                var parsedStartDate = DateTime.Parse(startDate);
                query = query.Where(t => t.DueDate >= parsedStartDate);
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                var parsedEndDate = DateTime.Parse(endDate);
                query = query.Where(t => t.DueDate <= parsedEndDate);
            }


            // Apply sorting
            query = sortBy.ToLower() switch
            {
                "createddate" => sortDescending 
                    ? query.OrderByDescending(t => t.CreatedDate)
                    : query.OrderBy(t => t.CreatedDate),
                "duedate" => sortDescending
                    ? query.OrderByDescending(t => t.DueDate)
                    : query.OrderBy(t => t.DueDate),
                "priority" => sortDescending
                    ? query.OrderByDescending(t => t.Priority)
                    : query.OrderBy(t => t.Priority),
                "status" => sortDescending
                    ? query.OrderByDescending(t => t.Status)
                    : query.OrderBy(t => t.Status),
                _ => sortDescending
                    ? query.OrderByDescending(t => t.CreatedDate)
                    : query.OrderBy(t => t.CreatedDate)
            };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Todo>> GetByStatusAsync(TodoStatus status)
        {
            return await _context.Todos
                .Where(t => t.Status == status)
                .OrderByDescending(t => t.CreatedDate)
                .ToListAsync();
        }

        public async Task<Todo> AddAsync(Todo todo)
        {
            todo.CreatedDate = DateTime.UtcNow;
            todo.LastModifiedDate = DateTime.UtcNow;
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo> UpdateAsync(Todo todo)
        {
            todo.LastModifiedDate = DateTime.UtcNow;
            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task DeleteAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
            }
        }
    }
} 