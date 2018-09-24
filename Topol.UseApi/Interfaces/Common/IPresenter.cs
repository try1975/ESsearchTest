using System.Windows.Forms;
using Topol.UseApi.Presenters.Common;

namespace Topol.UseApi.Interfaces.Common
{
    public interface IPresenter
    {
        IDataM�nager DataM�nager { get; set; }

        BindingSource BindingSource { get; }
        PresenterMode PresenterMode { get; }
        void SetDetailData();

        void Reopen();
        void AddNew();
        void Edit();
        void Save();
        void Cancel();
        void Delete();
    }
}