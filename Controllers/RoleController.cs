using DTS_WebApplication.Models;
using DTS_WebApplication.Repository.Contracts;
using DTS_WebApplication.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace DTS_WebApplication.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var entities = _roleRepository.GetAll();
            return View(entities);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var entity = _roleRepository.GetById(id);
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
        public IActionResult Create(Role role)
        {
            _roleRepository.Insert(role);
            return RedirectToAction("Index");
        }

        // Update
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var entity = _roleRepository.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Role role)
        {
            _roleRepository.Update(role);
            return RedirectToAction("Index");
        }

        // Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var entity = _roleRepository.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            _roleRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
