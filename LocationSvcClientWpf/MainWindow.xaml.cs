using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http;
using System.Runtime.InteropServices;

namespace LocationSvcClientWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string aadInstance = "https://login.microsoftonline.com/{0}";
        private static string tenant = "JYtechnology.onMicrosoft.com";
        private static string clientId = "a3d084fd-d36a-49ac-9db1-bf92157650d0";
        Uri redirecturl = new Uri("https://something");

        private static string authority = string.Format(aadInstance, tenant);
        private AuthenticationContext authContext = null;
        private AuthenticationResult result = null;
        private static string serviceResourceId = "https://JYTechnology.onmicrosoft.com/637952ee-4722-4d4d-8908-77b2893dd1df";
        public MainWindow()
        {
            InitializeComponent();
            authContext = new AuthenticationContext(authority, new fileCache());
        }

        private  async void callServiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (result== null)
            {
                ServiceResult.Text = "You need to sign in first";
            }
            try
            {
                result = authContext.AcquireToken(serviceResourceId, clientId, redirecturl, PromptBehavior.Never);
               
            }
            catch (Exception)
            {

                throw;
            }

            HttpClient client = new HttpClient();
            string servicebaseAddress = "https://localhost:44387";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.AccessToken);
            HttpResponseMessage response = await client.GetAsync(servicebaseAddress + "/api/location?cityName=dc");
            if (response.IsSuccessStatusCode)
            {
                string s = await response.Content.ReadAsStringAsync();
                ServiceResult.Text = s;
            }
            else
            {
                ServiceResult.Text = "Error occured" + response.StatusCode;
            }
        }

        private void signInButton_Click(object sender, RoutedEventArgs e)
        {
            if (signInButton.Content.ToString()=="Sign out")
            {
                authContext.TokenCache.Clear();
                ClearCookies();
                signInButton.Content = "Sign In";
                return;
            }
            try
            {
                result = authContext.AcquireToken(serviceResourceId, clientId, redirecturl,PromptBehavior.Always );
                signInButton.Content = "Sign out";
            }
            catch (AdalException ex)
            {

              
            }
        }

        private void ClearCookies()
        {
            const int INTERNET_OPTION_END_BROWSER_SESSION = 42;
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_END_BROWSER_SESSION, IntPtr.Zero, 0);
        }

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);

    }
}
