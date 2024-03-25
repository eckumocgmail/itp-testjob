namespace ItpDal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            using (var db = new ItpDbContext())
            {
                foreach(var task in db.Tasks.ToList())
                {
                    Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(task));
                }
            }
        }
    }
}