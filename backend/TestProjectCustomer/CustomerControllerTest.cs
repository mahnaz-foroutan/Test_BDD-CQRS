using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Application.Features.Customer.Queries.GetCustomersList;
using Application.Features.Customer.Commands.Insert;
using API.Controllers;
using Application.Features.Customer.Queries.Dtos;
using FluentAssertions;
using PhoneNumbers;
using Application.Features.Customer.Commands.Delete;
using Application.Features.Customer.Commands.Update;
using Application.Features.Customer.Queries.GetCustomerId;

namespace TestProjectCustomer
{
    public class CustomerControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CustomerController _controller;

        public CustomerControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new CustomerController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetCustomersAll_ReturnsOkObjectResultWithCustomers()
        {
            // Arrange
            var mockCustomerList = new List<CustomerToReturnDto>()
        {
         new CustomerToReturnDto
{
    Id = 1,
    Firstname = "John",
    Lastname = "Doe",
    DateOfBirth = new DateTime(1985, 10, 23),
    PhoneNumber = "+445863625896",
    Email = "john.doe@example.com",
    BankAccountNumber = "1234567890"
},
         new CustomerToReturnDto    {
    Id = 2,
    Firstname = "Jokm",
    Lastname = "Mhe",
    DateOfBirth = new DateTime(1988, 10, 23),
    PhoneNumber = "+955863625896",
    Email = "mhe.doe@example.com",
    BankAccountNumber = "1234567898"
},
            // Add more mock customers if needed
        };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomersListQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(mockCustomerList);

            // Act
            var result = await _controller.GetCustomersAll();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IReadOnlyList<CustomerToReturnDto>>(actionResult.Value);
            returnValue.Should().BeEquivalentTo(mockCustomerList);
        }

        [Fact]
        public async Task GetCustomerById_ReturnsOkObjectResultWithCustomer()
        {
            // Arrange
            var customerDto = new CustomerToReturnDto
            {
                Id = 1,
                Firstname = "John",
                Lastname = "Doe",
                DateOfBirth = new DateTime(1985, 10, 23),
                PhoneNumber = "+445863625896",
                Email = "john.doe@example.com",
                BankAccountNumber = "1234567890"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(customerDto);

            // Act
            var result = await _controller.GetCustomerById(1);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = actionResult.Value as CustomerToReturnDto;
            Assert.NotNull(returnValue);
            Assert.Equal(customerDto.Id, returnValue.Id);
            Assert.Equal(customerDto.Firstname, returnValue.Firstname);
        }

        [Fact]
        public async Task InsertCustomer_ReturnsNoContentResult()
        {
            // Arrange
            var command = new InsertCommand
            {
                Id = 1,
                Firstname = "John",
                Lastname = "Doe",
                DateOfBirth = new DateTime(1985, 10, 23),
                PhoneNumber = "+445863625896",
                Email = "john.doe@example.com",
                BankAccountNumber = "1234567890"
            };

            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Unit.Value);

            // Act
            var result = await _controller.InsertCustomer(command);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateCustomer_ReturnsNoContentResult()
        {
            // Arrange
            var command = new UpdateCommand
            {
                Id = 1,
                Firstname = "Johny",
                Lastname = "Doe",
                DateOfBirth = new DateTime(1985, 10, 23),
                PhoneNumber = "+445863625896",
                Email = "john.doe@example.com",
                BankAccountNumber = "1234567890"
            };

            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Unit.Value);

            // Act
            var result = await _controller.UpdateCustomer(command);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCustomer_ReturnsNoContentResult()
        {
            // Arrange
            var command = new DeleteCommand { Id = 1 };

            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Unit.Value);

            // Act
            var result = await _controller.DeleteCustomer(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

    }
}
