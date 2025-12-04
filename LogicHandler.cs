namespace Program
{
    class LogicHandler
    {
        static public void sortByWordsNumber(Text text)
        {
            text.sentences.Sort(new WordsCountComparer());
        }
        static public void sortByLength(Text text)
        {
            text.sentences.Sort(new LengthComparer());
        }
        static public List<Sentence> findInterrogativeSentences(Text text)
        {
            List<Sentence> result = new List<Sentence>();

            foreach(Sentence sentence in text.sentences)
            {
                if(sentence.type=="Interrogative")
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
                foreach(Word word in sentence.words)
                {
                    result.Add(word);
                }
            }

            return result.ToList();
        }

        static public void removeStopWords(Text text,string path)
        {
            string stopWordsString = File.ReadAllText(path);
            string[] stopWords = stopWordsString.Split(',');

            foreach(Sentence sentence in text.sentences)
            {
                for(int i=0;i<sentence.words.Count;i++)
                {
                    Word word = sentence.words[i];
                    if (stopWords.Contains(word.innerText))
                    {
                        sentence.words.Remove(word);
                        sentence.innerTokens.Remove(word);
                    }
                }
            }
        }

        static public void removeAllWordsStartingWithConsonant(Text text)
        {   
            foreach(Sentence sentence in text.sentences)
            {
                for (int i = 0; i < sentence.words.Count; i++)
                {
                    Word word = sentence.words[i];
                    if (word.startingFromConsonant)
                    {
                        sentence.words.Remove(word);
                        sentence.innerTokens.Remove(word);
                    }
                }
            }
        }
        static public void swapWordToString(Text text,int sentenceIndex,string wordToSwap,string swap)
        {
            Sentence sentence = text.sentences[sentenceIndex];
            for(int i=0;i<sentence.words.Count;i++)
            {
                if (sentence.words[i].innerText == wordToSwap)
                    sentence.words[i].innerText = swap;
            }
        }

        static public void printConcordance(Text text)
        {
            List<Sentence> sentences = text.sentences;
            SortedDictionary<Word,SortedSet<int>> concordance = new SortedDictionary<Word, SortedSet<int>>(new WordsComparator());

            for (int i=0;i<sentences.Count;i++)
            {
                foreach (Word word in sentences[i].words)
                    if (concordance.ContainsKey(word))
                    {
                        concordance[word].Add(i+1);
                    }
                    else
                    {
                        SortedSet<int> initial = new SortedSet<int>();
                        initial.Add(i+1);
                        concordance.Add(word,initial);
                    }
            }
            foreach(var item in concordance)
            {
                string answer =item.Key.innerText.ToLower()+"    ";
                foreach(int sentenceNumber in item.Value)
                {
                    answer = answer + (" " + sentenceNumber);
                }
                Console.WriteLine(answer);

            }
        }

    }
}