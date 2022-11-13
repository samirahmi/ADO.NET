using DataTypes.Applications.Employees;
using DataTypes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes.Views
{
    public class UpdateEmployeeView
    {
        private EmployeeAppService _employeeAppService;
        public UpdateEmployeeView(EmployeeAppService employeeAppService)
        {
            _employeeAppService = employeeAppService;
        }

        public void DisplayView()
        {
            Console.WriteLine("Update Employee");
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

            Console.Write("Do you want to update the record? (Y/N)");
            string choice = Console.ReadLine();

            if (choice.ToUpper().Equals("Y"))
            {
                _employeeAppService.UpdateEmp(employee);
            }
            else
            {
                Console.Write("Save cancelled!");
            }
        }
    }
}
