using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Sql;
using System.Data.SqlClient;


namespace ApprAttempt.Pages.Donors
{
    public class IndexModel : PageModel
    {
        public List<clientInfo> listClients = new List<clientInfo>();

        public void OnGet()
        {

            try
            {
                String connectionString = "Data Source=DESKTOP-49NOPH8;Initial Catalog=disaster_site;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM donors";
                    using(SqlCommand command = new SqlCommand(sql,connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                clientInfo clientInfo = new clientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.Donation = reader.GetString(2);
                                clientInfo.Disaster = reader.GetString(3);

                                listClients.Add(clientInfo);
                            }
                             
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
            }
        }
    }

    public class clientInfo  //client info = donor info
    {
        public string id;
        public string name;
        public string Donation;
        public string Disaster;

    }
}
