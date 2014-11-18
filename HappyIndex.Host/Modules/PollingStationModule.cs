using System.Configuration;
using HappyPortal.Lib.Data;
using HappyPortal.Lib.Data.MongoDb;
using HappyPortal.Models;
using Nancy;
using Nancy.ModelBinding;

namespace HappyPortal.Modules
{
    public class PollingStationModule : NancyModule
    {
        public PollingStationModule()
        {
            IPollingStationRepository _repo = new PollingStationRepository(ConfigurationManager.ConnectionStrings["happyIndex"].ConnectionString);

            Get["/api/pollingstations"] = _ => 
            {
                return _repo.GetAll();
            };

            Get["/api/pollingstations/{id:Guid}"] = parameters =>
            {
                return _repo.Get(parameters.id);
            };

            Post["/api/pollingstations"] = _ =>
            {
                var station = this.Bind<PollingStation>();
                var newStation = _repo.Add(station.Name);
                newStation.QuestionIds = station.QuestionIds;
                return _repo.Update(newStation);
            };

            Put["/api/pollingstations/{id:Guid}"] = parameters =>
            {
                var station = this.Bind<PollingStation>();
                return _repo.Update(new PollingStationData()
                {
                    Id = parameters.id,
                    Name = station.Name,
                    QuestionIds = station.QuestionIds
                });
            };
        }
    }
}