using NUnit.Framework;

[TestFixture]
public class QuadraticEquationTest
{
  [Test]
  public void ComputeRoots_PerfectSquare()
  {
    var qe = new QuadraticEquation(1, 3, -4);
    var actual = qe.ComputeRoots();
    Assert.AreEqual(2, actual.Length);
    Assert.AreEqual(1, actual[0]);
    Assert.AreEqual(-4, actual[1]);
  }

  [Test]
  public void ComputeRoots_NoRoot()
  {
    var qe = new QuadraticEquation(1, 0, 0.5);
    var actual = qe.ComputeRoots();
    Assert.AreEqual(0, actual.Length);
  }

  [Test]
  public void ComputeRoots_OneRoot()
  {
    var qe = new QuadraticEquation(-4, 4, -1);
    var actual = qe.ComputeRoots();
    Assert.AreEqual(1, actual.Length);
    Assert.AreEqual(0.5, actual[0]);
  }

  [Test]
  public void ComputeRoots_TwoRoots()
  {
    var qe = new QuadraticEquation(9, 3, -8);
    var actual = qe.ComputeRoots();
    Assert.AreEqual(2, actual.Length);
    Assert.AreEqual( 0.79076, actual[0], 0.00001);
    Assert.AreEqual(-1.12409, actual[1], 0.00001);
  }
}
