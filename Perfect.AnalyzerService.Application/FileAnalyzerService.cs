namespace Perfect.AnalyzerService.Application;

public class FileAnalyzerService
{
    public int CountOddLetters(string inputString)
    {
        var result = inputString.Count(x => x % 2 == 1);
        return result;
    }
}