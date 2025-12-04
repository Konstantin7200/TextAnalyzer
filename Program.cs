
namespace Program
{
    class MainClass
    {
        static public void Main()
        {
            Text text = TextParser.parseText("Text.txt");
            
            Exporter.exportToXML(text, "text.xml");
            Console.WriteLine(text.getInnerText()+"\n");
            LogicHandler.printConcordance(text);

            /*LogicHandler.removeAllWordsStartingWithConsonant(text);

            Console.WriteLine(text.getInnerText());*/
            
        }
    }
}
