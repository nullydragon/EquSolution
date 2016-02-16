using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace WebApp.DAL
{
    public class SqliteSimpleDatabase
    {
        private readonly string _dbConnection;

        public SqliteSimpleDatabase(string dataSource)
        {
            string dbLocation = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + $"\\{dataSource}.sqlite";
            _dbConnection = string.Format("Data Source = {0}", dbLocation);

            if (!System.IO.File.Exists(dbLocation))
            {
                SQLiteConnection.CreateFile(dbLocation);
            }
        }

        public DataTable GetDataTable(SQLiteCommand command, params SQLiteParameter[] sqlParam)
        {
            if (command == null) throw new ArgumentNullException("command");

            using (SQLiteConnection connection = new SQLiteConnection(_dbConnection))
            {
                connection.Open();
                command.Connection = connection;

                if (sqlParam != null)
                {
                    foreach (var o in sqlParam)
                    {
                        command.Parameters.Add(o);
                    }
                }
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    DataTable result = new DataTable();
                    try
                    {
                        result.Load(reader);
                    }
                    catch
                    {
                        var err = result.GetErrors();
                        throw;
                    }
                    return result;
                }
            }
        }

        public SQLiteCommand GetCommand(string sql)
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentNullException("sql");

            return new SQLiteCommand { CommandText = sql, CommandType = CommandType.Text };
        }

        public int ExecuteNonQuery(SQLiteCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            using (SQLiteConnection connection = new SQLiteConnection(_dbConnection))
            {
                connection.Open();
                command.Connection = connection;

                return command.ExecuteNonQuery();
            }
        }

        public void ExecuteInsert(SQLiteCommand command, params SQLiteParameter[] sqlParam)
        {
            if (command == null) throw new ArgumentNullException("command");

            using (SQLiteConnection connection = new SQLiteConnection(_dbConnection))
            {
                connection.Open();
                command.Connection = connection;

                if (sqlParam == null) throw new ArgumentNullException("missing sql parameters");

                foreach (var o in sqlParam)
                {
                    command.Parameters.Add(o);
                }

                command.ExecuteScalar();               
            }
        }
    }
}