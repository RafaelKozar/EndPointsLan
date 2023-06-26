using EndPoints.Core;
using EndPoints.Domain;

namespace EndPoints.Data.Repository
{
    public interface IEndPointRepository : IRepository<EndPointGyr>
    {
        Task Save(EndPointGyr endPoint);
        void Atualizar(EndPointGyr endPoint);
        void Remove(EndPointGyr endPoint);
        Task<EndPointGyr> GetBySerialNumber(string serialNumber);
        Task<IEnumerable<EndPointGyr>> GetAllEndPoints();

    }
}