using System.Configuration;
using HappyPortal.Lib.Data;
using HappyPortal.Lib.Data.MongoDb;
using HappyPortal.Models;
using Nancy;
using Nancy.ModelBinding;

namespace HappyPortal.Modules
{
    public class QuestionModule : NancyModule
    {
        public QuestionModule()
        {
            IQuestionRepository repo = new QuestionRepository(ConfigurationManager.ConnectionStrings["happyIndex"].ConnectionString);

            Get["/api/questions"] = _ =>
                {
                    var category = Request.Query.category;

                    //filter the results if a category was supplied
                    return string.IsNullOrEmpty(category) ? repo.GetAllQuestions() : repo.GetByCategory(category);
                };

            Post["/api/questions"] = _ =>
            {
                var q = this.Bind<Question>();
                return repo.Add(q.Text, q.Category);
            };
        }
    }
}