using System;
using NUnit.Framework;

[TestFixture]
public class SubversionClientTest
{
  [Test]
  public void CreateInfoArguments()
  {
    var actual = SubversionClient.
      CreateInfoArguments("c:/tmp/");
    Assert.AreEqual
      (@"info ""c:/tmp/"" --xml", actual);
  }

  [Test]
  public void ParseInfoFromXml()
  {
    var xml = @"<?xml version='1.0'?>
<info>
<entry
   kind='dir'
   path='.'
   revision='233'>
<url>https://testoriented.googlecode.com/svn/thesis</url>
<repository>
<root>https://testoriented.googlecode.com/svn</root>
<uuid>47239c5b-3324-0410-9ae9-6b9b946bea8e</uuid>
</repository>
<wc-info>
<schedule>normal</schedule>
<depth>infinity</depth>
</wc-info>
<commit
   revision='233'>
<author>olivier.dagenais</author>
<date>2011-07-14T15:40:03.796479Z</date>
</commit>
</entry>
</info>";
    var actual = SubversionClient.
      ParseInfoFromXml(xml);
    Assert.AreEqual(233, actual.Revision);
    Assert.AreEqual(new Uri(
      "https://testoriented.googlecode.com/svn/thesis"),
      actual.Url);
    Assert.AreEqual(
      new Uri("https://testoriented.googlecode.com/svn"),
      actual.Root);
    Assert.AreEqual(
      new Guid("47239c5b-3324-0410-9ae9-6b9b946bea8e"),
      actual.Uuid);
  }
}

