using Faker;
using HappyPortal.Lib.Data;
using HappyPortal.Lib.Data.MongoDb;
using NUnit.Framework;
using System.Configuration;

namespace HappyPortal.IntegrationTests
{
    [TestFixture]
    public class QuestionRepositoryTest
    {
        private IQuestionRepository _repo;

        [TestFixtureSetUp]
        public void Init()
        {
            _repo = new QuestionRepository(ConfigurationManager.ConnectionStrings["happyIndex"].ConnectionString);
        }

        [Test]
        public void Add_Success()
        {
            var sample = _repo.Add(Lorem.Sentence(), "Happy");
            var result = _repo.Get(sample.Id);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetByCategory_Success()
        {
            var category = Lorem.GetFirstWord();
            _repo.Add(Lorem.Sentence(), category);
            _repo.Add(Lorem.Sentence(), category);
            var result = _repo.GetByCategory(category);
            Assert.IsNotNull(result);
            Assert.Greater(result.Count,1);
        }
    }
}
