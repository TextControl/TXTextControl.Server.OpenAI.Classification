using TXTextControl.DocumentServer.PDF.Contents;

const int MAX_TOKENS = (3897 / 4);

// read filename from user input
Console.WriteLine("Enter the path to the document to classify:");
string filePath = Console.ReadLine();

// return if file does not exist
if (!System.IO.File.Exists(filePath))
{
    Console.WriteLine("File does not exist.");
    return;
}

var documentText = ExtractDocumentText(filePath);

// Limit to max tokens
if (documentText.Length > MAX_TOKENS)
{
    documentText = documentText.Substring(0, MAX_TOKENS);
}

// Classify document
string results = OpenAIChatHelper.ClassifyDocument(documentText);

// Output results to console
Console.WriteLine(results);
Console.WriteLine("Highest probability: " + GetHighestRankingEntry(results));

static string ExtractDocumentText(string filePath)
{
    var documentText = "";
    Lines lines = new Lines(filePath);

    foreach (ContentLine line in lines.ContentLines)
    {
        documentText += line.Text;
    }

    return documentText;
}

static string GetHighestRankingEntry(string rankings)
{
    var entries = rankings.Split(',')
                          .Select(entry => entry.Trim().Split(':'))
                          .ToDictionary(pair => pair[0], pair => double.Parse(pair[1]));

    var highestEntry = entries.Aggregate((l, r) => l.Value > r.Value ? l : r);

    return $"{highestEntry.Key}";
}