using System;
using System.IO;
using System.Xml;

class Program
{
    static void Main()
    {
        string mappSökväg = "/home/jr/kodprojekt/statistics/files/"; // Ange sökvägen till mappen här
        string outputFilePath = mappSökväg+"log.txt";
        File.WriteAllText(outputFilePath, string.Empty);
        
        
        string[] xmlFiler = Directory.GetFiles(mappSökväg, "*.xml");
        foreach (string filSökväg in xmlFiler)
        {
            LogXmlToFile(filSökväg, outputFilePath);
        }
    }

    static void LogXmlToFile(string xmlFilePath, string outputFilePath)
    {
        // Skapa en XmlTextReader för att läsa XML-filen
        using (XmlTextReader reader = new XmlTextReader(xmlFilePath))
        {
            // Skapa eller öppna textfilen i append-läget
            using (StreamWriter writer = new StreamWriter(outputFilePath, true))
            {
                DateTime now = DateTime.Now;
                string timestamp = now.ToString("yyyy-MM-dd HH:mm:ss");
                writer.WriteLine("##TEST##"+timestamp);
                // Sök igenom XML-filen
                while (reader.Read())
                {
                    // Kontrollera om den aktuella noden är ett element
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        // Om noden är "OverallResult"
                        if (reader.Name == "OverallResult")
                        {
                            reader.Read(); // Läs värdet av "OverallResult"
                            string overallResult = reader.Value;
                            writer.WriteLine("Overall Result: " + overallResult);
                        }

                        // Om noden är "ReportTime"
                        if (reader.Name == "ReportTime")
                        {
                            reader.Read(); // Läs värdet av "ReportTime"
                            string reportTime = reader.Value;
                            writer.WriteLine("Report Time: " + reportTime);
                        }

                        // Om noden är "ReportID"
                        if (reader.Name == "ReportID")
                        {
                            reader.Read(); // Läs värdet av "ReportID"
                            string reportID = reader.Value;
                            writer.WriteLine("Report ID: " + reportID);
                        }

                        // Om noden är "Sequence"
                        if (reader.Name == "Sequence")
                        {
                            reader.Read(); // Läs värdet av "Sequence"
                            string sequence = reader.Value;
                            writer.WriteLine("Sequence: " + sequence);
                        }

                        // Om noden är "ComputerName"
                        if (reader.Name == "ComputerName")
                        {
                            reader.Read(); // Läs värdet av "ComputerName"
                            string computerName = reader.Value;
                            writer.WriteLine("Computer Name: " + computerName);
                        }
                    }
                }
            }
        }
    }
}
