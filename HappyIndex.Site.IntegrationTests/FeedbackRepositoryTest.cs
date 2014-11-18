using System.Collections.Generic;
using Faker;
using HappyPortal.Lib.Data;
using HappyPortal.Lib.Data.MongoDb;
using NUnit.Framework;
using System;
using System.Configuration;

namespace HappyPortal.IntegrationTests
{
    [TestFixture]
    public class FeedbackRepositoryTest
    {
        private IFeedbackRepository _repo;

        [TestFixtureSetUp]
        public void Init()
        {
            _repo = new FeedbackRepository(ConfigurationManager.ConnectionStrings["happyIndex"].ConnectionString);
        }

        [Test]
        public void Add_Success()
        {
            var sample = _repo.Add(new Random().Next(), new Guid(), DateTime.UtcNow, Lorem.Sentence(), new List<Guid>());
            var result = _repo.GetById(sample.Id);
            Assert.IsNotNull(result);

            //tidy up
            _repo.DeleteById(sample.Id);
        }

        [Test]
        public void Add_ValidateCreateDateConvertedToUTC()
        {
            var createDate = DateTime.Now;
            var sample = _repo.Add(new Random().Next(), new Guid(), createDate, Lorem.Sentence(), new List<Guid>());
            var result = _repo.GetById(sample.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(createDate.ToUniversalTime().ToShortDateString(), result.CreateDate.ToShortDateString());
            Assert.AreEqual(createDate.ToUniversalTime().ToShortTimeString(), result.CreateDate.ToShortTimeString());

            //tidy up
            _repo.DeleteById(sample.Id);
        }

        [Test]
        public void Delete_Success()
        {
            var sample = _repo.Add(new Random().Next(), new Guid(), DateTime.UtcNow, Lorem.Sentence(), new List<Guid>());
            Assert.IsNotNull(_repo.GetById(sample.Id));
            _repo.DeleteById(sample.Id);
            Assert.IsNull(_repo.GetById(sample.Id));
        }

        [Test]
        public void Get_Success()
        {
            var sample = _repo.Add(new Random().Next(), new Guid(), DateTime.UtcNow, Lorem.Sentence(), new List<Guid>());
            var result = _repo.GetById(sample.Id);
            Assert.IsNotNull(result);

            //tidy up
            _repo.DeleteById(sample.Id);
        }

        [Test]
        public void FindByDateRange_Success()
        {
            const int futureDays = 1000000;
            var eventDate = DateTime.UtcNow;
            var searchStart = eventDate.AddSeconds(-1);
            var searchEnd = eventDate.AddSeconds(1);
            var sample = _repo.Add(new Random().Next(), Guid.NewGuid(), eventDate, Lorem.Sentence(), new List<Guid>());
            var result = _repo.FindByDateRange(searchStart, searchEnd);

            Assert.IsNotNull(result);
            Assert.AreEqual(1,result.Count);

            //tidy up
            _repo.DeleteById(sample.Id);
        }

        [Test]
        public void GetPollingStationFeedbackReport_Success()
        {
            const int indicator = 1;
            var station = Lorem.Sentence();
            var sample = _repo.Add(indicator, new Guid(), DateTime.UtcNow, station, new List<Guid>());
            var result = _repo.GetPollingStationFeedbackReport(DateTime.Now.AddDays(-5),DateTime.Now,station);
            Assert.IsNotNull(result);
            Assert.AreEqual(sample.PollingStation,result.PollingStation);
            Assert.AreEqual(100,result.HappinessIndex);

            //tidy up
            _repo.DeleteById(sample.Id);
        }
    }
}
