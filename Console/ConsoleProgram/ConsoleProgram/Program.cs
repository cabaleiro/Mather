using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector3 a = new Vector3(10, 10, 10);
            Vector3 b = new Vector3(1, 5, 3);

            Matrix c = Matrix.Parse("10 0 \r\n 10 0");

            Console.WriteLine((a^b).ToString());
            Console.WriteLine(c.ToString());







            Console.ReadLine();
        }
    }
}
