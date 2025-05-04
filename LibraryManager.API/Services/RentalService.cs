using LibraryManager.API.Interfaces;
using LibraryManager.API.Models;

namespace LibraryManager.API.Services
{
    public class RentalService : IRentalService
    {
        private readonly List<Rental> _rentals;

        public RentalService()
        {
            _rentals = [];
        }

        public List<Rental> GetAllRentals()
        {
            return _rentals;
        }

        public Rental GetRental(int readerNumber, int inventoryNumber)
        {
            return _rentals.Find(x => x.InventoryNumber == inventoryNumber && x.ReaderNumber == readerNumber);
        }

        public bool RentBook(Rental rental)
        {
            if (_rentals.Any(x => x.InventoryNumber == rental.InventoryNumber && x.ReaderNumber == rental.ReaderNumber))
            {
                return false;
            }
            _rentals.Add(rental);
            return true;
        }
        
        public bool ReturnBook(int readerNumber, int inventoryNumber)
        {
            var rental = _rentals.Find(x => x.InventoryNumber == inventoryNumber && x.ReaderNumber == readerNumber);

            if (rental == null)
            {
                return false;
            }

            _rentals.Remove(rental);
            return true;
        }
    }
}
