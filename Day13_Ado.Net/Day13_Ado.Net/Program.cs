using Npgsql;
using System.Data;

//=================================================================================================================================
// CONNECTION DATABASE VIA ADO.NET USING NPGSQL
//=================================================================================================================================
//class Program
//{
//    static void Main()
//    {
//        // deklarasi connection string , di sesuaikan dengan SQL Server masing2
//        var connectionString = "Host=localhost;Username=postgres;Password=sami;Database=WarungMakanBahari";

//        NpgsqlConnection connection = null;
//        try
//        {
//            connection = new NpgsqlConnection(connectionString);
//            connection.Open(); // membuka koneksi ke SQL Server atau ke RDBMS lain
//            Console.WriteLine("Successfully connected");
//        }
//        catch (PostgresException e)
//        {
//            Console.WriteLine(e.Message);
//            throw;
//        }
//        finally
//        {
//            connection.Close(); // tutup koneksi dari SQL Server atau RDBMS lain
//            Console.WriteLine("Closing connected");
//        }
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        // deklarasi connection string , di sesuaikan dengan SQL Server masing2
//        var connectionString = "Host=localhost;Username=postgres;Password=sami;Database=ShippingDB";

//        NpgsqlConnection connection = null;
//        try
//        {
//            connection = new NpgsqlConnection(connectionString);

//            // Command perintah untuk menjalankan perintah SQL
//            //NpgsqlCommand cmd = new NpgsqlCommand(@"CREATE TABLE Student (StudentId INT NOT NULL, Name VARCHAR(100))", connection);
//            NpgsqlCommand cmd = new NpgsqlCommand(@"INSERT INTO Student (StudentId, Name) VALUES (1002, 'Rahmatul Putri')", connection);
//            //NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT * FROM Student", connection);

//            connection.Open();
//            cmd.ExecuteNonQuery(); // Eksekusi perintah SQL (CREATE, INSERT)

//            //NpgsqlDataReader sqlDataReader = cmd.ExecuteReader(); // Melakukan eksekusi NpgsqlCommand (SELECT)

//            //while (sqlDataReader.Read())
//            //{
//            //    Console.WriteLine(sqlDataReader["StudentId"] + " - " + sqlDataReader["Name"]);
//            //}

//            Console.WriteLine("Record Inserted Successfully");
//        }
//        catch (PostgresException e)
//        {
//            Console.WriteLine($"Error Occured {e.Message}");
//            throw;
//        }
//        finally
//        {
//            connection.Close(); // tutup koneksi dari SQL Server atau RDBMS lain
//            Console.WriteLine("Closing connected");
//        }
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        // deklarasi connection string , di sesuaikan dengan SQL Server masing2
//        var connectionString = "Host=localhost;Username=postgres;Password=sami;Database=ShippingDB";

//        NpgsqlConnection connection = null;
//        try
//        {
//            connection = new NpgsqlConnection(connectionString);

//            // Command perintah untuk menjalankan perintah SQL
//            //NpgsqlCommand cmd = new NpgsqlCommand(@"CREATE TABLE Student (StudentId INT NOT NULL, Name VARCHAR(100))", connection);
//            //NpgsqlCommand cmd = new NpgsqlCommand(@"INSERT INTO Student (StudentId, Name) VALUES (1001, 'Sami Rahmi')", connection);
//            NpgsqlCommand cmd = new NpgsqlCommand(@"UPDATE Student SET Name = 'Rahmi Safitri' WHERE StudentId = 1001", connection);

//            connection.Open();
//            cmd.ExecuteNonQuery(); // Eksekusi perintah SQL (CREATE, INSERT)

//            // PERINTAH SELECT
//            NpgsqlCommand cm = new NpgsqlCommand(@"SELECT * FROM Student", connection);
//            NpgsqlDataReader sqlDataReader = cm.ExecuteReader(); // Melakukan eksekusi NpgsqlCommand (SELECT)

//            while (sqlDataReader.Read())
//            {
//                Console.WriteLine(sqlDataReader["StudentId"] + " - " + sqlDataReader["Name"]);
//            }

//            Console.WriteLine("Successfully connected");
//        }
//        catch (PostgresException e)
//        {
//            Console.WriteLine($"Error Occured {e.Message}");
//            throw;
//        }
//        finally
//        {
//            connection.Close(); // tutup koneksi dari SQL Server atau RDBMS lain
//            Console.WriteLine("Closing connected");
//        }
//    }
//}


// =====================================================================================================
// DATA SET
// = suatu object yang terdiri dari copy data yang kita minta ke SQL Server via SQL Statement
// 
//class Program
//{
//    static void Main()
//    {
//        var conn = "Host=localhost;Username=postgres;Password=sami;Database=ShippingDB";

//        NpgsqlConnection connection = null;
//        try
//        {
//            connection = new NpgsqlConnection(conn);

//            // NpgsqlDataAdapter mengcopy isi nya ke DataSet
//            NpgsqlDataAdapter sda = new NpgsqlDataAdapter(@"SELECT * FROM Student", connection);
//            // DATA SET
//            DataSet dataSet = new DataSet();
//            sda.Fill(dataSet);

//            Console.WriteLine("Using DataSet");
//            foreach (DataRow row in dataSet.Tables[0].Rows)
//            {
//                Console.WriteLine(row["StudentId"] + " - " + row["Name"]);
//            }

//            // DATA TABLE
//            DataTable dataTable = new DataTable();
//            sda.Fill(dataTable);

//            Console.WriteLine("\nUsing DataTable");
//            foreach (DataRow row in dataTable.Rows)
//            {
//                Console.WriteLine(row["StudentId"] + " - " + row["Name"]);
//            }


//            Console.WriteLine("\nSuccessfully connected");
//        }
//        catch (PostgresException e)
//        {
//            Console.WriteLine($"Error Occured {e.Message}");
//            throw;
//        }
//        finally
//        {
//            connection.Close(); // tutup koneksi dari SQL Server atau RDBMS lain
//            Console.WriteLine("Closing connected");
//        }
//    }
//}


//// MENAMPILKAN 2 TABLE DENGAN DATA ADAPTER
//class Program
//{
//    static void Main()
//    {
//        var conn = "Host=localhost;Username=postgres;Password=sami;Database=ShippingDB";

//        NpgsqlConnection connection = null;
//        try
//        {
//            connection = new NpgsqlConnection(conn);

//            NpgsqlDataAdapter sda = new NpgsqlDataAdapter(
//                @"SELECT * FROM Student; SELECT * FROM Company", connection);

//            sda.TableMappings.Add("Table", "Student");
//            sda.TableMappings.Add("Table1", "Company");

//            DataSet dataSet = new DataSet();
//            sda.Fill(dataSet);

//            Console.WriteLine("Student");
//            foreach (DataRow row in dataSet.Tables[0].Rows)
//            {
//                Console.WriteLine(row["StudentId"] + " - " + row["Name"]);
//            }

//            Console.WriteLine("\nCompany");
//            foreach (DataRow row in dataSet.Tables[1].Rows)
//            {
//                Console.WriteLine(row["CompanyId"] + " - " + row["CompanyName"]);
//            }

//            Console.WriteLine("\nSuccessfully connected");
//        }
//        catch (PostgresException e)
//        {
//            Console.WriteLine($"Error Occured {e.Message}");
//            throw;
//        }
//        finally
//        {
//            connection.Close(); // tutup koneksi dari SQL Server atau RDBMS lain
//            Console.WriteLine("Closing connected");
//        }
//    }
//}

//// MENAMPILKAN 2 TABLE DENGAN DATA READER
//class Program
//{
//    static void Main()
//    {
//        Console.WriteLine("MEMANGGIL 2 TABEL MENGGUNAKAN NpgsqlCommand & NpgsqlDataReader\n");
//        var conn = "Host=localhost;Username=postgres;Password=sami;Database=ShippingDB";

//        NpgsqlConnection connection = null;
//        try
//        {
//            connection = new NpgsqlConnection(conn);

//            NpgsqlCommand cmd = new NpgsqlCommand(
//                @"SELECT * FROM Student; SELECT * FROM Company", connection);

//            connection.Open();
//            NpgsqlDataReader reader = cmd.ExecuteReader();

//            DataSet dataSet = new DataSet();
//            while (!reader.IsClosed)
//            {
//                DataTable dt = new DataTable();
//                dt.Load(reader);
//                dataSet.Tables.Add(dt);
//            }

//            Console.WriteLine("Student");
//            foreach (DataRow row in dataSet.Tables[0].Rows)
//            {
//                Console.WriteLine(row["StudentId"] + " - " + row["Name"]);
//            }

//            Console.WriteLine("\nCompany");
//            foreach (DataRow row in dataSet.Tables[1].Rows)
//            {
//                Console.WriteLine(row["CompanyId"] + " - " + row["CompanyName"]);
//            }

//            Console.WriteLine("\nSuccessfully connected");
//        }
//        catch (PostgresException e)
//        {
//            Console.WriteLine($"Error Occured {e.Message}");
//            throw;
//        }
//        finally
//        {
//            connection.Close(); // tutup koneksi dari SQL Server atau RDBMS lain
//            Console.WriteLine("Closing connected");
//        }
//    }
//}


////ACID
//class Program
//{
//    public static string connectionString = "Host=localhost;Username=postgres;Password=sami;Database=ShippingDB";

//    static void Main()
//    {
//        Console.WriteLine("=====ACID=====\n");

//        try
//        {
//            Console.WriteLine("Before Transaction");
//            GetAccountBalance();
//            MoneyTransfer();
//            Console.WriteLine("\nAfter Transaction");
//            GetAccountBalance();
//        }
//        catch(Exception ex)
//        {
//            Console.WriteLine($"An error accured! {ex.Message}");
//        }

//    }
   
//    private static void MoneyTransfer()
//    {
//        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
//        {
//            connection.Open();

//            NpgsqlTransaction transaction = connection.BeginTransaction();
//            try
//            {
//                NpgsqlCommand cmd = new NpgsqlCommand("UPDATE Accounts SET Balance = Balance - 500 WHERE AccountNumber = 'acc01'", connection);
//                cmd.Transaction = transaction;
//                cmd.ExecuteNonQuery();

//                cmd = new NpgsqlCommand("UPDATE Accounts SET Balance = Balance + 500 WHERE AccountNumber = 'acc02'", connection);
//                cmd.Transaction = transaction;
//                cmd.ExecuteNonQuery();

//                transaction.Commit();
//                Console.WriteLine("=====Transaction Commited");
//            }
//            catch(PostgresException e)
//            {
//                transaction.Rollback();
//                Console.WriteLine("=====Transaction Rollback");
//            }
//        }

//    }

//    private static void GetAccountBalance()
//    {
//        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
//        {
//            connection.Open();
//            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM Accounts", connection);
//            NpgsqlDataReader reader = cmd.ExecuteReader();
//            while (reader.Read())
//            {
//                Console.WriteLine(reader["AccountNumber"] + " - " + reader["CustomerName"] + " - " + reader["balance"]);
//            }

//        }
//    }
//}

// ====================================================================================================================================
// READ XML
// ====================================================================================================================================
//class Program
//{
//    static void Main()
//    {
//        Console.WriteLine("======INI XML======");

//        DataSet dataSet = new DataSet();
//        DataTable dataTable = new DataTable("TableXML");
//        dataTable.Columns.Add("col1", typeof(string));
//        dataSet.Tables.Add(dataTable);

//        string xmlData = "<XmlDS>" +
//                            "<TableXML>" +
//                                "<col1> Value1 </col1>" +
//                            "</TableXML>" +
//                            "<TableXML>" +
//                                "<col1> Value2 </col1>" +
//                            "</TableXML>" +
//                        "</XmlDS>";

//        StringReader xmlSR = new StringReader(xmlData);
//        dataSet.ReadXml(xmlSR, XmlReadMode.IgnoreSchema);

//        foreach(DataRow row in dataSet.Tables[0].Rows)
//        {
//            Console.WriteLine(row[0]);
//        }

//        Console.WriteLine("======INI XML======");
//    }
//}