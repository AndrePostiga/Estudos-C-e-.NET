using System;
using Xunit;

namespace GradeBook.Tests
{
  public class TypeTests
  {

    public delegate string WriteLogDelegate(string LogMessage);

    [Fact]
    public void DelegateTesting()
    {
      WriteLogDelegate logFunc = ReturnMessage;
      logFunc += LogConsole;
      logFunc += ReturnMessage;

      var result = logFunc("Hello");


      Assert.Equal("Hello", result);
      Assert.Equal(3, count);
    }

    int count = 0;

    public string LogConsole(string LogMessage)
    {
      count++;
      System.Console.WriteLine(LogMessage);
      return LogMessage;
    }

    public string ReturnMessage(string LogMessage)
    {
      count++;
      return LogMessage;
    }

    [Fact]
    public void ValueTesting()
    {
      var x = GetInt();
      SetInt(x);
      Assert.Equal(3, x);
    }

    private void SetInt(int x)
    {
      x = 42;
    }

    private int GetInt()
    {
      return 3;
    }

    [Fact]
    public void StringsBehaveLikeValueTypes()
    {
      string name = "Scott";
      string upperName = MakeUppercase(name);


      Assert.Equal("Scott", name);
      Assert.Equal("SCOTT", upperName);
    }

    private string MakeUppercase(string name)
    {
      return name.ToUpper();
    }

    [Fact]
    public void CsharpCanPassByRef()
    {
      var book1 = GetBook("Book 1");
      GetBookSetNameByRef(out book1, "New Name");

      Assert.Equal("New Name", book1.Name);
    }

    private void GetBookSetNameByRef(out InMemoryBook book, string name)
    {
      book = new InMemoryBook(name);
    }

    [Fact]
    public void CsharpIsPassByValue()
    {
      var book1 = GetBook("book1");
      GetBookSetName(book1, "New Name");

      Assert.Equal("book1", book1.Name);
    }

    private void GetBookSetName(InMemoryBook book, string name)
    {
      book = new InMemoryBook(name);
    }

    [Fact]
    public void CanSetNameFromReference()
    {
      var book1 = GetBook("book1");
      SetName(book1, "New Name");

      Assert.Equal("New Name", book1.Name);
    }

    private void SetName(InMemoryBook book, string name)
    {
      book.Name = name;
    }

    [Fact]
    public void GetBookReturnsDifferentObjects()
    {
      var book1 = GetBook("Book 1");
      var book2 = GetBook("Book 2");

      Assert.Equal("Book 1", book1.Name);
      Assert.Equal("Book 2", book2.Name);
      Assert.NotSame(book1, book2);
    }

    [Fact]
    public void TwoVarsCanReferenceSameObject()
    {
      var book1 = GetBook("Book 1");
      var book2 = book1;

      Assert.Same(book1, book2);
      Assert.True(Object.ReferenceEquals(book1, book2));
    }

    InMemoryBook GetBook(string name)
    {
      return new InMemoryBook(name);
    }
  }
}
