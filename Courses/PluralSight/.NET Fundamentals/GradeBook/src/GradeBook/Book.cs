using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
  public delegate void GradeAddedDelegate(object sender, EventArgs args);

  public class NamedObject
  {
    public string Name { get; set; }

    public NamedObject(string name)
    {
      Name = name;
    }
  }


  public interface IBook
  {
    void AddGrade(double grade);
    Statistics GetStatistics();
    string Name { get; }
    event GradeAddedDelegate GradeAdded;
  }

  public abstract class Book : NamedObject, IBook
  {
    protected Book(string name) : base(name) {}

    public abstract event GradeAddedDelegate GradeAdded;
    public abstract void AddGrade(double grade);
    public abstract Statistics GetStatistics();
  }

  public class InMemoryBook : Book
  {
    private List<double> grades;
    public override event GradeAddedDelegate GradeAdded;

    public InMemoryBook(string name) : base(name)
    {
      this.grades = new List<double>();
    }

    public void AddGrade(char letter)
    {
      switch (letter)
      {
        case 'A':
          this.AddGrade(90);
          break;

        case 'B':
          this.AddGrade(80);
          break;

        case 'C':
          this.AddGrade(70);
          break;

        default:
          this.AddGrade(0);
          break;
      }
    }
    public override void AddGrade(double grade)
    {
      if (grade >= 0.0 && grade <= 100.0)
      {
        this.grades.Add(grade);
        if (GradeAdded != null)
        {
          GradeAdded(this, new EventArgs());
        }
      }
      else
      {
        throw new ArgumentException($"Invalid {nameof(grade)}: {grade}");
      }
    }
    public override Statistics GetStatistics()
    {
      var result = new Statistics();
      foreach (var grade in grades)
      {
        result.Add(grade);
      }
      return result;
    }
  }

  public class DiskBook : Book
  {
    public override event GradeAddedDelegate GradeAdded;

    public DiskBook(string name) : base(name)
    {}

    public override void AddGrade(double grade)
    {
      using(var file = File.AppendText($"{Name}.txt"))
      {
        file.WriteLine(grade.ToString());
        if (GradeAdded != null)
        {
          GradeAdded(this, new EventArgs());
        }
      }
    }
    public override Statistics GetStatistics()
    {
      var result = new Statistics();

      // var file = File.ReadLines($"{Name}.txt");
      // foreach (var grade in file)
      // {
      //   result.Add(double.Parse(grade));
      // }

      using(var reader = File.OpenText($"{Name}.txt"))
      {
        var line = reader.ReadLine();
        while(line != null)
        {
          var number = double.Parse(line);
          result.Add(number);
          line = reader.ReadLine();
        }
      }

      return result;
    }
  }
}
