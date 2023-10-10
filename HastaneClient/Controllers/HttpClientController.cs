using HastaneClient.Models.VMs;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace HastaneClient.Controllers
{
    public class HttpClientController : Controller
    {
        HttpClient _httpClient;
        public HttpClientController()
        {
            _httpClient = new HttpClient();
        }
        private string apiUrl = "https://localhost:7139/api/Hastanes/";
        public IActionResult Index()
        {
            List<HastaneListVM> hastanes = _httpClient.GetFromJsonAsync<List<HastaneListVM>>(apiUrl+ "GetHastanes").Result;   
            if (hastanes is not null)
            {
                return View(hastanes);
            }
            ViewData["info"] = "Görülecek hastane yok";
            return View("Index","Home");
        }
        public IActionResult GetById(int id)
        {
            HastaneListVM hastane = _httpClient.GetFromJsonAsync<HastaneListVM>(apiUrl + "GetHastanes/"+id).Result;
            return View(hastane);
        }
        public IActionResult Create(HastaneCreateVM hastaneCreateVM)
        {
            hastaneCreateVM.HastaneAd = "asdsad";
            hastaneCreateVM.Adres = "Rize";
            var result = _httpClient.PostAsJsonAsync<HastaneCreateVM>(apiUrl + "Create",hastaneCreateVM).Result;
            if (result.IsSuccessStatusCode)
            {
                return View(hastaneCreateVM);
            }
            return View();           
        }
        public IActionResult Update(HastaneListVM hastaneVM)
        {
            hastaneVM.HastaneId = 4;
            hastaneVM.HastaneAd = "asdsad";
            hastaneVM.Adres = "Rize";
            var result = _httpClient.PutAsJsonAsync(apiUrl + "Update", hastaneVM).Result;
            if (result.IsSuccessStatusCode)
            {
                return View(hastaneVM);
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            var result = _httpClient.DeleteAsync(apiUrl + "Delete/" + id);
            if (result.IsCompletedSuccessfully)
            {

            }
            return View();
        }
    }
}
