using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace HelloWorldCnsl
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Hello World...!");
                string id = Console.ReadLine();

                GetRequest(id).Wait();
                Console.ReadKey();
                Console.Clear();
            }
        }

        static async Task GetRequest(string ID)
        {
            switch (ID)
            {
                //Get Request    
                case "Get":
                    Console.WriteLine("Enter id:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:51639/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response;

                        //id == 0 means select all records    
                        if (id == 0)
                        {
                            response = await client.GetAsync("api/User");
                            if (response.IsSuccessStatusCode)
                            {
                                User[] users = await response.Content.ReadAsAsync<User[]>();
                                foreach (var user in users)
                                {
                                    Console.WriteLine("\n{0}\t{1}", user.UserId, user.UserName);
                                }
                            }
                        }
                        else
                        {
                            response = await client.GetAsync("api/User/" + id);
                            if (response.IsSuccessStatusCode)
                            {
                                User user = await response.Content.ReadAsAsync<User>();
                                Console.WriteLine("\n{0}\t{1}", user.UserId, user.UserName);
                            }
                        }
                    }
                    break;

               
            }

        }
    }
}
