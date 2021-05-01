using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using CumulativeProject3.Models;
using CumulativeProject3.Controllers;

namespace CumulativeProject3.Models
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }
        //GET : /Course/List
        /// <summary>
        /// To contect the Course datacontroller list method  with the  list view
        /// </summary>
        /// <param name="SearchKey"></param>
        /// <returns></returns>
        public ActionResult List(string SearchKey = null)
        {
            CourseDataController controller = new CourseDataController();
            IEnumerable<Course> Courses = controller.ListCourses(SearchKey);
            return View(Courses);
        }

        //GET : /Course/Show/{id}
        /// <summary>
        /// Connects the Course datacontroller find Course function to the show.html in the view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Show(int id)
        {
            CourseDataController controller = new CourseDataController();
            Course NewCourse = controller.FindCourse(id);


            return View(NewCourse);
        }
        /// <summary>
        /// To the users if they want to confirm the delete of a particular Course so the delete was not a mistake
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET : /Course/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            CourseDataController controller = new CourseDataController();
            Course NewCourse = controller.FindCourse(id);


            return View(NewCourse);
        }

        /// <summary>
        /// To delete the Course from the database 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //POST : /Course/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            CourseDataController controller = new CourseDataController();
            controller.DeleteCourse(id);
            return RedirectToAction("List");
        }
        /// <summary>
        /// To connect the new view
        /// </summary>
        /// <returns></returns>
        //GET : /Course/New
        public ActionResult New()
        {
            return View();
        }

        //GET : /Course/Create
        [HttpGet]
        public ActionResult Create(string CourseName, string CourseCode, DateTime CourseStartDate, DateTime CourseFinishDate)
        {
            //To check whether we get the data from the form

            Debug.WriteLine("I have accessed the data:");
            Debug.WriteLine(CourseName);
            Debug.WriteLine(CourseCode);
            Debug.WriteLine(CourseStartDate);
            Debug.WriteLine(CourseFinishDate);

            Course NewCourse = new Course();
            NewCourse.CourseName = CourseName;
            NewCourse.CourseCode = CourseCode;
            NewCourse.CourseStartDate = CourseStartDate;
            NewCourse.CourseFinishDate = CourseFinishDate;

            CourseDataController controller = new CourseDataController();
            controller.AddCourse(NewCourse);

            return RedirectToAction("List");
        }
    }
}