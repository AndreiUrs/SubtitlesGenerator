using SubtitlesConverter.Domain.Models;

namespace SubtitlesConverter.Domain
{
    internal class TextReader : ITextReader
    {
        public static ITextReader Empty { get;  } = new TextReader();

        private TextReader() { }
        public TimedText Read() => TimedText.Empty;
    }
}