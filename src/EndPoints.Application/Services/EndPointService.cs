using AutoMapper;
using EndPoints.Core;
using EndPoints.Data.Repository;
using EndPoints.Dto;

namespace EndPoints.Application.Services
{
    public class EndPointService : IEndPointService
    {
        private readonly IMapper _mapper;
        private readonly IEndPointRepository _endPointRepository;

        public EndPointService(IMapper mapper, IEndPointRepository endPointRepository)
        {
            _mapper = mapper;
            _endPointRepository = endPointRepository;
        }

        public async Task<IEnumerable<EndPointGyrDto>> GetAllEndPoints()
        {
           return _mapper.Map<IEnumerable<EndPointGyrDto>>(await _endPointRepository.GetAllEndPoints());
        }

        public async Task<EndPointGyrDto> GetBySerialNumber(string serialNumber)
        {
            var endPoint = await _endPointRepository.GetBySerialNumber(serialNumber);

            if (endPoint == null)
                throw new DomainException("Serial Number not found");

            return  _mapper.Map<EndPointGyrDto>(endPoint);
        }

        public async Task RemoveEndPoint(string serialNumber)
        {
            var endPoint = await _endPointRepository.GetBySerialNumber(serialNumber);

            if(endPoint == null)
                throw new DomainException("Serial Number not found");  
            
            _endPointRepository.Remove(endPoint);
            await _endPointRepository.UnitOfWork.Commit();
        }

        public async Task SaveEndPoint(EndPointGyrDto endPointDto)
        {
            var endPoint = _mapper.Map<Domain.EndPointGyr>(endPointDto); 
            var endPointExist = await _endPointRepository.GetBySerialNumber(endPoint.SerialNumber);
            if (endPointExist != null)
                throw new DomainException("Serial Number already exist");

            await _endPointRepository.Save(endPoint);   
            await _endPointRepository.UnitOfWork.Commit();
        }

        public async Task UpdateEndPoint(EndPointGyrDto endPointDto)
        {            
            var endPoint = await _endPointRepository.GetBySerialNumber(endPointDto.SerialNumber);
            if (endPoint == null)
                throw new DomainException("Serial Number not found");

            endPoint.SwitchState = (int)endPointDto.SwitchState;

            _endPointRepository.Update(endPoint);
            await _endPointRepository.UnitOfWork.Commit();
        }
    }
}
