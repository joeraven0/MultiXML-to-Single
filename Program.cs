using System;
using System.IO;
using System.Xml;

class Program
{
    static string[] xmlFiler;
    static string mappSökväg = "/home/jr/kodprojekt/statistics/files/"; // Ange sökvägen till mappen här
    string outputFilePath = mappSökväg + "log.txt";

    static List<string> UUT_SN;
    static List<string> UUT_Result;
    static List<string> UUT_Date;
    static void Main()
    {

        Program program = new Program();
        program.Get_MTD_files();

        UUT_SN = program.Get_XML_PropField("//Prop[@Name='SerialNumber']");
        UUT_Result = program.Get_XML_Attribute("//Report[@Type='UUT']", "UUTResult");
        UUT_Date = program.Get_XML_PropField("//Prop[@Name='StartDate']//Prop[@Name='ShortText']");
        List<List<string>> output = new List<List<string>>();
        List<string> innerList = new List<string>();
        for(int i=0;i<UUT_SN.Count;i++)
        {            
            innerList.Add(UUT_Date[i]);
            innerList.Add(UUT_SN[i]);
            innerList.Add(UUT_Result[i]);
            output.Add(innerList);
            innerList.Clear();
        }
        foreach (string item in output[0])
        {
            Console.WriteLine(item);
        }
        //program.printAll(UUT_SN, "", UUT_Result, "");
    }
    private List<string> Get_XML_Attribute(string xmlAttribute, string xmlAttributeValue)
    {
        // Skapa ett XmlDocument-objekt och ladda XML-koden

        // Skapa ett XmlDocument-objekt och ladda XML-koden
        List<string> tmpList = new List<string>();
        foreach (string filSökväg in xmlFiler)

        {

            XmlDocument xmlDoc = new XmlDocument();

            string xml = File.ReadAllText(filSökväg);
            xmlDoc.LoadXml(xml);



            // Hitta Report-elementet baserat på attributet Type
            XmlNode reportNode = xmlDoc.SelectSingleNode(xmlAttribute);

            if (reportNode != null)
            {
                // Hämta värdet för attributet UUTResult
                string attribute = reportNode.Attributes[xmlAttributeValue].Value;

                // Skriv ut resultatet
                //Console.WriteLine(uutResult);
                tmpList.Add(attribute);
            }
            else
            {
                Console.WriteLine("Report element not found.");
            }
        }
        return tmpList;
    }
    private void Get_MTD_files()
    {
        File.WriteAllText(outputFilePath, string.Empty);
        xmlFiler = Directory.GetFiles(mappSökväg, "*.mtd");
    }
    private void printAll(List<string> list1, string rule1, List<string> list2, string rule2)
    {
        if (list1.Count == list2.Count)
        {
            for (int num = 0; num < list1.Count; num++)
            {
                if ((rule1.Equals(list1[num])||rule1.Equals(""))&&(rule2.Equals(list2[num])||rule2.Equals("")))
                {
                    Console.WriteLine(list1[num] + " " + list2[num]);
                }
            }
        }
        else
        {
            Console.WriteLine("ERROR::printAll:List size not equal");
        }
    }
    private List<string> Get_XML_PropField(string xmlField)
    {
        List<string> tmpList = new List<string>();
        if (xmlField != null)
        {
            foreach (string filSökväg in xmlFiler)
            {

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filSökväg);
                // Find the desired Prop element based on the Name attribute
                string singlenodequery = xmlField;
                XmlNode prop;

                try
                {
                    prop = xmlDoc.SelectSingleNode(singlenodequery);
                }
                catch (System.Exception)
                {

                    throw;
                }

                if (prop != null)
                {
                    // Get the value from the Value element
                    XmlNode valueNode = prop.SelectSingleNode("Value");
                    string value = valueNode.InnerText;

                    // Output the value
                    if (value == null)
                    {
                        value = "";
                    }
                    tmpList.Add(value);
                }
                else
                {
                    Console.WriteLine("Prop element not found.");
                }
            }
        }
        return tmpList;
    }


}
