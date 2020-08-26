using System.Linq;
using System.Collections.Generic;

public static class Alphabet
{
    private const int AlphabetLetterCount = 26;

    private static IEnumerable<int> GetAlphabet(char startSymbol)
    {
        return Enumerable.Range(startSymbol, AlphabetLetterCount);
    }

    private static IEnumerable<int> GetLEAlphabet(bool reverse)
    {

        return GetAlphabet('a');
    }

    private static IEnumerable<int> GetBEAlphabet()
    {
        return GetAlphabet('A');
    }
}