using Chinook.Application;
using Chinook.Data;
using Chinook.Persistence;
using Microsoft.Practices.Unity;
using Xunit;

namespace Chinook.UnitTest.xUnit
{
    public class ApplicationTests : BaseTest
    {
        [Fact]
        public void Resolve_IChinookApplication()
        {
            using (UnityContainer container = new UnityContainer())
            {
                // Arrange
                RegisterMappings(container);
                RegisterMappingsEF(container);

                // Act
                var result = container.Resolve<IChinookApplication>();

                Assert.IsType<ChinookApplication>(result);
            }
        }

        [Fact]
        public void Resolve_IChinookGenericApplication_EF()
        {
            using (UnityContainer container = new UnityContainer())
            {
                // Arrange
                RegisterMappings(container);
                RegisterMappingsEF(container);

                // Act
                var result = container.Resolve<IChinookGenericApplication<Album>>();

                Assert.IsType<ChinookGenericApplication<Album>>(result);
            }
        }

        [Fact]
        public void Resolve_IChinookGenericApplicationDTO_EF()
        {
            using (UnityContainer container = new UnityContainer())
            {
                // Arrange
                RegisterMappings(container);
                RegisterMappingsEF(container);

                // Act
                var result = container.Resolve<IChinookGenericApplicationDTO<AlbumDTO, Album>>();

                Assert.IsType<ChinookGenericApplicationDTO<AlbumDTO, Album>>(result);
            }
        }

        [Fact]
        public void Resolve_IChinookGenericApplication_NH()
        {
            using (UnityContainer container = new UnityContainer())
            {
                // Arrange
                RegisterMappings(container);
                RegisterMappingsNH(container);

                // Act
                var result = container.Resolve<IChinookGenericApplication<Album>>();

                Assert.IsType<ChinookGenericApplication<Album>>(result);
            }
        }

        [Fact]
        public void Resolve_IChinookGenericApplicationDTO_NH()
        {
            using (UnityContainer container = new UnityContainer())
            {
                // Arrange
                RegisterMappings(container);
                RegisterMappingsNH(container);

                // Act
                var result = container.Resolve<IChinookGenericApplicationDTO<AlbumDTO, Album>>();

                Assert.IsType<ChinookGenericApplicationDTO<AlbumDTO, Album>>(result);
            }
        }
    }
}