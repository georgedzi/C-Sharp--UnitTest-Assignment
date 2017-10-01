using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

/// <summary>
/// This namespace consists of one class that is meant to take a file, and calculate total split cost of an activity
/// </summary>
namespace CorityCodingScenario
{
    /// <summary>
    /// This class is where the Main method is located. The code execution starts with this class
    /// It consists of two other methods aside from the Main method (ReadInput, WriteOutput)
    /// ReadInput reads the content of a txt file, and calculates bill splitting  according to the amount spent on a bill
    /// WriteInput writes the content provided in ReadInput to a txt.out file
    /// </summary>
    public class SplittingBill
    {
        /// <summary>
        /// Initiates the code by passing the txt file directory to the ReadInput method within the object "start"
        /// "start" is an object of this class  
        /// </summary>
        /// <param name="args">For passing command line parameters</param>
        static void Main(string[] args)
        {
            string fileName = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"Files\\expenses.txt");
            SplittingBill start = new SplittingBill();
            start.ReadInput(fileName);
        }

        /// <summary>
        /// This method takes a txt file with groups expense information
        /// It calculates how much one has spent on an activity, then determines if someone is owed money or owes money
        /// These results are achieved by taking the total spending money, dividing it by the amount of people in the activity, then subtracting the divided amount by the persons spending amount
        /// </summary>
        /// <param name="fileName">The directory path of the file that contains the bill spliting information</param>
        public void ReadInput(string fileName)
        {
            string line;

            List<List<decimal>> sectionTotals = new List<List<decimal>>();


            StreamReader file = new StreamReader(fileName);


            while (!(line = file.ReadLine()).Equals("0"))
            {
                int numOfPeople = Int32.Parse(line);
                decimal paymentTotal = 0;
                List<decimal> personTotals = new List<decimal>();

                for (int i = 0; i < numOfPeople; i++)
                {

                    int numOfPayments = Int32.Parse(file.ReadLine());

                    personTotals.Add(0);

                    for (int j = 0; j < numOfPayments; j++)
                    {
                        decimal payValue = decimal.Parse(file.ReadLine());
                        payValue = Math.Round(payValue, 2);
                        personTotals[i] += payValue;
                        paymentTotal += payValue;
                    }
                }

                for (int k = 0; k < personTotals.Count(); k++)
                {
                    personTotals[k] = (paymentTotal / numOfPeople) - personTotals[k];
                }

                sectionTotals.Add(personTotals);

            }
            WriteOutput(sectionTotals, fileName);

        }

        /// <summary>
        /// This method takes any information provided from ReadInput and outputs it to a txt.out file
        /// Negative amounts are surrounded in brackets, while positive ones are not
        /// If an amount is positive it means that person owes the group money
        /// If an amount is negative it means that person is owed money
        /// </summary>
        /// <param name="sectionTotals">A list of a List of decimals. This contains information on how much one owes the group, or how much one is owed</param>
        /// <param name="fileName">The directory path of the file that contains the bill splitting information</param>
        public void WriteOutput(List<List<decimal>> sectionTotals, string fileName)
        {

            if (File.Exists((fileName + ".out")))
            {
                File.Delete((fileName + ".out"));
            }
            List<String> fileOutput = new List<String>();
            int lineCounter = 0;

            for (int i = 0; i < sectionTotals.Count(); i++)
            {

                for (int j = 0; j < sectionTotals[i].Count; j++)
                {
                    if (sectionTotals[i][j] < 0)
                    {
                        sectionTotals[i][j] *= -1;
                        fileOutput.Add("($");
                        fileOutput[lineCounter] += Math.Round(sectionTotals[i][j], 2, MidpointRounding.AwayFromZero) + ")";
                        lineCounter++;
                        continue;
                    }

                  else
                    {
                        fileOutput.Add("$");
                        fileOutput[lineCounter] += Math.Round(sectionTotals[i][j], 2, MidpointRounding.AwayFromZero);
                        lineCounter++;
                        continue;
                    }
                    
                }

                if (i != sectionTotals.Count() - 1)
                {
                    fileOutput.Add("");
                    lineCounter++;
                }

            }


            using (StreamWriter outputFile = new StreamWriter(fileName + ".out", true))
            {

                for (int i = 0; i < fileOutput.Count(); i++)
                {
                    outputFile.WriteLine(fileOutput[i]);
                }
            }

        }

       
    }
}
