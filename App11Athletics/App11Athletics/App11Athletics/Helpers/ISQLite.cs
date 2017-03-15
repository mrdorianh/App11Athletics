using SQLite;

namespace App11Athletics.Helpers
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
