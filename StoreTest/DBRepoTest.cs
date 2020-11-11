using Microsoft.EntityFrameworkCore;
using StoreDB;
using StoreDB.Models;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace StoreTest
{
    public class DBRepoTest
    {
        private DBRepo repo;

        private Product testProduct = new Product()
        {
            Id = 1,
            Name = "Apple",
            Price = 1.29M,
            Description = "This is a test apple."
        };

        private InvItem testInvItem = new InvItem()
        {
            ProductId = 1,
            LocationId = 1,
            Quantity = 20
        };

        [Fact]
        private void AddInvItemShouldAdd()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("AddInvItemShouldAdd").Options;
            using var testContext = new StoreContext(options);
            repo = new DBRepo(testContext);

            //Act
            repo.AddInvItem(testInvItem);

            //Assert
            using var assertContext = new StoreContext(options);
            Assert.NotNull(assertContext.InvItems.Single(x => x.ProductId==testInvItem.ProductId && x.LocationId==testInvItem.LocationId));
        }

        [Fact]
        private void AddInvItemShouldAddProduct()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("AddInvItemShouldAddProduct").Options;
            using var testContext = new StoreContext(options);
            repo = new DBRepo(testContext);

            //Act
            testInvItem.Product = testProduct;
            repo.AddInvItem(testInvItem);

            //Assert
            using var assertContext = new StoreContext(options);
            Assert.NotNull(assertContext.Products.Single(x => x.Name==testProduct.Name));
        }

        [Fact]
        private void AddCartItemShouldAdd()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("AddCartItemShouldAdd").Options;
            using var testContext = new StoreContext(options);
            repo = new DBRepo(testContext);
            CartItem testCartItem = new CartItem()
            {
                ProductId = testInvItem.ProductId,
                CartId = 1,
                Quantity = testInvItem.Quantity
            };

            //Act
            repo.AddCartItem(testCartItem);

            //Assert
            using var assertContext = new StoreContext(options);
            Assert.NotNull(assertContext.CartItems.Single(x => x.ProductId==testCartItem.ProductId && x.CartId==testCartItem.CartId));
        }

        [Fact]
        private void GetInventoryShouldGet()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("GetInventoryShouldGet").Options;
            using var testContext = new StoreContext(options);
            testInvItem.Product = testProduct;  //GetInventory includes product
            List<InvItem> items = new List<InvItem>()
            {
                testInvItem,
                new InvItem() {ProductId=1, Product=testProduct, LocationId=2, Quantity=100}, //same product, diff loc
                new InvItem() {ProductId=1, Product=testProduct, LocationId=3, Quantity=100} //same product, diff loc
            };
            testContext.InvItems.AddRange(items);
            testContext.SaveChanges();

            //Act
            using var assertContext = new StoreContext(options);
            repo = new DBRepo(assertContext);
            var result = repo.GetInventory(1); //get from locationId=1

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count); //only one item was added at locationId=1
        }

        [Fact]
        private void GetLocationsShouldGet()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("GetLocationsShouldGet").Options;
            using var testContext = new StoreContext(options);
            List<Location> locations = new List<Location>()
            {
                new Location() {Id = 1, Name = "Metropolis"},
                new Location() {Id = 2, Name = "Gotham"},
                new Location() {Id = 3, Name = "Chincinnati"}
            };
            testContext.Locations.AddRange(locations);
            testContext.SaveChanges();

            //Act
            using var assertContext = new StoreContext(options);
            repo = new DBRepo(assertContext);
            List<Location> assertLocations = repo.GetLocations();

            //Assert
            Assert.NotNull(assertLocations);
            Assert.Equal(3, assertLocations.Count);
        }

        [Fact]
        private void GetCartItemsShouldGet()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("GetLocationsShouldGet").Options;
            using var testContext = new StoreContext(options);
            List<CartItem> items = new List<CartItem>()
            {
                new CartItem() {ProductId=1, Product=new Product(), CartId=1, Quantity=10},
                new CartItem() {ProductId=2, Product=new Product(), CartId=1, Quantity=10},
                new CartItem() {ProductId=3, Product=new Product(), CartId=1, Quantity=10},
                new CartItem() {ProductId=1, Product=new Product(), CartId=2, Quantity=10},
                new CartItem() {ProductId=1, Product=new Product(), CartId=3, Quantity=10}
            };
            testContext.CartItems.AddRange(items);
            testContext.SaveChanges();

            //Act
            using var assertContext = new StoreContext(options);
            repo = new DBRepo(assertContext);
            var result = repo.GetCartItems(1); //get where cartId=1

            //Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count); //there should be 3 cartItems where cartId=1
        }

        [Fact]
        private void AddToInvItemQuantityShouldAdd()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("AddToInvItemQuantityShouldAdd").Options;
            using var testContext = new StoreContext(options);
            testContext.InvItems.Add( new InvItem(){ProductId=1, LocationId=1, Quantity=30} ); //start with 30
            testContext.SaveChanges();

            //Act
            repo = new DBRepo(testContext);
            repo.AddToInvItemQuantity(1, 1, 20); //adding to productId=1, locationId=1, quantity=20
            
            //Arrange
            using var assertContext = new StoreContext(options);
            Assert.Equal(50, assertContext.InvItems.Single(x => x.ProductId==1 && x.LocationId==1).Quantity);
        }

        [Fact]
        private void UpdateCartItemQuantityShouldUpdate()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("UpdateCartItemQuantityShouldUpdate").Options;
            using var testContext = new StoreContext(options);
            CartItem item = new CartItem(){ProductId=1, CartId=1, Quantity=30}; //start with 30
            testContext.CartItems.Add(item);
            testContext.SaveChanges();

            //Act
            CartItem addItem = new CartItem(){ProductId=1, CartId=1, Quantity=50}; //same composite key
            repo = new DBRepo(testContext);
            repo.UpdateCartItemQuantity(addItem); //update to 50
            
            //Arrange
            using var assertContext = new StoreContext(options);
            Assert.Equal(50, assertContext.CartItems.Single(x => x.ProductId==1 && x.CartId==1).Quantity);
        }

        [Fact]
        private void EmptyCartShouldEmpty()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("EmptyCartShouldEmpty").Options;
            using var testContext = new StoreContext(options);
            testContext.Carts.Add(new Cart(){Id=1, LocationId=1, CustomerId=1}); //cart must exist
            List<CartItem> items = new List<CartItem>()
            {
                new CartItem() {ProductId=1, CartId=1, Quantity=5},
                new CartItem() {ProductId=2, CartId=1, Quantity=5},
                new CartItem() {ProductId=3, CartId=1, Quantity=5},
                new CartItem() {ProductId=1, CartId=2, Quantity=5},
                new CartItem() {ProductId=2, CartId=3, Quantity=5},
            };
            testContext.CartItems.AddRange(items);
            testContext.SaveChanges();

            //Act
            using var assertContext = new StoreContext(options);
            repo = new DBRepo(assertContext);
            repo.EmptyCart(1); //remove all CartItems where cartId=1

            //Assert
            Assert.Empty(assertContext.CartItems.Where(x => x.CartId==1).ToList());
        }

        [Fact]
        private void ReduceInventoryShouldReduce()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("ReduceInventoryShouldReduce").Options;
            using var testContext = new StoreContext(options);
            testContext.InvItems.Add(new InvItem(){Product=testProduct, ProductId=1, LocationId=1, Quantity=50});
            testContext.SaveChanges();

            //Act
            using var assertContext = new StoreContext(options);
            repo = new DBRepo(assertContext);
            repo.ReduceInventory(1, 1, 20); //reduce by 20

            //Assert
            Assert.Equal(30, assertContext.InvItems.First().Quantity); //50-20=30
        }
    }
}