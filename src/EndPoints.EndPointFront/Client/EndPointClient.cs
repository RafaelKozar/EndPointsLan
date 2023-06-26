using EndPoints.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPoints.EndPointFront.Client
{
    public class EndPointClient : BaseClient
    {
        public EndPointClient() 
        {
        }   

        public void Save(EndPointGyrDto endPoint)
        {
            Post<EndPointGyrDto>("EndPoint", endPoint); 
        }

        public void Update(EndPointGyrDto endPoint)
        {
            Post<EndPointGyrDto>("EndPoint/Update", endPoint);
        }   

        public List<EndPointGyrDto> GetAll()
        {
            return Get<List<EndPointGyrDto>>("EndPoint");
        }

        public EndPointGyrDto GetBySerialNumber(string serialNumber)
        {
            return Get<EndPointGyrDto>("EndPoint/GetBySerialNumber", new KeyValuePair<string, object>("serialNumber", serialNumber));
        }   

        public void Delete(string serialNumber)
        {
            Get<EndPointGyrDto>("EndPoint/Delete", new KeyValuePair<string, object>("serialNumber", serialNumber));
        }
    }
}
