using SQLite;

namespace App11Athletics.Models
{
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string LoggedDate { get; set; }
        public string Name { get; set; }
        public int WSets { get; set; }
        public int WReps { get; set; }
        public string Notes { get; set; }
        public bool Done { get; set; }
        public bool NotDone => !Done;
    }
}