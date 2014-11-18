using System;
using System.Collections.Generic;
using System.Linq;
using HappyPortal.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace HappyPortal.Lib.Data.MongoDb
{
    public class PollingStationRepository : IPollingStationRepository
    {
        private readonly MongoDatabase _database;
        private readonly MongoCollection _stationCollection;

        public PollingStationRepository(string connectionString)
        {
            _database = new MongoClient(connectionString).GetServer().GetDatabase("happyindex");
            _stationCollection = _database.GetCollection<PollingStationData>("pollingStation");
        }

        public IPollingStationData Add(string name)
        {
            var station = new PollingStationData()
            {
                Id = Guid.NewGuid(),
                Name = name
            };
            _stationCollection.Insert(station);
            return station;
        }

        public IPollingStationData Update(IPollingStationData pollingStation)
        {
            if (pollingStation == null)
                return null;

            _stationCollection.Save(pollingStation);

            return pollingStation;
        }

        public IList<IPollingStationData> GetAll()
        {
            var result = new List<IPollingStationData>();
            foreach (var stationData in _stationCollection.FindAllAs<PollingStationData>())
            {
                result.Add(new PollingStationData
                    {
                        Id = stationData.Id,
                        Name = stationData.Name,
                        QuestionIds = stationData.QuestionIds
                    });
            }
            return result;
        }

        public IPollingStationData Get(Guid id)
        {
            var query = Query<QuestionData>.EQ(q => q.Id, id);
            return _stationCollection.FindOneAs<PollingStationData>(query);
        }
    }
}