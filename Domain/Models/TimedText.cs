using System;
using System.Collections.Generic;
using System.Linq;
using SubtitlesConverter.Domain.TextProcessing;

namespace SubtitlesConverter.Domain.Models
{
    public class TimedText
    {
        public IEnumerable<string> Content { get; }
        public TimeSpan Duration { get; }

        public TimedText(IEnumerable<string> content, TimeSpan duration)
        {
            this.Content = content.ToList();
            this.Duration = duration;
        }

        public static TimedText Empty { get; } =
            new TimedText(Enumerable.Empty<string>(), TimeSpan.Zero);

        public TimedText Apply(ITextProcessor processor) =>
            new TimedText(processor.Execute(this.Content), this.Duration);
    }
}
