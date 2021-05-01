using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CumulativeProject3.Models
{
    public class Course
    {
        //primary key
        public int CourseId;
        public string CourseName;
        public string CourseCode;
        public DateTime CourseStartDate;
        public DateTime CourseFinishDate;
        //foreign key
        public int TeacherId;

        //can Execute Server Validation Logic here
        //see Author.cs as an example
        /*public bool IsValid()
        {
            return true;
        }*/

        //paramter-less constructor function
        //used for auto-binding article properties in ajax call to ArticleData Controller
        public Course() { }
    }
}