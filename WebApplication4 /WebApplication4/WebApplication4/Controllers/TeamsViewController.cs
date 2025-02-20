using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using WebApplication4.Dtos;

namespace FootballHub.Controllers
{
    public class TeamsViewController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:5011/api/teams"; // ✅ Change if needed

        public TeamsViewController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // ✅ LIST ALL TEAMS
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/list");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var teams = JsonSerializer.Deserialize<List<TeamDto>>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(teams);
            }

            ViewBag.Message = "Error fetching teams data.";
            return View(new List<TeamDto>());
        }

        // ✅ SHOW TEAM DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var team = JsonSerializer.Deserialize<TeamDto>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(team);
            }

            return NotFound();
        }

        // ✅ SHOW CREATE FORM
        public IActionResult Create()
        {
            return View();
        }

        // ✅ HANDLE CREATE FORM SUBMISSION
        [HttpPost]
        public async Task<IActionResult> Create(TeamDto team)
        {
            if (!ModelState.IsValid) return View(team);

            var jsonData = JsonSerializer.Serialize(team);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/add", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Message = "Error creating team.";
            return View(team);
        }

        // ✅ SHOW EDIT FORM
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var team = JsonSerializer.Deserialize<TeamDto>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(team);
            }

            return NotFound();
        }

        // ✅ HANDLE EDIT FORM SUBMISSION
        [HttpPost]
        public async Task<IActionResult> Edit(int id, TeamDto team)
        {
            if (!ModelState.IsValid) return View(team);

            var jsonData = JsonSerializer.Serialize(team);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/update/{id}", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Message = "Error updating team.";
            return View(team);
        }

        // ✅ SHOW DELETE CONFIRMATION PAGE
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var team = JsonSerializer.Deserialize<TeamDto>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(team);
            }

            return NotFound();
        }

        // ✅ HANDLE DELETE ACTION
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/delete/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Message = "Error deleting team.";
            return RedirectToAction("Index");
        }
    }
}
