using System;
using System.Collections.Generic;

namespace GradeBook
{
  class Program
  {
    static void Main(string[] args)
    {
      IBook book = new DiskBook("Andre Grade Book");
      book.GradeAdded += OnGradeAdded;

      EnterGrades(book);

      var stats = book.GetStatistics();

      Console.WriteLine($"The lowest grade is {stats.Low:N2}");
      Console.WriteLine($"The highest grade is {stats.High:N2}");
      Console.WriteLine($"The average grade is {stats.Average:N2}");
      Console.WriteLine($"The letter grade is {stats.Letter}");
    }

    private static void EnterGrades(IBook book)
    {
      while (true)
      {
        Console.WriteLine("Enter a grade: ");
        var input = Console.ReadLine();

        if (input == "q")
        {
          break;
        }

        try
        {
          var grade = double.Parse(input);
          book.AddGrade(grade);
        }
        catch (ArgumentException ex)
        {
          Console.WriteLine(ex.Message);
        }
        catch (FormatException ex)
        {
          Console.WriteLine(ex.Message);
        }
        finally
        {
          Console.WriteLine("**");
        }

      }
    }

    static void OnGradeAdded(Object sender, EventArgs e)
    {
      Console.WriteLine("A book was add to the list");
    }
  }
}
