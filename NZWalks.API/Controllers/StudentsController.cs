using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    // https://localhost:xxxx/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        public StudentsController() { }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentsNames = new string[]{ "John", "Jane" } ;

            return Ok(studentsNames);
        }
    }
}
