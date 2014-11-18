using System;
using System.Collections.Generic;
using System.Configuration;
using HappyPortal.Lib.Data;
using HappyPortal.Lib.Data.MongoDb;
using HappyPortal.Models;
using Nancy;
using Nancy.ModelBinding;

namespace HappyPortal.Modules
{
    public class FeedbackModule : NancyModule
    {
        public FeedbackModule()
        {
            IFeedbackRepository repo = new FeedbackRepository(ConfigurationManager.ConnectionStrings["happyIndex"].ConnectionString);

            Get["/api/feedback/{id:Guid}"] = parameters =>
            {
                return repo.GetById(parameters.id);
            };

            Get["/api/feedback"] = _ =>
            {
                var feedbackData = repo.GetAll();
                var feedback = new List<Feedback>();
                foreach (var f in feedbackData)
                {
                    feedback.Add(new Feedback() { 
                        CreateDate = f.CreateDate,
                        Id = f.Id,
                        Indicator = f.Indicator,
                        PollingStation = f.PollingStation,
                        QuestionId = f.QuestionId,
                        QuestionsShownIds = f.QuestionsShownIds
                    });
                }
                return feedback;
            };

            Post["/api/feedback"] = _ =>
            {
                var feedback = this.Bind<Feedback>();
                var newFeedback = repo.Add(feedback.Indicator, feedback.QuestionId, DateTime.UtcNow, feedback.PollingStation, feedback.QuestionsShownIds);
                return newFeedback;
            };

            Delete["/api/feedback/{id:Guid}"] = parameters =>
            {
                return repo.DeleteById(parameters.id);
            };
        }
    }
}