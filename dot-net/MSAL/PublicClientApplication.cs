﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSAL
{
    /// <summary>
    /// Native applications (desktop/phone/iOS/Android).
    /// </summary>
    public class PublicClientApplication
    {
        private const string DEFAULT_AUTHORTIY = "https://login.microsoftonline.com/common";

        /// <summary>
        /// Default consutructor of the application. It is here to emphasise the lack of parameters.
        /// </summary>
        public PublicClientApplication():this(DEFAULT_AUTHORTIY)
        {
        }

        public PublicClientApplication(string authority)
        {
            this.Authority = authority;
        }

        //TODO: consider this for other platforms
        /// <summary>
        /// TODO it would be nice to have a uber constructor just match API footprint with other platforms.
        /// Other platforms do not have syntatic sugar like
        /// new Instance(value){
        ///  Property = prop
        /// }
        /// It would be more developer friendly to have a constructor instead.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="redirectUri"></param>
        public PublicClientApplication(string authority, string clientId, string redirectUri)
                {
                    this.ClientId = clientId;
                    this.RedirectUri = redirectUri;
                }


        //default is true
        public bool ValidateAuthority { get; set; }


        /// <summary>
        /// Will be a default value. Can be overriden by the developer.
        /// </summary>
        public string ClientId { get; set; }
        

        /// <summary>
        /// Redirect Uri configured in the portal. Will have a default value. Not required, if the developer is using the default client ID.
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// .NET specific property that allows configuration of platform 
        /// specific properties. For example, in iOS/Android it woul include the flag to enable/disable broker.
        /// </summary>
        public IPlatformParameters PlatformParameters { get; set; }

        /// <summary>
        /// default false. TODO - We can uniquely identify the user. So, it can solve common problem. Why not have a flag in the cache that tells us if the user logged in at common endpoint
        /// </summary>
        public string RestrictToSingleUser { get; set; }

        /// <summary>
        /// Default will point to login.microsoftonline.com/common. Developer will be able to point to other instances like china/fairfax/blackforest.
        /// </summary>
        public string Authority { get; private set; }


        /// <summary>
        /// Default cache will be 
        /// </summary>
        public TokenCache TokenCache { get; set; }

        /// <summary>
        /// Returns a User centric view over the cache that provides a list of all the signed in users.
        /// </summary>
        public IEnumerable<User> ApplicationUsers
        {
            get;
        }

        /// <summary>
        /// Returns a User centric view over the cache that provides a list of all the signed in users matching the userIdentifierHint.
        /// This method should be an indexer in msal.net
        /// </summary>
        public IEnumerable<User> GetApplicationUsers(string identifier)
        {
            return null;
        }

        //TODO look into adding user userIdentifierHint when domain cannot be queried or privacy settings are against you
        /// <summary>
        /// .NET specific method for intergrated auth. To support Xamarin, we would need to move these to platform specific libraries.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> AcquireTokenWithIntegratedAuthAsync(string[] scope)
        {
            return null;
        }

        /// <summary>
        /// .NET specific method for intergrated auth. To support Xamarin, we would need to move these to platform specific libraries.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="authority"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> AcquireTokenWithIntegratedAuthAsync(string[] scope, string authority)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> AcquireTokenSilentAsync(string[] scope)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> AcquireTokenSilentAsync(string[] scope, string userIdentifier)
        {
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> AcquireTokenSilentAsync(string[] scope, User user)
        {
            return null;
        }
        /// <summary>
        /// Policy parameter is only available in the method with most parameters for now. 
        /// considering most develoeprs do not care about policy, we should not be pushing it on them
        /// in other methods.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="userIdentifier"></param>
        /// <param name="authority"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> AcquireTokenSilentAsync(string[] scope, string userIdentifier,
            string authority, string policy)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="user"></param>
        /// <param name="authority"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> AcquireTokenSilentAsync(string[] scope, User user,
            string authority, string policy)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> AcquireTokenAsync(string[] scope)
        {
            return null;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="userIdentifierHint"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> AcquireTokenAsync(string[] scope, string userIdentifierHint)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="userIdentiferHint"></param>
        /// <param name="extraQueryParameters"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> AcquireTokenAsync(string[] scope, string userIdentifierHint,
            UiOptions options, string extraQueryParameters)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="userId"></param>
        /// <param name="extraQueryParameters"></param>
        /// <param name="additionalScope"></param>
        /// <param name="authority"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> AcquireTokenAsync(string[] scope, string userIdentifierHint,
            UiOptions options, string extraQueryParameters, string[] additionalScope, 
            string authority, string policy)
        {
            return null;
        }

    }
}
