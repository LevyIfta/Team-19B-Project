using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem
{
    public class VerificationReal : VerificationInterface
    {
        private string url { get; set; }
        private static readonly HttpClient client = new HttpClient();
        private bool handshakePreformed = false;

        public VerificationReal(string url)
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

        public bool Pay(string userName, string creditNumber, string validity, string cvv)
        {
            return asyncPay(userName, creditNumber, validity, cvv).GetAwaiter().GetResult();
        }

        public async Task<bool> asyncPay(string userName, string creditNumber, string validity, string cvv)
        {
            if (!this.handshakePreformed)
                return false;

            string[] date = validity.Split('/');

            var postContent = new Dictionary<string, string>
            {
                {"action_type","pay" },
                { "card_number", creditNumber },
                { "month", date[0] },
                { "year", date[1] },
                { "holder", userName },
                { "ccv", cvv },
                { "id", "20444444" },
            };

            var content = new FormUrlEncodedContent(postContent);
            var response = await client.PostAsync(this.url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (responseString.Equals("-1"))
                return false;

            return true;
        }

        public void setReal(VerificationInterface real)
        {
            // none, this is the real
        }
    }
}
