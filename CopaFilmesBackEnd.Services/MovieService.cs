using System;
using CopaFilmesBackEnd.Services.Interfaces;
using RestSharp;
using CopaFilmesBackEnd.Helpers;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using CopaFilmesBackEnd.ViewModel;
using System.Linq;
using Microsoft.Extensions.Options;

namespace CopaFilmesBackEnd.Services
{
    public class MovieService : IMovieService
    {
        

        private const string getall_filmes_controller = "filmes";
        string hostName;
        public IConfiguration Configuration;
        private AppSettings MySettings { get; set; }

        public MovieService(IOptions<AppSettings> settings)
        {
            MySettings = settings.Value;
            hostName = MySettings.ApiHostAddress;
        }


        public string GetAll()
        {
            var httpClient = new RestClient(hostName);
            var request = new RestRequest(getall_filmes_controller, Method.GET);
            IRestResponse response = httpClient.Execute(request);
            return response.Content;
        }

        public List<VMMovie> generatePositionsBySomeMovies(List<VMMovie> list)
        {
            if (list.Count != 8)
            {
                throw new CustomHttpException(400, "To this process select 8 itens in movie list");
            }
            var generatedOrdenatedList = generateOrdenateList(list);
            var topPositions = getTheTopPositions(generatedOrdenatedList);
            return topPositions;

        }

        private List<VMMovie> getTheTopPositions(List<VMMovie> generatedOrdenatedList)
        {
            var winners = getWinners(generatedOrdenatedList);
            while(winners.Count != 2)
            {
                winners = getWinners(winners);
            }
            List<VMMovie> winnersSortedList = new List<VMMovie>();
            var winner = getWinner(winners[0], winners[1]);
            if (winner.Equals(winners[0]))
            {
                winnersSortedList.Add(winners[0]);
                winnersSortedList.Add(winners[1]);
            }
            else
            {
                winnersSortedList.Add(winners[1]);
                winnersSortedList.Add(winners[0]);
            }
            return winnersSortedList;
        }

        private List<VMMovie> getWinners(List<VMMovie> generateOrdenateList)
        {
            List<VMMovie> listWinner = new List<VMMovie>();
            var listEven = generateOrdenateList.Where((x, i) => i % 2 == 0).ToArray();
            var listOdd = generateOrdenateList.Where((x, i) => i % 2 != 0).ToArray();
            for (int i = 0; i < listEven.Count(); i++)
            {
                var element1 = listEven[i];
                var element2 = listOdd[i];
                var winner =getWinner(element1,element2);
                listWinner.Add(winner);
            }

            return listWinner;

        }

        private VMMovie getWinner(VMMovie element1, VMMovie element2)
        {
            if(element1.nota == element2.nota)
            {
                return getWinnerByGradeTie(element1, element2);
            }
            else
            {
                return getWinnerByGrade(element1, element2);
            }
        }

        private VMMovie getWinnerByGradeTie(VMMovie element1, VMMovie element2)
        {
            List<VMMovie> list = new List<VMMovie>();
            list.Add(element1);
            list.Add(element2);
            var sortedList=list.OrderBy(c => c.titulo).ToList();
            return sortedList.First();

        }

        private static VMMovie getWinnerByGrade(VMMovie element1, VMMovie element2)
        {
            if (element1.nota > element2.nota)
                return element1;
            else
                return element2;
        }

        private List<VMMovie> generateOrdenateList(List<VMMovie> list)
        {
            var generateOrdenateList = new List<VMMovie>();
            var halfPosition = getTheHalfPisitionInList(list)+1;
            var lastPositionInList = (list.Count - 1);
            var listPart1 = list.GetRange(0, halfPosition);
            var listPart2 = list.Skip(halfPosition).ToList();
            listPart2.Reverse();
            return Enumerable.Range(0, list.Count)
                          .Select(i => (i % 2 == 0 ? listPart1 : listPart2)[i/2]).ToList();
        }

        private static int getTheHalfPisitionInList(List<VMMovie> list)
        {
            return ((list.Count - 1) / 2);
        }

       
    }
}
