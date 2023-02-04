namespace Perfect.AnalyzerService.Application;

public class FileAnalyzerService
{
    public int CountOddLettersFromString(string inputString)
    {
        var result = CountOddLetters(inputString);
        return result;
    }

    public int CountOddLettersFromFile(string fileName)
    {
        // var fileContent = IFile
        return 0;
    }

    private int CountOddLetters(string inputString)
    {
        return inputString.Count(x => x % 2 == 1);
    }
}