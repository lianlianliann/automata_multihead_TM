

using Microsoft.AspNetCore.Mvc;
using TM_MULTIHEAD_PHISHING_DETECTOR.Models;

namespace TM_MULTIHEAD_PHISHING_DETECTOR.Controllers

{
    public class AnalyzerController : Controller
    {
        [HttpGet]
        public IActionResult Analyze()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Analyze(string inputText )
        {
            if (string.IsNullOrWhiteSpace(inputText ))
            {
                return RedirectToAction("Index");
            }

            var engine = new MHTMEngine();
            var result = engine.Process(inputText );

            return View("~/Views/Home/Result.cshtml", result); // passes AnalysisResult to Result.cshtml
        }
    }
}
