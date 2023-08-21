using System.Diagnostics;
using FieldEdgeWebDeveloper.Models;
using Microsoft.AspNetCore.Mvc;

namespace FieldEdgeWebDeveloper.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly StudentClientService _httpClient;

        public StudentController(ILogger<StudentController> logger, StudentClientService studentClientService)
        {
            _logger = logger;
            _httpClient = studentClientService;            
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _httpClient.GetStudentsAsync());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CustomerModel customerModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Random rnd = new Random();
            int num = rnd.Next(101, 1000);
            customerModel.Id = (num).ToString();
            HttpResponseMessage response = await _httpClient.CreateStudentsAsync(customerModel);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            CustomerModel customers;
            customers = await _httpClient.GetStudentsByIdAsync(id);
            if (customers != null)
            {
                return View(customers);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerModel customerModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            HttpResponseMessage response = await _httpClient.UpdateStudentsAsync(customerModel?.Id, customerModel);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteStudentsAsync(id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}