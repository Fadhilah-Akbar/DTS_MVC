using DTS_WebApplication.Models;
using DTS_WebApplication.Repository.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DTS_WebApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var entities = _employeeRepository.GetAll();
            return View(entities);
        }
        [HttpGet]
        public IActionResult Details(string id)
        {
            var entity = _employeeRepository.GetById(id);
            return View(entity);
        }

        // Insert
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            _employeeRepository.Insert(employee);
            return RedirectToAction("Index");
        }

        // Update
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var entity = _employeeRepository.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
            _employeeRepository.Update(employee);
            return RedirectToAction("Index");
        }

        // Delete
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var entity = _employeeRepository.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(string id)
        {
            _employeeRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
