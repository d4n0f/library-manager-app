using LibraryManager.Shared.Models;

namespace LibraryManager.Client.Interfaces
{
    public interface IReaderService
    {
        Task<List<Reader>> GetAllReadersAsync();

        Task<Reader> GetReaderAsync(int readerNumber);

        Task AddReaderAsync(Reader reader);

        Task UpdateReaderAsync(int readerNumber, Reader reader);

        Task RemoveReaderAsync(int readerNumber);
    }
}