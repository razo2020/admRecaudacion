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
            var consulta =
                from banco in XElement.Load("C:\\Users\\lalvarado\\Source\\Repos\\AdmRecaudacion\\AdmRecaudacion\\bin\\Debug\\Bancos.XML").Elements("BANCO").Elements("CONF")//Ponemos la direccion del archivo y el nombre de los elementos que queremos obtener
                //where (Convert.ToString(banco.Attribute("name").Value).Trim()).Equals(Banco) &&
                //    (Convert.ToString(banco.Element("CONF").Attribute("name").Value).Trim()).Equals(Config)
                select new Element
                {
                    key = (string)banco.Attribute("key").Value,
                    valueX = Convert.ToInt32(banco.Attribute("valueX").Value),
                    valueY = Convert.ToInt16(banco.Attribute("valueY").Value),
                    type = (string)banco.Attribute("type").Value,
                    titulo = banco.Value

                };//Fin de consulta.
            List<Element> juegos = consulta.ToList<Element>();
            return juegos;
        }//Fin de metdo leerXML.

        //public static void GetConfigKEP()
        //{

        //    var bancos = new Dictionary<string, object>();
        //    var config = new Dictionary<string, object>();
        //    var elementos = new List<Element>();
        //    Element column;
        //    string banco, conf, banc = "";
        //    XmlTextReader reader = new XmlTextReader("Bancos.xml");
        //    reader.Read();
        //    while (reader.Read())
        //    {
        //        XmlNodeType nType = reader.NodeType;
        //        if ((nType == XmlNodeType.Element) && (reader.Name == "BANCO"))
        //        {
        //            banco = reader.GetAttribute("name");
        //            if (!banco.Equals(banc))
        //            {
        //                banc = banco;
        //                bancos.Add(key: banco, value: null);
        //            }
        //            reader.Read();
        //            conf = reader.Name;
        //            while (reader.Read())
        //            {
        //                nType = reader.NodeType;
        //                if ((nType == XmlNodeType.Element) && (reader.Name == "col"))
        //                {
        //                    column = new Element();
        //                    column.key = reader.GetAttribute("key");
        //                    column.valueX = Convert.ToInt32(reader.GetAttribute("valueX"));
        //                    column.valueY = Convert.ToInt16(reader.GetAttribute("valueY"));
        //                    column.type = reader.GetAttribute("type");
        //                    reader.Read();
        //                    column.titulo = reader.Value;
        //                }
        //            }
        //        }
        //    }
        //}

        //private static void IterateThruDictionary()
        //{
        //    Dictionary<string, Element> elements = BuildDictionary();

        //    foreach (KeyValuePair<string, Element> kvp in elements)
        //    {
        //        Element theElement = kvp.Value;

        //        Console.WriteLine("key: " + kvp.Key);
        //        Console.WriteLine("values: " + theElement.Symbol + " " +
        //            theElement.Name + " " + theElement.AtomicNumber);
        //    }
        //}

        //private static Dictionary<string, Element> BuildDictionary()
        //{
        //    var elements = new Dictionary<string, Element>();

        //    AddToDictionary(elements, "K", "Potassium", 19);
        //    AddToDictionary(elements, "Ca", "Calcium", 20);
        //    AddToDictionary(elements, "Sc", "Scandium", 21);
        //    AddToDictionary(elements, "Ti", "Titanium", 22);

        //    return elements;
        //}

        //private static void AddToDictionary(Dictionary<string, Element> elements,
        //    string symbol, string name, int atomicNumber)
        //{
        //    Element theElement = new Element();

        //    theElement.Symbol = symbol;
        //    theElement.Name = name;
        //    theElement.AtomicNumber = atomicNumber;

        //    elements.Add(key: theElement.Symbol, value: theElement);
        //}

        
    }
}
