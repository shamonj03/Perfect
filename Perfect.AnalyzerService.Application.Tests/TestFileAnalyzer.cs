using Perfect.AnalyzerService.Application;

namespace Perfect.AnalyzerService.Application.Tests;

public class TestFileAnalyzer
{
    [Fact]
    public void Test_CountOddLetters()
    {
        var input = "abcde";
        var expected = 3;

        var validator = new FileAnalyzerService();

        var actual = validator.CountOddLettersFromString(input);

        Assert.Equal(expected, actual);
    }
}