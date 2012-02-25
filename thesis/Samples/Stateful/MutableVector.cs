using System;

public class MutableVector
{
  private double _x, _y, _z;
  public MutableVector(double x, double y, double z)
  {
    _x = x; _y = y; _z = z;
  }
  public double X { get { return _x; } }
  public double Y { get { return _y; } }
  public double Z { get { return _z; } }
  public void Normalize()
  {
    var length = Math.Sqrt(_x * _x + _y * _y + _z * _z);
    _x = _x / length;
    _y = _y / length;
    _z = _z / length; 
  }
}

public class EuclideanVectorSpace
{
  public static double CalculateAngle
    (MutableVector a, MutableVector b)
  {
    a.Normalize();  
    b.Normalize();
    var theta = Math.Acos(a.X * b.X + a.Y * b.Y + a.Z * b.Z);
    return theta * 180 / Math.PI;
  }
}
