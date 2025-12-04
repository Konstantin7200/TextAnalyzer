using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Program
{
    [XmlRoot("text")]
    public class Text
    {
        [XmlElement("sentence")]
        public List<Sentence> sentences { get; }
        

        public Text(List<Sentence> innerTokens)
        {
            this.sentences = innerTokens;
        }


        public Text() { }


        public string getInnerText()
        {
            string result = "";
            foreach (Sentence sentence in sentences)
            {
                result += sentence.getInnerText();
            }
            return result;
        }
    }
}
