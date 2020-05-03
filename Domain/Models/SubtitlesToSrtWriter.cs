namespace SubtitlesConverter.Domain.Models
{
    public class SubtitlesToSrtWriter : ISubtitlesVisitor
    {
        private ITextWriter Destination { get; }
        private int LastOrdinal { get; set; }

        public SubtitlesToSrtWriter(ITextWriter destination)
        {
            this.Destination = destination;
            this.LastOrdinal = 0;
        }

        public void Visit(SubtitleLine line)
        {
            this.LastOrdinal += 1;
            this.Destination.AppendLines(
                $"{this.LastOrdinal}",
                $"{line.BeginOffset:hh\\:mm\\:ss\\,fff} --> {line.EndOffset:hh\\:mm\\:ss\\,fff}",
                $"{line.Content}",
                string.Empty);
        }
    }
}
