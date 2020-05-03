using System.Collections.Generic;
using SubtitlesConverter.Domain.Models;
using SubtitlesConverter.Domain.TextProcessing;

namespace SubtitlesConverter.Domain
{
    public class SubtitlesBuilder
    {
        private ITextReader Reader { get; set; } = TextReader.Empty;

        private ITextProcessor Processing { get; set; } = new DoNothing();

        public SubtitlesBuilder For(ITextReader source)
        {
            this.Reader = source;
            return this;
        }

        public SubtitlesBuilder Using(ITextProcessor processor)
        {
            this.Processing = this.Processing.Then(processor);
            return this;
        }

        public Subtitles Build()
        {
            TimedText processed = Reader.Read().Apply(this.Processing);

            TextDurationMeter durationMeter = new TextDurationMeter(processed);
            IEnumerable<TimedLine> subtitles = durationMeter.MeasureLines();

            return new Subtitles(subtitles);
        }
    }
}