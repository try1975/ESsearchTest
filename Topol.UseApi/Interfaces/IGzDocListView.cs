namespace Topol.UseApi.Interfaces
{
    public interface IGzDocListView
    {
        void ClearData();
        void Add(string docName, string docUrl);
        void FillByReestrNum(string reestrNumber);
        void GetData(out string url, out string filename);
    }
}