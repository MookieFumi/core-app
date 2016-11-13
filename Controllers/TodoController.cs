using System.Collections.Generic;
using ConsoleApplication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ILogger<TodoController> _logger;
        private readonly IOptions<AuthorInfo> _authorInfo;
        public TodoController(IOptions<AuthorInfo> authorInfo, ILogger<TodoController> logger)
        {
            _logger = logger;
            _authorInfo = authorInfo;
            _logger.LogInformation(1, _authorInfo.Value.PoweredBy);
        }

        [HttpGet]
        public IEnumerable<int> GetAll()
        {
            _logger.LogInformation(1, "Listing all items");
            return new List<int>() { 1, 2 };
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(string id)
        {
            return new ObjectResult(1);
        }
    }
}