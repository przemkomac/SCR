namespace DataAccess.Models
{
    public abstract class BaseThread
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
    }
}