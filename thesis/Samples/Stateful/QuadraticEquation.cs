using System;

public class QuadraticEquation
{
  private readonly double _a, _b, _c;
  public QuadraticEquation
    (double a, double b, double c)
  { _a = a; _b = b; _c = c; }

  public double[] ComputeRoots()
  {
    double[] result;
    var discriminant = _b * _b - 4 * _a * _c;
    if (discriminant == 0)
    {
      result = new[] { -_b / (2 * _a) };
    }
    else if (discriminant < 0)
    {
      result = new double[0];
    }
    else
    {
      result = new[]
      {
        (-_b + Math.Sqrt(discriminant)) / (2 * _a),
        (-_b - Math.Sqrt(discriminant)) / (2 * _a),
      };
    }
    return result;
  }
}
