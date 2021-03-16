using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson008
{
    class Program
    {
        static void Main(string[] args)
        {
            Tasker ListTasks = new Tasker();
            while (true)
            {
                ListTasks.Show();
                ListTasks.HandleKeys();

                Console.Clear();
            }
        }
    }
}
