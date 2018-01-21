using System;
using System.IO;
using Accord.Statistics.Models.Fields;
using Accord.Statistics.Models.Fields.Functions;
using Accord.Statistics.Models.Fields.Learning;

namespace crf
{
    class Program
    {
        static void Main(string[] args)
        {
            // Suppose we would like to learn how to classify the
            // following set of sequences among three class labels:

            int[][] inputSequences =
            {
    // First class of sequences: starts and
    // ends with zeros, ones in the middle:
    new[] { 0, 1, 1, 1, 0 },
    new[] { 0, 0, 1, 1, 0, 0 },
    new[] { 0, 1, 1, 1, 1, 0 },

    // Second class of sequences: starts with
    // twos and switches to ones until the end.
    new[] { 2, 2, 2, 2, 1, 1, 1, 1, 1 },
    new[] { 2, 2, 1, 2, 1, 1, 1, 1, 1 },
    new[] { 2, 2, 2, 2, 2, 1, 1, 1, 1 },

    // Third class of sequences: can start
    // with any symbols, but ends with three.
    new[] { 0, 0, 1, 1, 3, 3, 3, 3 },
    new[] { 0, 0, 0, 3, 3, 3, 3 },
    new[] { 1, 0, 1, 2, 2, 2, 3, 3 },
    new[] { 1, 1, 2, 3, 3, 3, 3 },
    new[] { 0, 0, 1, 1, 3, 3, 3, 3 },
    new[] { 2, 2, 0, 3, 3, 3, 3 },
    new[] { 1, 0, 1, 2, 3, 3, 3, 3 },
    new[] { 1, 1, 2, 3, 3, 3, 3 },
};

            // Now consider their respective class labels
            int[] outputLabels =
            {
    /* Sequences  1-3 are from class 0: */ 0, 0, 0,
    /* Sequences  4-6 are from class 1: */ 1, 1, 1,
    /* Sequences 7-14 are from class 2: */ 2, 2, 2, 2, 2, 2, 2, 2
};


            HiddenConditionalRandomField<int> classifier = null;

            var classifierFilename = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            if (classifierFilename != null)
            {
                classifierFilename = Path.Combine(classifierFilename, "hcrf.txt");

                if (File.Exists(classifierFilename))
                {
                    classifier = HiddenConditionalRandomField<int>.Load(classifierFilename);
                }
                else
                {
                    // Create the Hidden Conditional Random Field using a set of discrete features
                    var function = new MarkovDiscreteFunction(states: 3, symbols: 4, outputClasses: 3);
                    classifier = new HiddenConditionalRandomField<int>(function);
                    // Create a learning algorithm
                    var teacher = new HiddenResilientGradientLearning<int>(classifier)
                    {
                        Iterations = 50
                    };

                    // Run the algorithm and learn the models
                    //teacher.Run(inputSequences, outputLabels);
                    teacher.Learn(inputSequences, outputLabels);

                    classifier.Save(classifierFilename);
                }
            }

            // After training has finished, we can check the
            // output classificaton label for some sequences.

            if (classifier != null)
            {
                int y1 = classifier.Compute(new[] { 0, 1, 1, 1, 0 }); // output is y1 = 0

                int y2 = classifier.Compute(new[] { 0, 0, 1, 1, 0, 0 }); // output is y1 = 0

                int y3 = classifier.Compute(new[] { 2, 2, 2, 2, 1, 1 }); // output is y2 = 1
                int y4 = classifier.Compute(new[] { 2, 2, 1, 1 });       // output is y2 = 1

                int y5 = classifier.Compute(new[] { 0, 0, 1, 3, 3, 3 }); // output is y3 = 2
                int y6 = classifier.Compute(new[] { 2, 0, 2, 2, 3, 3 }); // output is y3 = 2
            }
        }
    }
}
