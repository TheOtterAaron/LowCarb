using System;
using System.IO;
using System.Xml;

namespace FileBaker
{
    public class CSProjectRewriter
    {
        static public void RewriteProject(string project, string newFile, EProjectFiletype newFiletype)
        {
            if (!File.Exists(project))
            {
                throw new FileNotFoundException(String.Format("Project '{0}' not found.", project));
            }

            XmlDocument csproj = new XmlDocument();
            csproj.Load(project);

            XmlNamespaceManager namespaceMgr = new XmlNamespaceManager(csproj.NameTable);
            namespaceMgr.AddNamespace("msbuild", "http://schemas.microsoft.com/developer/msbuild/2003");

            XmlNode itemGroup = null;
            string itemType = "";

            switch (newFiletype)
            {
                case EProjectFiletype.Contract:
                    itemGroup = csproj.SelectSingleNode("/msbuild:Project/msbuild:ItemGroup[msbuild:Content]", namespaceMgr);
                    itemType = "Content";
                    break;
                case EProjectFiletype.UnitTest:
                    itemGroup = csproj.SelectSingleNode("/msbuild:Project/msbuild:ItemGroup[msbuild:Compile]", namespaceMgr);
                    itemType = "Compile";
                    break;
            }

            if (itemGroup == null)
            {
                throw new XmlException("Item group node not found.");
            }

            bool nodeAlreadyExists = false;
            foreach (XmlNode node in itemGroup.SelectNodes("*[@Include]"))
            {
                if (node.Attributes.GetNamedItem("Include").Value == newFile)
                {
                    nodeAlreadyExists = true;
                    break;
                }
            }

            if (!nodeAlreadyExists)
            {
                XmlElement item = csproj.CreateElement(itemType);

                XmlAttribute include = csproj.CreateAttribute("Include");
                include.Value = newFile;
                item.Attributes.Append(include);

                if (newFiletype == EProjectFiletype.Contract)
                {
                    XmlElement copyToOutputDirectory = csproj.CreateElement("CopyToOutputDirectory");
                    copyToOutputDirectory.InnerText = "Always";
                    item.AppendChild(copyToOutputDirectory);
                }

                itemGroup.AppendChild(item);

                csproj.Save(project);
            }
        }
    }
}
