using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Security.Cryptography;
using System.IO;

namespace LocationSvcClientWpf
{
    internal class fileCache:TokenCache
    {
        private string TokenCachePath;
        private static readonly object FileLock = new object();

        public fileCache(string filePath =@".\TokenCache.dat")
        {
            TokenCachePath = filePath;
            this.AfterAccess = AfterAccessNotification;
            this.BeforeAccess = BeforeAccessNotification;
            lock (FileLock)
            {
                this.Deserialize(File.Exists(TokenCachePath) ? ProtectedData.Unprotect(
                    File.ReadAllBytes(TokenCachePath), null, DataProtectionScope.CurrentUser) : null);
            }
        }

        public override void Clear()
        {
            base.Clear();
            File.Delete(TokenCachePath);
        }

        void BeforeAccessNotification(TokenCacheNotificationArgs args) {
            lock (FileLock)
            {
                this.Deserialize(File.Exists(TokenCachePath) ? ProtectedData.Unprotect(
                    File.ReadAllBytes(TokenCachePath), null, DataProtectionScope.CurrentUser) : null);
            }
        }

        void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            if (this.HasStateChanged)
            {
                lock (FileLock)
                {
                    File.WriteAllBytes(TokenCachePath, ProtectedData.Protect(this.Serialize(), null, DataProtectionScope.CurrentUser));
                    this.HasStateChanged = false;
                }
            }
        }
    }
}