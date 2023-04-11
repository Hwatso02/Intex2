//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;

//namespace Intex2.Controllers
//{
    
//    [ApiController]
//    [Route("[controller]")]
//    public class PredictionController : ControllerBase
//    {
//        private readonly ILogger<PredictionController> _logger;

//        public PredictionController(ILogger<PredictionController> logger)
//        {
//            _logger = logger;
//        }

//        [HttpPost]
//        public void Predict()
//        {
//            // Initialize the model loader (you can also do this in Startup.cs or another initialization method)
//            // Create a new instance of the ModelPrediction class
//            var model = new ModelLoader();

//            // Set your input data
//            double[] inputValues = { 15, 5.5, 120, 7, 50, 80, 100, 1, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 };

//            // Get the predicted class
//            string prediction = model.Predict(inputValues);

//            Console.WriteLine(prediction);
//            // Return the prediction as a JSON response
         
//        }
//    }
//}

