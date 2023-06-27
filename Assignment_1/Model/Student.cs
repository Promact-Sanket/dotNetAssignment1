using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment_1.Model
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int studentId { get; set; }
        public string studentName { get; set; }=string.Empty;
    }
}
