using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using WebApplication4.Dtos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace FootballHub.Controllers
{
    public class PlayersViewController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:5011/api/players"; // ✅ Adjust as needed

        public PlayersViewController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // ✅ LIST PLAYERS
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/list");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var players = JsonSerializer.Deserialize<List<PlayerDto>>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(players);
            }

            ViewBag.Message = "Error fetching players.";
            return View(new List<PlayerDto>());
        }
        // ✅ PLAYER DETAILS
        [Authorize]

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var player = JsonSerializer.Deserialize<PlayerDto>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(player);
            }

            return NotFound();
        }
[Authorize]

        // ✅ SHOW CREATE FORM
        public IActionResult Create()
        {
            return View();
        }

        // ✅ HANDLE CREATE FORM SUBMISSION
        [HttpPost]
        public async Task<IActionResult> Create(PlayerDto player)
        {
            if (!ModelState.IsValid) return View(player);

            var jsonData = JsonSerializer.Serialize(player);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/add", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Message = "Error creating player.";
            return View(player);
        }
[Authorize]

        // ✅ SHOW EDIT FORM
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var player = JsonSerializer.Deserialize<PlayerDto>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(player);
            }

            return NotFound();
        }

        // ✅ HANDLE EDIT FORM SUBMISSION
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PlayerDto player)
        {
            if (!ModelState.IsValid) return View(player);

            var jsonData = JsonSerializer.Serialize(player);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/update/{id}", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Message = "Error updating player.";
            return View(player);
        }
        [Authorize]


        // ✅ DELETE CONFIRMATION PAGE
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var player = JsonSerializer.Deserialize<PlayerDto>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(player);
            }

            return NotFound();
        }

        // ✅ HANDLE DELETE REQUEST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/delete/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Message = "Error deleting player.";
            return RedirectToAction("Index");
        }
    }
}
