using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ApprAttempt.Pages.Donors
{
    public class DonateModel : PageModel
    {

        public clientInfo clientInfo = new clientInfo();

        public string errorMessage = "";
        public string successMessage = "";
        public string addedMessage = "";



        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.Donation = Request.Form["Donation"];
            clientInfo.Disaster = Request.Form["Disaster"];

            if (clientInfo.name.Length == 0 || clientInfo.Donation.Length == 0 || clientInfo.Disaster.Length == 0) 
            {
                errorMessage = "All fields required";
                return;
            }

            //save the new client into the database

            try
            {
                string connectionString = "Data Source=DESKTOP-49NOPH8;Initial Catalog=disaster_site;Integrated Security=True";
                using (SqlConnection connection= new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Donors" +
                                "(Name, Donation,Disaster) Values" +
                                "(@name,@Donation,@Disaster);";

                    using (SqlCommand command= new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);  //if errors arise check the names of these parameters
                        command.Parameters.AddWithValue("@Donation", clientInfo.Donation);
                        command.Parameters.AddWithValue("@Disaster", clientInfo.Disaster);

                        command.ExecuteNonQuery();
                    }


                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            clientInfo.name = ""; clientInfo.Donation = ""; clientInfo.Donation = "";
            successMessage = " New Client Added Correctly";

            addedMessage = "Donation Added succesfully";
            Response.Redirect("/index");
            //Response.Redirect("/Donors/index");
        }
    }
}
