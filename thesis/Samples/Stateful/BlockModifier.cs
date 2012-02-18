namespace Textile
{
  public class BlockModifier
  {
    protected internal BlockModifier()
    {
    }

    public virtual string ModifyLine(string line)
    {
      return line;
    }

    public virtual string Conclude(string line)
    {
      return line;
    }
  }
}
