using Chinook.Data;
using Chinook.Persistence;
using EasyLOB.Persistence;
using Microsoft.Practices.Unity;
using Xunit;

namespace Chinook.UnitTest.xUnit
{
    public class EntityFrameworkTests : BaseTest
    {
        [Fact]
        public void Resolve_IChinookGenericRepository()
        {
            using (UnityContainer container = new UnityContainer())
            {
                // Arrange
                RegisterMappings(container);
                RegisterMappingsEF(container);

                // Act
                ResolverOverride[] overrides = new ResolverOverride[]
                {
                new ParameterOverride("unitOfWork", container.Resolve<IChinookUnitOfWork>())
                };
                var result = container.Resolve<IChinookGenericRepository<Album>>(overrides);

                // Assert
                Assert.IsType<ChinookGenericRepositoryEF<Album>>(result);
            }
        }

        [Fact]
        public void Resolve_IChinookUnitOfWork()
        {
            using (UnityContainer container = new UnityContainer())
            {
                // Arrange
                RegisterMappings(container);
                RegisterMappingsEF(container);

                // Act
                var result = container.Resolve<IChinookUnitOfWork>();

                // Assert
                Assert.IsType<ChinookUnitOfWorkEF>(result);
            }
        }

        [Fact]
        public void Resolve_IChinookAlbum()
        {
            using (UnityContainer container = new UnityContainer())
            {
                // Arrange
                RegisterMappings(container);
                RegisterMappingsEF(container);

                // Act
                IUnitOfWork unitOfWork = (IUnitOfWork)container.Resolve<IChinookUnitOfWork>();
                IGenericRepository<Album> repository = unitOfWork.GetRepository<Album>();
                var result = repository.GetById(1);

                // Assert
                Assert.IsType<Album>(result);
            }
        }
    }
}