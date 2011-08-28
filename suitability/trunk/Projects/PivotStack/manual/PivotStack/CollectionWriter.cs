using System;
using System.IO;
using System.Xml;

namespace PivotStack
{
    internal class CollectionWriter : InterceptingXmlWriter
    {
        private bool _isIntercepting;
        private readonly Action<CollectionWriter> _callback;

        public CollectionWriter (Stream output, XmlWriterSettings settings, Action<CollectionWriter> callback)
            : base (output, settings)
        {
            _isIntercepting = false;
            _callback = callback;
        }

        public override void WriteStartElement (string prefix, string localName, string ns)
        {
            base.WriteStartElement (prefix, localName, ns);
            if (IsCollectionItems(localName, ns))
            {
                _isIntercepting = true;
            }
        }

        internal static bool IsCollectionItems(string localName, string ns)
        {
            return Namespaces.Collection.NamespaceName == ns && "Items" == localName;
        }

        public override void WriteFullEndElement ()
        {
            if (_isIntercepting)
            {
                _callback (this);
                _isIntercepting = false;
            }
            base.WriteFullEndElement ();
        }
    }
}
