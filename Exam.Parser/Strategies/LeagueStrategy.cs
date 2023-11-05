using AutoMapper;
using Exam.Domain.Entities;
using Exam.Domain.IRepository;
using Exam.Parser.DTOs;
using Exam.Parser.Response;
using Newtonsoft.Json;
using RestSharp;

namespace Exam.Parser.Strategies {
    public class LeagueStrategy : IStrategy {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILeagueRepository _leagueRepository;
        private readonly IMapper _mapper;
        public LeagueStrategy(ICategoryRepository categoryRepository, IMapper mapper, ILeagueRepository leagueRepository) {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _leagueRepository = leagueRepository;
        }
        public async void Execute() {

            var categories = await  _categoryRepository.GetAsync();

            foreach (var category in categories) {

                var client = new RestClient("https://footapi7.p.rapidapi.com/api/tournament/all/category/"+category.SystemId.ToString());
                var request = new RestRequest(Method.GET);
                request.AddHeader("X-RapidAPI-Key", "8b54d23209msh4ffc2814f5e6347p1b5591jsnbf51fe2ca96e");
                request.AddHeader("X-RapidAPI-Host", "footapi7.p.rapidapi.com");

                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful) {
                    string result = response.Content;
                    var responseDto = JsonConvert.DeserializeObject<LeagueResponseDto>(result);

                    var leagues = _mapper.Map<List<LeagueEntity>>(responseDto.groups);

                    foreach (var leagueRef in leagues) {
                        Console.WriteLine($"Id: {leagueRef.Id}, CategoryId: , Name: {leagueRef.Name}, UserCount: {leagueRef.UserCount}");
                    }
                } else {
                    Console.WriteLine("Ошибка при выполнении запроса: " + response.ErrorMessage);
                }
            }

           
        }
    }
}
