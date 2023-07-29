using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ApprAttempt.Pages.Donors
{
    public class EditModel : PageModel
    {

        public clientInfo clientInfo = new clientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {

            String id = Request.Query["id"];
            try
            {

                String connectionString = "Data Source=DESKTOP-49NOPH8;Initial Catalog=disaster_site;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM donors WHERE ID=@id";
                    //changed ID here too

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.Donation = reader.GetString(2);
                                clientInfo.Disaster = reader.GetString(3);
                            }
                        }
                    }

                }

            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

        }

        public void OnPost() 

        {

            clientInfo.id = Request.Form["id"];
            clientInfo.name = Request.Form["Name"];
            clientInfo.Donation = Request.Form["Donation"];
            clientInfo.Disaster = Request.Form["Disaster"];

            if( clientInfo.name.Length==0 
               || clientInfo.Donation.Length==0|| clientInfo.Disaster.Length==0)
            {
                errorMessage = "All fields requiered";
                return;
            }

            try
            {

                String connectionString = "Data Source=DESKTOP-49NOPH8;Initial Catalog=disaster_site;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE donors" +
                        "SET Name=@name, Donation=@Donation,Disaster=@Disaster" +
                         "WHERE ID=@id";
                         // perhaps play around with the capitals of name and @name

                        using(SqlCommand command =new  SqlCommand(sql,connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@Donation", clientInfo.Donation);
                        command.Parameters.AddWithValue("@Disaster", clientInfo.Disaster);

                        command.ExecuteNonQuery();


                    }
                }

            }

            catch( Exception ex) 
            {
                errorMessage = ex.Message;
                return;

            }
            Response.Redirect("/Donors/Index");

        }

    }
}
