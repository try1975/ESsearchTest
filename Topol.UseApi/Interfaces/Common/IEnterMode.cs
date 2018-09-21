namespace Topol.UseApi.Interfaces.Common
{
    public interface IEnterMode
    {
        #region Enter mode

        void EnterAddNewMode();

        void EnterEditMode();

        void EnterDetailsMode();

        void EnterReadMode();

        void EnterMultiSelectMode();

        void ClearInputFields();
        void EnableInput();
        void DisableInput();

        #endregion //Enter mode
    }
}