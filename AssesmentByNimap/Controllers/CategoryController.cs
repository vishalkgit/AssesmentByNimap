using AssesmentByNimap.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AssesmentByNimap.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IConfiguration configuration;
        CategoryCrud cd;

        public CategoryController(IConfiguration configuration)
        {
            this.configuration = configuration;
            cd = new CategoryCrud(this.configuration);
        }

        // GET: CategoryController
        public ActionResult Index()
        {
            var result = cd.GetAllCategories();
            return View(result);

        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var result = cd.GetCategoryById(id);
            return View(result);

        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                int result = cd.AddCategory(category);
                if (result >= 1)
                {

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMsg = "Something went wrong!!!";
                    return View();

                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }

        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var res = cd.GetCategoryById(id);
            return View(res);

        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            try
            {
                int res = cd.UpdateCategory(category);
                if (res >= 1)
                {

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMsg = "Something went wrong!!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }

        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var res = cd.DeleteCategory(id);
            return View(res);

        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int response = cd.DeleteCategory(id);
                if (response >= 1)
                {

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMsg = "Something went wrong";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }

        }
    }
}
