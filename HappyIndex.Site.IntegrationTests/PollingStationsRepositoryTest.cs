using System;
using System.Collections.Generic;
using Faker;
using HappyPortal.Lib.Data;
using HappyPortal.Lib.Data.MongoDb;
using NUnit.Framework;
using System.Configuration;

namespace HappyPortal.IntegrationTests
{
    [TestFixture]
    public class PollingStationRepositoryTest
    {
        private IPollingStationRepository _repo;

        [TestFixtureSetUp]
        public void Init()
        {
            _repo = new PollingStationRepository(ConfigurationManager.ConnectionStrings["happyIndex"].ConnectionString);
        }

        [Test]
        public void Add_Success()
        {
            var sample = _repo.Add(Lorem.Sentence());
            var result = _repo.Get(sample.Id);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Update_Success()
        {
            var sample = _repo.Add(Lorem.Sentence());
            var result = _repo.Get(sample.Id);
            Assert.IsNotNull(result);
            sample.QuestionIds = new List<Guid>();
            var qid = Guid.NewGuid();
            sample.QuestionIds.Add(qid);
            _repo.Update(sample);
            result = _repo.Get(sample.Id);
            Assert.IsTrue(result.QuestionIds.Contains(qid));
        }

        [Test]
        public void GetAll_Success()
        {
            var sample = _repo.Add(Lorem.Sentence());
            var result = _repo.GetAll();
            Assert.IsNotNull(result);
            Assert.Greater(result.Count,1);
            var found = false;
            foreach (var station in result)
            {
                if (station.Id != sample.Id) continue;
                found = true;
                return;
            }
            Assert.IsTrue(found);
        }
    }
}
