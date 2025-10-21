
namespace Program
{
    class MainClass
    {
        static public void Main()
        {
            Text text = TextParser.parseText("Text.txt");

            LogicHandler.sortByLength(text);
            Exporter.exportToXML(text,"text.xml");
        }
    }
}
