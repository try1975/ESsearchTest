using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using AutoMapper;
using Common.Dto;
using log4net;
using Topol.UseApi.Interfaces.Common;

namespace Topol.UseApi.Presenters.Common
{
    public abstract class TypedPresenter<T, TK> : IPresenter where T : class, IDto<TK>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ITypedDataM�nager<T, TK> _typedDataM�nager;
        protected readonly ITypedView<T, TK> View;


        protected TypedPresenter(ITypedView<T, TK> view, ITypedDataM�nager<T, TK> typedDataM�nager,
            IDataM�nager dataM�nager, PresenterMode presenterMode = PresenterMode.Read)
        {
            View = view;
            _typedDataM�nager = typedDataM�nager;
            DataM�nager = dataM�nager;

            BindingSource = new BindingSource();
            BindingSource.CurrentChanged += BindingSourceOnCurrentChanged;
            if (presenterMode == PresenterMode.Read) SetItems();
        }

        public IDataM�nager DataM�nager { get; set; }

        public PresenterMode PresenterMode { get; private set; }

        public BindingSource BindingSource { get; }

        public void SetDetailData()
        {
            try
            {
                T item = null;
                if (BindingSource.Current != null)
                {
                    var current = (DataRowView) BindingSource.Current;
                    item = ToDto(current);
                }
                Mapper.Map(item, View);
                View.EnterDetailsMode();
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                throw;
            }
        }

        public void Reopen()
        {
            SetItems();
        }

        public void AddNew()
        {
            PresenterMode = PresenterMode.AddNew;
            View.EnterAddNewMode();
        }

        public void Edit()
        {
            PresenterMode = PresenterMode.Edit;
            View.EnterEditMode();
        }

        public virtual void Save()
        {
            switch (PresenterMode)
            {
                case PresenterMode.AddNew:
                    Create();
                    break;
                case PresenterMode.Edit:
                    Update();
                    break;
                default:
                    MessageBox.Show(@"Error: You can't use 'Save' button in this mode.", @"Error");
                    break;
            }
        }

        public void Cancel()
        {
            PresenterMode = PresenterMode.Read;
            View.EnterReadMode();
        }

        public virtual async void Delete()
        {
            const string question = @"You really want to delete this record?";
            const string caption = @"Delete warning";
            if (MessageBox.Show(question, caption, MessageBoxButtons.OKCancel) != DialogResult.OK) return;

            var item = Mapper.Map<T>(View);

            //var piChangeBy = typeof(T).GetProperty(nameof(ITrackedDto.ChangeBy));
            //piChangeBy?.SetValue(item, CurrentUser.Login);

            var success = await _typedDataM�nager.DeleteItem(item);
            if (success)
                BindingSource.RemoveCurrent();
            else
                MessageBox.Show(@"Error: You can't delete this item.", @"Error");

            View.EnterReadMode();
            PresenterMode = PresenterMode.Read;
        }

        private void BindingSourceOnCurrentChanged(object sender, EventArgs eventArgs)
        {
            SetDetailData();
        }

        private async void SetItems()
        {
            try
            {
                Log.Debug($"SetItems BindingSource.DataSource");
                Log.Debug($"_typedDataM�nager is null {_typedDataM�nager == null}");
                if (_typedDataM�nager != null)
                {
                    var getItems = await _typedDataM�nager.GetItems();
                    Log.Debug($"getItems is null {getItems == null}");
                    if (getItems != null)
                        BindingSource.DataSource = ToDataTable(getItems.ToList());
                }
                Log.Debug($"SetItems RefreshItems");
                View.RefreshItems();
                View.SetEventHandlers();
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                throw;
            }
        }

        private async void Create()
        {
            var item = Mapper.Map<T>(View);

            //var piChangeBy = typeof(T).GetProperty(nameof(ITrackedDto.ChangeBy));
            //piChangeBy?.SetValue(item, CurrentUser.Login);

            item = await _typedDataM�nager.PostItem(item);
            Mapper.Map(item, View);

            if (item != null)
                BindingSource.DataSource = ToDataTable((await _typedDataM�nager.GetItems()).ToList());
            PresenterMode = PresenterMode.Read;
            View.EnterReadMode();
        }

        private async void Update()
        {
            var item = Mapper.Map<T>(View);

            //var piChangeBy = typeof(T).GetProperty(nameof(ITrackedDto.ChangeBy));
            //piChangeBy?.SetValue(item, CurrentUser.Login);

            item = await _typedDataM�nager.PutItem(item);
            Mapper.Map(item, View);

            BindingSource.DataSource = ToDataTable((await _typedDataM�nager.GetItems()).ToList());

            View.EnterReadMode();
            PresenterMode = PresenterMode.Read;
        }

        private static DataTable ToDataTable(IEnumerable<T> items)
        {
            Log.Info($"ToDataTable start {typeof(T).Name}");
            var dataTable = new DataTable(typeof(T).Name);

            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                var type = prop.PropertyType.IsGenericType &&
                           prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                    ? Nullable.GetUnderlyingType(prop.PropertyType)
                    : prop.PropertyType;
                if (type != null) dataTable.Columns.Add(prop.Name, type);
                Log.Info($"prop.Name {prop.Name}");
            }
            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                    Log.Info($"values[i] {props[i].GetValue(item, null)}");
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        //private static void ToRow(DataRowView data, T item)
        //{
        //    var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //    for (var i = 0; i < props.Length; i++)
        //    {
        //        data.Row.ItemArray[i] = props[i].GetValue(item, null);
        //    }
        //}

        private static T ToDto(DataRowView data)
        {
            var result = (T) Activator.CreateInstance(typeof(T), null);
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            for (var i = 0; i < props.Length; i++)
            {
                var value = data.Row.ItemArray[i];
                if (value != DBNull.Value)
                    props[i].SetValue(result, data.Row.ItemArray[i]);
            }
            return result;
        }
    }
}