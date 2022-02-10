using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TXQ.Utils.Tool
{
    public static class EXXml
    {
        public static T XmlEXToModel<T>(this string xmlData)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            XmlElement element = xmlDoc.DocumentElement;

            T objModel = System.Activator.CreateInstance<T>();
            foreach (XmlNode childNode in element.ChildNodes)
            {
                PropertyInfo pi = objModel.GetType().GetProperty(childNode.Name);
                if (pi == null) continue;
                if (!String.IsNullOrEmpty(childNode.InnerXml.Trim()))
                    pi.SetValue(objModel, childNode.InnerXml, null);
            }

            return objModel;
        }
    }
}
