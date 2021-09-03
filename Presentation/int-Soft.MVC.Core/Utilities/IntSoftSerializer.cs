using System.IO;
using System.Linq;
using System.Xml;
using IntSoft.DAL.Common;

namespace intSoft.MVC.Core.Utilities
{
    public static class IntSoftSerializer
    {
        public static string Serialize<T>(this T model) where T : IEntity
        {
            var settings = new XmlWriterSettings
            {
                ConformanceLevel = ConformanceLevel.Fragment,
                OmitXmlDeclaration = true
            };

            var properties = model.GetType().GetProperties().Where(x =>
                !x.PropertyType.IsInterface && !x.PropertyType.IsGenericType &&
                !x.PropertyType.GetInterfaces().Contains(typeof (IEntity)))
                .ToList();
            using (var stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    xmlWriter.WriteStartElement(model.GetType().Name);

                    foreach (var notIncludedProp in properties)
                    {
                        var value = notIncludedProp.GetValue(model) == null
                            ? string.Empty
                            : notIncludedProp.GetValue(model).ToString();
                        xmlWriter.WriteElementString(notIncludedProp.Name, value);
                    }
                    xmlWriter.WriteEndElement();
                }
                return stringWriter.ToString();
            }
        }
    }
}