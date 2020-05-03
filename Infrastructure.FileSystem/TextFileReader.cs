using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using SubtitlesConverter.Domain;
using SubtitlesConverter.Domain.Models;

namespace SubtitlesConverter.Infrastructure.FileSystem
{
    public class TextFileReader : ITextReader
    {
        public TimedText Read() => this.ParseSource();

        private FileInfo Source { get; }

        public TextFileReader(FileInfo source)
        {
            this.Source = source;
        }

        private TimedText ParseSource()
        {
            if (this.Source is null) return TimedText.Empty;

            TimeSpan? initial = null;
            TimeSpan? final = null;
            List<string> content = new List<string>();
            bool beginsInTimeStamp = true;
            bool endedInTimeSpan = false;

            foreach (string line in File.ReadAllLines(this.Source.FullName, Encoding.UTF8))
            {
                if (this.Parse(line) is TimeSpan time)
                {
                    initial = initial ?? time;
                    final = time;
                    endedInTimeSpan = true;
                }
                else
                {
                    content.Add(line);
                    beginsInTimeStamp = beginsInTimeStamp && initial.HasValue;
                    endedInTimeSpan = false;
                }
            }

            if (!beginsInTimeStamp || !endedInTimeSpan)
                throw new InvalidOperationException("Source file is not structured correctly.");

            TimeSpan duration = final.Value.Subtract(initial.Value);
            return new TimedText(content, duration);
        }

        private object Parse(string line)
        {
            Regex timePattern = new Regex(@"^\s*(?<minutes>\d+):(?<seconds>\d+)\s*$");
            Match match = timePattern.Match(line);

            if (!match.Success) return line;

            int minutes = int.Parse(match.Groups["minutes"].Value);
            int seconds = int.Parse(match.Groups["seconds"].Value);

            return new TimeSpan(0, minutes, seconds);
        }
    }
}