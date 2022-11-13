using DataTypes.Applications.Employees;
using DataTypes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes.Views
{
    public class CreateEmployeeView
    {
        private EmployeeAppService _employeeAppService;
        public CreateEmployeeView(EmployeeAppService employeeAppService)
        {
            _employeeAppService = employeeAppService;
        }

        public void DisplayView()
        {
            Console.WriteLine("Create Employee");
            Console.WriteLine("-----------------");
            
            Guid g = Guid.NewGuid();
            var empId = g;

            Console.Write("Employee Name : ");
            string empName = Console.ReadLine();

            Console.Write("Salary : ");
            int salary = Convert.ToInt32(Console.ReadLine());

            var employee = new Employee();
            employee.EmployeeId = empId.ToString();
            employee.EmployeeName = empName;
            employee.Salary = salary;

            Console.Write("Do you want to save the record? (Y/N)");
            string choice = Console.ReadLine();

            if (choice.ToUpper().Equals("Y"))
            {
                _employeeAppService.SaveEmp(employee);
            }
            else
            {
                Console.Write("Save cancelled!");
            }
        }
    }
}
