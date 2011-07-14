using System;

public class PolarCoordinate
{
  private readonly double _r, _theta;
  public PolarCoordinate(double r, double theta)
  { _r = r; _theta = theta; }
  public double R { get { return _r; } }
  public double Theta { get { return _theta; } }
}

public class CartesianCoordinate
{
  private readonly double _x, _y;
  public CartesianCoordinate(double x, double y)
  { _x = x; _y = y; }

  public PolarCoordinate ToPolar()
  {
    var xSquared = _x * _x;
    var ySquared = _y * _y;
    var r = Math.Sqrt(xSquared + ySquared);
    var radians = Math.Atan2(_y, _x);
    var theta = radians * ( 180 / Math.PI );
    return new PolarCoordinate(r, theta);
  }
}
