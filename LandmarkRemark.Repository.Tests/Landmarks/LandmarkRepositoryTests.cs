using Moq;
using Moq.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NUnit.Framework;

using LandmarkRemark.Entities;
using LandmarkRemark.Repository.Landmarks;

namespace LandmarkRemark.Repository.Tests.Landmarks
{
    [TestFixture]
    public class LandmarkRepositoryTests
    {
        /// <summary>
        /// Tests creating a new landmark.
        /// </summary>
        [Test]
        public async Task TestCreateLandmarkWorksSucessfully()
        {
            // Set up the context and repository.
            var configuration = Mock.Of<IConfiguration>();

            var landmarks = new List<Landmark>();
            var context = new Mock<LandmarkRemarkContext>(configuration);
            context.SetupGet(x => x.Landmarks).ReturnsDbSet(landmarks);

            var repository = new LandmarkRepository(context.Object);

            // Set up the test data.
            var landmark = new Landmark()
            {
                Notes = "This is a test",
                Location = new Point(149.125241, -35.307003), // Parliament House, Canberra.
                UserId = 1
            };

            // Create the landmark.
            await repository.Create(landmark);

            // Verify a new landmark was added and saved.
            context.Verify(c => c.Landmarks.Add(It.Is<Landmark>(l => l.Created.Day == DateTime.UtcNow.Day
                && l.Updated.Day == DateTime.UtcNow.Day && !l.Deleted)));
            context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}