using System.Xml.Serialization;
using System.Collections.Generic;

namespace Program
{
    public class Token
    {
        public string tokenName;
    }

    public class SimpleToken : Token
    {
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
            tokenName = "Word";
        }

        override public int GetHashCode()
        {
            return HashCode.Combine(this.getInnerText().ToLower(), this.tokenName);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Word object1 = (Word)obj;
            return (this.getInnerText().ToLower() == object1.getInnerText().ToLower()) && (this.tokenName == object1.tokenName);
        }
    }

    public class Punctuation : SimpleToken
    {
        public Punctuation() { }

        public Punctuation(string innerText) : base(innerText)
        {
            tokenName = "Punctuation";
        }
    }

    public class Sentence : Token
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
            tokenName = "Sentence";
        }

        public Sentence() { }

        public string getInnerText()
        {
            string result = "";
            foreach (SimpleToken token in getInnerTokens())
            {
                if (token.tokenName == "Punctuation" && result.Length != 0)
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
                if (token.tokenName == "Word")
                {
                    result++;
                }
            }
            return result;
        }

        public int getLength()
        {
            int result = 0;
            foreach (SimpleToken token in innerTokens)
            {
                result += token.innerText.Length;
            }
            return result;
        }
    }

    [XmlRoot("text")]
    public class Text : Token
    {
        [XmlElement("sentence")]
        public List<Sentence> innerTokens { get; set; }

        public Text(List<Sentence> innerTokens)
        {
            this.innerTokens = innerTokens;
            tokenName = "Text";
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