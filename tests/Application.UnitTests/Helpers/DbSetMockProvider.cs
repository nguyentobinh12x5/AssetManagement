using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using Moq;

namespace AssetManagement.Application.UnitTests.Helpers
{
    public static class DbSetMockProvider
    {
        public static Mock<DbSet<T>> GetDbSetMock<T>(IQueryable<T> data) where T : class
        {
            var mockDbSet = new Mock<DbSet<T>>();

            // Setup synchronous operations
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<T>(data.Provider));
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            // Setup asynchronous operations
            mockDbSet.As<IAsyncEnumerable<T>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<T>(data.GetEnumerator()));

            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<T>(data.Provider));

            // Setup DbSet operations
            mockDbSet.Setup(d => d.Add(It.IsAny<T>())).Callback((T entity) =>
            {
                var list = data.ToList();
                list.Add(entity);
                data = list.AsQueryable();
            });

            return mockDbSet;
        }
    }
}