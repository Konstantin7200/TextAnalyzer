using System.Xml.Serialization;
using System.Collections.Generic;

namespace Program
{
    public class SimpleToken
    {
        [XmlText]
        public string innerText;

        protected SimpleToken(string innerText)
        {
            this.innerText = innerText;
        }

        public SimpleToken() { }

        public string getInnerText()
        {
            return innerText;
        }
    }

    public class Word : SimpleToken
    {
        public Word() { }

        public Word(string innerText) : base(innerText)
        {
        }

        override public int GetHashCode()
        {
            return HashCode.Combine(this.getInnerText().ToLower(),typeof(Word));
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Word object1 = (Word)obj;
            return (this.getInnerText().ToLower() == object1.getInnerText().ToLower()) && (this.GetType() == object1.GetType());
        }
    }

    public class Punctuation : SimpleToken
    {
        public Punctuation() { }

        public Punctuation(string innerText) : base(innerText)
        {
        }
    }

    public class Sentence
    {
        [XmlElement("word", Type = typeof(Word))]
        [XmlElement("punctuation", Type = typeof(Punctuation))]
        public List<SimpleToken> innerTokens { get; set; }

        public List<SimpleToken> getInnerTokens()
        {
            return innerTokens;
        }

        public Sentence(List<SimpleToken> innerTokens)
        {
            this.innerTokens = innerTokens;
        }

        public Sentence() { }

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

        public int getWordsNumber()
        {
            int result = 0;
            foreach (SimpleToken token in innerTokens)
            {
                if (token.GetType() == typeof(Word))
                {
                    result++;
                }
            }
            return result;
        }

        public int getLength()
        {
            return getInnerText().Length-1;
        }
    }

   [XmlRoot("text")]
    public class Text
    {
        [XmlElement("sentence")]
        public List<Sentence> innerTokens { get; set; }

        public Text(List<Sentence> innerTokens)
        {
            this.innerTokens = innerTokens;
        }

        public Text() { }

        public List<Sentence> getInnerTokens()
        {
            return innerTokens;
        }

        public string getInnerText()
        {
            string result = "";
            foreach (Sentence sentence in getInnerTokens())
            {
                result += sentence.getInnerText();
            }
            return result;
        }
    }
}