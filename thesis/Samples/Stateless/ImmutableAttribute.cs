using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct,
  AllowMultiple = false, Inherited = true)]
public class ImmutableAttribute : Attribute
{
}

[Immutable]
public class EarthLocation
{
  private readonly double _lat, _lon, _alt;
  public EarthLocation(double latitude,
    double longitude, double altitude)
  {
    _lat = latitude; 
    _lon = longitude;
    _alt = altitude;
  }
  public double Latitude { get { return _lat; } }
  public double Longitude { get { return _lon; } }
  public double Altitude { get { return _alt; } }
}
