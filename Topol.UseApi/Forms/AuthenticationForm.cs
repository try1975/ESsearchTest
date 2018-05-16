using System;
using System.Reflection;
using System.Security.Principal;
using System.Windows.Forms;
using log4net;
using Topol.UseApi.Administration;
using Topol.UseApi.Ninject;

namespace Topol.UseApi.Forms
{
    public partial class AuthenticationForm : Form
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public AuthenticationForm()
        {
            InitializeComponent();
            tbLogin.Text = GetCurrentUserNameAsLogin();
        }

        private static string GetCurrentUserNameAsLogin()
        {
            var result = WindowsIdentity.GetCurrent().Name;
            var backSlashIndex = result.IndexOf("\\", StringComparison.Ordinal);
            if (backSlashIndex >= 0) result = result.Substring(backSlashIndex + 1);
            return result.ToLower();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
#if DEBUG
            CurrentUser.Login = GetCurrentUserNameAsLogin();
            var mainForm = CompositionRoot.Resolve<Form1>();
            Hide();
            Log.Debug("Start mainform");
            mainForm.Show();
#else
            if (string.IsNullOrEmpty(tbLogin.Text) || string.IsNullOrEmpty(tbPassword.Text))
            {
                MessageBox.Show(@"Please enter login and password.");
                return;
            }
            using (var administrationDataManager = new AdministrationDataManager())
            {
                CurrentUser.Login = tbLogin.Text;
                CurrentUser.Password = tbPassword.Text;
                CurrentUser.Roles = administrationDataManager.Authenticate(tbLogin.Text, tbPassword.Text);
                if (CurrentUser.Roles == null)
                {
                    MessageBox.Show(@"Wrong login or password.");
                }
                else
                {
                    var mainForm = CompositionRoot.Resolve<MainForm>();
                    Hide();
                    mainForm.Show();
                }
            }
#endif
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}