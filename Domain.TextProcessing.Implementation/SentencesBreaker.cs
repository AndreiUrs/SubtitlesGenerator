namespace SubtitlesConverter.Domain.TextProcessing.Implementation
{
    public class SentencesBreaker : RuleBasedProcessor
    {
        protected override IMultiwaySplitter Splitter { get; } = 
            new[]
            {
                RegexSplitter.LeftAndRightExtractor(@"^(?<left>[^\?*]+\?)\s*(?<right>.*)$"),
                RegexSplitter.LeftAndRightExtractor(@"^(?<left>[^\!*]+\!)\s*(?<right>.*)$"),
                RegexSplitter.LeftAndRightExtractor(@"^(?<left>(?:(?:\.\.\.)|[^\.])+)\.\s*(?<right>.*)$"),
                RegexSplitter.LeftAndRightExtractor(
                    @"(?<left>^.*\.\.\.)(?=(?:\s+\p{Lu})|(?:\s+\p{Lt})|\s*$)\s*(?<right>.*)$"),
                RegexSplitter.LeftExtractor(@"^(?<left>.*(?<!\.))\.$"),
                RegexSplitter.LeftExtractor(@"^(?<left>.*)(?:[\:\;\,]|\s+-\s*)$"),
            }
            .WithShortestLeft()
            .Repeat();
    }
}
