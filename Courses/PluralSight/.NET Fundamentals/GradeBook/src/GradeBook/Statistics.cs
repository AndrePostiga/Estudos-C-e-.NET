using System;
using System.Collections.Generic;

namespace GradeBook
{
  public class Statistics
  {
    private int count;
    private double sum;
    public double Average
    {
      get
      {
        return sum / count;
      }
    }
    public double High;
    public double Low;
    public char Letter
    {
      get
      {
        switch (Average)
        {
          case var d when d >= 90.0:
            return 'A';

          case var d when d >= 80.0:
            return 'B';

          case var d when d >= 70.0:
            return 'C';

          case var d when d >= 60.0:
            return 'D';

          default:
            return 'F';
        }
      }
    }

    readonly List<double> Grades;

    public void Add(double number)
    {
      sum += number;
      count += 1;
      High = Math.Max(number, High);
      Low = Math.Min(number, Low);
    }

    public Statistics()
    {
      High = double.MinValue;
      Low = double.MaxValue;
      sum = 0.0;
      count = 0;
    }
  }
}
