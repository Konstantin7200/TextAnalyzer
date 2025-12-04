namespace Program
{
    class TextParser{
        static public Text parseText(string path)
        {
            string textString = File.ReadAllText(path);
            List<SimpleToken> sentence = new List<SimpleToken>();
            List<Sentence> sentences = new List<Sentence>();
            string currentWord = "";
            string wordSplitters = ",:; -";
            string sentenceEnders = ".!?";

            foreach (char symbol in textString)
            {
                if (wordSplitters.Contains(symbol))
                {
                    if (currentWord != "")
                    {
                        if (currentWord.Contains('\n'))
                            currentWord = currentWord.Substring(2);
                        sentence.Add(new Word(currentWord));
                    }
                    currentWord = "";
                    if (symbol != ' ')
                    {
                        sentence.Add(new Punctuation(symbol + ""));
                    }
                }
                else
                {
                    if (sentenceEnders.Contains(symbol))
                    {
                        if (currentWord != "")
                        {
                            sentence.Add(new Word(currentWord));
                        }
                        currentWord = "";
                        sentence.Add(new Punctuation(symbol + ""));
                        sentences.Add(new Sentence(sentence));
                        sentence = new List<SimpleToken>();
                    }
                    else currentWord += symbol;
                }
            }
            return new Text(sentences);
        }
    }
}