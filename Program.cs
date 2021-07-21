using System;
using Xero.NetStandard.OAuth2.Model;
using Xero.NetStandard.OAuth2.Api;
using Xero.NetStandard.OAuth2.Client;
using Xero.NetStandard.OAuth2.Config;
using Xero.NetStandard.OAuth2.Token;
using System.Threading.Tasks;

namespace AsyncMain
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DotNetEnv.Env.Load();
            var helloWorld = await GetHelloWorldAsync();
            Console.WriteLine(helloWorld);
        }

        static async Task<string> GetHelloWorldAsync()
        {   
            XeroConfiguration XeroConfig = new XeroConfiguration
            {
                ClientId = System.Environment.GetEnvironmentVariable("CLIENT_ID"),
                ClientSecret = System.Environment.GetEnvironmentVariable("CLIENT_SECRET"),
            };

            var client = new XeroClient(XeroConfig);
            var xeroToken = await client.RequestClientCredentialsTokenAsync();

            try {
                var apiInstance = new AccountingApi();
                var ifModifiedSince = DateTime.Parse("2000-02-06T12:17:43.202-08:00");
                var where = "Status==\"ACTIVE\"";
                var xeroTenantId = "";
                var result = await apiInstance.GetAccountsAsync(xeroToken.AccessToken, xeroTenantId, ifModifiedSince, where, null);
                return result.ToJson();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception when calling apiInstance.GetInvoice: " + e.Message );
                return e.ToString();
            }
        }
    }
}