using System;
using System.IO;
using myDataAccessApp.Droid.Persistance;
using myDataAccessApp.Persistence;
using SQLite;
using Xamarin.Forms;

[assembly:Dependency(typeof(SQLiteDb))]
namespace myDataAccessApp.Droid.Persistance
{ 
        public class SQLiteDb : ISQLiteDb
        {
            public SQLiteAsyncConnection GetConnection()
            {
                var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var path = Path.Combine(documentPath, "MySQLite.db3");
                return new SQLiteAsyncConnection(path);
            }
        }
}
