using Microsoft.Azure.Management.CognitiveServices;
using Microsoft.Azure.Management.CognitiveServices.Models;
using Microsoft.Azure.Management.Compute.Fluent;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest.Azure.Authentication;
using System;
using System.Collections.Generic;

namespace CoreManagerVm
{
    class Program
    {
        public static string resourceGroupName = "yuvmtest";
        public static string accountName = "face-account-test";

        public static string domain = "b388b808-0ec9-4a09-a414-a7cbbd8b7e9b";
        public static string clientId = "16a94c84-c6ba-4f19-aa48-3353f8ffe18e";
        public static string clientSecret = "123456";
        public static string subscriptionId = "e0fbea86-6cf2-4b2d-81e2-9c59f4f96bcb";

        //
        public static string DefaultLocation = "global";
        public static Dictionary<string, string> DefaultTags = new Dictionary<string, string>
        {
            {"key1","value1"},
            {"key2","value2"}
         };

        public const string DefaultSkuName = SkuName.S0;
        public const string DefaultKind = Kind.Face;


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //使用AD获取认证
            var credentials = ApplicationTokenProvider.LoginSilentAsync(domain, new ClientCredential(clientId, clientSecret), ActiveDirectoryServiceSettings.AzureChina).Result;

            //var computeManagementClient = new ComputeManagementClient(new Uri("https://management.chinacloudapi.cn"), credentials) {SubscriptionId = subscriptionId };

            //关闭虚拟机
            //computeManagementClient.VirtualMachines.BeginPowerOff("yuvmtest", "yuvmtest");

            //启动虚拟机
            //computeManagementClient.VirtualMachines.BeginStart("yuvmtest", "yuvmtest");

            //重新启动
            //computeManagementClient.VirtualMachines.Restart("yuvmtest", "yuvmtest");

            //Console.WriteLine("power off success!");

            CognitiveServicesManagementClient cognitiveServicesManagementClient = new CognitiveServicesManagementClient(new Uri("https://management.chinacloudapi.cn"),credentials) { SubscriptionId = subscriptionId };

            CognitiveServicesAccountCreateParameters parameters = new CognitiveServicesAccountCreateParameters
            {
                Location = DefaultLocation,
                Tags = DefaultTags,
                Sku = new Sku { Name = DefaultSkuName },
                Kind = DefaultKind,
                Properties = new object(),
            }; ;

            cognitiveServicesManagementClient.CognitiveServicesAccounts.Create(resourceGroupName,accountName,parameters);
            Console.WriteLine("creatre success!");
            Console.ReadKey(true);
        }
    }
}