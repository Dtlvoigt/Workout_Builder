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
        public readonly int _maxExercises;
        public readonly int _maxSets;

        public WorkoutController(IWorkoutService context, ILogger<WorkoutController> logger, IConfiguration configuration)
        {
            _workoutContext = context;
            _logger = logger;
            _configuration = configuration;
            _maxExercises = _configuration.GetValue<int>("MaxNumExercises");
            _maxSets = _configuration.GetValue<int>("MaxNumSets");
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
            var newWorkoutVM = new WorkoutVM()
            {
                Workout = new Workout(),
                //Exercises = new List<Exercise>(),
                ExerciseModels = new List<ExerciseVM>(),
                //NewExercise = new Exercise(),
                //AddExercise = false,
                NumExercises = 1,
                MaxNumExercises = _maxExercises,
                MaxNumSets = _maxSets,
            };

            var exerciseList = await _workoutContext.GetExerciseSelectList();
            for (int i = 0; i < _maxExercises; i++)
            {
                var newModel = new ExerciseVM()
                {
                    Name = "",
                    Order = i,
                    NumSets = 1,
                    ExerciseList = exerciseList,
                    MaxNumExercises = _maxExercises,
                    SetsList = new List<Set>(),
                    CustomSets = false,
                };

                for (int j = 0; j < _maxSets; j++)
                {
                    newModel.SetsList.Add(new Set());
                }

                newWorkoutVM.ExerciseModels.Add(newModel);
            }

            return View(newWorkoutVM);
        }

        [HttpPost]
        public async Task<IActionResult> NewWorkout(WorkoutVM newWorkoutVM)
        {
            if (newWorkoutVM == null || newWorkoutVM.ExerciseModels == null)
            {
                throw new Exception("View Model is missing data");
            }

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
                int workoutID = await _workoutContext.AddWorkout(newWorkoutVM.Workout).ConfigureAwait(false);

                //add exercises to database
                for (int i = 0; i < newWorkoutVM.NumExercises; i++)
                {
                    //obtain exercise information
                    var model = newWorkoutVM.ExerciseModels[i];
                    var exerciseType = await _workoutContext.GetExerciseByName(model.Name);

                    //use custom name field if the entered exercise doesn't match any in the db
                    string customType = "";
                    if(exerciseType == null)
                    {
                        customType = model.Name;
                    }

                    //create json string that records individual sets info
                    string setsJsonString = "";
                    if(model.CustomSets)
                    {
                        setsJsonString = _workoutContext.CreateSetJsonString(model.SetsList);
                    }

                    //build new exercise
                    var newExercise = new Exercise()
                    {
                        Workout = newWorkoutVM.Workout,
                        Order = model.Order,
                        NumSets = model.NumSets,
                        MasterReps = model.MasterReps,
                        MasterWeight = model.MasterWeight,
                        ExerciseType = exerciseType,
                        CustomExerciseName = customType,
                        IsPounds = newWorkoutVM.IsPounds,
                        CustomSets = model.CustomSets,
                        SetsJsonString = setsJsonString,
                    };

                    int exerciseID = await _workoutContext.AddExercise(newExercise);
                }

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

        [HttpGet]
        [Route("Workout/EditWorkout/{workoutID:int}")]
        public async Task<IActionResult> EditWorkout(int workoutID)
        {

            var newWorkoutVM = new WorkoutVM()
            {
                Workout = new Workout(),
                ExerciseModels = new List<ExerciseVM>(),
                NumExercises = 1,
                MaxNumExercises = _maxExercises,
                MaxNumSets = _maxSets,
            };

            var exerciseList = await _workoutContext.GetExerciseSelectList();
            for (int i = 0; i < _maxExercises; i++)
            {
                var newModel = new ExerciseVM()
                {
                    Name = "",
                    Order = i,
                    NumSets = 1,
                    ExerciseList = exerciseList,
                    MaxNumExercises = _maxExercises,
                    SetsList = new List<Set>(),
                    CustomSets = false,
                };

                for (int j = 0; j < _maxSets; j++)
                {
                    newModel.SetsList.Add(new Set());
                }

                newWorkoutVM.ExerciseModels.Add(newModel);
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
