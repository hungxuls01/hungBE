using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using DataAccessLayer.DAL;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Service.Models;

namespace Service.provider
{

    public class OAuthAppProvider : OAuthAuthorizationServerProvider
    {
        #region Private Properties  

        /// <summary>  
        /// Public client ID property.  
        /// </summary>  
        private readonly string _publicClientId;
        #endregion

        #region Default Constructor method.  

        /// <summary>  
        /// Default Constructor method.  
        /// </summary>  
        /// <param name="publicClientId">Public client ID parameter</param>  
        public OAuthAppProvider(string publicClientId)
        {
            //TODO: Pull from configuration  
            if (publicClientId == null)
            {
                throw new ArgumentNullException(nameof(publicClientId));
            }

            // Settings.  
            _publicClientId = publicClientId;
        }

        #endregion

        #region Grant resource owner credentials override method.  

        /// <summary>  
        /// Grant resource owner credentials overload method.  
        /// </summary>  
        /// <param name="context">Context parameter</param>  
        /// <returns>Returns when task is completed</returns> 
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "*" });
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "*" });
            var studentService = new StudentDAL();
            var user = studentService.GetByEmail(context.UserName);
            if (user == null)
            {
                context.SetError("1", "Tài khoản không tồn tại.");
                return;
            }
            if (user.Campusid != Convert.ToInt32(context.Password))
            {
                context.SetError("1", "Bạn không thuộc chi nhánh này.");
                return;
            }
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.FirstName + " " + user.MiddleName + " " + user.LastName));
            claims.Add(new Claim("userid", user.id.ToString()));

            // Setting Claim Identities for OAUTH 2 protocol.  
            ClaimsIdentity oAuthClaimIdentity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesClaimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationType);

            // Setting user authentication.  
            AuthenticationProperties properties = CreateProperties(user.FirstName + " " + user.MiddleName + " " + user.LastName, user.id.ToString());
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthClaimIdentity, properties);

            // Grant access to authorize user.  
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesClaimIdentity);

        }
        #endregion

        #region Token endpoint override method.

        /// <summary>
        /// Token endpoint override method
        /// </summary>
        /// <param name="context">Context parameter</param>
        /// <returns>Returns when task is completed</returns>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                // Adding.
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            // Return info.
            return Task.FromResult<object>(null);
        }

        #endregion

        #region Validate Client authntication override method

        /// <summary>
        /// Validate Client authntication override method
        /// </summary>
        /// <param name="context">Contect parameter</param>
        /// <returns>Returns validation of client authentication</returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                // Validate Authoorization.
                context.Validated();
            }

            // Return info.
            return Task.FromResult<object>(null);
        }

        #endregion

        #region Validate client redirect URI override method

        /// <summary>
        /// Validate client redirect URI override method
        /// </summary>
        /// <param name="context">Context parmeter</param>
        /// <returns>Returns validation of client redirect URI</returns>
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            // Verification.
            if (context.ClientId == _publicClientId)
            {
                // Initialization.
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                // Verification.
                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    // Validating.
                    context.Validated();
                }
            }

            // Return info.
            return Task.FromResult<object>(null);
        }

        #endregion

        #region Create Authentication properties method.

        /// <summary>
        /// Create Authentication properties method.
        /// </summary>
        /// <param name="userName">User name parameter</param>
        /// <returns>Returns authenticated properties.</returns>
        public static AuthenticationProperties CreateProperties(string name, string id)
        {
            // Settings.
            IDictionary<string, string> data = new Dictionary<string, string>
                                               {
                                                   { "name", name },
                                                   { "id", id }
                                               };

            // Return info.
            return new AuthenticationProperties(data);
        }

        #endregion
    }
}
