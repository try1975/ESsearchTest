namespace PriceCommon.Norm
{
    public interface INorm : INormResult
    {
        string InitialName { get; set; }
        string NormResult { get; set; }
    }
}