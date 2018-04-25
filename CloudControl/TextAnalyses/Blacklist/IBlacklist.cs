namespace Gma.CodeCloud.Controls.TextAnalyses.Blacklist
{
    public interface IBlacklist
    {
        int Count { get; }
        bool Countains(string word);
    }
}