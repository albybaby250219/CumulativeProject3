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
    public class CourseDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database which we defined in the models
        private SchoolDbContext School = new SchoolDbContext();

        //This function Will access the classes table of schooldb database.
        /// <summary>
        /// Returns a list of classes from the schooldb database
        /// </summary>
        /// <returns>
        /// A list of course Objects with properties same as the database column values (classid,classname,classcode).
        /// </returns>
        /// <example>GET api/CourseData/ListCourses -> {List of classes}</example>
        
        [HttpGet]
        [Route("api/CourseData/ListCourses/{Searchkey}")]
        public IEnumerable<Course> ListCourses(string SearchKey = null)
        {
            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Connection.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from classes where UPPER(classname) like UPPER(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Authors
            List<Course> Courses = new List<Course> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int CourseId = Convert.ToInt32(ResultSet["classid"]);
                string CourseName = ResultSet["classname"].ToString();
                string CourseCode = ResultSet["classcode"].ToString();
                DateTime CourseStartDate = (DateTime)ResultSet["startdate"];
                DateTime CourseFinishDate = (DateTime)ResultSet["finishdate"];

                Course NewCourse = new Course();
                NewCourse.CourseId = CourseId;
                NewCourse.CourseName = CourseName;
                NewCourse.CourseCode = CourseCode;
                NewCourse.CourseStartDate = CourseStartDate;
                NewCourse.CourseFinishDate = CourseFinishDate;
                //Add the class Name to the List
                Courses.Add(NewCourse);
            }

            //Close the connection between the MySQL Database and the WebServer
            Connection.Close();

            //Return the final list of classes names
            return Courses;
        }
        
        /// <summary>
        /// Finds a Course in the database given the Courseid
        /// </summary>
        /// <param name="id">The Courseid</param>
        /// <returns>A teacher object  with the details of the particular Course</returns>
        /// <example>GET api/Coursedata/findCourse/5 =========> Coursedetails with the id of 5</example>
        [HttpGet]
        [Route("api/Coursedata/findCourse/{Courseid}")]
        public Course FindCourse(int id)
        {
            Course NewCourse = new Course();

            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand command = Connection.CreateCommand();

            //SQL QUERY
            command.CommandText = "Select * from classes where classid =@id";

            command.Parameters.AddWithValue("@id", id);

            command.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = command.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int CourseId = Convert.ToInt32(ResultSet["classid"]);
                string CourseName = ResultSet["classname"].ToString();
                string CourseCode = ResultSet["classcode"].ToString();
                DateTime CourseStartDate = (DateTime)ResultSet["startdate"];
                DateTime CourseFinishDate = (DateTime)ResultSet["finishdate"];
                

                NewCourse.CourseId = CourseId;
                NewCourse.CourseName = CourseName;
                NewCourse.CourseCode = CourseCode;
                NewCourse.CourseStartDate = CourseStartDate;
                NewCourse.CourseFinishDate = CourseFinishDate;
            }
            Connection.Close();
            Debug.WriteLine("The details of the Course is:");
            Debug.WriteLine(NewCourse);
            return NewCourse;
        }

        /// <summary>
        /// Deletes a Course from the connected MySQL Database if the ID of that Course exists.
        /// </summary>
        /// <param name="id">The ID of the course.</param>
        /// <example>POST /api/CourseData/DeleteCourse/3</example>==>the Course with Courseid 3 is deleted
        /// <example>POST /api/CourseData/DeleteCourse/6</example>==>the Course with Courseid 6 is deleted
        [HttpPost]
        public void DeleteCourse(int id)
        {
            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Connection.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from classes where classid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Connection.Close();


        }
        ///Add a new Course to the database
        /// <summary>
        /// Inserts  Course details into the database
        /// </summary>
        /// <param name="NewCourse"> it is an object with the Course details that is needed to be inserted 
        /// into the system</param>

        [HttpGet]
        public void AddCourse(Course NewCourse)
        {
            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Connection.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Insert into classes(classname,classcode,startdate,finishdate) " +
                "values(@classname,@classcode,@startdate,@finishdate,@salary)";

            cmd.Parameters.AddWithValue("@classname", NewCourse.CourseName);
            cmd.Parameters.AddWithValue("@classcode", NewCourse.CourseCode);
            cmd.Parameters.AddWithValue("@startdate", NewCourse.CourseStartDate);
            cmd.Parameters.AddWithValue("@finishdate", NewCourse.CourseFinishDate);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            Connection.Close();

        }

        
    }
}
