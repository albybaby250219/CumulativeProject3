using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using CumulativeProject3.Models;
//to test the code using debug.writeline()
using System.Diagnostics;
using System.Web.Http.Cors;

namespace CumulativeProject3.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database which we defined in the models
        private SchoolDbContext School = new SchoolDbContext();

        //This function Will access the teachers table of schooldb database.
        /// <summary>
        /// Returns a list of Teachers from the schooldb database
        /// </summary>
        /// <returns>
        /// A list of Teacher Objects with properties same as the database column values (teacherid,teacherfname,teacherlname).
        /// </returns>
        /// <example>GET api/TeacherData/ListTeachers -> {List of teachers}</example>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{Searchkey}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Connection.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where UPPER(teacherfname) like UPPER(@key) or UPPER(teacherlname)" +
                                "like UPPER(@key) or UPPER(CONCAT(teacherfname, ' ', teacherlname)) like UPPER(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Authors
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = Convert.ToInt32(ResultSet["Teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                Decimal TeacherSalary = (Decimal)ResultSet["salary"];
                string TeacherEmployeeNum = ResultSet["employeenumber"].ToString();
                DateTime TeacherHireDate = (DateTime)ResultSet["hiredate"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.TeacherEmployeeNum = TeacherEmployeeNum;
                NewTeacher.TeacherHireDate = TeacherHireDate;
                NewTeacher.TeacherSalary = TeacherSalary;
                //Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Connection.Close();

            //Return the final list of author names
            return Teachers;
        }

        /// <summary>
        /// Finds a teacher in the database given the teacherid
        /// </summary>
        /// <param name="id">The teacherid</param>
        /// <returns>An author object  with the details of the particular teacher</returns>
        /// <example>GET api/teacherdata/findteacher/5 =========> teacherdetails with the id of 5</example>
        [HttpGet]
        [Route("api/teacherdata/findteacher/{teacherid}")]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand command = Connection.CreateCommand();

            //SQL QUERY
            command.CommandText = "Select * from teachers where teacherid =@id";

            command.Parameters.AddWithValue("@id", id);

            command.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = command.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string TeacherEmployeeNum = ResultSet["employeenumber"].ToString();
                DateTime TeacherHireDate = (DateTime)ResultSet["hiredate"];
                decimal TeacherSalary = (decimal)ResultSet["salary"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.TeacherEmployeeNum = TeacherEmployeeNum;
                NewTeacher.TeacherHireDate = TeacherHireDate;
                NewTeacher.TeacherSalary = TeacherSalary;
            }
            Connection.Close();
            Debug.WriteLine("The details of the teacher is:");
            Debug.WriteLine(NewTeacher);
            return NewTeacher;
        }

        /// <summary>
        /// Deletes a Teacher from the connected MySQL Database if the ID of that teacher exists.
        /// </summary>
        /// <param name="id">The ID of the author.</param>
        /// <example>POST /api/TeacherData/DeleteTeacher/3</example>==>the teacher with teacherid 3 is deleted
        /// <example>POST /api/TeacherData/DeleteTeacher/6</example>==>the teacher with teacherid 6 is deleted
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Connection.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Connection.Close();


        }
        ///Add a new teacher to the database
        /// <summary>
        /// Inserts  Teacher details into the database
        /// </summary>
        /// <param name="NewTeacher"> it is an object with the teacher details that is needed to be inserted 
        /// into the system</param>

        [HttpGet]
        public void AddTeacher(Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Connection.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Insert into Teachers(teacherfname,teacherlname,employeenumber,hiredate,salary) " +
                "values(@fname,@lname,@employeenum,CURRENT_DATE(),@salary)";

            cmd.Parameters.AddWithValue("@fname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@lname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@employeenum", NewTeacher.TeacherEmployeeNum);
            cmd.Parameters.AddWithValue("@salary", NewTeacher.TeacherSalary);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            Connection.Close();

        }

        /// <summary>
        /// Adds a Teacher to the MySQL Database.
        /// </summary>
        /// <param name="NewTeacher">An object with fields that map to the columns of the teachers table.</param>
        /// <example>
        /// POST api/TeacherData/AddAJAXTeacher
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Abigail",
        ///	"TeacherLname":"Fred",
        ///	"TeacherEmployeeNum":"T678",
        ///	"TeacherSalary":"45.75"
        /// }
        /// </example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddAJAXTeacher([FromBody] Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            Debug.WriteLine(NewTeacher.TeacherFname);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Insert into Teachers(teacherfname,teacherlname,employeenumber,hiredate,salary) " +
                "values(@fname,@lname,@employeenum,CURRENT_DATE(),@salary)";

            cmd.Parameters.AddWithValue("@fname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@lname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@employeenum", NewTeacher.TeacherEmployeeNum);
            cmd.Parameters.AddWithValue("@salary", NewTeacher.TeacherSalary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
        /// <summary>
        /// Updates a Teacher on the MySQL Database. Non-Deterministic.
        /// </summary>
        /// <param name="TeacherInfo">An object with fields that map to the columns of the author's table.</param>
        /// <example>
        /// POST api/TeacherData/UpdateTeacher/6
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        /// "TeacherFname":"Anuradha",
        ///	"TeacherLname":"Nambiyar",
        ///	"TeacherEmployeeNum":"T690",
        ///	"TeacherSalary":"56.75"
        /// }
        /// </example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void UpdateTeacher(int id, [FromBody] Teacher TeacherInfo)
        {

                //Create an instance of a connection
                MySqlConnection Conn = School.AccessDatabase();

                //Open the connection between the web server and database
                Conn.Open();

                //Establish a new command (query) for our database
                MySqlCommand cmd = Conn.CreateCommand();

                //SQL QUERY
                cmd.CommandText = "UPDATE teachers SET teacherfname=@TeacherFname, teacherlname=@TeacherLname, employeenumber=@TeacherEmployeeNum, salary=@TeacherSalary WHERE teacherid=@TeacherId";
                cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
                cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
                cmd.Parameters.AddWithValue("@TeacherEmployeeNum", TeacherInfo.TeacherEmployeeNum);
                cmd.Parameters.AddWithValue("@TeacherSalary", TeacherInfo.TeacherSalary);
                cmd.Parameters.AddWithValue("@TeacherId", id);
                cmd.Prepare();

                cmd.ExecuteNonQuery();

                //Close the connection between the MySQL Database and the WebServer
                Conn.Close();

        }
    }
}
