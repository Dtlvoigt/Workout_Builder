using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Workout_Builder.Models;
using Workout_Builder.Services;
using Workout_Builder.ViewModels;

namespace Workout_Builder.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly IWorkoutService _data;
        private readonly ILogger<WorkoutController> _logger;

        public WorkoutController(IWorkoutService data, ILogger<WorkoutController> logger)
        {
            _data = data;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("User is null");
            var templates = await _data.GetWorkoutTemplates(userID).ConfigureAwait(false);
            var homeVM = new HomeViewModel()
            {
                WorkoutTemplates = templates,
            };
            return View(homeVM);
        }

        [HttpGet]
        public async Task<IActionResult> NewWorkout()
        {
            var newWorkout = new Workout();
            newWorkout.IsTemplate = true;
            var createVM = new FormViewModel()
            {
                Workout = newWorkout
            };

            return View(createVM);
        }

        [HttpPost]
        public async Task<IActionResult> NewWorkout(FormViewModel createVM)
        {
            //add workout to database

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
