using System.Collections;
using System.Drawing;

public class MortonLayout
{
  private const int NumberOfBits = 32;

  public static Point Decode(int index)
  {
    var bits = new BitArray(new[] {index});
    var x = DecodeAxis(bits, 1);
    var y = DecodeAxis(bits, 0);
    var result = new Point(x, y);
    return result;
  }

  internal static int DecodeAxis
    (BitArray bits, int offset)
  {
    var result = 0;
    for (int o = 1, powerOfTwo = 1;
        o < NumberOfBits; o += 2, powerOfTwo <<= 1)
    {
      if (bits[o - offset])
      {
        result += powerOfTwo;
      }
    }
    return result;
  }
}
