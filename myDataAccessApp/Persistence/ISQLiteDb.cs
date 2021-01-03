using System;
using SQLite;
namespace myDataAccessApp.Persistence
{
    public interface ISQLiteDb
    {
         SQLiteAsyncConnection GetConnection();
    }
}
