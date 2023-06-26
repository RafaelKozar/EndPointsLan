using EndPoints.Core;
using EndPoints.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EndPoints.Data.Repository
{
    public class EndPointRepository : IEndPointRepository
    {
        private readonly EndPointContext _context;  

        public EndPointRepository(EndPointContext context)
        {
            _context = context;
        }   
        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<EndPointGyr>> GetAllEndPoints()
        {
           return await _context.EndPoints.ToListAsync();   
        }

        public async Task<EndPointGyr> GetBySerialNumber(string serialNumber)
        {
            return await _context.EndPoints.FirstOrDefaultAsync(x => x.SerialNumber == serialNumber);
        }

        public void Remove(EndPointGyr endPoint)
        {
            _context.EndPoints.Remove(endPoint);
        }

        public async Task Save(EndPointGyr endPoint)
        {
            await _context.AddAsync(endPoint);    
        }

        public void Atualizar(EndPointGyr endPoint)
        {
            _context.Update(endPoint);
        }
    }
}
