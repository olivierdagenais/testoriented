using System;
using System.Collections.Generic;

public struct LatLon
{
  private readonly double _lat, _lon;
  public LatLon(double lat, double lon)
  { _lat = lat; _lon = lon; }
  public double Lat { get { return _lat; } }
  public double Lon { get { return _lon; } }
}

public class FlyingSalespersonProblem
{
  private const double AverageEarthRadius = 6372.8;
  private readonly int _numberOfCities;
  private readonly IDictionary<int, LatLon> _cities;
  private readonly IList<int> _visitingOrder;

  public FlyingSalespersonProblem
    (ICollection<double[]> cities)
  {
    _numberOfCities = cities.Count;
    _cities =
      new Dictionary<int, LatLon>(_numberOfCities);
    _visitingOrder = new List<int>(_numberOfCities);
    foreach (double[] triplet in cities)
    {
      var point = new LatLon(triplet[1], triplet[2]);
      _cities.Add ((int) triplet[0], point);
      _visitingOrder.Add(0);
    }
  }

  public IList<int> VisitingOrder
  { get { return _visitingOrder; } }

  public double ComputeTourLength()
  {
    double tourLength = 0;
    for (int i = 0; i < _numberOfCities; i++)
    {
      int j = (i + 1) % _numberOfCities;
      var from = _cities[_visitingOrder[i]];
      var to = _cities[_visitingOrder[j]];
      var fromLat = from.Lat;
      var fromLon = from.Lon;
      var toLat = to.Lat;
      var toLon = to.Lon;
      var delta1 = ToRadians(fromLat);
      var lambda1 = ToRadians(fromLon);
      var delta2 = ToRadians(toLat);
      var lambda2 = ToRadians(toLon);

      var greatCircleDistance = Math.Acos(
        Math.Cos(delta1)
        * Math.Cos(delta2)
        * Math.Cos(lambda1 - lambda2)
        + Math.Sin(delta1) * Math.Sin(delta2)
        );
      tourLength += AverageEarthRadius
        * greatCircleDistance;
    }
    return tourLength;
  }

  internal static double ToRadians(double degrees)
  {
    return Math.PI * degrees / 180.0;
  }
}
