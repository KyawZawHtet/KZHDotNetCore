using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

String jsonString = await File.ReadAllTextAsync("mtk-data.json");
var model = JsonConvert.DeserializeObject<MainDto>(jsonString);

// Console.WriteLine(jsonString);

if (model != null)
{
    foreach (var question in model.questions)
    {
        Console.WriteLine(question.questionNo);
    }
}

// JSON to C# => newton package
// C# to JSON

Console.ReadLine();

public class MainDto
{
    public Questions[] questions { get; set; }
    public Answers[] answers { get; set; }
    public string[] numberList { get; set; }
}

public class Questions
{
    public int questionNo { get; set; }
    public string questionName { get; set; }
}

public class Answers
{
    public int questionNo { get; set; }
    public int answerNo { get; set; }
    public string answerResult { get; set; }
}

