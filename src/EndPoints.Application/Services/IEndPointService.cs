using EndPoints.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EndPoints.Application.Services
{
    public interface IEndPointService 
    {
        Task SaveEndPoint(EndPointGyrDto endPoint);
        Task UpdateEndPoint(EndPointGyrDto endPoint);   
        Task RemoveEndPoint(string serialNumber);   
        Task<EndPointGyrDto> GetBySerialNumber(string serialNumber);
        Task<IEnumerable<EndPointGyrDto>> GetAllEndPoints();    

    }
}
