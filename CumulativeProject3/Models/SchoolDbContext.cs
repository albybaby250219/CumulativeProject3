using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//This is used for the connection of sql to the webpage .
//It is a must to include this for the sql connection
using MySql.Data.MySqlClient;


namespace CumulativeProject3.Models
{
    public class SchoolDbContext
    {
        //To have a connection to the database in the localhost we must provide these properties. 
        //Only this model can acess these properties
        //Get these from the MAMP ->PHP My Admin
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "schooldb"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        //ConnectionString is a string with the properties defined above and is  used to connect to the database.
        protected static string ConnectionString
        {
            get
            {
                //convert zero datetime is a db connection setting which returns NULL if the date is 0000-00-00
                //this can allow C# to have an easier interpretation of the date (no date instead of 0 BCE)
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";
            }
        }
        //This is the method to get the database!
        /// <summary>
        /// Returns a connection to the schooldb database.
        /// </summary>
        /// <example>
        /// private SchoolDbContext School = new SchoolDbContext();
        /// MySqlConnection Connection = School.AccessDatabase();
        /// </example>
        /// <returns>A MySqlConnection Object</returns>
        public MySqlConnection AccessDatabase()
        {
            //We are instantiating the MySqlConnection Class to create an object
            //the object is a specific connection to our schooldb database on port 3306 of localhost
            return new MySqlConnection(ConnectionString);
        }
    }
}