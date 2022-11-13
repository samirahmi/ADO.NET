using DataTypes.Applications.Employees.Dto;
using DataTypes.Interfaces;
using DataTypes.Models;
using DataTypes.SqlServices;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.Intrinsics.X86;

namespace DataTypes.Applications.Employees
{
    public class EmployeeAppService : IEmployeeAppService
    {
        private SqlConnection _sqlConnection;
        private SqlCommands _sqlCommands;
        private SqlTransaction _sqlTransaction;
        public EmployeeAppService(
            SqlConnection sqlConnection
            )
        {
            _sqlConnection = sqlConnection;
        }

        public void DeleteEmp(string id)
        {
            try
            {
                _sqlConnection.Open();
                _sqlTransaction = _sqlConnection.BeginTransaction();
                
                var data = _sqlCommands.ExecuteNonQuery("DELETE FROM Employee WHERE EmployeeId = '" + id + "'");               
                _sqlTransaction.Commit();
            }
            catch (DbException dbex)
            {
                _sqlTransaction.Rollback();
            }
            _sqlConnection.Close();
        }

        public List<EmployeeDto> GetAllEmployee()
        {
            var listEmployee = new List<EmployeeDto>();

            try
            {
                _sqlConnection.Open();
                _sqlTransaction = _sqlConnection.BeginTransaction();
                _sqlCommands = new SqlCommands(_sqlConnection, _sqlTransaction, 10000);

                SqlDataAdapter sde = new SqlDataAdapter("SELECT * FROM Employee", _sqlConnection);
                _sqlTransaction.Commit();

                DataSet ds = new DataSet();
                sde.Fill(ds);

                foreach (DataRow row in ds.Tables["Table"].Rows)
                {
                    var employee = new EmployeeDto();
                    employee.EmployeeId = row["EmployeeId"].ToString();
                    employee.EmployeeName = row["EmployeeName"].ToString();
                    employee.Salary = Convert.ToInt32(row["Salary"]);
                    listEmployee.Add(employee);
                }
            }
            catch
            {
                listEmployee = new List<EmployeeDto>();
            }

            _sqlConnection.Close();
            return listEmployee;
        }

        public EmployeeDto GetById(string id)
        {
            var employee = new EmployeeDto();
            try
            {
                _sqlConnection.Open();
                SqlDataAdapter sde = new SqlDataAdapter("SELECT * FROM Employee WHERE EmployeeId = '" + id + "'", _sqlConnection);
                DataSet ds = new DataSet();
                sde.Fill(ds);

                foreach (DataRow row in ds.Tables["Table"].Rows)
                {
                    employee.EmployeeId = row["EmployeeId"].ToString();
                    employee.EmployeeName = row["EmployeeName"].ToString();
                    employee.Salary = Convert.ToInt32(row["Salary"]);
                }
            }
            catch
            {
                employee = new EmployeeDto();
            }

            _sqlConnection.Close();
            return employee;
        }

        public void SaveEmp(Employee emp)
        {
            SqlParameter[] sqlParameters = new SqlParameter[3];
                        
            SqlParameter val1 = new SqlParameter();
            val1.ParameterName = "@val1";
            val1.SqlDbType = SqlDbType.NVarChar;
            val1.Direction = ParameterDirection.Input;
            val1.Value = emp.EmployeeId;

            SqlParameter val2 = new SqlParameter();
            val2.ParameterName = "@val2";
            val2.SqlDbType = SqlDbType.NVarChar;
            val2.Direction = ParameterDirection.Input;
            val2.Value = emp.EmployeeName;

            SqlParameter val3 = new SqlParameter();
            val3.ParameterName = "@val3";
            val3.SqlDbType = SqlDbType.Int;
            val3.Direction = ParameterDirection.Input;
            val3.Value = emp.Salary;

            sqlParameters[0] = val1;
            sqlParameters[1] = val2;
            sqlParameters[2] = val3;

            _sqlCommands = new SqlCommands(_sqlConnection, _sqlTransaction, 10000);

            try
            {
                _sqlConnection.Open();
                _sqlTransaction = _sqlConnection.BeginTransaction();
                var data = _sqlCommands.ExecuteNonQuery("INSERT INTO Employee (EmployeeId, EmployeeName, Salary) " +
                    "VALUES(@val1, @val2, @val3)", sqlParameters);

                _sqlTransaction.Commit();
            }
            catch (DbException dbex)
            {
                _sqlTransaction.Rollback();
            }

            _sqlConnection.Close();
        }

        public void UpdateEmp(Employee emp)
        {
            SqlParameter[] sqlParameters = new SqlParameter[3];

            _sqlConnection.Open();

            SqlParameter val1 = new SqlParameter();
            val1.ParameterName = "@val1";
            val1.SqlDbType = SqlDbType.NVarChar;
            val1.Direction = ParameterDirection.Input;
            val1.Value = emp.EmployeeId;

            SqlParameter val2 = new SqlParameter();
            val2.ParameterName = "@val2";
            val2.SqlDbType = SqlDbType.NVarChar;
            val2.Direction = ParameterDirection.Input;
            val2.Value = emp.EmployeeName;

            SqlParameter val3 = new SqlParameter();
            val3.ParameterName = "@val3";
            val3.SqlDbType = SqlDbType.Int;
            val3.Direction = ParameterDirection.Input;
            val3.Value = emp.Salary;

            sqlParameters[0] = val1;
            sqlParameters[1] = val2;
            sqlParameters[2] = val3;

            _sqlCommands = new SqlCommands(_sqlConnection, _sqlTransaction, 10000);

            try
            {
                _sqlConnection.Open();
                _sqlTransaction = _sqlConnection.BeginTransaction();
                var data = _sqlCommands.ExecuteNonQuery("UPDATE statement");               
                _sqlTransaction.Commit();
            }
            catch (DbException dbex)
            {
                _sqlTransaction.Rollback();
            }

            _sqlConnection.Close();
        }
    }
}
