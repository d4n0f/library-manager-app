using LibraryManager.Shared.Models;

namespace LibraryManager.API.Interfaces
{
    public interface IReaderService
    {
        Reader GetReader(int readerNumber);

        List<Reader> GetAllReaders();

        void AddReader(Reader reader);

        void RemoveReader(int readerNumber);

        void UpdateReader(Reader reader);
    }
}
