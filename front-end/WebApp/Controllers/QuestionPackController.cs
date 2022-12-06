using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.BusinessLogic;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class QuestionPackController : Controller
    {
        private QuestionPackLogic _questionPackLogic;

        public QuestionPackController()
        {
            _questionPackLogic = new QuestionPackLogic();
        }

        // GET: QuestionPackController
        public async Task<IActionResult> Index()
        {
            List<QuestionPack>? questionPacks = await _questionPackLogic.GetAllQuestionPacks();

            return View(questionPacks);
        }

        // GET: QuestionPackController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QuestionPackController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuestionPackController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionPack questionPack)
        {
            Console.WriteLine(questionPack);
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestionPackController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuestionPackController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestionPackController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuestionPackController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
