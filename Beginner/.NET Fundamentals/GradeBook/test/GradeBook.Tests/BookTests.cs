using System;
using Xunit;

namespace GradeBook.Tests
{
  public class BookTests
  {
    [Fact]
    public void BookCalculatesAnAverageGrades()
    {
      // arrange
      var book = new InMemoryBook("");
      book.AddGrade(89.1);
      book.AddGrade(90.5);
      book.AddGrade(77.3);

      // act
      var act = book.GetStatistics();

      // asssert
      Assert.Equal(85.6, act.Average, 1);
      Assert.Equal(90.5, act.High);
      Assert.Equal(77.3, act.Low);
      Assert.Equal('B', act.Letter);
    }
  }
}
