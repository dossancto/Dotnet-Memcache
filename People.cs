public class People
{
    public string Name { get; set; } = default!;
    public int Age { get; set; } = default!;

    public static People GetPeople(string id)
    {
        Console.WriteLine("FETCH");
        return
         new()
         {
             Name = "Test",
             Age = 18
         };

    }
}
