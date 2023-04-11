using Intex2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Controllers
{
    public class HomeController : Controller
    {
        private readonly PredictionApiClient _predictionApiClient;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _predictionApiClient = new PredictionApiClient();
        }

        [HttpPost]
        public async Task<IActionResult> MakePrediction(DirectionPrediction dp)
        {
            int[] inputValues = new int[] { dp.squarenorthsouth,
                dp.depth,
                dp.southtohead,
                dp.squareeastwest,
                dp.westtohead,
                dp.westtofeet,
                dp.southtofeet,
                dp.northsouth_N,
                dp.eastwest_E,
                dp.eastwest_W,
                dp.wrapping_B,
                dp.wrapping_H,
                dp.wrapping_S,
                dp.wrapping_W,
                dp.samplescollected_false,
                dp.samplescollected_true,
                dp.area_NE,
                dp.area_NNW,
                dp.area_NW,
                dp.area_SE,
                dp.area_SW
            };
            //{ 15, 5, 120, 7, 50, 80, 100, 1, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 };
            string prediction = await _predictionApiClient.GetPredictionFromApiAsync(inputValues);

            // Process the prediction and return a view or JSON result
            // For example, pass the prediction to a view or return it as JSON
            return View("Predict", prediction);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Data()
        {
            return View();
        }

        public IActionResult GeneratePrediction()
        {
            return View("PredictionInfo");
        }

        [HttpGet]
        public IActionResult PredictionInfo()
        {
            return View();
        }
        
        private DirectionPrediction dirpred = new DirectionPrediction();

        //[HttpPost]
        //public IActionResult PredictionInfo(DirectionPrediction dp)
        //{

        //    // Parse the input values from the request body
        //double[] inputValues = { /*15, 5.5, 120, 7, 50, 80, 100, 1, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 };*/
        //    dp.squarenorthsouth,
        //        dp.depth,
        //        dp.southtohead,
        //        dp.squareeastwest,
        //        dp.westtohead,
        //        dp.westtofeet,
        //        dp.southtofeet,
        //        dp.northsouth_N,
        //        dp.eastwest_E,
        //        dp.eastwest_W,
        //        dp.wrapping_B,
        //        dp.wrapping_H,
        //        dp.wrapping_S,
        //        dp.wrapping_W,
        //        dp.samplescollected_false,
        //        dp.samplescollected_true,
        //        dp.area_NE,
        //        dp.area_NNW,
        //        dp.area_NW,
        //        dp.area_SE,
        //        dp.area_SW
        //    };

        //// Call the Predict method of your model
        //string prediction = dirpred.Predict(inputValues);

        //    // Return the prediction result to the client
        //    return View("Predict", prediction);
        //}


        //public async Task<IActionResult> MakePrediction()
        //{
        //    double[] inputValues = new double[] { 15, 5.5, 120, 7, 50, 80, 100, 1, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 };
        //    PredictionApiClient apiClient = new PredictionApiClient();
        //    double prediction = await apiClient.GetPredictionFromApiAsync(inputValues);

        //    // Process the prediction and return a view or JSON result
        //}


        //THIS IS A WORK IN PROGRESS
        //[HttpPost]
        //public void Predict()
        //{
        //    // Initialize the model loader (you can also do this in Startup.cs or another initialization method)
        //    // Create a new instance of the ModelPrediction class
        //    var model = new DirectionPrediction();

        //    // Set your input data
        //    double[] inputValues = { 15, 5.5, 120, 7, 50, 80, 100, 1, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 };

        //    // Get the predicted class
        //    string prediction = model.Predict(inputValues);

        //    Console.WriteLine(prediction);
        //    // Return the prediction as a JSON response

        //}

        public IActionResult Supervised()
        {
            return View();
        }

        public IActionResult Unsupervised()
        {
            return View();
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
