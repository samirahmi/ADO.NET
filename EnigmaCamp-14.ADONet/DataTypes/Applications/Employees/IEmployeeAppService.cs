using DataTypes.Applications.Employees.Dto;
using DataTypes.Models;

namespace DataTypes.Applications.Employees
{
    public interface IEmployeeAppService
    {
        void SaveEmp(Employee emp);
        void UpdateEmp(Employee emp);
        void DeleteEmp(string id);
        EmployeeDto GetById(string id);
        List<EmployeeDto> GetAllEmployee();
    }
}
