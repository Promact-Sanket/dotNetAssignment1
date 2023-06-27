using Assignment_1.Model;
using Microsoft.EntityFrameworkCore;

namespace Assignment_1.Data
{
    public class Student_Context : DbContext
    {
        public Student_Context(DbContextOptions<Student_Context> options) : base(options) 
        { }
        public DbSet<Student> Students { get; set; }        
    }
}
