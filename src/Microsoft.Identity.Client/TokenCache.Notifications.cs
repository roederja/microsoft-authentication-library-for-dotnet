﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client
{
    public sealed partial class TokenCache : ITokenCacheInternal
    {
        /// <summary>
        /// Notification method called before any library method accesses the cache.
        /// </summary>
        internal TokenCacheCallback BeforeAccess { get; set; }

        /// <summary>
        /// Notification method called before any library method writes to the cache. This notification can be used to reload
        /// the cache state from a row in database and lock that row. That database row can then be unlocked in the
        /// <see cref="AfterAccess"/>notification.
        /// </summary>
        internal TokenCacheCallback BeforeWrite { get; set; }

        /// <summary>
        /// Notification method called after any library method accesses the cache.
        /// </summary>
        internal TokenCacheCallback AfterAccess { get; set; }

        internal Func<TokenCacheNotificationArgs, Task> AsyncBeforeAccess { get; set; }
        internal Func<TokenCacheNotificationArgs, Task> AsyncAfterAccess { get; set; }
        internal Func<TokenCacheNotificationArgs, Task> AsyncBeforeWrite { get; set; }

        internal async Task OnAfterAccessAsync(TokenCacheNotificationArgs args)
        {
            AfterAccess?.Invoke(args);

            if (AsyncAfterAccess != null)
            {
                await AsyncAfterAccess.Invoke(args).ConfigureAwait(false);
            }
        }

        internal async Task OnBeforeAccessAsync(TokenCacheNotificationArgs args)
        {
            BeforeAccess?.Invoke(args);
            if (AsyncBeforeAccess != null)
            {
                await AsyncBeforeAccess
                    .Invoke(args)
                    .ConfigureAwait(false);
            }
        }

        internal async Task OnBeforeWriteAsync(TokenCacheNotificationArgs args)
        {
#pragma warning disable CS0618 // Type or member is obsolete, but preserve old behavior until it is deleted
            HasStateChanged = true;
#pragma warning restore CS0618 // Type or member is obsolete
            args.HasStateChanged = true;
            BeforeWrite?.Invoke(args);

            if (AsyncBeforeWrite != null)
            {
                await AsyncBeforeWrite.Invoke(args).ConfigureAwait(false);
            }
        }

#if !ANDROID_BUILDTIME && !iOS_BUILDTIME

        /// <summary>
        /// Sets a delegate to be notified before any library method accesses the cache. This gives an option to the
        /// delegate to deserialize a cache entry for the application and accounts specified in the <see cref="TokenCacheNotificationArgs"/>.
        /// See https://aka.ms/msal-net-token-cache-serialization
        /// </summary>
        /// <param name="beforeAccess">Delegate set in order to handle the cache deserialiation</param>
        /// <remarks>In the case where the delegate is used to deserialize the cache, it might
        /// want to call <see cref="Deserialize(byte[])"/></remarks>
        public void SetBeforeAccess(TokenCacheCallback beforeAccess)
        {
            GuardOnMobilePlatforms();
            BeforeAccess = beforeAccess;
        }

        /// <summary>
        /// Sets a delegate to be notified after any library method accesses the cache. This gives an option to the
        /// delegate to serialize a cache entry for the application and accounts specified in the <see cref="TokenCacheNotificationArgs"/>.
        /// See https://aka.ms/msal-net-token-cache-serialization
        /// </summary>
        /// <param name="afterAccess">Delegate set in order to handle the cache serialization in the case where the <see cref="TokenCache.HasStateChanged"/>
        /// member of the cache is <c>true</c></param>
        /// <remarks>In the case where the delegate is used to serialize the cache entierely (not just a row), it might
        /// want to call <see cref="Serialize()"/></remarks>
        public void SetAfterAccess(TokenCacheCallback afterAccess)
        {
            GuardOnMobilePlatforms();
            AfterAccess = afterAccess;
        }

        /// <summary>
        /// Sets a delegate called before any library method writes to the cache. This gives an option to the delegate
        /// to reload the cache state from a row in database and lock that row. That database row can then be unlocked in the delegate
        /// registered with <see cref="SetAfterAccess(TokenCacheCallback)"/>
        /// </summary>
        /// <param name="beforeWrite">Delegate set in order to prepare the cache serialization</param>
        public void SetBeforeWrite(TokenCacheCallback beforeWrite)
        {
            GuardOnMobilePlatforms();
            BeforeWrite = beforeWrite;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="beforeAccess"></param>
        public void SetBeforeAccessAsync(Func<TokenCacheNotificationArgs, Task> beforeAccess)
        {
            GuardOnMobilePlatforms();
            AsyncBeforeAccess = beforeAccess;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="afterAccess"></param>
        public void SetAfterAccessAsync(Func<TokenCacheNotificationArgs, Task> afterAccess)
        {
            GuardOnMobilePlatforms();
            AsyncAfterAccess = afterAccess;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="beforeWrite"></param>
        public void SetBeforeWriteAsync(Func<TokenCacheNotificationArgs, Task> beforeWrite)
        {
            GuardOnMobilePlatforms();
            AsyncBeforeWrite = beforeWrite;
        }
#endif

        private static void GuardOnMobilePlatforms()
        {
#if ANDROID || iOS
        throw new PlatformNotSupportedException("You should not use these TokenCache methods on mobile platforms. " +
            "They are meant to allow applications to define their own storage strategy on .net desktop and non-mobile platforms such as .net core. " +
            "On mobile platforms, MSAL.NET implements a secure and performant storage mechanism. " +
            "For more details about custom token cache serialization, visit https://aka.ms/msal-net-serialization");
#endif
        }
    }
}
