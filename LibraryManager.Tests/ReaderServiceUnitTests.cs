using LibraryManager.API.Services;
using LibraryManager.Shared.Models;

namespace LibraryManager.Tests
{
    public class ReaderServiceTests
    {
        [Fact]
        public void Reader_AddReader_StoresReader()
        {
            var service = new ReaderService();
            var reader = new Reader { ReaderNumber = 1, Name = "Mock Reader" };

            service.AddReader(reader);
            var result = service.GetReader(1);

            Assert.Equal("Mock Reader", result.Name);
        }

        [Fact]
        public void Reader_GetAllReaders_ReturnsAllReaders()
        {
            var service = new ReaderService();
            service.AddReader(new Reader { ReaderNumber = 2 });
            service.AddReader(new Reader { ReaderNumber = 3 });

            var result = service.GetAllReaders();

            Assert.True(result.Count == 2);
        }

        [Fact]
        public void Reader_GetReader_ReturnsCorrectReader()
        {
            var service = new ReaderService();
            var reader = new Reader { ReaderNumber = 4, Name = "Mock Reader2" };
            service.AddReader(reader);

            var result = service.GetReader(4);

            Assert.Equal(reader, result);
        }

        [Fact]
        public void Reader_RemoveReader_RemovesFromStored()
        {
            var service = new ReaderService();
            var reader = new Reader { ReaderNumber = 5, Name = "Mock Reader3" };
            service.AddReader(reader);

            service.RemoveReader(5);
            var result = service.GetReader(5);

            Assert.Null(result);
        }

        [Fact]
        public void Reader_UpdateReader_ReaderIsModified()
        {
            var service = new ReaderService();
            var reader = new Reader
            {
                ReaderNumber = 6,
                Name = "Mock Reader4",
                Address = "Mock Address",
                BirthDate = new DateOnly(2000, 1, 1)
            };
            service.AddReader(reader);

            var updatedReader = new Reader
            {
                ReaderNumber = 6,
                Name = "New Mock Reader",
                Address = "New Mock Address",
                BirthDate = new DateOnly(1990, 5, 10)
            };

            service.UpdateReader(updatedReader);
            var result = service.GetReader(6);

            Assert.Equal("New Mock Reader", result.Name);
            Assert.Equal("New Mock Address", result.Address);
            Assert.Equal(new DateOnly(1990, 5, 10), result.BirthDate);
        }
    }
}
