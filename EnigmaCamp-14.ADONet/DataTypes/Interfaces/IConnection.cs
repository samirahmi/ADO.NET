using System.Data;

namespace DataTypes.Interfaces
{
    internal interface IConnection
    {
        string ConnectionString { get; }

        IDbConnection DatabaseConnection { get;}

        bool InTransaction { get; set; }

        void Close();

        void Open();
    }
}