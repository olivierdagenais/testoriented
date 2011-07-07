using System;
using NUnit.Framework;

[TestFixture]
public class FlyingSalespersonProblemTest
{
  /// <summary>
  /// Based on the "Worked example" in Wikipedia's
  /// article on "Great-circle distance",
  /// confirmed with the "Great Circle Calculator" at
  /// http://williams.best.vwh.net/gccalc.htm
  /// </summary>
  [Test]
  public void ComputeTourLength_BnaToLax ()
  {
    // arrange
    var cities = new double[][]
    {
      new double[] {1, 36.12, -86.67},
      new double[] {2, 33.94, -118.40},
    };
    var fsp = new FlyingSalespersonProblem(cities);
    fsp.VisitingOrder[0] = 1;
    fsp.VisitingOrder[1] = 2;

    // act
    // since we computed to AND from, we divide by 2
    var actual = fsp.ComputeTourLength() / 2;

    // assert
    Assert.AreEqual(2887.26, actual, 0.01);
  }

  [Test]
  public void ToRadians()
  {
    Assert.AreEqual(0.0,
      FlyingSalespersonProblem.ToRadians(0.0),
      0.0001);
    Assert.AreEqual(Math.PI,
      FlyingSalespersonProblem.ToRadians(180.0),
      0.0001);
    Assert.AreEqual(2 * Math.PI,
      FlyingSalespersonProblem.ToRadians(360.0),
      0.0001);
  }

}
