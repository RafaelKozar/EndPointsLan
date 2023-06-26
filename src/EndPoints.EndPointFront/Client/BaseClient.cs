
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;


namespace EndPoints.EndPointFront.Client
{
    public class BaseClient
    {

        public T Get<T>(string path, params KeyValuePair<string, object>[] parameters) where T : new()
        {
            var client = GetClient();
            var request = new RestRequest(path, Method.Get);
            parameters.ToList().ForEach(p => request.AddQueryParameter(p.Key, p.Value.ToString()));

            var response = client.Execute<T>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(response.Content);
            }

            return response.Data;
        }

        public T Post<T>(string path, object obj) where T : new()
        {
            var client = GetClient();
            var request = new RestRequest(path, Method.Post);
            request.AddJsonBody(obj);
            var response = client.Execute<T>(request);
            if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(response.Content);
            }
            return response.Data;
        }

        private RestClient GetClient()
        {
            var client = new RestClient("https://localhost:7151");           
            return client;
        }
    }
}
