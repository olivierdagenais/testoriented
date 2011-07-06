using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;

public class ImageCollection
{
    private static readonly XNamespace DeepZoom2008
            = "http://schemas.microsoft.com/deepzoom/2008";
    private static readonly XName ItemNodeName = DeepZoom2008 + "I";
    private static readonly XName SizeNodeName = DeepZoom2008 + "Size";

    private readonly string _imageFormatName;
    private readonly XElement _sizeNode;
    private readonly string _fileNameIdFormat;
    private readonly IList<int> _postIds = new List<int>();

    public ImageCollection(string imageFormatName, string fileNameIdFormat, int width, int height)
    {
        _imageFormatName = imageFormatName;
        _fileNameIdFormat = fileNameIdFormat;

        // the <Size> element is the same for all <I> elements
        #region <Size Width="800" Height="400" />
        _sizeNode = new XElement(SizeNodeName);
        _sizeNode.SetAttributeValue("Width", width);
        _sizeNode.SetAttributeValue("Height", height);
        #endregion
    }

    public void AddPostId(int postId)
    {
        _postIds.Add (postId);
    }

    public XElement GenerateXml(string relativePathToRoot)
    {
        var itemsNode = new XElement(DeepZoom2008 + "Items");
        var collectionNode = new XElement(DeepZoom2008 + "Collection",
            new XAttribute("MaxLevel", 7),
            new XAttribute("TileSize", 256),
            new XAttribute("Format", _imageFormatName),
            itemsNode
        );

        var mortonNumber = 0;
        var maxPostId = 0;
        foreach (var postId in _postIds)
        {
            #region <I N="0" Id="351" Source="../../../0351.dzi" />
            var itemNode = new XElement(ItemNodeName);
            itemNode.SetAttributeValue("N", mortonNumber);
            itemNode.SetAttributeValue("Id", postId);
            var fileName = postId.ToString (_fileNameIdFormat);
            var relativeDziSubPath = Path.ChangeExtension(fileName, "dzi");
            var relativeDziPath =
                Path.Combine(relativePathToRoot, relativeDziSubPath);
            itemNode.SetAttributeValue("Source", relativeDziPath);
            #endregion

            itemNode.Add(_sizeNode);
            itemsNode.Add(itemNode);

            mortonNumber++;
            maxPostId = Math.Max(maxPostId, postId);
        }

        collectionNode.SetAttributeValue("NextItemId", maxPostId + 1);
        return collectionNode;
    }
}
