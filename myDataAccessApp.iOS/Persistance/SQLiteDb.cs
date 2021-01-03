using System;
using myDataAccessApp.iOS;
using myDataAccessApp.iOS.Persistance;
using myDataAccessApp.Persistence;
using SQLite;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency(typeof(SQLiteDb))]
namespace myDataAccessApp.iOS.Persistance
{
    public class SQLiteDb:ISQLiteDb
    {
       public SQLiteAsyncConnection GetConnection()
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentPath, "MySQLite.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}
