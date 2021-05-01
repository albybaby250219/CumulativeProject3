using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CumulativeProject3.Models
{
    public class Teacher
    {

        //The following fields define the teacher
        public int TeacherId;
        public string TeacherFname;
        public string TeacherLname;
        public string TeacherEmployeeNum;
        public DateTime TeacherHireDate;
        public Decimal TeacherSalary;

        //Can execute the server side validation we can write the code to validate on the serverside
        public bool IsValid()
        {
            return true;
        }
        //parameter-less constructor function
        public Teacher() { }
    }
}