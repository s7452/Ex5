using Ex5.DAL;
using Ex5.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Ex5.Controllers
{
    [ApiController]
    [Route("api/students")]

    public class StudentsController : ControllerBase
    {


        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudents(string id)
        {

            using (var con = new SqlConnection("Data Source=db-mssql; Initial Catalog=s7452; Integrated Security=True;"))
            using (var com = new SqlCommand())
            {
                if (id != null)
                {
                    com.Connection = con;
                    com.CommandText = $"select * from Student where indexNumber=@id";
                    com.Parameters.AddWithValue("id", id);
                    con.Open();
                    var dr = com.ExecuteReader();
                    var st = new Student();
                    while (dr.Read())
                    {
                        st.FirstName = dr["FirstName"].ToString();
                        st.LastName = dr["LastName"].ToString();
                        st.IndexNumber = dr["IndexNumber"].ToString();
                    }
                    return Ok(st);
                }
                else
                {
                    var _students = new List<Student>();
                    com.Connection = con;
                    com.CommandText = "select * from Student";

                    con.Open();
                    var dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        var st = new Student();
                        st.FirstName = dr["FirstName"].ToString();
                        st.LastName = dr["LastName"].ToString();
                        st.IndexNumber = dr["IndexNumber"].ToString();
                        _students.Add(st);
                    }
                    return Ok(_students);
                }
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(string id)
        {

            using (var con = new SqlConnection("Data Source=db-mssql; Initial Catalog=s7452; Integrated Security=True"))
            using (var com = new SqlCommand())
            {

                com.Connection = con;
                com.CommandText = $"select * from Student where indexNumber=@id";
                com.Parameters.AddWithValue("id", id);

                con.Open();
                var dr = com.ExecuteReader();
                var st = new Student();
                while (dr.Read())
                {
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                }
                return Ok(st);
            }
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id)
        {
            if (id == 1)
            {
                return Ok("Studnet zaktualizowany");
            }
            return NotFound("Nie znaleziono studenta"); ;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            if (id == 2)
            {
                return Ok("Student został usunięty");
            }
            return NotFound("Nie znaleziono studenta");
        }
    }
}
