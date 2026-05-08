namespace BuilderHacker.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("BuilderHacker Console Application");
            System.Console.WriteLine("Supports: .NET Framework 4.5+, .NET Core 2.0+, and modern .NET 5-10");

            var obj = TestClass.Builder()
                .Name("John")
                .Age(30)
                .Id(1)
                .Build();

            System.Console.WriteLine($"Name: {obj.Name}, Age: {obj.Age}, A: {obj.A}, Id: {obj.GetId}");
        }
    }
}
