using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.RepositoryTests
{
    [TestFixture]
    public class RepositoryTests
    {
        private HealthHubDbContext _dbContext;
        private Repository<Address> _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HealthHubDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new HealthHubDbContext(options);
            _repository = new Repository<Address>(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public void GetAll_ShouldReturnAllEntities()
        {
            // Arrange
            var entity1 = new Address { Street = "Shevchenka", City = "Kyiv", Country = "UA", ZipCode = "123456"  };
            var entity2 = new Address { Street = "Lysenka", City = "Kyiv", Country = "UA", ZipCode = "123457" };
            _dbContext.Set<Address>().AddRange(entity1, entity2);
            _dbContext.SaveChanges();

            // Act
            var result = _repository.GetAll().ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(e => e.Id == entity1.Id));
            Assert.IsTrue(result.Any(e => e.Id == entity2.Id));
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnEntityById()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            var entity = new Address { Id = entityId, Street = "Shevchenka", City = "Kyiv", Country = "UA", ZipCode = "123456" };
            _dbContext.Set<Address>().Add(entity);
            _dbContext.SaveChanges();

            // Act
            var result = await _repository.GetByIdAsync(entityId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entityId, result.Id);
        }

        [Test]
        public async Task AddAsync_ShouldAddNewEntity()
        {
            // Arrange
            var entity = new Address { Street = "Shevchenka", City = "Kyiv", Country = "UA", ZipCode = "123456" };

            // Act
            var resultId = await _repository.AddAsync(entity);
            var result = await _repository.GetByIdAsync(resultId);

            // Assert
            Assert.AreEqual(resultId, result.Id);
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateExistingEntity()
        {
            // Arrange
            var entity = new Address { Street = "Shevchenka", City = "Kyiv", Country = "UA", ZipCode = "123456" };
            _dbContext.Set<Address>().Add(entity);
            _dbContext.SaveChanges();

            // Act
            entity.Country = "US";
            await _repository.UpdateAsync(entity);
            var updatedEntity = await _repository.GetByIdAsync(entity.Id);

            // Assert
            Assert.AreEqual("US", updatedEntity.Country);
        }

        [Test]
        public async Task SoftDeleteAsync_ShouldSoftDeleteEntity()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            var entity = new Address { Id = entityId, Street = "Shevchenka", City = "Kyiv", Country = "UA", ZipCode = "123456" };
            _dbContext.Set<Address>().Add(entity);
            _dbContext.SaveChanges();

            // Act
            await _repository.SoftDeleteAsync(entityId);
            var softDeletedEntity = await _repository.GetByIdAsync(entityId);

            // Assert
            Assert.IsNotNull(softDeletedEntity);
            Assert.IsTrue(softDeletedEntity.IsDeleted);
        }
    }
}