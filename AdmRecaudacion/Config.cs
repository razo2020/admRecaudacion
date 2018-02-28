using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AdmRecaudacion
{
    public static class Config
    {
        public class Element
        {
            public string titulo { get; set; }
            public string key { get; set; }
            public string type { get; set; }
            public int valueX { get; set; }
            public int valueY { get; set; }
            public string very { get; set; }
            public Element[] url { get; set; }
            public override string ToString() { return key +": "+ titulo; }
        }

        public static List<Element> leerColumnasXML(string Banco, string Config)
        {
            //Load xml
            XElement root = XElement.Load(@"Bancos.XML");

            var bancos = from banco in root.Elements("BANCO")
                        where (string)banco.Attribute("key") == Banco
                        select banco;

            var confi = from conf in bancos.Elements("CONF")
                        where (string)conf.Attribute("key") == Config
                        select conf;

            var column = from col in confi.Elements("col")
                select new Element
                {
                    key = (string)col.Attribute("key").Value,
                    valueX = Convert.ToInt32(col.Attribute("valueX").Value),
                    valueY = Convert.ToInt16(col.Attribute("valueY").Value),
                    type = (string)col.Attribute("type").Value,
                    very = col.Attribute("valor") != null ? col.Attribute("valor").Value : "",
                    titulo = col.Value
                };//Fin de consulta.
            List<Element> elementos = column.ToList<Element>();
            return elementos;
            //return consulta;
        }//Fin de metdo leerXML.

        public static Element[] leerBancosXML()
        {
            XElement root = XElement.Load(@"Bancos.XML");
            var bancos = from banco in root.Elements("BANCO")
                         let rem = from conf in banco.Elements("CONF")
                                   select new Element
                                   {
                                       key = (string)conf.Attribute("key").Value,
                                       titulo = (string)conf.Attribute("url").Value
                                   }
                         select new Element
                         {
                             key = (string)banco.Attribute("key").Value,
                             titulo = (string)banco.Attribute("name").Value,
                             url = rem.ToArray<Element>()
                         };

            Element[] elementos = bancos.ToArray<Element>();
            return elementos;
        }
    }
}
