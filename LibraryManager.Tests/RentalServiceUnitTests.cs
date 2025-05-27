using LibraryManager.Shared.Models;
using LibraryManager.API.Services;

namespace LibraryManager.Tests
{
    public class RentalServiceTests
    {
        
        [Fact]
        public void Rental_RentBook_StoresRental()
        {
            var service = new RentalService();
            var rental = new Rental
            {
                InventoryNumber = 1,
                ReaderNumber = 10,
                RentalDate = DateOnly.FromDateTime(DateTime.Today),
                DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(14))
            };

            service.RentBook(rental);

            var result = service.GetRental(10, 1);

            Assert.Equal("1", result.InventoryNumber.ToString());
        }
        

        [Fact]
        public void Rental_GetAllRentals_ReturnsAllRentals()
        {
            var service = new RentalService();
            service.RentBook(new Rental { InventoryNumber = 2, ReaderNumber = 11 });
            service.RentBook(new Rental { InventoryNumber = 3, ReaderNumber = 12 });

            var result = service.GetAllRentals();

            Assert.True(result.Count == 2);
        }

        [Fact]
        public void Rental_GetRental_ReturnsCorrectRental()
        {
            var service = new RentalService();
            var rental = new Rental { InventoryNumber = 4, ReaderNumber = 13 };
            service.RentBook(rental);

            var result = service.GetRental(13, 4);

            Assert.Equal(rental, result);
        }

        [Fact]
        public void Rental_ReturnBook_RemovesRental()
        {
            var service = new RentalService();
            var rental = new Rental { InventoryNumber = 5, ReaderNumber = 14 };
            service.RentBook(rental);

            var returnSuccess = service.ReturnBook(14, 5);
            var result = service.GetRental(14, 5);

            Assert.True(returnSuccess);
            Assert.Null(result);
        }

        [Fact]
        public void Rental_RentBook_PreventsDuplicate()
        {
            var service = new RentalService();
            var rental = new Rental { InventoryNumber = 6, ReaderNumber = 15 };
            service.RentBook(rental);

            var duplicateRent = service.RentBook(rental);

            Assert.False(duplicateRent);
        }

        
        [Fact]
        public void Rental_UpdateRental_RentalIsModified()
        {
            var service = new RentalService();
            var originalRental = new Rental
            {
                ReaderNumber = 6,
                InventoryNumber = 600,
                RentalDate = new DateOnly(2015,1,1),
                DueDate = new DateOnly(2015, 1, 7),
            };
            service.RentBook(originalRental);

            var updatedRental = new Rental
            {
                ReaderNumber = 6,
                InventoryNumber = 600,
                RentalDate = new DateOnly(2015, 2, 1),
                DueDate = new DateOnly(2015, 2, 7),
            };

            service.UpdateRental(updatedRental);
            var result = service.GetRental(6, 600);

            Assert.Equal(new DateOnly(2015, 2, 1), result.RentalDate);
            Assert.Equal(new DateOnly(2015, 2, 7), result.DueDate);
        }
        
    }
}
