using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Datalistener.Owin.CalAmp.Client
{
    public class CanReceiverClient
    {
        string _hostUri;
        public CanReceiverClient(string hostUri)
        {
            _hostUri = hostUri;
        }

        private HttpClient CreateClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(new Uri(_hostUri), "../api/CanReceiver/");
            return client;
        }
        private HttpClient CreateClient(Int32 id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(new Uri(_hostUri), "../api/CanReceiver/" + id);
            return client;
        }
        private HttpClient CreateClient(string param1)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(new Uri(_hostUri), "../api/CanReceiver/Get/" + param1);
            return client;
        }
        private HttpClient CreateClient(string param1, string param2)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(new Uri(_hostUri), "../api/CanReceiver/" + param1 + "/" + param2);
            return client;
        }

        private HttpClient CreateClient(string param1, string param2, string param3, string param4)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(new Uri(_hostUri), "../api/CanReceiver/" + param1 + "/" + param2 + "/" + param3 + "/" + param4);
            return client;
        }

        public IEnumerable<string> GetCanReceiver()
        {
            HttpResponseMessage response;
            using (var client = CreateClient())
            {
                response = client.GetAsync(client.BaseAddress).Result;
            }
            var result = response.Content.ReadAsAsync<IEnumerable<string>>().Result;
            return result;
        }
        public String GetCanReceiver(int id)
        {
            HttpResponseMessage response;
            using (var client = CreateClient(id))
            {
                response = client.GetAsync(client.BaseAddress).Result;
            }
            var result = response.Content.ReadAsAsync<String>().Result;
            return result;
        }
        public String GetCanReceiver(string id)
        {
            HttpResponseMessage response;
            using (var client = CreateClient(id))
            {
                response = client.GetAsync(client.BaseAddress).Result;
            }
            var result = response.Content.ReadAsAsync<String>().Result;
            return result;
        }

        public String GetCanReceiver(string truckid, string msg)
        {
            HttpResponseMessage response;
            using (var client = CreateClient(truckid,msg))
            {
                response = client.GetAsync(client.BaseAddress).Result;
            }
            var result = response.Content.ReadAsAsync<String>().Result;
            return result;
        }

        public String GetCanReceiver(string truckid, decimal lat, decimal lng, string time)
        {
            HttpResponseMessage response;
            using (var client = CreateClient(truckid, lat.ToString(), lng.ToString(), time))
            {
                response = client.GetAsync(client.BaseAddress).Result;
            }
            var result = response.Content.ReadAsAsync<String>().Result;
            return result;
        }

        public System.Net.HttpStatusCode PostCanReceiver(String value)
        {
            HttpResponseMessage response;
            using (var client = CreateClient())
            {
                response = client.PostAsJsonAsync(client.BaseAddress, value).Result;
            }
            return response.StatusCode;
        }
    }
}
