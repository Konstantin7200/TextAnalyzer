namespace Program
{
    class LogicHandler
    {
        static public void sortByWordsNumber(Text text)
        {
            List<Sentence> sentences = text.getInnerTokens();
            List<(int, Sentence)> tuple = new List<(int, Sentence)>();
            foreach (Sentence sentence in sentences)
            {
                tuple.Add((sentence.getWordsNumber(), sentence));
            }
            sortTuple(tuple);
            Exporter.showTuple(tuple);
        }
        static public void sortByLength(Text text)
        {
            List<Sentence> sentences = text.getInnerTokens();
            List<(int, Sentence)> tuple = new List<(int, Sentence)>();
            foreach (Sentence sentence in sentences)
            {
                tuple.Add((sentence.getLength(), sentence));
            }
            sortTuple(tuple);
            Exporter.showTuple(tuple);
        }
        static public List<Sentence> findInterrogative(Text text)
        {
            List<Sentence> sentences = text.getInnerTokens();
            List<Sentence> result = new List<Sentence>();

            foreach(Sentence sentence in sentences)
            {
                
                string sentenceText = sentence.getInnerText();
                if (sentenceText[sentenceText.Length-2]=='?')
                {
                    result.Add(sentence);
                }
            }

            return result;
        }
        static public List<Word> findAllWords(int length,List<Sentence> sentences)
        {
            HashSet<Word> result = new HashSet<Word>();

            foreach(Sentence sentence in sentences)
            {
                foreach(SimpleToken token in sentence.getInnerTokens())
                {
                    if(token.tokenName=="Word")
                    {
                        if(token.innerText.Length==length)
                        {
                            result.Add((Word) token);
                        }
                    }
                }
            }

            return result.ToList();
        }

        static public Text removeStopWords(Text text,string path)
        {
            List<Sentence> newSentences = new List<Sentence>();
            List<Sentence> sentences = text.getInnerTokens();
            string stopWordsString = File.ReadAllText(path);
            string[] stopWords = stopWordsString.Split(',');

            foreach(Sentence sentence in sentences)
            {
                List<SimpleToken> newSentence = new List<SimpleToken>();
                foreach(SimpleToken token in sentence.getInnerTokens())
                {
                    if (token.tokenName == "Punctuation" ||!stopWords.Contains(token.innerText))
                       newSentence.Add(token);
                }
                newSentences.Add(new Sentence(newSentence));
            }

            return new Text(newSentences);
        }

        static public Text removeAllWordsStartingWithConsonant(Text text)
        {
            List<Sentence> newSentences = new List<Sentence>();
            List<Sentence> sentences = text.getInnerTokens();
            string vowels = "àå¸èîóûýþÿ";
            foreach(Sentence sentence in sentences)
            {
                List<SimpleToken> sentenceComponents = sentence.getInnerTokens();
                List<SimpleToken> newSentenceComponents =new List<SimpleToken>();
                foreach(SimpleToken token in sentenceComponents)
                {
                    if (token.tokenName == "Word")
                    {
                        string word = token.getInnerText().ToLower();
                        if (vowels.Contains(word[0]))
                        {
                            newSentenceComponents.Add(token);
                        }
                    }
                    else newSentenceComponents.Add(token);
                }
                newSentences.Add(new Sentence(newSentenceComponents));
            }


            return new Text(newSentences);
        }
        static public Sentence swapWordToString(Text text,int sentenceNumber,string wordToSwap,string swap)
        {
            List<SimpleToken> sentenceString = new List<SimpleToken>();
            Sentence sentence = text.getInnerTokens()[sentenceNumber];

            foreach(SimpleToken token in sentence.getInnerTokens())
            {
                if(token.tokenName=="Word"&& token.getInnerText() == wordToSwap)
                { 
                    sentenceString.Add(new Word(swap));
                }
                else sentenceString.Add(token);
            }

            return new Sentence(sentenceString);
        }

        static void sortTuple(List<(int, Sentence)> tuple)
        {
            tuple.Sort((x, y) => x.Item1.CompareTo(y.Item1));
        }
    }
}