using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Day13_Ado.Net.Interfaces
{
    internal interface ISqlCommands
    {
        int ExecuteNonQuery(string commandText);
        int ExecuteNonQuery(string commandText, params NpgsqlParameter[] parameters);
        int ExecuteNonQuery(out NpgsqlCommand cmd, string commandText, params NpgsqlParameter[] parameters);
        object ExecuteScalar(string commandText);
        object ExecuteScalar(string commandText, params NpgsqlParameter[] parameters);
        object ExecuteScalar(out NpgsqlCommand cmd, string commandText, params NpgsqlParameter[] parameters);
        NpgsqlDataReader ExecuteReader(string commandText);
        NpgsqlDataReader ExecuteReader(string commandText, params NpgsqlParameter[] parameters);
        DataTable ExecuteDataTable (string commandText);
        DataTable ExecuteDataTable(string commandText, params NpgsqlParameter[] parameters);
        DataTable ExecuteDataTable(out NpgsqlCommand cmd, string commandText, params NpgsqlParameter[] parameters);
        DataSet ExecuteDataSet (string commandText);
        DataSet ExecuteDataSet(string commandText, params NpgsqlParameter[] parameters);
        DataSet ExecuteDataSet(out NpgsqlCommand cmd, string commandText, params NpgsqlParameter[] parameters);
        XmlReader ExecuteXmlReader(string commandText);
        XmlReader ExecuteXmlReader(string commandText, params NpgsqlParameter[] parameters);
        XmlReader ExecuteXmlReader(out NpgsqlCommand cmd, string commandText, params NpgsqlParameter[] parameters);
    }
}
