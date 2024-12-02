// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var days = new List<IDay>
{
    new Day1(),
    new Day2()
};

foreach (var day in days)
{
    Console.WriteLine(day.GetResult());
}