using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swingy.Models;
using Swingy.DataAcces;
using System.Data.SqlClient;
using Swingy.Controllers;

namespace Swingy
{
    class Program
    {      
        static void Main(string[] args)
        {
            try
            {
                SwingController Controller = new SwingController();

                Controller.InitHero();
                Controller.InitBoard();
                Controller.NavigateHero();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.Write("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
