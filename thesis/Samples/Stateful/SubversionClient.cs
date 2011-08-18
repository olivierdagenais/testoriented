using System;
using System.Diagnostics;
using System.Text;
using System.Xml;

public class Info {
  public int Revision { get; set; }
  public Uri Url { get; set; }
  public Uri Root { get; set; }
  public Guid Uuid { get; set; }
}

public partial class SubversionClient {
  private readonly string _pathToSvnProgram;
  public SubversionClient(string pathToSvnProgram)
  { _pathToSvnProgram = pathToSvnProgram; }

  public Info Info(string pathToWorkingCopy) {
    var sb = new StringBuilder();
    sb.Append("info").Append(' ');
    sb.Append('"');
    sb.Append(pathToWorkingCopy);
    sb.Append('"').Append(' ');
    sb.Append("--xml");
    var arguments = sb.ToString();

    var output = ExecuteSvn(arguments);

    var doc = new XmlDocument();
    doc.LoadXml(output);

    var urlNode = doc.SelectSingleNode(
      "/info/entry/url");
    var rootNode = doc.SelectSingleNode(
      "/info/entry/repository/root");
    var uuidNode = doc.SelectSingleNode(
      "/info/entry/repository/uuid");
    var revisionNode = doc.SelectSingleNode(
      "/info/entry/@revision");

    var result = new Info {
      Revision =
        Convert.ToInt32(revisionNode.Value, 10),
      Url = new Uri(urlNode.InnerText),
      Root = new Uri(rootNode.InnerText),
      Uuid = new Guid(uuidNode.InnerText),
    };
    return result;
  }
}

public partial class SubversionClient {
  private string ExecuteSvn(string arguments) {
    string output;
    using (var p = new Process()) {
      p.StartInfo.UseShellExecute = false;
      p.StartInfo.RedirectStandardOutput = true;
      p.StartInfo.FileName = _pathToSvnProgram;
      p.StartInfo.Arguments = arguments;
      p.Start();
      output = p.StandardOutput.ReadToEnd();
      p.WaitForExit();
    }
    return output;
  }
}
