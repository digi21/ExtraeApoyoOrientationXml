using System;
using System.Xml;
using System.Globalization;
using System.IO;

namespace ExtraeApoyoOrientationXml
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Muestra las coordenadas terreno de los puntos de apoyo de una orientación .abs.xml.");

            if (args.Length < 1)
            {
                Console.Error.WriteLine("Error: No has proporcionado los parámetros suficientes.");
                Console.Error.WriteLine("[ruta del archivo .abs.xml a generar con las coordenadas transformadas]");
                Console.Error.WriteLine("");
                Console.Error.WriteLine("Ejemplo:");
                Console.Error.WriteLine(@"ExtraeApoyoOrientationXml E:\Tickets\435\a.abs.xml");
                return;
            }

            var abs = new XmlDocument();
            abs.Load(args[0]);
            
            var nsManager = new XmlNamespaceManager(abs.NameTable);
            nsManager.AddNamespace("abs", "http://schemas.digi21.net/Digi3D/AbsoluteOrientation/v1.0");

            var puntos = abs.SelectNodes("/abs:absolute/abs:points/abs:point", nsManager);
            foreach (XmlNode punto in puntos)
            {
                var nombre = punto.Attributes.GetNamedItem("id").Value;

                var x = double.Parse(punto.SelectSingleNode("abs:ground/abs:x", nsManager).InnerText, CultureInfo.InvariantCulture);
                var y = double.Parse(punto.SelectSingleNode("abs:ground/abs:y", nsManager).InnerText, CultureInfo.InvariantCulture);
                var z = double.Parse(punto.SelectSingleNode("abs:ground/abs:z", nsManager).InnerText, CultureInfo.InvariantCulture);

                Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} {1} {2} {3}", nombre, x, y, z));
            }
        }
    }
}

