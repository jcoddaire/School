using System;

namespace School.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");

            var db = new School.Data.SchoolContext();

            foreach(var p in db.Person)
            {
                System.Console.WriteLine($"{p.FirstName} {p.LastName}");
            }
        }
    }
}
