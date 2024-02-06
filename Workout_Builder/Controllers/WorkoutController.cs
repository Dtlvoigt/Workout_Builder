using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using Workout_Builder.Models;
using Workout_Builder.Properties;
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

        public IActionResult NewExercisePartial(int orderNum)
        {
            var newExercise = new Exercise()
            {
                Order = orderNum
            };
            return PartialView("_NewExercise", newExercise);
        }

        [HttpPost]
        public async Task<string> FillAutoCompleteExercises(string input, int order)
        {
            var exercises = await _workoutContext.AutofillExerciseTypes(input);
            StringBuilder sb = new StringBuilder();

            if(exercises != null && exercises.Count > 0) 
            {
                sb.Append("<select id=\"autoCompleteSelect_" + order + "\" size=\"5\">");

                foreach(var exercise in exercises)
                {
                    sb.Append("<option value=\"" + exercise.Id + "\">" + exercise.Name + "</option>");
                }

                sb.Append("</select>");
            }

            return sb.ToString();
        }

        [HttpPost]
        public IActionResult PartialTest(string[] strings)
        {
            return PartialView("_NewExercise", new Exercise());
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
