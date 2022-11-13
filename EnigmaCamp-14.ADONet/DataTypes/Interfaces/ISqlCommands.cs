using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Xml;

namespace DataTypes.Interfaces
{
    public interface ISqlCommands
    {
        int ExecuteNonQuery(string commandText);
        int ExecuteNonQuery(string commandText, params SqlParameter[] parameters);
        int ExecuteNonQuery(out SqlCommand cmd, string commandText, params SqlParameter[] parameters);
        object ExecuteScalar(string commandText);
        object ExecuteScalar(string commandText, params SqlParameter[] parameters);
        object ExecuteScalar(out SqlCommand cmd, string commandText, params SqlParameter[] parameters);
        SqlDataReader ExecuteReader(string commandText);
        SqlDataReader ExecuteReader(string commandText, params SqlParameter[] parameters);
        DataTable ExecuteDataTable(string commandText);
        DataTable ExecuteDataTable(string commandText, params SqlParameter[] parameters);
        DataTable ExecuteDataTable(out SqlCommand cmd, string commandText, params SqlParameter[] parameters);
        DataSet ExecuteDataSet(string commandText);
        DataSet ExecuteDataSet(string commandText, params SqlParameter[] parameters);
        DataSet ExecuteDataSet(out SqlCommand cmd, string commandText, params SqlParameter[] parameters);                
        XmlReader ExecuteXmlReader(string commandText);              
        XmlReader ExecuteXmlReader(string commandText, params SqlParameter[] parameters);
        XmlReader ExecuteXmlReader(out DbCommand cmd, string commandText, params SqlParameter[] parameters);
    }
}