using System;
using System.Configuration;
using HappyPortal.Lib.Data;
using HappyPortal.Lib.Data.MongoDb;
using Nancy;
using System.Linq;

namespace HappyPortal.Modules
{
    public class ReportsModule : NancyModule
    {
        readonly IFeedbackRepository _feedbackRepo;
        readonly IQuestionRepository _questionRepo;

        public ReportsModule()
        {
            _feedbackRepo = new FeedbackRepository(ConfigurationManager.ConnectionStrings["happyIndex"].ConnectionString);
            _questionRepo = new QuestionRepository(ConfigurationManager.ConnectionStrings["happyIndex"].ConnectionString);

            Get["/api/reports/{name}"] = parameters => 
            {
                object result = null;
                string reportName = parameters.name;
                DateTime start, end;
                if (!DateTime.TryParse(Request.Query.start, out start))
                    start = DateTime.MinValue;
                if (!DateTime.TryParse(Request.Query.end, out end))
                    end = DateTime.MaxValue;
                switch (reportName)
                {
                    case "company":
                        result = BuildCompanyReport(start, end);
                        break;
                    case "pollingstation":
                        result = _feedbackRepo.GetPollingStationFeedbackReport(start, end, Request.Query.name);
                        break;
                }
                return result;
            };
        }

        private object BuildCompanyReport(DateTime start, DateTime end)
        {
            var questions = _questionRepo.GetAllQuestions();

            var report = _feedbackRepo.GetCompanyFeedbackReport(start, end);
            foreach (var reason in report.HappyReasons)
            {
                var question = questions.FirstOrDefault(q => q.Id == reason.QuestionId);
                reason.QuestionText = question != null ? question.Text : "";
            }
            foreach (var reason in report.UnhappyReasons)
            {
                var question = questions.FirstOrDefault(q => q.Id == reason.QuestionId);
                reason.QuestionText = question != null ? question.Text : "";
            }

            return report;
        }
    }
}