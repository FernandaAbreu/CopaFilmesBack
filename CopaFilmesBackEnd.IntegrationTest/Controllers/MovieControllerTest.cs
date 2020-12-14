using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CopaFilmesBackEnd.ViewModel;
using Newtonsoft.Json;
using Xunit;

namespace CopaFilmesBackEnd.IntegrationTest.Controllers
{
    public class MovieControllerTest : BaseIntegration
    {

        [Fact]
        public async Task Generate_Sucessful_New_Winner()
        {
            var list = generateListObject();

            var response = await PostJsonAsync(list, $"{hostBaseApi}movie", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var resultPost = JsonConvert.DeserializeObject<List<VMMovie>>(postResult);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(resultPost);
            Assert.True(resultPost.Count() ==2);
            Assert.True(resultPost.Where(r => r.id.Equals("tt3606756") ).Count() == 1);
            Assert.True(resultPost.Where(r => r.id.Equals("tt4154756")).Count() == 1);
        }

        private List<VMMovie> generateListObject()
        {
            List<VMMovie> list = new List<VMMovie>();

            VMMovie mMovie1 = new VMMovie
            {
                titulo = "Os Incríveis 2",
                id = "tt3606756",
                ano = 2018,
                nota = 8.5
            };


            VMMovie mMovie2 = new VMMovie
            {
                titulo = "Jurassic World: Reino Ameaçado",
                id = "tt4881806",
                ano = 2018,
                nota = 6.7
            };

            VMMovie mMovie3 = new VMMovie
            {
                titulo = "Oito Mulheres e um Segredo",
                id = "tt5164214",
                ano = 2018,
                nota = 6.3
            };

            VMMovie mMovie4 = new VMMovie
            {
                titulo = "Hereditário",
                id = "tt7784604",
                ano = 2018,
                nota = 7.8
            };

            VMMovie mMovie5 = new VMMovie
            {
                titulo = "Vingadores: Guerra Infinita",
                id = "tt4154756",
                ano = 2018,
                nota = 8.8
            };

            VMMovie mMovie6 = new VMMovie
            {
                titulo = "Deadpool 2",
                id = "tt5463162",
                ano = 2018,
                nota = 8.1
            };


            VMMovie mMovie7 = new VMMovie
            {
                titulo = "Han Solo: Uma História Star Wars",
                id = "tt3778644",
                ano = 2018,
                nota = 7.2
            };

            VMMovie mMovie8 = new VMMovie
            {
                titulo = "Thor: Ragnarok",
                id = "tt3501632",
                ano = 2017,
                nota = 7.9
            };

            list.Add(mMovie1);
            list.Add(mMovie2);
            list.Add(mMovie3);
            list.Add(mMovie4);
            list.Add(mMovie5);
            list.Add(mMovie6);
            list.Add(mMovie7);
            list.Add(mMovie8);

            return list;
        }
    }
}