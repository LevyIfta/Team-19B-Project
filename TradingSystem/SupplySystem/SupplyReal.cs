using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SupplySystem
{
    public class SupplyReal : SupplyInterface
    {
        private string url { get; set; }
        private static readonly HttpClient client = new HttpClient();
        private bool handshakePreformed = false;

        public SupplyReal(string url)
        {
            this.url = url;
            this.handshake();
        }

        private async void handshake()
        {
            // make a handshake for once
            var postContent = new Dictionary<string, string>
            {
                {"action_type", "handshake" },
            };

            var content = new FormUrlEncodedContent(postContent);
            var response = await client.PostAsync(this.url, content);
            var responseString = await response.Content.ReadAsStringAsync(); // this should be "OK"

            // chack if handshake was performed properly
            if (responseString.Equals("OK"))
                this.handshakePreformed = true;
        }

        public void InformStore(string storeName)
        {

        }

        public bool OrderPackage(string storeName, string userName, string address, string orderInfo)
        {
            return asyncOrderPackage(storeName, userName, address, orderInfo).GetAwaiter().GetResult();
        }

        public async Task<bool> asyncOrderPackage(string storeName, string userName, string address, string orderInfo)
        {
            if (!this.handshakePreformed)
                return false;

            var postContent = new Dictionary<string, string>
            {
                { "action_type", "supply" },
                { "name", userName },
                { "address", address },
                { "city", "Beer Sheva" },
                { "country", "Israel" },
                { "zip", "8458527" },
            };

            var content = new FormUrlEncodedContent(postContent);
            var response = await client.PostAsync(this.url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (responseString.Equals("-1"))
                return false;

            return true;
        }

        public bool RemoveStore(string storeName)
        {
            return true;
        }

        public void setReal(SupplyInterface real)
        {

        }
    }
}
