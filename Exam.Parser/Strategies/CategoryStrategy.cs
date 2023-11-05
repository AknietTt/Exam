using AutoMapper;

using Exam.Domain.Entities;
using Exam.Domain.IRepository;
using Exam.Domain.SeedWork;
using Exam.Parser.Response;

using Newtonsoft.Json;

using RestSharp;
using RestSharp.Authenticators;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Parser.Strategies {
    public class CategoryStrategy:IStrategy {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryStrategy(ICategoryRepository categoryRepository, IMapper mapper) {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async void Execute() {

            var client = new RestClient("https://footapi7.p.rapidapi.com/api/tournament/categories"); 
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-RapidAPI-Key", "8b54d23209msh4ffc2814f5e6347p1b5591jsnbf51fe2ca96e");
            request.AddHeader("X-RapidAPI-Host", "footapi7.p.rapidapi.com");

            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful) {
                string result = response.Content;
                CategoriesResponse categoriesResponse = JsonConvert.DeserializeObject<CategoriesResponse>(result);

                var _list = _mapper.Map<List<CategoryEntity>>(categoriesResponse.Categories);
                await _categoryRepository.AddRangeAsync(_list.ToArray());
                await _categoryRepository.SaveChangesAsync();
                
            } else {
                Console.WriteLine("Ошибка при выполнении запроса: " + response.ErrorMessage);
            }

        }
    }
}
