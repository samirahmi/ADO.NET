using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Xml;
using DataTypes.Interfaces;

namespace DataTypes.SqlServices
{
    public class SqlCommands : ISqlCommands
    {
        private readonly SqlConnection _currentConnection;
        private readonly SqlTransaction _currentTransaction;
        private readonly int _commandTimeOut;

        public SqlCommands(SqlConnection currentConnection, SqlTransaction currentTransaction, int commandTimeOut)
        {
            if (currentConnection == null) throw new ArgumentNullException("currentConnection");
            if (currentTransaction == null) throw new ArgumentNullException("currentTransaction");
            _currentConnection = currentConnection;
            _currentTransaction = currentTransaction;
            _commandTimeOut = commandTimeOut;
        }
              
        public int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(commandText, null);
        }
                
        public int ExecuteNonQuery(string commandText, params SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(commandText);

            int rowsAffected = 0;
            try
            {
                rowsAffected = ExecuteNonQuery(out cmd, commandText, parameters);
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Dispose();
            }

            return rowsAffected;
        }

        public int ExecuteNonQuery(out SqlCommand cmd, string commandText, params SqlParameter[] parameters)
        {
            int rowsAffected = 0;
          
            SqlCommand cmdExecute = BuildCommand(commandText, parameters);
            rowsAffected = cmdExecute.ExecuteNonQuery();

            cmd = cmdExecute;

            return rowsAffected;
        }

        public object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(commandText, null);
        }

        public object ExecuteScalar(string commandText, params SqlParameter[] parameters)
        {
            SqlCommand cmd;
            object result = ExecuteScalar(out cmd, commandText, parameters);
            cmd.Parameters.Clear();
            cmd.Dispose();

            return result;
        }

        public object ExecuteScalar(out SqlCommand cmd, string commandText, params SqlParameter[] parameters)
        {
            // Find the command to execute
            object data = null;

            SqlCommand cmdScalar = BuildCommand(commandText, parameters);

            data = cmdScalar.ExecuteScalar();
            
            cmd = cmdScalar;
            return data;
        }
       
        public SqlDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(commandText, null);
        }
              
        public SqlDataReader ExecuteReader(string commandText, params SqlParameter[] parameters)
        {
            SqlDataReader reader = null;

            using (SqlCommand cmdReader = new SqlCommand(commandText, _currentConnection))
            {
                cmdReader.CommandType = CommandType.StoredProcedure;
                cmdReader.Transaction = _currentTransaction;

                if (parameters != null && parameters.Length > 0)
                    cmdReader.Parameters.AddRange(parameters);

                reader = cmdReader.ExecuteReader(CommandBehavior.CloseConnection);
            }

            return reader;
        }

        public DataTable ExecuteDataTable(string commandText)
        {
            return ExecuteDataTable(commandText, null);
        }
      
        public DataTable ExecuteDataTable(string commandText, params SqlParameter[] parameters)
        {
            SqlCommand cmd =null;
            DataTable results;
            try
            {
                results = ExecuteDataTable(out cmd, commandText, parameters);
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Dispose();
            }

            return results;
        }

        public DataTable ExecuteDataTable(out SqlCommand cmd, string commandText, params SqlParameter[] parameters)
        {
            SqlCommand cmdDataTable = BuildCommand(commandText, parameters);

            DataTable result = new DataTable();

            using (SqlDataAdapter da = new SqlDataAdapter(cmdDataTable))
            {
                da.Fill(result);
            }

            cmd = cmdDataTable;
            return result;
        }

        public DataSet ExecuteDataSet(string commandText)
        {
            return ExecuteDataSet(commandText, null);
        }

        public DataSet ExecuteDataSet(string commandText, params SqlParameter[] parameters)
        {
            SqlCommand cmd;
            DataSet results = ExecuteDataSet(out cmd, commandText, parameters);
            cmd.Parameters.Clear();
            cmd.Dispose();

            return results;
        }

        public DataSet ExecuteDataSet(out SqlCommand cmd, string commandText, params SqlParameter[] parameters)
        {
            SqlCommand cmdDataSet = BuildCommand(commandText, parameters);

            DataSet result = new DataSet();

            using (SqlDataAdapter adapter = new SqlDataAdapter(cmdDataSet))
            {
                adapter.Fill(result);
            }

            cmd = cmdDataSet;
            return result;
        }

        public XmlReader ExecuteXmlReader(string commandText)
        {
            return ExecuteXmlReader(commandText, null);
        }

        public XmlReader ExecuteXmlReader(string commandText, params SqlParameter[] parameters)
        {
            DbCommand cmd;
            XmlReader result = ExecuteXmlReader(out cmd, commandText, parameters);
            cmd.Parameters.Clear();
            cmd.Dispose();

            return result;
        }

        public XmlReader ExecuteXmlReader(out DbCommand cmd, string commandText, params SqlParameter[] parameters)
        {
            SqlCommand cmdXmlReader = BuildCommand(commandText, parameters);

            XmlReader outputReader = cmdXmlReader.ExecuteSafeXmlReader();
            cmd = cmdXmlReader;
            return outputReader;
        }

        private SqlCommand BuildCommand(string query, params SqlParameter[] parameters)
        {
            SqlCommand newCommand = new SqlCommand(query, _currentConnection)
            {
                Transaction = _currentTransaction,
                CommandType = CommandType.Text
            };

            if (_commandTimeOut > 0)
            {
                newCommand.CommandTimeout = _commandTimeOut;
            }

            if (parameters != null)
                newCommand.Parameters.AddRange(parameters);

            return newCommand;
        }
    }
}