using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DAL;
using System.Data.SQLite;
using System.Data;

namespace WebApp.Repository
{
    public class RequestRepositiory : IRequestRepository
    {

        private SqliteSimpleDatabase database;

#if DEBUG
        private string dbName = "devReqDb";

        private void BuildDummyData()
        {
            this.Create(new UnusualRequests() { FullName = "Matt Smith", Occurrence = DateTime.Now.AddDays(-24), QuestionOrRequest = "Some strange request regarding time lords or something?" });
            this.Create(new UnusualRequests() { FullName = "David Tenant", Occurrence = DateTime.Now.AddDays(-52), QuestionOrRequest = "Some strange request regarding time lords or something?" });
            this.Create(new UnusualRequests() { FullName = "Amelia Pond", Occurrence = DateTime.Now.AddDays(-365), QuestionOrRequest = "Had a question about deadlocks and angels or something?" });
        }

#else
        private string dbName = "reqDb";
#endif

        public RequestRepositiory()
        {
            database = new SqliteSimpleDatabase(dbName);
            CreateNotExist();//setup our database
        }
        
        private void CreateNotExist()
        {
            var create = database.GetCommand("CREATE TABLE IF NOT EXISTS Requests (Id integer primary key, FullName nvarchar(256), IllogicalRequest nvarchar(1024), Occurance DATE)");
            database.ExecuteNonQuery(create);
        }

        public void Create(UnusualRequests item)
        {
			var insert = database.GetCommand("INSERT INTO Requests(FullName, IllogicalRequest, Occurance) VALUES (@fn,@ir,@oc)");
            
            database.ExecuteInsert(insert, 
                        new SQLiteParameter() { ParameterName = "@fn", Value = item.FullName }, 
                        new SQLiteParameter() { ParameterName = "@ir", Value = item.QuestionOrRequest }, 
                        new SQLiteParameter() { ParameterName = "@oc", Value = item.Occurrence });
        }
        
        public IEnumerable<UnusualRequests> GetAll()
        {
            var command = database.GetCommand("SELECT * FROM Requests");
            var results = database.GetDataTable(command);

            List<UnusualRequests> data = Parse(results);
            return data;
        }

        private List<UnusualRequests> Parse(DataTable results)
        {
            List<UnusualRequests> data = new List<UnusualRequests>();
            foreach (DataRow row in results.Rows)
            {
                UnusualRequests ur = new UnusualRequests();
                ur.RequestId = int.Parse(row.ItemArray[0].ToString());
                ur.FullName = row.ItemArray[1].ToString();
                ur.QuestionOrRequest = row.ItemArray[2].ToString();
                ur.Occurrence = DateTime.Parse(row.ItemArray[3].ToString());

                data.Add(ur);
            }
            return data;
        }

        public UnusualRequests GetById(int id)
        {
            var command = database.GetCommand("SELECT * FROM Requests WHERE Id = @id");
            var results = database.GetDataTable(command,
                new SQLiteParameter() { ParameterName = "@id", Value = id });

            if (results == null) throw new Exception("No results");
            return Parse(results).First();
        }

        public void Remove(int id)
        {
            var command = database.GetCommand("DELETE FROM Requests WHERE Id = @id");
            var results = database.GetDataTable(command,
               new SQLiteParameter() { ParameterName = "@id", Value = id });
        }

        public void Update(UnusualRequests item)
        {
			var insert = database.GetCommand("UPDATE Requests SET FullName = @fn, IllogicalRequest = @ir, Occurance = @oc WHERE Id = @id");

            database.ExecuteInsert(insert,
                    new SQLiteParameter() { ParameterName = "@fn", Value = item.FullName },
                    new SQLiteParameter() { ParameterName = "@ir", Value = item.QuestionOrRequest },
                    new SQLiteParameter() { ParameterName = "@oc", Value = item.Occurrence },
                    new SQLiteParameter() { ParameterName = "@id", Value = item.RequestId });
        }
    }
}