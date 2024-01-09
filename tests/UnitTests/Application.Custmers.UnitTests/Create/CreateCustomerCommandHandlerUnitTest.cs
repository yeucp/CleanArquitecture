using Application.Customers.Create;
using Domain.Customers;
using Domain.CustomerStatuses;
using Domain.DomainErrors;
using Domain.Primitives;

namespace Application.Custmers.UnitTests.Create
{
    public class CreateCustomerCommandHandlerUnitTest
    {
        /*
         Prueba
         Escenario
         Resultado esperado
         */

        private Mock<ICustomerRepository> _mockCustomerRepository;
        private Mock<ICustomerStatusRepository> _mockCustomerStatusRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly CreateCustomerCommandHandler _handler;

        public CreateCustomerCommandHandlerUnitTest() 
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockCustomerStatusRepository = new Mock<ICustomerStatusRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new CreateCustomerCommandHandler(
                _mockCustomerRepository.Object,
                _mockCustomerStatusRepository.Object,
                _mockUnitOfWork.Object
            );
        }

        [Fact]
        public async Task HandleCreateCustomer_WhenPhoneNumberHasBadFormat_ShouldReturnValidationError()
        {
            //Arrange
            //Configure enrty parameters
            CreateCustomerCommand command = new CreateCustomerCommand("Yeudi", "Carazo", "yexxxxxxxx@gmail.com", "71941273", Guid.Parse("70158537-D4A0-477A-8A5D-CEC1A7E76816"));
            //Act
            //Method execution
            var result = await _handler.Handle(command, default);
            //Assert
            //Veriry return data
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
            result.FirstError.Code.Should().Be(Errors.Customer.PhoneNumberWithBadFormat.Code);
            result.FirstError.Description.Should().Be(Errors.Customer.PhoneNumberWithBadFormat.Description);
        }
    }
}