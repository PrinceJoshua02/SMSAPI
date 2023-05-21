using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SMSAPI.Data;

namespace SMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        SMSDBContext _context;

        public StudentController(SMSDBContext Task)
        {
            _context = Task;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _context.Students.FromSqlRaw("EXEC SP_GetAllStudent").ToListAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent(string First_Name, string Last_Name, string Middle_Name, string Address, string DOB)
        {
            var parameters = new[] {
                new SqlParameter("@First_Name", First_Name),
                new SqlParameter("@Last_Name", Last_Name),
                new SqlParameter("@Middle_Name", Middle_Name),
                new SqlParameter("@Address", Address),
                new SqlParameter("@DOB", DOB)

                };
            var result = await _context.Database.ExecuteSqlRawAsync("EXEC SP_AddStudent @First_Name, @Last_Name,@Middle_Name,@Address,@DOB ", parameters);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateStudent(int Id, string First_Name, string Last_Name, string Middle_Name, string Address, string DOB)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", Id),
                new SqlParameter("@First_Name", First_Name),
                new SqlParameter("@Last_Name", Last_Name),
                new SqlParameter("@Middle_Name", Middle_Name),
                new SqlParameter("@Address", Address),
                new SqlParameter("@DOB", DOB)
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC SP_PutStudent @Id, @First_Name, @Last_Name, @Middle_Name, @Address, @DOB", parameters);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(int Id)
        {
            var parameter = new SqlParameter("@Id", Id);
            await _context.Database.ExecuteSqlRawAsync("EXEC SP_DeleteStudentById @Id", parameter);
            return Ok();
        }
        [HttpGet("{Id}")]
        public IActionResult GetStudentById(int Id)
        {
            var parameter = new SqlParameter("@Id", Id);
            var result = _context.Students.FromSqlInterpolated($"EXEC SP_GetStudentById {parameter}").AsEnumerable().FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}