using P02_DatabaseFirst_Exercise.Data;
using System;
using System.Linq;

namespace P02_DatabaseFirst
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var db = new SoftUniContext())
            {
                var employees = db.Employees
                    .OrderBy(e => e.EmployeeId)
                    .Select(e => new
                    {
                        Name = $"{e.FirstName} {e.LastName} {e.MiddleName}",
                        e.JobTitle,
                        Salary = $"{e.Salary:F2}"
                    });

                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.Name} {employee.JobTitle} {employee.Salary}");
                }
            }
        }
    }
}