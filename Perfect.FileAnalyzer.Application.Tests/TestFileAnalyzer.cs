using Perfect.AnalyzerService.Application;

namespace Perfect.FileAnalyzer.Application.Tests;

public class TestFileAnalyzer
{
    [Fact]
    public void Test_CountOddLetters()
    {
        var input = "abcde";
        var expected = 3;

        var validator = new FileAnalyzerService();

        var actual = validator.CountOddLetters(input);
        
        Assert.Equal(expected, actual);
    }
}