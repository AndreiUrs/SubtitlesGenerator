using System.Collections.Generic;
using System.Linq;

namespace SubtitlesConverter.Domain.TextProcessing.Implementation
{
    class TwoWayRepeater : IMultiwaySplitter
    {
        private ITwoWaySplitter Splitter { get; }
     
        public TwoWayRepeater(ITwoWaySplitter splitter)
        {
            this.Splitter = splitter;
        }

        public IEnumerable<string> ApplyTo(string line)
        {
            string remaining = line.Trim();
            while (remaining.Length > 0)
            {
                (string extracted, string rest) = 
                    this.Splitter.ApplyTo(remaining).First();

                yield return extracted;
                remaining = rest.Trim();
            }
        }
    }
}
