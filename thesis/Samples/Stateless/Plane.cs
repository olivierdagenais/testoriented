using System;

public class Vector
{
  private readonly double _x, _y, _z;
  public Vector(double x, double y, double z)
  {
    _x = x; _y = y; _z = z;
  }
  public double X { get { return _x; } }
  public double Y { get { return _y; } }
  public double Z { get { return _z; } }
}

public class Plane
{
  private readonly Vector _p1, _p2, _p3;
  public Plane(Vector p1, Vector p2, Vector p3)
  {
    _p1 = p1; _p2 = p2; _p3 = p3;
  }
  public double DistanceFromPlane(Vector p)
  {
    var a = new Vector(_p2.X - _p1.X, _p2.Y - _p1.Y, _p2.Z - _p1.Z);
    var b = new Vector(_p3.X - _p1.X, _p3.Y - _p1.Y, _p3.Z - _p1.Z);
    var n = CrossProduct(a, b);
    var length = Math.Sqrt(n.X * n.X + n.Y * n.Y + n.Z * n.Z);
    var normal = new Vector(n.X/length, n.Y/length, n.Z/length);
    var v = new Vector(p.X - _p1.X, p.Y - _p1.Y, p.Z - _p1.Z);
    var distance = DotProduct(v, normal);
    return distance;
  }
  internal static Vector CrossProduct(Vector a, Vector b)
  {
    return new Vector(
      a.Y * b.Z - a.Z * b.Y,
      a.Z * b.X - a.X * b.Z,
      a.X * b.Y - a.Y * b.X);
  }
  internal static double DotProduct(Vector a, Vector b)
  {
    return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
  }
}
