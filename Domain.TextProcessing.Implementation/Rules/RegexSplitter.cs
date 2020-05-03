using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SubtitlesConverter.Domain.TextProcessing.Implementation
{
    internal class RegexSplitter : ITwoWaySplitter
    {
        private Regex Pattern { get; }
        private Func<Match, string> ExtractLeft { get; }
        private Func<Match, string> ExtractRight { get; }

        public RegexSplitter(
            Regex pattern, 
            Func<Match, string> extractLeft, 
            Func<Match, string> extractRight)
        {
            this.Pattern = pattern;
            this.ExtractLeft = extractLeft;
            this.ExtractRight = extractRight;
        }

        public static ITwoWaySplitter LeftAndRightExtractor(string pattern) =>
            new RegexSplitter(
                new Regex(pattern),
                match => match.Groups["left"].Value,
                match => match.Groups["right"].Value);

        public static ITwoWaySplitter LeftExtractor(string pattern) =>
            new RegexSplitter(
                new Regex(pattern),
                match => match.Groups["left"].Value,
                _ => "");

        public IEnumerable<(string left, string right)> ApplyTo(string line) =>
            this.Pattern
                .Matches(line)
                .Select(match => (this.ExtractLeft(match), this.ExtractRight(match)));
    }
}