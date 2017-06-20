using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using Price.WebApi.Logic;

namespace Price.WebApi
{
   public class TokenModule : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public string ModuleName
        {
            get { return nameof(TokenModule); }
        }

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += OnAuthenticateRequest;
        }

        private void OnAuthenticateRequest(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            var context = application.Context;

            var principal = new GenericPrincipal(AuthHelper.GetUserByToken(Guid.Empty), new[] { "" });

            if (!HttpContext.Current.Request.HttpMethod.Equals($"{HttpMethod.Options}", StringComparison.InvariantCultureIgnoreCase))
            {
                var authHeader = HttpContext.Current.Request.Headers[$"{HttpRequestHeader.Authorization}"];
                if (authHeader != null)
                {
                    Guid token;
                    if (Guid.TryParse(authHeader, out token))
                    {
                        if (token != Guid.Empty)
                        {
                            var user = AuthHelper.GetUserByToken(token);
                            string[] roles = { user.AuthenticationType };
                            principal = new GenericPrincipal(user, roles);
                        }
                    }
                }
            }
            context.User = principal;
        }

        #endregion
    }

    internal static class AuthHelper
    {
        public static IIdentity GetUserByToken(Guid token)
        {
            if (token.Equals(new Guid(AppGlobal.ExternalToken)))
            {
                return new WalletIdentity(PriceApiContants.WebUsersAndRoles.AdminUser);
            }
            return new WalletIdentity(nameof(AuthenticationTypes.Unknow)); ;
        }
    }

    public class WalletIdentity : IIdentity
    {
        public WalletIdentity(string name)
        {
            Name = name;
        }
        public string Name { get; }

        public string AuthenticationType
        {
            get
            {
                if (Name.Equals(PriceApiContants.WebUsersAndRoles.AdminUser, StringComparison.InvariantCultureIgnoreCase))
                {
                    return nameof(AuthenticationTypes.External);
                }
                return nameof(AuthenticationTypes.Unknow);
            }
        }

        public bool IsAuthenticated => !AuthenticationType.Equals(nameof(AuthenticationTypes.Unknow), StringComparison.InvariantCultureIgnoreCase);
    }

    public enum AuthenticationTypes
    {
        Unknow,
        External
    }
}