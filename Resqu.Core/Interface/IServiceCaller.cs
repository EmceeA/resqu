using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Interface
{
    public interface IServiceCaller<T> where T : class
    {
        IRestResponse GetRequest(string url);
        IRestResponse PostRequest(T request , string url);
    }
}
