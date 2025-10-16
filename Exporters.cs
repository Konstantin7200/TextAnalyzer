using System.Xml.Serialization;
using System;

namespace Program
{
    class Exporter
    {
        public static void showTuple(List<(int, Sentence)> tuple)
        {
            for (int i = 0; i < tuple.Count; i++)
            {
                Console.WriteLine(tuple[i].Item2.getInnerText()+tuple[i].Item1);
            }
        }

        public static void exportToXML(Text text,string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Word));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, text.getInnerTokens()[0].getInnerTokens()[0]);
            }
        }
    }
}