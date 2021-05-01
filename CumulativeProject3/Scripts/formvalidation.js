//event listener for when the page loads
window.onload = pageready;
//defining the function
function pageready() {
    //getting the ids from the forms and storing in variables
    var formHandle = document.forms.FormNewTeacher;
    var fnamebox = document.getElementById("TeacherFname");
    var lnamebox = document.getElementById("TeacherLname");
    var empnumbox = document.getElementById("TeacherEmployeeNum");
    var salarybox = document.getElementById("TeacherSalary");
    var fnamemsg = document.getElementById("fnameerrmsg")
    var lnamemsg = document.getElementById("lnameerrmsg")
    var empnummsg = document.getElementById("empnumerrmsg")
    var salarymsg = document.getElementById("salarymsg")
    //event listener when the user submits the form for the form validation
    formHandle.onsubmit = processform;
    //function for validating the userdata
    function processform() {
        //getting the fname value and checking whether it is null or undefined
        var fname = formHandle.TeacherFname;
        var fnamevalue = fname.value;
        if (fnamevalue === null || fnamevalue === "") {
            //changing the style of the text box
            fnamebox.style.background = "red";
            fnamebox.style.color = "white";
            //making the cursor stay on the field
            fnamebox.focus();
            //giving a message to the user what is wrong
            fnamemsg.innerHTML = "please enter your first name";
            fnamemsg.style.fontSize = "16px";
            //this is to return to the form after submitting
            return false;
        }
        else {
            //to change the style back after the error is cleared
            fnamebox.style.background = "white";
            fnamebox.style.color = "black";
        }
        //getting the lname value and checking whether it is null or undefined
        var lname = formHandle.TeacherLname;
        var lnamevalue = lname.value;
        if (lnamevalue === null || lnamevalue === "") {
            //changing the style of the text box
            lnamebox.style.background = "red";
            lnamebox.style.color = "white";
            //making the cursor stay on that field
            lnamebox.focus();
            //giving a message to the user 
            lnamemsg.innerHTML = "please enter your last name";
            lnamemsg.style.fontSize = "16px";
            //stay on the form page even after submitting the form
            return false;
        }
        else {
            //changing the style back even after it is cleared
            lnamebox.style.background = "white";
            lnamebox.style.color = "black";
        }

        //getting the emp num details and checking for null value or undefined
        var empnum = formHandle.TeacherEmployeeNum;
        var empnumvalue = empnum.value;
        if (empnumvalue === null || empnumvalue === "") {
            //changing the style of the text box
            empnumbox.style.background = "red";
            empnumbox.style.color = "white";
            //the cursor stays on the same field
            empnumbox.focus();
            //giving a message to the user
            empnummsg.innerHTML = "please enter your employee num";
            empnummsg.style.fontSize = "16px";
            //stay on the form page even after submitting the form
            return false;
        }
        else {
            //changing the style back after the error is cleared
            empnumbox.style.background = "white";
            empnumbox.style.color = "black";
        }
        ///The salary column is of decimal type which cannot be a null value so it will automatically go into error
        //getting the salary details and checking for null value or undefined
        var salary = formHandle.TeacherSalary;
        var salaryvalue = salary.value;
        if (salaryvalue === null || salaryvalue === "") {
            //changing the style of the text box
            salarybox.style.background = "red";
            salarybox.style.color = "white";
            //the cursor stays on the same field
            salarybox.focus();
            //giving a message to the user
            salarymsg.innerHTML = "please enter your salary per hour";
            salarymsg.style.fontSize = "16px";
            //staying on the form page even after submitting the form
            return false;
        }
        else {
            salarybox.style.background = "white";
            salarybox.style.color = "black";
        }
    }
}