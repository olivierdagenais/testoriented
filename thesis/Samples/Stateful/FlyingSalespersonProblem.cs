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
      var delta1 = Math.PI * from.Lat / 180.0;
      var lambda1 = Math.PI * from.Lon / 180.0;
      var delta2 = Math.PI * to.Lat / 180.0;
      var lambda2 = Math.PI * to.Lon / 180.0;

      tourLength += AverageEarthRadius
        * Math.Acos(
            Math.Cos(delta1)
          * Math.Cos(delta2) 
          * Math.Cos(lambda1 - lambda2) 
          + Math.Sin(delta1) * Math.Sin(delta2)
        );
    }
    return tourLength;
  }
}
