// See https://aka.ms/new-console-template for more information
using advent24;

Console.WriteLine("Hello, World!");

var days = new List<IDay>
{
    // new Day1(),
    // new Day2(),
    // new Day3(),
    // new Day4(),
    //new Day5()
    //new Day6(),
    new Day11()
};

foreach (var day in days)
{
    Console.WriteLine(day.GetResult());
}