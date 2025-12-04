using Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Program
{

    public class Sentence
    {
        [XmlElement("word", Type = typeof(Word))]
        [XmlElement("punctuation", Type = typeof(Punctuation))]
        public List<SimpleToken> innerTokens { get; set; }

        [XmlIgnore]
        public List<Word> words { get; }

        public int length { get; }
        public string type { get; }

        public List<SimpleToken> getInnerTokens()
        {
            return innerTokens;
        }

        public Sentence(List<SimpleToken> innerTokens)
        {
            this.innerTokens = innerTokens;
            length = this.getLength();
            type = getSentenceType(innerTokens[innerTokens.Count - 1].innerText);
            words = getWords(innerTokens);
        }

        public Sentence() { }

        public List<Word> getWords(List<SimpleToken> tokens)
        {
            List<Word> result = new List<Word>();
            foreach (SimpleToken token in tokens)
            {
                if (token.GetType() == typeof(Word))
                {
                    result.Add((Word)token);
                }
            }
            return result;
        }

        private string getSentenceType(string symbol)
        {
            switch (symbol)
            {
                case "?":
                    {
                        return "Interrogative";
                        break;
                    }
                case "!":
                    {
                        return "Imperative";
                        break;
                    }
                default:
                    {
                        return "Declarative";
                        break;
                    }
            }
        }
        public string getInnerText()
        {
            string result = "";
            foreach (SimpleToken token in getInnerTokens())
            {
                if (token.GetType() == typeof(Punctuation) && result.Length != 0)
                    result = result.Substring(0, result.Length - 1);
                result += token.getInnerText() + " ";
            }
            return result;
        }

        public int getWordsCount()
        {
            return words.Count;
        }

        public int getLength()
        {
            return getInnerText().Length - 1;
        }
    }

    public class LengthComparer : IComparer<Sentence>
    {
        public int Compare(Sentence sentence1, Sentence sentence2)
        {
            return sentence1.length.CompareTo(sentence2.length);
        }
    }

    public class WordsCountComparer : IComparer<Sentence>
    {
        public int Compare(Sentence sentence1, Sentence sentence2)
        {
            return sentence1.words.Count.CompareTo(sentence2.words.Count);
        }
    }
}
