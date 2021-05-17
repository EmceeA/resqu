using Newtonsoft.Json;
using Resqu.Core.Interface;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Services
{
    public  class ServiceCaller<T>
    {
        public IRestResponse GetRequest(string url)
        {
            var client = new RestClient(url);
            RestRequest request = new RestRequest();
            request.Method = Method.GET;
            var response = client.Execute(request);
            return response;
        }

        public IRestResponse PostRequest(T request, string url, string token)
        {

            var client = new RestClient(url);
            RestRequest rest = new RestRequest();
            rest.Method = Method.POST;
            var req = JsonConvert.SerializeObject(request);
            rest.AddParameter("application/json", req, ParameterType.RequestBody);
            rest.AddHeader("Authorization", $"Bearer {token}");
            var response = client.Execute(rest);
            return response;
        }
    }
}
