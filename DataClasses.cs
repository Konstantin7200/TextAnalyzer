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
        public bool startingFromConsonant{get;}
        public int length { get; }
        public Word() { }


        
        public Word(string innerText) : base(innerText)
        {
            startingFromConsonant = checkIfStartingFromConsonant();
            length = innerText.Length;
        }


        private bool checkIfStartingFromConsonant()
        {
            string vowels = "àå¸èîóûýþÿ";
            if (vowels.Contains(innerText.ToLower()[0]))
                return false;
            else return true;
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

    public class WordsComparator:IComparer<Word>
    {
        public int Compare(Word word1,Word word2)
        {
            return word1.innerText.ToLower().CompareTo(word2.innerText.ToLower());
        }
    }

    public class Punctuation : SimpleToken
    {
        public Punctuation() { }

        public Punctuation(string innerText) : base(innerText)
        {
        }
    }


   
}