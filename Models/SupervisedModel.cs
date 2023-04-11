using Python.Runtime;
using System.IO;

public class ModelLoader
{
    public string Predict(double[] inputValues)
    {
        // Set the path to your pickled model file
        string modelPath = "../decision_tree_classifier.pkl";

        // Load the pickled model and get the prediction
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
