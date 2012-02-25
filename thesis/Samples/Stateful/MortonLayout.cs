using System.Collections;
using System.Drawing;

public class MortonLayout
{
  private const int NumberOfBits = 32;
  public static Point Decode(int index)
  {
    var bits = new BitArray(new[] {index});
    int x = 0, y = 0;
    for (int o = 1, power = 1; o < NumberOfBits; o += 2, power <<= 1)
    {
      if (bits[o - 1])
      {
        x += power;
      }
      if (bits[o])
      {
        y += power;
      }
    }
    var result = new Point(x, y);
    return result;
  }
}
