using Day13_Ado.Net.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Day13_Ado.Net.SqlSevices
{
    internal class SqlCommands : ISqlCommands
    {
        private readonly NpgsqlConnection _currentConnection;
        private readonly NpgsqlTransaction _currentTransaction;
        private readonly int _commandTimeOut;

        public SqlCommands(NpgsqlConnection currentConnection, NpgsqlTransaction currentTransaction, int commandTimeOut)
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

        public int ExecuteNonQuery(string commandText, params NpgsqlParameter[] parameters)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(commandText);

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

        public int ExecuteNonQuery(out NpgsqlCommand cmd, string commandText, params NpgsqlParameter[] parameters)
        {
            int rowsAffected = 0;

            NpgsqlCommand cmdExecute = BuildCommand(commandText, parameters);
            rowsAffected = cmdExecute.ExecuteNonQuery();

            cmd = cmdExecute;

            return rowsAffected;
        }

        public object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(commandText, null);
        }

        public object ExecuteScalar(string commandText, params NpgsqlParameter[] parameters)
        {
            NpgsqlCommand cmd;
            object result = ExecuteScalar(out cmd, commandText, parameters);
            cmd.Parameters.Clear();
            cmd.Dispose();

            return result;
        }

        public object ExecuteScalar(out NpgsqlCommand cmd, string commandText, params NpgsqlParameter[] parameters)
        {
            // Find the command to execute
            object data = null;

            NpgsqlCommand cmdScalar = BuildCommand(commandText, parameters);

            data = cmdScalar.ExecuteScalar();

            cmd = cmdScalar;
            return data;            
        }

        public NpgsqlDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(commandText, null);
        }

        public NpgsqlDataReader ExecuteReader(string commandText, params NpgsqlParameter[] parameters)
        {
            NpgsqlDataReader reader = null;

            using (NpgsqlCommand cmdReader = new NpgsqlCommand(commandText, _currentConnection))
            {
                cmdReader.CommandType = CommandType.Text;
                cmdReader.Transaction = _currentTransaction;

                if(parameters != null && parameters.Length > 0)
                        cmdReader.Parameters.AddRange(parameters);

                reader = cmdReader.ExecuteReader(CommandBehavior.CloseConnection);
            }

            return reader;
        }

        public DataTable ExecuteDataTable(string commandText)
        {
            return ExecuteDataTable(commandText, null);
        }

        public DataTable ExecuteDataTable(string commandText, params NpgsqlParameter[] parameters)
        {
            NpgsqlCommand cmd = null;
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

        public DataTable ExecuteDataTable(out NpgsqlCommand cmd, string commandText, params NpgsqlParameter[] parameters)
        {
            NpgsqlCommand cmdDataTable = BuildCommand(commandText, parameters);

            DataTable results = new DataTable();

            using(NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmdDataTable))
            {
                da.Fill(results);
            }

            cmd = cmdDataTable;
            return results;
        }

        public DataSet ExecuteDataSet(string commandText)
        {
            return ExecuteDataSet(commandText, null);
        }

        public DataSet ExecuteDataSet(string commandText, params NpgsqlParameter[] parameters)
        {
            NpgsqlCommand cmd;
            DataSet results = ExecuteDataSet(out cmd, commandText, parameters);
            cmd.Parameters.Clear();
            cmd.Dispose();

            return results;
        }

        public DataSet ExecuteDataSet(out NpgsqlCommand cmd, string commandText, params NpgsqlParameter[] parameters)
        {
            NpgsqlCommand cmdDataSet = BuildCommand(commandText, parameters);

            DataSet results = new DataSet();

            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmdDataSet))
            {
                adapter.Fill(results);
            }

            cmd = cmdDataSet;
            return results;
        }
        
        public XmlReader ExecuteXmlReader(string commandText)
        {
            return ExecuteXmlReader(commandText, null);
        }

        public XmlReader ExecuteXmlReader(string commandText, params NpgsqlParameter[] parameters)
        {
            DbCommand cmd;
            XmlReader results = ExecuteXmlReader(out cmd, commandText, parameters);
            cmd.Parameters.Clear();
            cmd.Dispose();

            return results;
        }

        public XmlReader ExecuteXmlReader(out NpgsqlCommand cmd, string commandText, params NpgsqlParameter[] parameters)
        {
            NpgsqlCommand cmdXmlReader = BuildCommand(commandText, parameters);

            XmlReader outputReader = cmdXmlReader.ExecuteSafeXmlReader();
            cmd = cmdXmlReader;
            return outputReader;
        }

        private NpgsqlCommand BuildCommand(string query, params NpgsqlParameter[] parameters)
        {
            NpgsqlCommand newCommand = new NpgsqlCommand(query, _currentConnection)
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
