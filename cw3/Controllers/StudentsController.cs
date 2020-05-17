using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using cw3.DAL;
using cw3.Models;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Controllers
{
    [ApiController]
    [Route("api/students")]

public class StudentsController : ControllerBase
    {

        private const string ConString = "Data Source=localhost, 1433;Initial Catalog=sa;Integrated Security=True";
        

        [HttpGet]
        public IActionResult GetStudents()
        {
            var studentList = new List<Student>();

            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "Select * from Students";

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                }
            }
            return Ok(studentList);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            if (id == 1)  
            {
                return Ok("Kowalski");
            } else if (id == 2)
            {
                return Ok("Nowak");
            }

            return NotFound("Nie znaleziono studenta");
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpDelete("{id}")]
        public IActionResult deleteStudent()
        {
            return Ok("Usuwanie zakończone");
        }

        [HttpPut("{id}")]
        public IActionResult putStudent()
        {
            return Ok("Aktualizacja zakończona");
        }
    }
}