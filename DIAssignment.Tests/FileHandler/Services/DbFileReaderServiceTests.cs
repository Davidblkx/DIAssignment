using Xunit;
using DIAssignment.FileHandler.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIAssignment.Core.DBFile;
using DIAssignment.Core.Models.Entity;

namespace DIAssignment.Tests.FileHandler.Services
{
    public class DbFileReaderServiceTests
    {
        [Fact]
        public async Task TestReadRows()
        {
            var file = "Assets/artist_collection";
            var service = new DbFileReaderService();

            var subject = new List<DbFileRow>();

            await service.ReadDbFile(file, e => subject.Add(e));

            Assert.Equal(3, subject.Count);

            var item1 = subject[0];
            Assert.Equal("1", item1.GetValue("#export_date"));
            Assert.Equal("10440", item1.GetValue("artist_id"));
            Assert.Equal("1549232495", item1.GetValue("collection_id"));
            Assert.Equal("1", item1.GetValue("is_primary_artist"));
            Assert.Equal("1", item1.GetValue("role_id"));

            var item2 = subject[1];
            Assert.Equal("1", item2.GetValue("#export_date"));
            Assert.Equal("10440", item2.GetValue("artist_id"));
            Assert.Equal("1549232495", item2.GetValue("collection_id"));
            Assert.Equal("0", item2.GetValue("is_primary_artist"));
            Assert.Equal("7", item2.GetValue("role_id"));

            var item3 = subject[2];
            Assert.Equal("1", item3.GetValue("#export_date"));
            Assert.Equal("10459", item3.GetValue("artist_id"));
            Assert.Equal("1473969404", item3.GetValue("collection_id"));
            Assert.Equal("0", item3.GetValue("is_primary_artist"));
            Assert.Equal("1", item3.GetValue("role_id"));
        }

        [Fact]
        public async Task TestSerializeRows()
        {
            var file = "Assets/artist_collection";
            var service = new DbFileReaderService();

            var subject = new List<ArtistCollection>();
            void Serialize(DbFileRow row) => subject
                .Add(DbFileRowSerializer.Serialize<ArtistCollection>(row));

            await service.ReadDbFile(file, Serialize);

            Assert.Equal(3, subject.Count);

            var item1 = subject[0];
            Assert.Equal(10440, item1.ArtistId);
            Assert.Equal(1549232495, item1.CollectionId);
            Assert.True(item1.IsPrimaryArtist);
            Assert.Equal(1, item1.RoleId);

            var item2 = subject[1];
            Assert.Equal(10440, item2.ArtistId);
            Assert.Equal(1549232495, item2.CollectionId);
            Assert.False(item2.IsPrimaryArtist);
            Assert.Equal(7, item2.RoleId);

            var item3 = subject[2];
            Assert.Equal(10459, item3.ArtistId);
            Assert.Equal(1473969404, item3.CollectionId);
            Assert.False(item3.IsPrimaryArtist);
            Assert.Equal(1, item3.RoleId);
        }
    }
}
