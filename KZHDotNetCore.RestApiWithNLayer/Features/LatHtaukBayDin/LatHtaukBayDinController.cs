using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KZHDotNetCore.RestApiWithNLayer.Features.LatHtaukBayDin
{
    [Route("api/[controller]")]
    [ApiController]
    public class LatHtaukBayDinController : ControllerBase
    {
        private async Task<LatHtaukBayDin> GetDataAsync()
        {
            String jsonString = await System.IO.File.ReadAllTextAsync("mtk-data.json");
            var model = JsonConvert.DeserializeObject<LatHtaukBayDin>(jsonString);
            return model!;
        }

        // api/lathtaukbaydin/questions
        [HttpGet("questions")]
        public async Task<IActionResult> Questions()
        {
            var model = await GetDataAsync();
            return Ok(model.questions);
        }

        [HttpGet]
        public async Task<IActionResult> NumberList()
        {
            var model = await GetDataAsync();
            return Ok(model.numberList);
        }
        
        [HttpGet("{questionno}/{number}")]
        public async Task<IActionResult> Answer(int questionno, int number)
        {
            var model = await GetDataAsync();
            return Ok(model.answers.FirstOrDefault(x => x.questionNo == questionno && x.answerNo == number));
        }
    }

    public class LatHtaukBayDin
    {
        public Questions[] questions { get; set; }
        public Answers[] answers { get; set; }
        public string[] numberList { get; set; }
    }

    public class Questions
    {
        public int questionNo { get; set; }
        public string questionName { get; set; }
    }

    public class Answers
    {
        public int questionNo { get; set; }
        public int answerNo { get; set; }
        public string answerResult { get; set; }
    }
}