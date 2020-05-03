using System;
using System.Collections.Generic;
using System.Linq;

namespace SubtitlesConverter.Domain.Models
{
    public class Subtitles
    {
        private IEnumerable<TimedLine> Lines { get; }

        public Subtitles(IEnumerable<TimedLine> lines)
        {
            this.Lines = lines.ToList();
        }

        public void Accept(ISubtitlesVisitor visitor)
        {
            TimeSpan begin = new TimeSpan(0);
            foreach (TimedLine line in Lines)
            {
                TimeSpan end = begin + line.Duration;
                visitor.Visit(new SubtitleLine(begin, end, line.Content));
                begin = end;
            }
        }
    }
}
