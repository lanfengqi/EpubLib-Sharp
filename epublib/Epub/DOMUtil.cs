using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using nl.siegmann.epublib.util;

namespace nl.siegmann.epublib.epub
{
    public class DOMUtil
    {
        public static String getAttribute(XElement element, String Attribute)
        {
            String result = string.Empty;
            XAttribute attribute = element.Attribute(Attribute);
            if (attribute != null)
            {
                result = attribute.Value;
            }
            return result;
        }

        public static XElement getFirstElementByTagNameNS(XElement parentElement, String Namespace, String tagName)
        {
            IEnumerable<XElement> nodes = parentElement.Elements(Namespace + tagName).Elements<XElement>();
            if (nodes == null)
            {
                return null;
            }
            return nodes.FirstOrDefault();
        }


        public static String getFindAttributeValue(XElement document, String Namespace, String elementName, String findAttributeName, String findAttributeValue, String resultAttributeName)
        {
            IEnumerable<XElement> nodes = document.Elements(Namespace + elementName).Elements<XElement>();
            IEnumerator enumer = nodes.GetEnumerator();

            while (enumer.MoveNext())
            {
                XElement metaElement = (XElement)enumer.Current;
                if (findAttributeValue.Equals(getAttribute(metaElement, findAttributeName))
                    && StringUtil.isNotBlank(getAttribute(metaElement, resultAttributeName)))
                {
                    return getAttribute(metaElement, resultAttributeName);
                }
            }
            return null;
        }
    }
}
