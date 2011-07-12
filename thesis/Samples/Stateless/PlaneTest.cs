using NUnit.Framework;

[TestFixture]
public class PlaneTest
{
  [Test]
  public void DistanceFromPlane_PointOnPlane()
  {
    var p1 = new Vector(0, 0, 0);
    var p2 = new Vector(3, -3, 1);
    var p3 = new Vector(4, 9, 2);
    var p = new Plane(p1, p2, p3);
    var actual = p.DistanceFromPlane(p3);
    Assert.AreEqual(0, actual);
  }

  [Test]
  public void DotProduct ()
  {
    var actual = Plane.DotProduct(
      new Vector(10, 2, 30),
      new Vector(3, 9, 4)
    );
    Assert.AreEqual(168, actual);
  }

  [Test]
  public void CrossProduct()
  {
    var actual = Plane.CrossProduct(
      new Vector(3, -3, 1),
      new Vector(4, 9, 2)
    );
    Assert.AreEqual(-15, actual.X);
    Assert.AreEqual( -2, actual.Y);
    Assert.AreEqual( 39, actual.Z);
  }
}
