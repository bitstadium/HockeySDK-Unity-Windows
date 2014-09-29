using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

#if (UNITY_WP8 && !UNITY_EDITOR)
using HockeyApp;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Threading.Tasks;
using HockeyApp.Model;
using System.IO.IsolatedStorage;
using System.IO;
using HockeyApp.Internal;
#endif

namespace HockeyAppUnity
{
    public class HockeyClientUnity
    {
        private static HockeyClientUnity _instance = new HockeyClientUnity();
        public static HockeyClientUnity Current { get { return _instance; } }

        private string appId = null;

        private Type _wp8Extensions;

        public void Configure(string appIdentifier, string apiDomain) {
            #if (UNITY_WP8 && !UNITY_EDITOR)
            appId = appIdentifier;
            Dispatcher.InvokeOnUIThread(() =>
            {
                    _wp8Extensions = Type.GetType("HockeyApp.HockeyClientWP8SLExtension,HockeyApp");
                    HockeyClient client = (HockeyClient)_wp8Extensions.GetMethod("Configure").Invoke(null, new object[] { 
                                                                            HockeyClient.Current, 
                                                                            appIdentifier, 
                                                                            (PhoneApplicationFrame)Application.Current.RootVisual });
                    client.SetApiDomain(apiDomain);
                    client.SdkName = HockeyUnityConstants.SdkName;
                    client.SdkVersion = HockeyUnityConstants.SdkVersion;
            });
            #endif
        }

 #if (UNITY_WP8 && !UNITY_EDITOR)
        public async void HandleCrashes(bool sendAutomatically = false) {
 #else
        public void HandleCrashes(bool sendAutomatically = false) {
 #endif
 #if (UNITY_WP8 && !UNITY_EDITOR)
            await (Task)_wp8Extensions.GetMethod("HandleCrashesAsync").Invoke(null, new object[] { HockeyClient.Current, sendAutomatically });
 #endif
        }

        public void CheckForUpdates() {
#if (UNITY_WP8 && !UNITY_EDITOR)
            //TODO implement extension method in wp8 sdk to allow for simple bool/string options instead of settings-object...
            _wp8Extensions.GetMethod("CheckForUpdates").Invoke(null, new object[] { HockeyClient.Current, null });
#endif

         }

        public void HandleUnityLogException(string logString, string stackTrace) {
#if (UNITY_WP8 && !UNITY_EDITOR)
            var clientInternal = (HockeyClient)HockeyApp.HockeyClient.Current;
            var crashData = clientInternal.CreateCrashData(logString, stackTrace);
            
            var crashId = Guid.NewGuid();
            try
            {
                IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
                if (!store.DirectoryExists(HockeyUnityConstants.CrashDirectoryName))
                {
                    store.CreateDirectory(HockeyUnityConstants.CrashDirectoryName);
                }

                String filename = string.Format("{0}{1}.log", HockeyUnityConstants.CrashFilePrefix, crashId);
                FileStream stream = store.CreateFile(Path.Combine(HockeyUnityConstants.CrashDirectoryName, filename));
                crashData.Serialize(stream);
                stream.Close();
            }
            catch
            {
                // Ignore all exceptions
            }
#endif
        }

        public void OpenFeedbackPage(string initialUsername = null, string initialEmail = null)
        {
#if (UNITY_WP8 && !UNITY_EDITOR)
            
            HockeyApp.HockeyClient.Current.UpdateContactInfo(initialUsername, initialEmail);
            Type.GetType("HockeyApp.HockeyClientWP8SLExtension,HockeyApp").GetMethod("ShowFeedback").Invoke(null, new object[] { HockeyClient.Current });
#endif
        } 
    }
}
