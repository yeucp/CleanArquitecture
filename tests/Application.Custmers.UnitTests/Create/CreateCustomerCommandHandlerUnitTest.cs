using Application.Customers.Create;
using Domain.Customers;
using Domain.CustomerStatuses;
using Domain.DomainErrors;
using Domain.Primitives;
using MediatR;

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
		public async Task HandleCreateCustomer_WhenAllDataIsCorrect_ShouldSuccess()
		{
			//Arrange
			//Configure enrty parameters
			CreateCustomerCommand command = new CreateCustomerCommand("Yeudi", "Carazo", "yexxxxxxxx@gmail.com", "7194-1273", Guid.NewGuid());
            _mockCustomerStatusRepository.Setup(cr => cr.GetByIdAsync(It.IsAny<CustomerStatusId>()))
                .ReturnsAsync(new CustomerStatus(new CustomerStatusId(Guid.NewGuid())));
            //Act
            //Method execution
            var result = await _handler.Handle(command, default);
			//Assert
			//Veriry return data
			result.IsError.Should().BeFalse();
            result.Value.Should().Be(Unit.Value);
		}


		[Fact]
        public async Task HandleCreateCustomer_WhenPhoneNumberHasBadFormat_ShouldReturnValidationError()
        {
            //Arrange
            //Configure enrty parameters
            CreateCustomerCommand command = new CreateCustomerCommand("Yeudi", "Carazo", "yexxxxxxxx@gmail.com", "71941273", Guid.NewGuid());
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

		[Fact]
		public async Task HandleCreateCustomer_WhenCustomerStatusIdNotFoud_ShouldReturnValidationError()
		{
			//Arrange
			//Configure enrty parameters
			CreateCustomerCommand command = new CreateCustomerCommand(string.Empty, "Carazo", "yexxxxxxxx@gmail.com", "7194-1273", Guid.NewGuid());
			//Act
			//Method execution
			var result = await _handler.Handle(command, default);
			//Assert
			//Veriry return data
			result.IsError.Should().BeTrue();
			result.FirstError.Type.Should().Be(ErrorType.Validation);
			result.FirstError.Code.Should().Be(Errors.Customer.CustomerStatusNotFound.Code);
			result.FirstError.Description.Should().Be(Errors.Customer.CustomerStatusNotFound.Description);
		}
	}
}