using Microsoft.EntityFrameworkCore;
using StoreDB;
using StoreDB.Models;
using Xunit;
using StoreLib;
using System;

namespace StoreTest
{
    public class CustomerServiceTest
    {
        private CustomerService service;

        Customer testCustomer = new Customer()
        {
            UserName = "andres",
            Password = "12345"
        };

        [Fact]
        private void SignUpNewCustomerShouldAdd()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("SignUpNewCustomerShouldAdd").Options;
            using var testContext = new StoreContext(options);
            service = new CustomerService(new DBRepo(testContext));

            //Act
            service.SignUpNewCustomer(testCustomer);

            //Assert
            using var assertContext = new StoreContext(options);
            DBRepo repo = new DBRepo(assertContext);
            Assert.NotNull(repo.GetCustomer("andres"));
        }

        [Fact]
        private void SignUpNewCustomerShouldThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("SignUpNewCustomerShouldThrowException").Options;
            using var testContext = new StoreContext(options);
            service = new CustomerService(new DBRepo(testContext));
            service.SignUpNewCustomer(testCustomer);

            //Act and Assert
            Assert.Throws<ArgumentException>(() => service.SignUpNewCustomer(testCustomer)); //duplicate sign up
        }

        [Fact]
        private void SignInCustomerShouldReturnCustomer()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("SignInCustomerShouldReturnCustomer").Options;
            using var testContext = new StoreContext(options);
            service = new CustomerService(new DBRepo(testContext));
            service.SignUpNewCustomer(testCustomer);

            //Act
            Customer customer = service.SignInExistingCustomer("andres", "12345");

            //Assert
            Assert.NotNull(customer);
            Assert.Equal("andres", customer.UserName);
        }

        [Fact]
        private void SignInCustomerShouldThrowArgumentException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("SignInCustomerShouldThrowArgumentException").Options;
            using var testContext = new StoreContext(options);
            service = new CustomerService(new DBRepo(testContext));
            service.SignUpNewCustomer(testCustomer);

            //Act and Assert
            Assert.Throws<ArgumentException>(() => service.SignInExistingCustomer("thisnamedoesnotexist", "12345"));
        }

        [Fact]
        private void SignInCustomerShouldThrowUnauthorizedAccessException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("SignInCustomerShouldThrowAccessException").Options;
            using var testContext = new StoreContext(options);
            service = new CustomerService(new DBRepo(testContext));
            service.SignUpNewCustomer(testCustomer);

            //Act and Assert
            Assert.Throws<UnauthorizedAccessException>(() => service.SignInExistingCustomer("andres", "thispasswordisincorrect"));
        }
    }
}