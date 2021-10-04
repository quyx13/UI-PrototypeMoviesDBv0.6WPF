using System.Collections.Generic;
using System.Xml;

namespace UI_PrototypeMoviesDBv0._6WPF.Model
{
    public enum WorkerState
    {
        ready,
        running,
        stopped,
        done,
        abort
    }

    internal static class Xml
    {
        internal static void SaveSettings(string path, Dictionary<string, string> settings)
        {
            XmlDocument xml = new XmlDocument();
            XmlNode rootNode = xml.CreateElement("settings");
            xml.AppendChild(rootNode);

            foreach (var key in settings.Keys)
            {
                XmlNode node = xml.CreateElement(key);
                node.InnerText = settings[key];
                rootNode.AppendChild(node);
            }

            xml.Save(path);
        }

        internal static Dictionary<string, string> LoadSettings(string path)
        {
            var settings = new Dictionary<string, string>();
            XmlDocument xml = new XmlDocument();

            xml.Load(path);
            XmlNodeList nodes = xml.SelectSingleNode("//settings").ChildNodes;

            foreach (XmlNode node in nodes)
            {
                settings.Add(node.Name, node.InnerText);
            }

            return settings;
        }
    }
}