using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SubtitlesConverter.Domain.Models;

namespace SubtitlesConverter.Domain
{
    internal class TextDurationMeter
    {
        private TimedText Text { get; }

        internal TextDurationMeter(TimedText text)
        {
            this.Text = text;
        }

        public IEnumerable<TimedLine> MeasureLines() =>
            this.MeasureLines(this.GetFullTextWeight());

        private IEnumerable<TimedLine> MeasureLines(double fullTextWeight) =>
            this.Text.Content
                .Select(line => (
                    line: line,
                    relativeWeight: this.CountReadableLetters(line) / fullTextWeight))
                .Select(tuple => (
                    line: tuple.line,
                    milliseconds: this.Text.Duration.TotalMilliseconds * tuple.relativeWeight))
                .Select(tuple => new TimedLine(
                    tuple.line,
                    TimeSpan.FromMilliseconds(tuple.milliseconds)));

        private double GetFullTextWeight() =>
            this.Text.Content.Sum(this.CountReadableLetters);

        private int CountReadableLetters(string text) =>
            Regex.Matches(text, @"\w+")
                .Sum(match => match.Value.Length);
    }
}