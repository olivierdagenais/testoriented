using System;

[AttributeUsage(AttributeTargets.Method,
  AllowMultiple = false, Inherited = true)]
public class StatelessAttribute : Attribute
{
}

public static class DateFormatter
{
  const string ExtendedIso8601Utc =
    "{0:0000}-{1:00}-{2:00}T{3:00}:{4:00}:{5:00}Z";

  [Stateless]
  public static string FormatIso8601Utc(
    int year, int month, int day,
    int hour, int minute, int second)
  {
    var result = String.Format(
      ExtendedIso8601Utc,
      year, month, day, hour, minute, second);
    return result;
  }
}
