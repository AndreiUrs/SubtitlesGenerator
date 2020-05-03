using System.Collections.Generic;

namespace SubtitlesConverter.Domain.TextProcessing.Implementation
{
    interface ITwoWaySplitter
    {
        IEnumerable<(string left, string right)> ApplyTo(string line);
    }
}