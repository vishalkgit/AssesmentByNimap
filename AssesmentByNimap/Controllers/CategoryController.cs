using AssesmentByNimap.Models;
using AssesmentByNimap.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AssesmentByNimap.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IConfiguration configuration;
        private readonly ICategoryService categoryService;
        CategoryCrud cd;

        public CategoryController(IConfiguration configuration, ICategoryService categoryService)
        {
            this.configuration = configuration;
            this.categoryService = categoryService; 
            cd = new CategoryCrud(this.configuration);
        }

        // GET: CategoryController
        public ActionResult Index(int pg = 1)
        {
            var result = categoryService.GetAllCategories();

            const int pagesize = 10;
            if (pg < 1)
            {
                pg = 1;
            }

            int recscount = result.Count();

            var pager = new Pager(recscount, pg, pagesize);

            int recskip = (pg - 1) * pagesize;

            var data = result.Skip(recskip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;


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
                int result = categoryService.AddCategory(category);
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
                int res = categoryService.UpdateCategory(category);
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
            var res = categoryService.GetCategoryById(id);
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
                int response = categoryService.DeleteCategory(id);
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
