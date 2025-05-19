using LibraryManager.API.Interfaces;
using LibraryManager.Shared.Models;

namespace LibraryManager.API.Services
{
    public class ReaderService : IReaderService
    {
        private readonly List<Reader> _readers;

        public ReaderService()
        {
            _readers = [];
        }

        public void AddReader(Reader reader)
        {
            _readers.Add(reader);
        }

        public List<Reader> GetAllReaders()
        {
            return _readers;
        }

        public Reader GetReader(int readerNumber)
        {
            return _readers.Find(x => x.ReaderNumber == readerNumber);
        }

        public void RemoveReader(int readerNumber)
        {
            _readers.RemoveAll(x => x.ReaderNumber == readerNumber);
        }

        public void UpdateReader(Reader reader)
        {
            var oldReader = GetReader(reader.ReaderNumber);

            oldReader.ReaderNumber = reader.ReaderNumber;
            oldReader.Name = reader.Name;
            oldReader.Address = reader.Address;
            oldReader.BirthDate = reader.BirthDate;
        }
    }
}
