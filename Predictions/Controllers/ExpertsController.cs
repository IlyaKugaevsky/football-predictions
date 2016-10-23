using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Predictions.DAL;
using Predictions.Models;
using Predictions.Services;
using Predictions.ViewModels;

namespace Predictions.Controllers
{
    public class ExpertsController : Controller
    {
        private readonly PredictionsContext _context;
        private readonly ExpertService _expertService;
        private readonly TourService _tourService;
        private readonly PredictionService _predictionService;
        private readonly MatchService _matchService;
        private readonly TeamService _teamService;
        public ExpertsController()
        {
            _context = new PredictionsContext();
            _expertService = new ExpertService(_context);
            _tourService = new TourService(_context);
            _predictionService = new PredictionService(_context);
            _matchService = new MatchService(_context);
            _teamService = new TeamService(_context);
        }


        public ActionResult Statistics()
        {
            return View();
        }

        // GET: Experts
        public ActionResult Index()
        {
            var tourList = _tourService.GenerateSelectList();
            var results = _predictionService.GenerateExpertInfo(2);
            var resultsTable = new ResultsTableViewModel(tourList, results);

            return View(resultsTable);
        }

        [HttpPost]
        public ActionResult GetResultsTable(int tourId)
        {
            return PartialView("ResultsTable", _predictionService.GenerateExpertInfo(tourId));
        }



        // GET: Experts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expert expert = _context.Experts.Find(id);
            if (expert == null)
            {
                return HttpNotFound();
            }
            return View(expert);
        }

        // GET: Experts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Experts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpertId,Nickname")] Expert expert)
        {
            if (ModelState.IsValid)
            {
                _context.Experts.Add(expert);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expert);
        }

        // GET: Experts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expert expert = _context.Experts.Find(id);
            if (expert == null)
            {
                return HttpNotFound();
            }
            return View(expert);
        }

        // POST: Experts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpertId,Nickname")] Expert expert)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(expert).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expert);
        }

        // GET: Experts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expert expert = _context.Experts.Find(id);
            if (expert == null)
            {
                return HttpNotFound();
            }
            return View(expert);
        }

        // POST: Experts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expert expert = _context.Experts.Find(id);
            _context.Experts.Remove(expert);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
