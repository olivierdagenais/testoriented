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
      result = new[] { Root(_b, discriminant, _a) };
    }
    else if (discriminant < 0)
    {
      result = new double[0];
    }
    else
    {
      var sqrt = Math.Sqrt(discriminant);
      result = new[]
      {
        Root(_b, sqrt, _a),
        Root(_b, -sqrt, _a),
      };
    }
    return result;
  }

  internal static double Root
    (double b, double discriminant, double a)
  {
    return (-b + discriminant) / (2 * a);
  }
}
