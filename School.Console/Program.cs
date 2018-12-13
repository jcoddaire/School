using School.Data;
using System;

namespace School.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");

            ISchoolData dataService = new School.Data.SchoolServiceRepository(new SchoolContext());
            var people = dataService.GetAllPersons();

            foreach (var p in people)
            {
                System.Console.WriteLine($"{p.FirstName} {p.LastName}");
            }
        }
    }
}
