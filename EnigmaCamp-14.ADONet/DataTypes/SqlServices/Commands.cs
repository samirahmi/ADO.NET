using DataTypes.Interfaces;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Xml;

namespace DataTypes.SqlServices
{
    public class Commands : ICommands
    {
        private readonly SqlTransaction currentTransaction;
        private readonly IConnection currentConnection;
        private readonly int commandTimeOut;
        private readonly SqlCommandType commandTypeToUse;

        internal Commands(IConnection currentConnection, IDbTransaction currentTransaction, int commandTimeOut)
        {
            if (currentConnection == null) throw new ArgumentNullException("currentConnection");

            this.currentTransaction = currentTransaction as SqlTransaction;
            this.currentConnection = currentConnection;
            this.commandTimeOut = commandTimeOut;
            commandTypeToUse = new SqlCommandType(this.currentConnection.ConnectionString);
        }

        public int ExecuteNonQuery(string commandText, params DbParameter[] parameters)
        {
            return Execute(x => x.ExecuteNonQuery(),  commandText, parameters);
        }
        public int ExecuteNonQuery(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            return Execute(x => x.ExecuteNonQuery(), out cmd, commandText, parameters);
        }

        public object ExecuteScalar(string commandText, params DbParameter[] parameters)
        {
            return Execute(x => x.ExecuteScalar(), commandText, parameters);
        }

        public object ExecuteScalar(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            return Execute(x => x.ExecuteScalar(), out cmd, commandText, parameters); 
        }

        public DbDataReader ExecuteReader(string commandText, params DbParameter[] parameters)
        {
            SqlDataReader reader;

            currentConnection.Open();

            using (SqlCommand readerCommand = new SqlCommand(commandText, (SqlConnection)currentConnection.DatabaseConnection))
            {
                readerCommand.CommandType = commandTypeToUse.Get(commandText);
                readerCommand.Transaction = currentTransaction;

                if (parameters != null && parameters.Length > 0)
                    readerCommand.Parameters.AddRange(parameters);

                reader = readerCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }

            return reader;
        }

        public DataTable ExecuteDataTable(string commandText, params DbParameter[] parameters)
        {
            DbCommand cmd =null;
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

        public DataTable ExecuteDataTable(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            DataTable result = new DataTable();
            SqlCommand cmdDataTable;

            try
            {
                currentConnection.Open();
                cmdDataTable = BuildCommand(commandText, parameters);

                using (SqlDataAdapter da = new SqlDataAdapter(cmdDataTable))
                {
                    da.Fill(result);
                }
            }
            finally
            {
                currentConnection.Close();
            }

            cmd = cmdDataTable;
            return result;
        }

        public DataSet ExecuteDataSet(string commandText, params DbParameter[] parameters)
        {
            DbCommand cmd;
            DataSet results = ExecuteDataSet(out cmd, commandText, parameters);
            cmd.Parameters.Clear();
            cmd.Dispose();

            return results;
        }

        public DataSet ExecuteDataSet(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            SqlCommand cmdDataSet;

            DataSet result = new DataSet();

            try
            {
                currentConnection.Open();
                cmdDataSet = BuildCommand(commandText, parameters);

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmdDataSet))
                {
                    adapter.Fill(result);
                }
            }
            finally
            {
                currentConnection.Close();
            }

            cmd = cmdDataSet;
            return result;
        }

        public XmlReader ExecuteXmlReader(string commandText, params DbParameter[] parameters)
        {
            DbCommand cmd;
            XmlReader result = ExecuteXmlReader(out cmd, commandText, parameters);
            cmd.Parameters.Clear();
            cmd.Dispose();

            return result;
        }

        public XmlReader ExecuteXmlReader(out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            currentConnection.Open();
            SqlCommand cmdXmlReader = BuildCommand(commandText, parameters);

            XmlReader outputReader = cmdXmlReader.ExecuteSafeXmlReader();
            cmd = cmdXmlReader;
            return outputReader;
        }

        /// <returns>SqlCommand object ready for use</returns>
        private SqlCommand BuildCommand(string commandText, params DbParameter[] parameters)
        {
            SqlCommand newCommand = new SqlCommand(commandText, (SqlConnection) currentConnection.DatabaseConnection)
            {
                Transaction = currentTransaction,
                CommandType = commandTypeToUse.Get(commandText)
            };

            if (commandTimeOut > 0)
            {
                newCommand.CommandTimeout = commandTimeOut;
            }

            if (parameters != null)
                newCommand.Parameters.AddRange(parameters);

            return newCommand;
        }

        private T Execute<T>(Func<SqlCommand, T> commandToExecute, string commandText, params DbParameter[] parameters)
        {
            DbCommand cmd = null;
            T result;
            try
            {
                result = Execute(commandToExecute, out cmd, commandText, parameters);
            }
            finally
            {
                cmd.Parameters.Clear();
                cmd.Dispose();
            }

            return result;            
        }

        private T Execute<T>(Func<SqlCommand, T> commandToExecute, out DbCommand cmd, string commandText, params DbParameter[] parameters)
        {
            SqlCommand toExecute;
            object result;

            try
            {
                currentConnection.Open();
                toExecute = BuildCommand(commandText, parameters);
                result = commandToExecute(toExecute);
                
                cmd = toExecute;
            }
            finally
            {
                currentConnection.Close();
            }

            return (T) result;
        }

    }
}