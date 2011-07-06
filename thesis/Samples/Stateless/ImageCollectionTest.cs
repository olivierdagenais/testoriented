﻿using NUnit.Framework;

[TestFixture]
public class ImageCollectionTest
{
    [Test]
    public void GenerateXml_OneItem()
    {
        var ic = new ImageCollection ("png", "0000", 800, 600);
        ic.AddPostId(1);
        var actual = ic.GenerateXml ("../../");
        Assert.AreEqual(
            "<Collection"
            + " MaxLevel=\"7\" TileSize=\"256\""
            + " Format=\"png\" NextItemId=\"2\""
            + " xmlns=\"http://schemas.microsoft.com/deepzoom/2008\""
            + ">\r\n"
            + "  <Items>\r\n"
            + "    <I N=\"0\" Id=\"1\" Source=\"../../0001.dzi\">\r\n"
            + "      <Size Width=\"800\" Height=\"600\" />\r\n"
            + "    </I>\r\n"
            + "  </Items>\r\n"
            + "</Collection>", actual.ToString());
    }

    [Test]
    public void GenerateItemNode()
    {
        var actual = ImageCollection.GenerateItemNode (0, 1, "0000", "../../");
        Assert.AreEqual(
            "<I N=\"0\" Id=\"1\" Source=\"../../0001.dzi\""
            + " xmlns=\"http://schemas.microsoft.com/deepzoom/2008\" />",
            actual.ToString());
    }
}
