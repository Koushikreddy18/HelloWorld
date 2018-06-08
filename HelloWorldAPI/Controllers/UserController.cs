using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common.Models;
using System.Data.SqlClient;

namespace HelloWorldAPI.Controllers
{
    public class UserController : ApiController
    {


        List<User> objUsers = new List<User>();
        public List<User> Users()
        {
            
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TestDB"].ToString()))
            {
                SqlCommand command = new SqlCommand("select * from UserDetails_master", connection);                
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        User objuser = new User();
                        objuser.UserId = Convert.ToInt32((Object)reader["UserId"]);
                        objuser.UserName = Convert.ToString((Object)reader["UserName"]);

                        objUsers.Add(objuser);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return objUsers;

        }
            

        [HttpGet]
        public List<User> Get()
        {

            objUsers = Users();
            return objUsers;
        }

        [HttpGet]
        public User Get(int id)
        {
            return Users().Find((r) => r.UserId == id);
        }

        [HttpPost]
        public bool Post(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TestDB"].ToString()))
                {
                    
                    try
                    {
                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "INSERT UserDetails_master (UserName) VALUES ('"+user.UserName+"')";
                        cmd.Connection = connection;

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            try
            {
                var itemToRemove = Users().Find((r) => r.UserId == id);
                Users().Remove(itemToRemove);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
