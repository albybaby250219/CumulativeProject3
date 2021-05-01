using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using CumulativeProject3.Models;

namespace CumulativeProject3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        //GET : /Teacher/List
        /// <summary>
        /// To contect the teacher datacontroller list method  with the  list view
        /// </summary>
        /// <param name="SearchKey"></param>
        /// <returns></returns>
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        /// <summary>
        /// Connects the teacher datacontroller find teacher function to the show.html in the view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }
        /// <summary>
        /// To the users if they want to confirm the delete of a particular teacher so the delete was not a mistake
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }

        /// <summary>
        /// To delete the teacher from the database 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }
        /// <summary>
        /// To connect the new view
        /// </summary>
        /// <returns></returns>
        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }
        //GET : /Teacher/Ajax_New
        public ActionResult Ajax_New()
        {
            return View();

        }

        //GET : /Teacher/Create
        [HttpGet]
        public ActionResult Create(string TeacherFname, string TeacherLname, string TeacherEmployeeNum, decimal TeacherSalary)
        {
            //To check whether we get the data from the form

            Debug.WriteLine("I have accessed the data:");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(TeacherEmployeeNum);
            Debug.WriteLine(TeacherSalary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.TeacherEmployeeNum = TeacherEmployeeNum;
            NewTeacher.TeacherSalary = TeacherSalary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }
        /// <summary>
        /// Routes to a dynamically generated "Teacher Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>A dynamic "Update Teacher" webpage which provides the current information of the Teacher and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Teacher/Update/5</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        public ActionResult Ajax_Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }


        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the system, with new values. 
        /// Conveys this information to the API, and redirects to the "Teacher Show" page of our updated teacher.
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the teacher</param>
        /// <param name="TeacherLname">The updated last name of the teacher</param>
        /// <param name="TeacherEmplyoeeNum">The updated employee number of the teacher.</param>
        /// <param name="TeacherSalary">The updated salary of the teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// POST : /Teacher/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        /// "TeacherFname":"Anuradha",
        ///	"TeacherLname":"Nambiyar",
        ///	"TeacherEmployeeNum":"T690",
        ///	"TeacherSalary":"56.75"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string TeacherEmployeeNum, decimal TeacherSalary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.TeacherEmployeeNum = TeacherEmployeeNum;
            TeacherInfo.TeacherSalary = TeacherSalary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }

    }
}