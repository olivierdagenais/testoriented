namespace Textile
{
  public class BlockModifier
  {
    internal BlockModifier()
    {
    }

    public virtual string ModifyLine(string line)
    {
      return line;
    }

    internal virtual string Conclude(string line)
    {
      return line;
    }
  }
}
