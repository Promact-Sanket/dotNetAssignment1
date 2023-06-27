using Assignment_1.Data;
using Assignment_1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Assignment_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly Student_Context _dbContext;

        public StudentController(Student_Context dbContext) 
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>>GetStudent()
        {
            var students = await _dbContext.Students.ToListAsync();
            if (students == null)
            {
                return NotFound();
            }
            return students;

        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Student>> GetStudentById(int Id)
        {
            var student = await _dbContext.Students.FindAsync(Id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }

        [HttpPost]
        [HttpPut]
        public async Task<ActionResult<List<Student>>> AddStudent(Student model)
        {
            var student = await _dbContext.Students.FindAsync(model.studentId);
            if (HttpContext.Request.Method == HttpMethods.Post)
            {               
                if (student != null)
                    return Conflict(new { message = $"An existing record with the Student id '{model.studentId}' was already found." });

                _dbContext.Students.Add(model);
                await _dbContext.SaveChangesAsync();
                return await _dbContext.Students.ToListAsync();
            }
            else
            {
                if (student == null)
                    return NotFound();

                student.studentName = model.studentName;
                await _dbContext.SaveChangesAsync();
                return Ok(student);
            }                       
        }

        [HttpPatch("{Id}/{Name}")]
        public async Task<ActionResult<Student>> UpdateStudentName(int Id, string Name)
        {
            var student = await _dbContext.Students.FindAsync(Id);
            if (student==null)       
                return NotFound();
            
            student.studentName = Name;
            await _dbContext.SaveChangesAsync();
            return Ok(student);
        }

        [HttpDelete ("{Id}")]
        public async Task<ActionResult<List<Student>>>DeleteStudentById(int Id)
        {
            var Student = await _dbContext.Students.FindAsync(Id);
            if (Student == null)
                return NotFound();

            _dbContext.Remove(Student);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Students.ToListAsync();
        }
    }
}
