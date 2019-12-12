using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FolderByPatternCreator
{
    public class PatternProcessor
    {
        private const string SplitPattern = @"([^\[^\]])*";
        private const string EnumerationSplitter = ",";

        private const string SequenceSplitter = "..";

        public List<string> Get(string patternToProcess)
        {
            var items = new List<string>();

            var regex = new Regex(SplitPattern);
            var matches = regex.Matches(patternToProcess);
            var data = new Queue<string>(matches.Where(m => !String.IsNullOrEmpty(m.Value)).Select(d => d.Value));
            while (data.Count > 0)
            {
                var symbol = data.Dequeue();

                if (symbol.Contains(EnumerationSplitter))
                {
                    items = AddEnumeration(items, symbol);
                }
                else if (symbol.Contains(SequenceSplitter))
                {
                    items = AddSequence(items, symbol);
                }
                else
                {
                    items = Add(items, symbol);
                }
            }

            items.ForEach(f => Console.WriteLine(f));

            return items;
        }

        private const int AlphabetLetterCount = 26;

        private static IEnumerable<int> GetAlphabet()
        {
            var alphabet = Enumerable.Range('A', AlphabetLetterCount);
            return alphabet.Concat(Enumerable.Range('a', AlphabetLetterCount));
        }

        private static void ThrowSequenceDefinitionException(string symbol, Exception inner = null)
        {
            throw new SequenceDefinitionException($"Wrong sequence definition{symbol}, see documentation for correct definition", inner);
        }

        private static List<string> ProcessSequence(List<string> items, List<string> sequence)
        {
            var newItems = new List<string>();

            if (items.Count > 0)
            {
                foreach (var sequenceItem in sequence)
                {
                    items.ForEach(i => newItems.Add($"{i}{sequenceItem}"));
                }
            }
            else
            {
                sequence.ForEach(e => newItems.Add(e));
            }

            return newItems;
        }

        private static List<string> AddSequence(List<string> items, string symbol)
        {
            var newItems = new List<string>();

            var sequence = symbol.Split(SequenceSplitter);
            if (sequence.Count() != 2)
            {
                ThrowSequenceDefinitionException(symbol);
            }

            List<string> sequenceList = new List<string>();

            if (Int32.TryParse(sequence[0], out Int32 min) && Int32.TryParse(sequence[1], out Int32 max))
            {
                // number definition
                sequenceList = Enumerable.Range(min, max - min + 1).Select(i => i.ToString()).ToList();
            }
            else
            {
                // alphabet definition
                var alphabet = GetAlphabet();
                var xx = alphabet.Select(c => ((char)c).ToString()).ToList();
                try
                {
                    if (Char.TryParse(sequence[0], out char firstLatter) && Char.TryParse(sequence[1], out char lastLatter))
                    {
                        sequenceList = alphabet.Where(l => (l >= firstLatter) && (l <= lastLatter)).Select(c => ((char)c).ToString()).ToList();
                    }
                }
                catch (Exception e)
                {
                    ThrowSequenceDefinitionException(symbol, e);
                }
            }

            return ProcessSequence(items, sequenceList);
        }

        private static List<string> AddEnumeration(List<string> items, string symbol)
        {
            return ProcessSequence(items, symbol.Split(EnumerationSplitter).ToList());
        }

        private static List<string> Add(List<string> items, string symbol)
        {
            var newItems = new List<string>();

            if (items.Count > 0)
            {
                items.ForEach(i => newItems.Add($"{i}{symbol}"));
            }
            else
            {
                newItems.Add(symbol);
            }

            return newItems;
        }

    }
}