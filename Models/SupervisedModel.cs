using Python.Runtime;
using System;
using System.IO;

namespace Intex2.Models
{

    public class DirectionPrediction
    {
        public int squarenorthsouth { get; set; }
        public int depth { get; set; }
        public int southtohead { get; set; }
        public int squareeastwest { get; set; }
        public int westtohead { get; set; }
        public int westtofeet { get; set; }
        public int southtofeet { get; set; }
        public int northsouth_N { get; set; }
        public int eastwest_E { get; set; }
        public int eastwest_W { get; set; }
        public int wrapping_B { get; set; }
        public int wrapping_H { get; set; }
        public int wrapping_S { get; set; }
        public int wrapping_W { get; set; }
        public int samplescollected_false { get; set; }
        public int samplescollected_true { get; set; }
        public int area_NE { get; set; }
        public int area_NNW { get; set; }
        public int area_NW { get; set; }
        public int area_SE { get; set; }
        public int area_SW { get; set; }






        
        
        
        public string Predict(int[] inputValues)
        {
            // Set the path to your pickled model file
            string modelPath = "../decision_tree_classifier.pkl";

            string pythonDllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "venv", "python311.dll");
            Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", pythonDllPath);



            // Load the pickled model and get the prediction
            PythonEngine.PythonHome = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "venv");

            using (Py.GIL())
            {
                dynamic sys = Py.Import("sys");
                dynamic pickle = Py.Import("pickle");
                dynamic numpy = Py.Import("numpy");
                dynamic sklearn = Py.Import("sklearn");

                // Add the directory containing your pickled model file to the Python path
                sys.path.append(Path.GetDirectoryName(modelPath));

                // Load the pickled model
                // Read the contents of the pickled model file into a byte array
                byte[] modelBytes = File.ReadAllBytes(modelPath);

                // Deserialize the pickled model
                dynamic model = pickle.loads(modelBytes);

                // Convert the input data to a NumPy array
                dynamic inputData = numpy.array(inputValues).reshape(1, -1);

                // Get the prediction from the model
                dynamic prediction = model.predict(inputData);

                // Convert the prediction to a string and return it
                return prediction[0].ToString();
            }
        }

    }
}
