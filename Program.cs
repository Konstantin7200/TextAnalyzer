
namespace Program
{
    class MainClass
    {
        static public void Main()
        {
            Text text = TextParser.parseText("Text.txt");

            Exporter.exportToXML(text, "text.xml");
        }
    }
}
