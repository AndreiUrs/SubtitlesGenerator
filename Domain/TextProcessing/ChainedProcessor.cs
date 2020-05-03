using System.Collections.Generic;

namespace SubtitlesConverter.Domain.TextProcessing
{
    class ChainedProcessor : ITextProcessor
    {
        private ITextProcessor Inner { get; }
        private ITextProcessor Next { get; }
        
        public ChainedProcessor(ITextProcessor inner, ITextProcessor next)
        {
            this.Inner = inner;
            this.Next = next;
        }

        public IEnumerable<string> Execute(IEnumerable<string> text) =>
            this.Next.Execute(this.Inner.Execute(text));
    }
}
