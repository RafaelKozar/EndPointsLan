using AutoMapper;
using EndPoints.Application.AutoMapper;
using EndPoints.Application.Services;
using EndPoints.Core;
using EndPoints.Data.Repository;
using EndPoints.Domain;
using EndPoints.Dto;
using FluentAssertions;
using Moq;

namespace EndPoints.Tests
{
    public class UnitTestInServiceEndPoint
    {
        private readonly IEndPointService _endPointService;
        private readonly Mock<IEndPointRepository> _endPointRepositoryMock;
        private readonly IMapper _mapper;

        public UnitTestInServiceEndPoint()
        {
            _endPointRepositoryMock = new Mock<IEndPointRepository>();            

            var config = new MapperConfiguration(x =>
            {
                x.AddProfile(new DomainToDtoMapping());
                x.AddProfile(new DtoToDomainMapping()); 
            });

            _mapper = config.CreateMapper();

            _endPointService = new EndPointService(_mapper, _endPointRepositoryMock.Object);  
        }

        [Fact(DisplayName = "Teste para quando não econtrar pelo serial number na funçao GetBySerialNumber")]
        public async Task TestNotFindSerialNumber()
        {
            _endPointRepositoryMock.Setup(x => x.GetBySerialNumber(It.IsAny<string>())).ReturnsAsync((Domain.EndPointGyr)null);
           
            Func<Task> act = async () => { await _endPointService.GetBySerialNumber("1234567890"); };
            await act.Should().ThrowAsync<DomainException>();
        }


        [Fact(DisplayName = "Teste para quando econtrar pelo serial number na funçao GetBySerialNumber")]
        public async Task TestFindSerialNumber()
        {
            var endPoint = GenerateEndPoint();

            _endPointRepositoryMock.Setup(x => x.GetBySerialNumber(It.IsAny<string>())).ReturnsAsync(endPoint);

            var result =  await _endPointService.GetBySerialNumber("1234567890");
            result.Should().NotBeNull();
            result.Should().BeOfType<EndPointGyrDto>();
        }

        [Fact(DisplayName = "Teste para quando quando já existe um serial number quando vai salvar um EndPoint")]
        public async Task TestAlreadyExistSerialNumber()
        {
            var endPoint = GenerateEndPoint();
            var endPointDto = _mapper.Map<EndPointGyrDto>(endPoint); 

            _endPointRepositoryMock.Setup(x => x.GetBySerialNumber(It.IsAny<string>())).ReturnsAsync(endPoint);


            Func<Task> act = async () => { await _endPointService.SaveEndPoint(endPointDto); };
            await act.Should().ThrowAsync<DomainException>();
            
        }


        [Fact(DisplayName = "Teste para salvar um EndPoint")]
        public async Task TestSaveEndPoint()
        {
            var endPoint = GenerateEndPoint();
            var endPointDto = _mapper.Map<EndPointGyrDto>(endPoint);

            _endPointRepositoryMock.Setup(x => x.GetBySerialNumber(It.IsAny<string>())).ReturnsAsync((Domain.EndPointGyr)null);
            _endPointRepositoryMock.Setup(x => x.Save(It.IsAny<Domain.EndPointGyr>())).Returns(Task.CompletedTask);
            _endPointRepositoryMock.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
            

            Func<Task> act = async () => { await _endPointService.SaveEndPoint(endPointDto); };
            act.Should().NotThrowAsync();

        }


        public EndPointGyr GenerateEndPoint()
        {
            return new EndPointGyr
            {
                MeterFirmwareVersion = "1.0.0",
                MeterModelId = 16,
                MeterNumber = 10,
                SerialNumber = "TTT123",
                SwitchState = 1
            };
        }
    }
}