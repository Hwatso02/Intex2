using Intex2.Models;
using Intex2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly egyptContext _context;
        private readonly PredictionApiClient _predictionApiClient;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, egyptContext context)
        {
            _logger = logger;
            _predictionApiClient = new PredictionApiClient();
            _context = context;
        }

        public IActionResult TestConnection()
        {
            string testQuery = "SELECT 1";
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = testQuery;
                    _context.Database.OpenConnection();
                    using (var result = command.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            ViewData["TestResult"] = "Connection successful!";
                        }
                        else
                        {
                            ViewData["TestResult"] = "Connection failed: No results.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewData["TestResult"] = $"Connection failed: {ex.Message}";
            }
            finally
            {
                _context.Database.CloseConnection();
            }

            return View();
        }
        [HttpPost]
        public IActionResult MoreInfo(long itemId)
        {
            // Retrieve the specific record using the id (Replace "dbContext" with the name of your database context variable)
            var burial = _context.BurialMain.FirstOrDefault(b => b.Id == itemId);

            // Check if the record exists
            if (burial == null)
            {
                return NotFound(); // Return a 404 Not Found response if the record doesn't exist
            }

            // Create an instance of the MoreInfoViewModel and populate it with the data from the burial object
            MoreInfoViewModel viewModel = new MoreInfoViewModel
            {
                Id = burial.Id,
                Sex = burial.Sex,
                Depth = burial.Depth,
                AgeAtDeath = burial.Ageatdeath,
                // Populate other properties as needed from the burial object
            };

            // Pass the viewModel to the MoreInfo view
            return View(viewModel);
        }

        //[HttpGet]
        //public IActionResult MoreInfo(long itemId)
        //{
        //    var viewModel = new MoreInfoViewModel();

        //    // Get the burial and textile info based on the provided itemId


        //    var allBurialsWithInfo = (from b in _context.BurialMain
        //                              join bt in _context.BurialmainTextile on b.Id equals bt.MainBurialmainid into b_bt
        //                              from bt in b_bt.DefaultIfEmpty()
        //                              join tft in _context.TextilefunctionTextile on bt.MainTextileid equals tft.MainTextileid into bt_tft
        //                              from tft in bt_tft.DefaultIfEmpty()
        //                              join tf in _context.Textilefunction on tft.MainTextilefunctionid equals tf.Id into tft_tf
        //                              from tf in tft_tf.DefaultIfEmpty()
        //                              join ct in _context.ColorTextile on bt.MainTextileid equals ct.MainTextileid into bt_ct
        //                              from ct in bt_ct.DefaultIfEmpty()
        //                              join cl in _context.Color on ct.MainColorid equals cl.Id into ct_cl
        //                              from cl in ct_cl.DefaultIfEmpty()
        //                              join st in _context.StructureTextile on bt.MainTextileid equals st.MainTextileid into bt_st
        //                              from st in bt_st.DefaultIfEmpty()
        //                              join s in _context.Structure on st.MainStructureid equals s.Id into st_s
        //                              from s in st_s.DefaultIfEmpty()
        //                              select new TestBurialsViewModel
        //                              {
        //                                  Id = b.Id,
        //                                  Sex = b.Sex,
        //                                  Depth = b.Depth,
        //                                  AgeAtDeath = b.Ageatdeath,
        //                                  TextileFunction = tf.Value,
        //                                  Color = cl.Value,
        //                                  TextileStructure = s.Value
        //                              });
        //    //var burialsWithInfo = from b in _context.BurialMain
        //    //                      join bt in _context.BurialmainTextile on b.Id equals bt.MainBurialmainid into b_bt
        //    //                      from bt in b_bt.DefaultIfEmpty()
        //    //                      join ymt in _context.YarnmanipulationTextile on bt.MainTextileid equals ymt.MainTextileid into bt_ymt
        //    //                      from ymt in bt_ymt.DefaultIfEmpty()
        //    //                      join ym in _context.Yarnmanipulation on ymt.MainYarnmanipulationid equals ym.Id into ymt_ym
        //    //                      from ym in ymt_ym.DefaultIfEmpty()
        //    //                      where b.Id == itemId
        //    //                      select new MoreInfoViewModel
        //    //                      {
        //    //                          Id = b.Id,
        //    //                          Component = ym.Component,
        //    //                          Material = ym.Material,
        //    //                          Direction = ym.Direction,
        //    //                          Count = ym.Count,
        //    //                          Ply = ym.Ply,
        //    //                          Depth = b.Depth,
        //    //                          Sex = b.Sex,
        //    //                          AgeAtDeath = b.Ageatdeath,
        //    //                          Color = b.Haircolor,
        //    //                          TextileStructure = bt.Structure,
        //    //                          TextileFunction = bt.Textilefunction,
        //    //                          HairColor = b.Haircolor,
        //    //                          HeadDirection = b.Headdirection,
        //    //                      };

        //    viewModel.BurialInfo = allBurialsWithInfo.SingleOrDefault();

        //    return View("MoreInfo", viewModel);
        //}

        //[HttpPost]
        //public IActionResult MoreInfo(int id)
        //{
        //    // Retrieve the specific record using the id (Replace "dbContext" with the name of your database context variable)
        //    var burial = _context.BurialMain.FirstOrDefault(b => b.Id == id);

           

        //    // Pass the retrieved record to the MoreInfo view
        //    return View(burial);
        //}

        ////[HttpPost]
        //public IActionResult MoreInfo(long itemId)
        //{
        //    var viewModel = new MoreInfoViewModel();

        //    var burialsWithInfo = from b in _context.BurialMain
        //                          join bt in _context.BurialmainTextile on b.Id equals bt.MainBurialmainid into b_bt
        //                          from bt in b_bt.DefaultIfEmpty()
        //                          join ymt in _context.YarnmanipulationTextile on bt.MainTextileid equals ymt.MainTextileid into bt_ymt
        //                          from ymt in bt_ymt.DefaultIfEmpty()
        //                          join ym in _context.Yarnmanipulation on ymt.MainYarnmanipulationid equals ym.Id into ymt_ym
        //                          from ym in ymt_ym.DefaultIfEmpty()
        //                          join st in _context.StructureTextile on bt.MainTextileid equals st.MainTextileid into bt_st
        //                          from st in bt_st.DefaultIfEmpty()
        //                          join s in _context.Structure on st.MainStructureid equals s.Id into st_s
        //                          from s in st_s.DefaultIfEmpty()
        //                          join tft in _context.TextilefunctionTextile on bt.MainTextileid equals tft.MainTextileid into bt_tft
        //                          from tft in bt_tft.DefaultIfEmpty()
        //                          join tf in _context.Textilefunction on tft.MainTextilefunctionid equals tf.Id into tft_tf
        //                          from tf in tft_tf.DefaultIfEmpty()
        //                          where b.Id == itemId
        //                          select new MoreInfoViewModel
        //                          {
        //                              Id = b.Id,
        //                              Component = ym.Component,
        //                              Material = ym.Material,
        //                              Direction = ym.Direction,
        //                              Count = ym.Count,
        //                              Ply = ym.Ply,
        //                              Depth = b.Depth,
        //                              Sex = b.Sex,
        //                              AgeAtDeath = b.Ageatdeath,
        //                              Color = b.Haircolor,
        //                              TextileStructure = s.Value,
        //                              TextileFunction = tf.Value,
        //                              HairColor = b.Haircolor,
        //                              HeadDirection = b.Headdirection,
        //                          };

        //    viewModel.BurialInfo = burialsWithInfo.SingleOrDefault();

        //    return View("MoreInfo", viewModel);
        //}


        public IActionResult DisplayBurials(int pageNum = 1)
        {
            int pageSize = 15;

            var viewModel = new BurialsViewModel
            {
                Burialmains = _context.BurialMain
                    //.OrderBy(b => b.Burialid)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),

                PageInfo = new PageInfo
                {
                    TotalNumBurials = _context.BurialMain.Count(),
                    BurialsPerPage = pageSize,
                    CurrentPage = pageNum
                },

                Bodyanalysischarts = _context.BodyAnalysisChart
                    //.OrderBy(b => b.Id)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .ToList()
            };

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult GetTestDisplayBurials(int pageNum = 1, bool showOnlyWithColor = false, string sexFilter = null, string textileFunctionFilter = null, string textileStructureFilter = null, string showDepthNotNull = null, string showAgeAtDeathNotNull = null, string showSexNotNull = null, string showIdNotNull = null)
        {
            int pageSize = 15;
            var viewModel = new TestBurialsPageViewModel();

            var allBurialsWithInfo = (from b in _context.BurialMain
                                        join bt in _context.BurialmainTextile on b.Id equals bt.MainBurialmainid into b_bt
                                        from bt in b_bt.DefaultIfEmpty()
                                        join tft in _context.TextilefunctionTextile on bt.MainTextileid equals tft.MainTextileid into bt_tft
                                        from tft in bt_tft.DefaultIfEmpty()
                                        join tf in _context.Textilefunction on tft.MainTextilefunctionid equals tf.Id into tft_tf
                                        from tf in tft_tf.DefaultIfEmpty()
                                        join ct in _context.ColorTextile on bt.MainTextileid equals ct.MainTextileid into bt_ct
                                        from ct in bt_ct.DefaultIfEmpty()
                                        join cl in _context.Color on ct.MainColorid equals cl.Id into ct_cl
                                        from cl in ct_cl.DefaultIfEmpty()
                                        join st in _context.StructureTextile on bt.MainTextileid equals st.MainTextileid into bt_st
                                        from st in bt_st.DefaultIfEmpty()
                                        join s in _context.Structure on st.MainStructureid equals s.Id into st_s
                                        from s in st_s.DefaultIfEmpty()
                                        select new TestBurialsViewModel
                                        {
                                            Id = b.Id,
                                            Sex = b.Sex,
                                            Depth = b.Depth,
                                            AgeAtDeath = b.Ageatdeath,
                                            TextileFunction = tf.Value,
                                            Color = cl.Value,
                                            TextileStructure = s.Value
                                        });

            if (showOnlyWithColor)
            {
                allBurialsWithInfo = allBurialsWithInfo.Where(b => b.Color != null).OrderBy(b => b.Color);

            }


            if (!string.IsNullOrEmpty(sexFilter))
            {
                allBurialsWithInfo = allBurialsWithInfo.Where(b => b.Sex == sexFilter).OrderBy(b => b.Sex);
            }

            if (!string.IsNullOrEmpty(textileFunctionFilter))
            {
                allBurialsWithInfo = allBurialsWithInfo.Where(b => b.TextileFunction != null).OrderBy(b => b.TextileFunction);
            }

            if (!string.IsNullOrEmpty(textileStructureFilter))
            {
                allBurialsWithInfo = allBurialsWithInfo.Where(b => b.TextileStructure != null).OrderBy(b => b.TextileStructure);
            }
            if (!string.IsNullOrEmpty(showDepthNotNull))
            {
                allBurialsWithInfo = allBurialsWithInfo.Where(b => b.Depth != null).OrderByDescending(b => b.Depth);
            }

            if (!string.IsNullOrEmpty(showAgeAtDeathNotNull))
            {
                allBurialsWithInfo = allBurialsWithInfo.Where(b => b.AgeAtDeath != null).OrderByDescending(b => b.AgeAtDeath);
            }


            viewModel.TestBurials = allBurialsWithInfo.Skip((pageNum - 1) * pageSize)
                                                 .Take(pageSize)
                                                 .ToList();
            
            viewModel.PageInfo = new PageInfo
            {
                TotalNumBurials = allBurialsWithInfo.Count(),
                BurialsPerPage = pageSize,
                CurrentPage = pageNum
            };

            return View("TestDisplayBurials", viewModel);
        }


        [HttpPost]
        public IActionResult TestDisplayBurials(int pageNum = 1, bool showOnlyWithColor = false, string sexFilter = null, string textileFunctionFilter = null, string textileStructureFilter = null, string showDepthNotNull = null, string showAgeAtDeathNotNull = null, string showSexNotNull = null)
        {
            return RedirectToAction("GetTestDisplayBurials", new { pageNum, showOnlyWithColor, sexFilter, textileFunctionFilter, textileStructureFilter, showDepthNotNull, showAgeAtDeathNotNull, showSexNotNull  });
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

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Roles()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Crud Functions
        //Add
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(BurialMain burial)
        {
            _context.BurialMain.Add(burial);
            _context.SaveChanges();
            return View("Confirmation", burial);
        }
        //Edit
        [HttpGet]
        public IActionResult Edit(int recordid)
        {
            var record = _context.BurialMain.Single(m => m.Id == recordid);

            return View("Add", record);
        }
        [HttpPost]
        public IActionResult Edit(BurialMain burial)
        {
            _context.BurialMain.Update(burial);
            //_context.BurialMain.SaveChanges();

            return RedirectToAction("DisplayBurials");
        }
    }
}
