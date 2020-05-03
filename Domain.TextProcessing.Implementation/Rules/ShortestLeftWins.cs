using System.Collections.Generic;
using System.Linq;
using SubtitlesConverter.Common;

namespace SubtitlesConverter.Domain.TextProcessing.Implementation
{
    internal class ShortestLeftWins : ITwoWaySplitter
    {
        private IEnumerable<ITwoWaySplitter> Splitters { get; }
     
        public ShortestLeftWins(IEnumerable<ITwoWaySplitter> splitters)
        {
            this.Splitters = splitters;
        }

        public IEnumerable<(string left, string right)> ApplyTo(string line) => 
            this.Splitters.SelectMany(rule => rule.ApplyTo(line))
                .DefaultIfEmpty((left: line, right: string.Empty))
                .WithMinimumOrEmpty(tuple => tuple.left.Length);
    }
}