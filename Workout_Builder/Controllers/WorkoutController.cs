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
        private readonly IWorkoutService _workoutContext;
        private readonly ILogger<WorkoutController> _logger;

        public WorkoutController(IWorkoutService data, ILogger<WorkoutController> logger)
        {
            _workoutContext = data;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //if the user isn't logged in, show them the login screen
            if(User.Identity != null && !User.Identity.IsAuthenticated)
            {
                return Redirect("Identity/Account/Login");
            }

            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("userID is null");
            var templates = await _workoutContext.GetUserTemplates(userID).ConfigureAwait(false);
            var homeVM = new HomeViewModel()
            {
                WorkoutTemplates = templates,
            };
            return View(homeVM);
        }

        [HttpGet]
        public async Task<IActionResult> NewWorkout()
        {
            var newWorkoutVM = new NewWorkoutVM()
            {
                Workout = new Workout()
            };

            return View(newWorkoutVM);
        }

        [HttpPost]
        public async Task<IActionResult> NewWorkout(NewWorkoutVM newWorkoutVM)
        {
            if (ModelState.IsValid)
            {
                if (newWorkoutVM.Workout == null)
                {
                    throw new Exception("Workout is null");
                }

                //create workout for current user
                var userID = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("UserId is null");
                newWorkoutVM.Workout.UserId = userID;

                //add workout to database
                await _workoutContext.AddWorkout(newWorkoutVM.Workout).ConfigureAwait(false);

                return RedirectToAction("Index");
            }

            return View(newWorkoutVM);
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
