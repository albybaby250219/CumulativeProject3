// The code for AJAX to add teacher


function AddTeacher() {

	//To send a request through the curl in cli because it is a post request
	//POST : http://localhost:54472/api/TeacherData/AddAJAXTeacher
	//with POST data of teacherfname teacher lname salary and so on
	//the url from the webpage
	var URL = "http://localhost:54472/api/TeacherData/AddAJAXTeacher/";

	var request = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var TeacherEmployeeNum = document.getElementById('TeacherEmployeeNum').value;
	var TeacherSalary = document.getElementById('TeacherSalary').value;


	//storing the data from the form in an object
	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"TeacherEmployeeNum": TeacherEmployeeNum,
		"TeacherSalary": TeacherSalary
	};


	request.open("POST", URL, true);
	request.setRequestHeader("Content-Type", "application/json");
	request.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (request.readyState == 4 && request.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	request.send(JSON.stringify(TeacherData));

}
//The code to update the Teacher
function UpdateTeacher(TeacherId) {

	//goal: send a request which looks like this:
	//POST : http://localhost:51326/api/TeacherData/UpdateTeacher/{id}
	//with POST data of Teachername, employeenumber,salary etc.

	var URL = "http://localhost:51326/api/TeacherData/UpdateTeacher/" + TeacherId;

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var TeacherEmployeeNum = document.getElementById('TeacherEmployeeNum').value;
	var TeacherSalary = document.getElementById('TeacherSalary').value;



	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"TeacherEmployeeNum": TeacherEmployeeNum,
		"TeacherSalary": TeacherSalary
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}