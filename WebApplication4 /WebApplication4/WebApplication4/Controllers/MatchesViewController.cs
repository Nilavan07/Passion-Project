using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication4.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace FootballHub.Controllers
{
    public class MatchesViewController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:5011/api/matches"; 

        public MatchesViewController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
      

        //  LIST MATCHES
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/list");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var matches = JsonSerializer.Deserialize<List<MatchDto>>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(matches);
            }

            ViewBag.Message = "Error fetching matches.";
            return View(new List<MatchCreateDto>());
        }
  [Authorize]
        // MATCH DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var match = JsonSerializer.Deserialize<MatchDto>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(match);
            }

            return NotFound();
        }
        [Authorize]

        // SHOW CREATE FORM
        public IActionResult Create()
        {
            return View();
        }

        // HANDLE CREATE FORM SUBMISSION
        [HttpPost]
        public async Task<IActionResult> Create(MatchCreateDto match)
        {
            if (!ModelState.IsValid) return View(match);

            var jsonData = JsonSerializer.Serialize(match);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/add", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Message = "Error creating match.";
            return View(match);
        }
  [Authorize]
        // SHOW EDIT FORM
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var match = JsonSerializer.Deserialize<MatchCreateDto>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(match);
            }

            return NotFound();
        }

        // HANDLE EDIT FORM SUBMISSION
        [HttpPost]
        public async Task<IActionResult> Edit(int id, MatchCreateDto match)
        {
            if (!ModelState.IsValid) return View(match);

            var jsonData = JsonSerializer.Serialize(match);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/update/{id}", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Message = "Error updating match.";
            return View(match);
        }

        [Authorize]

        // DELETE CONFIRMATION PAGE
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var match = JsonSerializer.Deserialize<MatchDto>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(match);
            }

            return NotFound();
        }

        //  HANDLE DELETE REQUEST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/delete/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Message = "Error deleting match.";
            return RedirectToAction("Index");
        }
    }
}
