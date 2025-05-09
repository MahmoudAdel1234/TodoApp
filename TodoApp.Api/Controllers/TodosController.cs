using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;
using TodoApp.Core.Entities;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodos(
            [FromQuery] TodoStatus? status = null,
            [FromQuery] TodoPriority? priority = null,
            [FromQuery] string? startDate = null,
            [FromQuery] string? endDate = null,
            [FromQuery] string sortBy = "CreatedDate",
            [FromQuery] bool sortDescending = true)
        {
            var todos = await _todoService.GetAllAsync(status, priority, startDate, endDate, sortBy, sortDescending);
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDto>> GetTodo(Guid id)
        {
            var todo = await _todoService.GetByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<TodoDto>> CreateTodo(CreateTodoDto createTodoDto)
        {
            var todo = await _todoService.CreateAsync(createTodoDto);
            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(Guid id, UpdateTodoDto updateTodoDto)
        {
            

            var result = await _todoService.UpdateAsync(id,updateTodoDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            var result = await _todoService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public async Task<IActionResult> MarkAsComplete(Guid id)
        {
            var result = await _todoService.MarkAsCompleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
} 