using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using Common.Dto;
using Price.WebApi.Logic;

namespace Price.WebApi.Auth
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
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

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public void Dispose()
        {
            //clean-up code here.
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += OnAuthenticateRequest;
        }

        private void OnAuthenticateRequest(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            var context = application.Context;

            var principal = new GenericPrincipal(AuthHelper.GetUserByToken(Guid.Empty, ""), new[] { "" });

            if (!HttpContext.Current.Request.HttpMethod.Equals($"{HttpMethod.Options}", StringComparison.InvariantCultureIgnoreCase))
            {
                var authHeader = HttpContext.Current.Request.Headers[$"{HttpRequestHeader.Authorization}"];
                var userName = HttpContext.Current.Request.Headers[CustomHeaders.UserName] ?? "";
                if (authHeader != null)
                {
                    Guid token;
                    if (Guid.TryParse(authHeader, out token))
                    {
                        if (token != Guid.Empty)
                        {
                            var user = AuthHelper.GetUserByToken(token, userName);
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
        public static IIdentity GetUserByToken(Guid token, string userName)
        {
            if (token.Equals(new Guid(AppGlobal.ExternalToken)))
            {
                return string.IsNullOrEmpty(userName) ? new TopolIdentity(PriceApiContants.WebUsersAndRoles.AdminUser) : new TopolIdentity(userName);
            }
            return new TopolIdentity(nameof(AuthenticationTypes.Unknow)); 
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class TopolIdentity : IIdentity
    {

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public TopolIdentity(string name)
        {
            Name = name;
        }
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public string Name { get; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public string AuthenticationType
        {
            get
            {
                if (Name.Equals(PriceApiContants.WebUsersAndRoles.AdminUser, StringComparison.InvariantCultureIgnoreCase))
                {
                    return nameof(AuthenticationTypes.External);
                }
                if (!string.IsNullOrEmpty(Name))
                {
                    return nameof(AuthenticationTypes.Internal);
                }
                return nameof(AuthenticationTypes.Unknow);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public bool IsAuthenticated => !AuthenticationType.Equals(nameof(AuthenticationTypes.Unknow), StringComparison.InvariantCultureIgnoreCase);
    }
}
