using System;
using System.Collections.Generic;
using System.Linq;
using HappyPortal.Models;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace HappyPortal.Lib.Data.MongoDb
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly MongoDatabase _database;
        private readonly MongoCollection _feedbackCollection;

        public FeedbackRepository(string connectionString)
        {
            _database = new MongoClient(connectionString).GetServer().GetDatabase("happyindex");
            _feedbackCollection = _database.GetCollection<FeedbackData>("feedback");
        }

        public IFeedbackData Add(int indicator, Guid questionId, DateTime createDate, string pollingStation, IList<Guid> questionsShownIds)
        {
            var feedback = new FeedbackData
                {
                    CreateDate = createDate,
                    Id = Guid.NewGuid(),
                    Indicator = indicator,
                    QuestionId = questionId,
                    PollingStation = pollingStation
                };
            _feedbackCollection.Insert(feedback);
            return feedback;
        }

        public IFeedbackData GetById(Guid id)
        {
            var query = Query<FeedbackData>.EQ(f => f.Id,id);
            return _feedbackCollection.FindOneAs<FeedbackData>(query);
        }

        public bool DeleteById(Guid id)
        {
            var query = Query<FeedbackData>.EQ(f => f.Id, id);
            var result = _feedbackCollection.Remove(query);
            return result.Ok;
        }

        public IList<IFeedbackData> FindByDateRange(DateTime startDate, DateTime endDate)
        {
            var query = from f in _feedbackCollection.AsQueryable<FeedbackData>()
                        where f.CreateDate >= startDate && f.CreateDate <= endDate
                        select f;

            var result = new List<IFeedbackData>();
            foreach (var feedback in query)
            {
                result.Add(feedback);
            }
            return result;
        }

        public IList<IFeedbackData> GetAll()
        {
            var cursor = _feedbackCollection.FindAllAs<FeedbackData>();
            var result = new List<IFeedbackData>();
            foreach (var feedback in cursor)
            {
                result.Add(feedback);
            }
            return result;
        }

        public int GetIndexByDateRange(DateTime startDate, DateTime endDate)
        {
            var indicators = from f in _feedbackCollection.AsQueryable<FeedbackData>()
                    where f.CreateDate >= startDate && f.CreateDate <= endDate
                    select f.Indicator;

            return Services.HappyIndexCalculator.Calculate(indicators.ToList());
        }

        public List<IReasonData> GetUnhappyReasons(DateTime startDate, DateTime endDate)
        {
            var data = new List<IReasonData>();

            var reasonList = (from f in _feedbackCollection.AsQueryable<FeedbackData>()
                              where f.CreateDate >= startDate && f.CreateDate <= endDate && f.Indicator == -1 && f.QuestionId != Guid.Empty
                              select f.QuestionId).ToList();

            var reasons = reasonList.Distinct().ToList();

            foreach (var reason in reasons)
            {
                var reasonCount = reasonList.Count(_ => _.Equals(reason));
                data.Add(new UnhappyData { QuestionId = reason, Count = reasonCount });
            }

            return data;
        }

        public List<IReasonData> GetUnhappyReasonsByPollingStation(string name, DateTime startDate, DateTime endDate)
        {
            var data = new List<IReasonData>();

            var reasonList = (from f in _feedbackCollection.AsQueryable<FeedbackData>()
                              where f.PollingStation == name && 
                              f.CreateDate >= startDate && 
                              f.CreateDate <= endDate && 
                              f.Indicator == -1 && f.QuestionId != null
                              select f.QuestionId).ToList();

            var reasons = reasonList.Distinct().ToList();

            foreach (var reason in reasons)
            {
                var reasonCount = reasonList.Count(_ => _.Equals(reason));
                data.Add(new UnhappyData { QuestionId = reason, Count = reasonCount });
            }

            return data;
        }

        public List<IReasonData> GetHappyReasons(DateTime startDate, DateTime endDate)
        {
            var data = new List<IReasonData>();

            var reasonList = (from f in _feedbackCollection.AsQueryable<FeedbackData>()
                              where f.CreateDate >= startDate && f.CreateDate <= endDate && f.Indicator == 1 && f.QuestionId != Guid.Empty
                              select f.QuestionId).ToList();

            var reasons = reasonList.Distinct().ToList();

            foreach (var reason in reasons)
            {
                var reasonCount = reasonList.Count(_ => _.Equals(reason));
                data.Add(new UnhappyData { QuestionId = reason, Count = reasonCount });
            }

            return data;
        }

        public List<IReasonData> GetHappyReasonsByPollingStation(string name, DateTime startDate, DateTime endDate)
        {
            var data = new List<IReasonData>();

            var reasonList = (from f in _feedbackCollection.AsQueryable<FeedbackData>()
                              where f.PollingStation == name &&
                              f.CreateDate >= startDate &&
                              f.CreateDate <= endDate &&
                              f.Indicator == 1 && f.QuestionId != Guid.Empty
                              select f.QuestionId).ToList();

            var reasons = reasonList.Distinct().ToList();

            foreach (var reason in reasons)
            {
                var reasonCount = reasonList.Count(_ => _.Equals(reason));
                data.Add(new UnhappyData { QuestionId = reason, Count = reasonCount });
            }

            return data;
        }

        public IPollingStationFeedbackReport GetPollingStationFeedbackReport(DateTime startDate, DateTime endDate, string pollingStation)
        {
            var query = from f in _feedbackCollection.AsQueryable<FeedbackData>()
                        where f.CreateDate >= startDate && f.CreateDate <= endDate && f.PollingStation.Equals(pollingStation??"")
                        select f;
            
            var indicators = new List<int>();
            foreach (var feedback in query)
            {
                indicators.Add(feedback.Indicator);
            }
            var result = new PollingStationFeedbackReport();
            result.HappinessIndex = Services.HappyIndexCalculator.Calculate(indicators);
            result.PollingStation = pollingStation;
            result.FeedbackCount = query.Count();
            result.HappinessIndexToday = GetIndexByDateRange(DateTime.Today, DateTime.Today.AddHours(24));
            
            return result;
        }

        public ICompanyFeedbackReport GetCompanyFeedbackReport(DateTime startDate, DateTime endDate)
        {
            var query = from f in _feedbackCollection.AsQueryable<FeedbackData>()
                        where f.CreateDate >= startDate && f.CreateDate <= endDate
                        select f;
            var feedback = query.ToList();
            var result = new CompanyFeedbackReport();

            //try get the daily report, first group by day
            var dailyGroups = feedback.GroupBy(f => f.CreateDate.Date);
            //for each day calculate the happiness index
            foreach (var dailyGroup in dailyGroups)
            {
                var dailyIndex = new DailyIndexData
                    {
                        Index = Services.HappyIndexCalculator.Calculate(dailyGroup.Select(f => f.Indicator).ToList()),
                        ReportDate = dailyGroup.First().CreateDate.Date
                    };
                dailyIndex.Count = dailyGroup.Count();
                result.DailyIndexes.Add(dailyIndex);
            }
            result.HappinessIndex = Services.HappyIndexCalculator.Calculate(feedback.Select(f => f.Indicator).ToList());
            result.HappinessIndexToday = GetIndexByDateRange(DateTime.Today, DateTime.Today.AddHours(24));
            result.FeedbackCount = feedback.Count();
            result.UnhappyReasons = GetUnhappyReasons(startDate, endDate);
            result.HappyReasons = GetHappyReasons(startDate, endDate);

            return result;
        }
    }
}