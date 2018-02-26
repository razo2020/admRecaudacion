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
        }

        public static List<Element> leerXML(string Banco, string Config)
        {
            //Load xml
            XElement root = XElement.Load(@"Bancos.XML");

            var bancos = from banco in root.Elements("BANCO")
                        where (string)banco.Attribute("name") == Banco
                        select banco;

            var confi = from conf in bancos.Elements("CONF")
                        where (string)conf.Attribute("name") == Config
                        select conf;
            var column = from col in confi.Elements("col")
                select new Element
                {
                    key = (string)col.Attribute("key").Value,
                    valueX = Convert.ToInt32(col.Attribute("valueX").Value),
                    valueY = Convert.ToInt16(col.Attribute("valueY").Value),
                    type = (string)col.Attribute("type").Value,
                    titulo = col.Value
                };//Fin de consulta.
            List<Element> elementos = column.ToList<Element>();
            return elementos;
            //return consulta;
        }//Fin de metdo leerXML.
    }
}
