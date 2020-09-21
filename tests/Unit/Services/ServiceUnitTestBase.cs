using AutoMapper;
using Moq;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Unit.Services
{
    public class ServiceUnitTestBase
    {
        public Mock<ICartCalculatorService> CartCalculatorService = new Mock<ICartCalculatorService>();
        public Mock<ICartRepository> CartRepository = new Mock<ICartRepository>();
        public Mock<IMapper> Mapper = new Mock<IMapper>();
    }
}
