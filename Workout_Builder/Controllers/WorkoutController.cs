using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        private readonly IConfiguration _configuration;

        public WorkoutController(IWorkoutService context, ILogger<WorkoutController> logger, IConfiguration configuration)
        {
            _workoutContext = context;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //if the user isn't logged in, show them the login screen
            if (User.Identity != null && !User.Identity.IsAuthenticated)
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
            int maxExercises = _configuration.GetValue<int>("MaxNumExercises");
            int maxSets = _configuration.GetValue<int>("MaxNumSets");

            var newWorkoutVM = new NewWorkoutVM()
            {
                Workout = new Workout(),
                //Exercises = new List<Exercise>(),
                ExerciseModels = new List<NewExerciseViewModel>(),
                //NewExercise = new Exercise(),
                //AddExercise = false,
                NumExercises = 1,
                MaxNumExercises = maxExercises,
                MaxNumSets = maxSets,
            };

            var exerciseList = await _workoutContext.GetExerciseSelectList();
            for (int i = 0; i < maxExercises; i++)
            {
                var newModel = new NewExerciseViewModel()
                {
                    Name = "",
                    //Deleted = false,
                    Order = i,
                    Exercise = new Exercise()
                    {
                        NumSets = 1
                    },
                    ExerciseList = exerciseList,
                    MaxNumExercises = maxExercises,
                    SetsList = new List<Set>(),
                };

                for (int j = 0; j < maxSets; j++)
                {
                    newModel.SetsList.Add(new Set());
                }

                newWorkoutVM.ExerciseModels.Add(newModel);
            }

            return View(newWorkoutVM);
        }

        [HttpPost]
        public async Task<IActionResult> NewWorkout(NewWorkoutVM newWorkoutVM)
        {
            if (newWorkoutVM == null || newWorkoutVM.ExerciseModels == null)
            {
                throw new Exception("View Model is missing data");
            }

            //remove unused exercises
            //newWorkoutVM.ExerciseModels.RemoveAll(e => e.Order >= newWorkoutVM.NumExercises);
            //for (int i = newWorkoutVM.MaxNumExercises; i > newWorkoutVM.NumExercises; i--)
            //{
                
            //    newWorkoutVM.ExerciseModels.RemoveAt(i);
            //}
            //newWorkoutVM.Exercises.RemoveAll(e => e.NumSets == 0);

            //if the user added a new exercise, return the viewmodel
            //if (newWorkoutVM.AddExercise == true)
            //{
            //    //foreach (var exercise in  newWorkoutVM.Exercises)
            //    //{
            //    //    if (exercise.NumSets == 0)
            //    //    {
            //    //        newWorkoutVM.Exercises.Remove(exercise);
            //    //    }
            //    //}

            //    //add new exercise
            //    //newWorkoutVM.Exercise.Add(new Exercise()
            //    //{
            //    //    NumSets = 1,
            //    //});
            //    //newWorkoutVM.ExerciseModels.ForEach(async e => { e.ExerciseList = await _workoutContext.GetExerciseSelectList(); });
            //    foreach(var exerciseModel in newWorkoutVM.ExerciseModels)
            //    {
            //        exerciseModel.ExerciseList = await _workoutContext.GetExerciseSelectList().ConfigureAwait(false);
            //    }

            //    newWorkoutVM.ExerciseModels.Add(new NewExerciseViewModel()
            //    {
            //        Deleted = false,
            //        Exercise = new Exercise()
            //        {
            //            NumSets = 1
            //        },
            //        ExerciseList = await _workoutContext.GetExerciseSelectList(),
            //    });

            //    return View(newWorkoutVM);
            //}
            if (ModelState.IsValid)
            {
                if (newWorkoutVM.Workout == null)
                {
                    throw new Exception("Workout is null");
                }

                //check if any exercise names are empty
                for(int i = 0; i <= newWorkoutVM.NumExercises; i++)
                {
                    //if (String.IsNullOrEmpty(newWorkoutVM.ExerciseModels[i].Exercise.ExerciseType.Name))
                    //{
                    //        ModelState.AddModelError(nameof(newWorkoutVM.ExerciseModels[i].Exercise.ExerciseType.Name), "Exercise name required");
                    //}

                }

                //remove deleted exercises
                //newWorkoutVM.ExerciseModels.RemoveAll(e => e.Deleted == true);

                //create workout for current user
                var userID = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("UserId is null");
                newWorkoutVM.Workout.UserId = userID;

                //add workout to database
                int workoutID = await _workoutContext.AddWorkout(newWorkoutVM.Workout).ConfigureAwait(false);
                newWorkoutVM.ExerciseModels.ForEach(e => { e.Exercise.Workout.Id = workoutID; });

                return RedirectToAction("Index");
            }

            //remove placeholder names on exercises
            //newWorkoutVM.ExerciseModels.Select(e => e.Exercise.ExerciseType.Name).
            //                           .Where(n => n == "0").ToList()
            //                           .ForEach(n => n = "");

            //newWorkoutVM.ExerciseModels.Find(e => e.Exercise.ExerciseType.Name == "0").Exercise.ExerciseType.Name = "";
            //foreach(var exerciseModel in newWorkoutVM.ExerciseModels)
            //{
            //    if(exerciseModel.Exercise.ExerciseType.Name == "0")
            //    {
            //        exerciseModel.Exercise.ExerciseType.Name = "";
            //    }
            //}
            //ModelState.

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
        public async Task<string> FillAutoCompleteExercises(string input/*, int order*/)
        {
            var exercises = await _workoutContext.AutofillExerciseTypes(input);
            StringBuilder sb = new StringBuilder();

            if (exercises != null && exercises.Count > 0)
            {
                sb.Append("<select class=\"ExerciseNameSelectList\" size=\"5\">");

                foreach (var exercise in exercises)
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
