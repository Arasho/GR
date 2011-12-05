using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace _3D_Madness
{
    class XML_Parser
    {
        private XmlDocument xDoc {get; set;}
        private XmlNodeList xNode {get; set;}

        public XML_Parser()
        {
            xDoc = new XmlDocument();
            xDoc.Load("Elements.xml");
            xNode = xDoc.GetElementsByTagName("Element");
        }

        public List<Element> XDocParse()
        { 
            List<Element> tempElements = new List<Element>();
            Element tempElement;
            foreach (XmlNode elem in xNode)
            {
                tempElement = new Element(elem.FirstChild.InnerText, elem.FirstChild.NextSibling.InnerText, elem.FirstChild.NextSibling.NextSibling.InnerText, elem.FirstChild.NextSibling.NextSibling.NextSibling.InnerText, elem.LastChild.PreviousSibling.InnerText, elem.LastChild.InnerText);
                tempElements.Add(tempElement);
            }
            return tempElements;
        }
    }

}
