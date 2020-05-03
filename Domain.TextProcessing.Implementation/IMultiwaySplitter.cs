using System.Collections.Generic;

namespace SubtitlesConverter.Domain.TextProcessing.Implementation
{
    public interface IMultiwaySplitter
    {
        IEnumerable<string> ApplyTo(string line);
    }
}
