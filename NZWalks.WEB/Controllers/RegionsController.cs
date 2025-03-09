﻿using Microsoft.AspNetCore.Mvc;
using NZWalks.WEB.Models;
using NZWalks.WEB.Models.DTO;
using System.Text;
using System.Text.Json;

namespace NZWalks.WEB.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();
            
            try
            {
                // Get All Regions from Web API
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.GetAsync("https://localhost:7250/api/regions");
                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
            }
            catch (Exception ex)
            {
                // Log the Exception
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var client = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7250/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage =  await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if(response is not null)
                return RedirectToAction("Index", "Regions");
            
            return View();
        }
    }
}
