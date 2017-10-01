using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using CorityCodingScenario;

/// <summary>
/// This namespace consists of one class that is meant to test the SplittingBill class within the CorityCodingScenario namespace
/// </summary>
namespace CorityCodingScenarioTest
{
    /// <summary>
    /// This test class is made to test different scenarios of the SplittingBill class
    /// </summary>
    [TestClass]
    public class SplittingBillTest
    {

        SplittingBill splittingBill;

        /// <summary>
        /// Every test method needs an object of SplittingBill, so it will be initialized here before every test method
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            splittingBill = new SplittingBill();
        }

        /// <summary>
        /// This test method will test to see if the SplitingBill class outputs the expected values
        /// This will cover every path within the SplittingBill class (aside from main)
        /// This tests both the ReadInput method and WriteOutput method within SplittingBill class
        /// This test passes when the code works as expected
        /// </summary>
        [TestMethod]
        public void StandardInputTest()
        {

            string fileName = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"Files\\expenses.txt");

            splittingBill.ReadInput(fileName);
            List<String> expectedOutput = new List<String>();
            List<String> actualOutput = ReadOutput(fileName);
            expectedOutput.Add("($1.99)");
            expectedOutput.Add("($8.01)");
            expectedOutput.Add("$10.01");
            expectedOutput.Add("");
            expectedOutput.Add("$0.98");
            expectedOutput.Add("($0.98)");

            CollectionAssert.AreEqual(expectedOutput, actualOutput);

        }

        /// <summary>
        /// Typically for a scenario such as this looping till EndOfStream would suffice
        /// However, as part of the requirements the last activity must be followed by a 0
        /// This test is assuming that the 0 is mandatory
        /// So, it will fail when the file does not have a 0 at the end, since in ReadInput the while loop can only stop at a 0
        /// This test passes if the code fails, since the code should not work under such conditions
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException), "Object reference not set to an instance of an object")]
        public void NoneZeroEndTest()
        {
            string fileName = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"Files\\none-zero.txt");

            splittingBill.ReadInput(fileName);
        }

        /// <summary>
        /// This test considers the possibility of there being a letter within the input file
        /// It tests the integer portions of the input file
        /// In case of something like this the code is expected to fail, since at some point a decimal will be attempted to be parsed as an int
        /// This test passes if the code fails, since the code should not work under such conditions
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.FormatException), "Input String was not in a correct format")]
        public void LetterAtIntegerTest()
        {
            string fileName = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"Files\\expenses-letter-integer.txt");

            splittingBill.ReadInput(fileName);
        }

        /// <summary>
        /// This test considers the possibility of there being a letter within the input file
        /// It tests the decimal portions of the input file
        /// In case of something like this the code is expected to fail, since at some point a decimal will be attempted to be parsed as an int
        /// This test passes if the code fails, since the code should not work under such conditions
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.FormatException), "Input String was not in a correct format")]
        public void LetterAtDecimalTest()
        {
            string fileName = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"Files\\expenses-letter-decimal.txt");

            splittingBill.ReadInput(fileName);
        }

        /// <summary>
        /// This test considers the possibility of the file containing inputs that are in the wrong order
        /// In case of something like this the code is expected to fail, since at some point a decimal will be attempted to be parsed as an int
        /// This test passes if the code fails, since the code should not work under such conditions
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.FormatException), "Input String was not in a correct format")]
        public void WrongOrderTest()
        {
            string fileName = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"Files\\expenses-wrong-order.txt");

            splittingBill.ReadInput(fileName);
        }

        /// <summary>
        /// This test considers the possibility of the number of people (n) value being wrong.
        /// In case of something like this the code is expected to fail, since at some point a decimal will be attempted to be parsed as an int
        /// This test passes if the code fails, since the code should not work under such conditions
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.FormatException), "Input String was not in a correct format")]
        public void WrongNValueTest()
        {
            string fileName = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"Files\\expenses-wrong-n.txt");

            splittingBill.ReadInput(fileName);
        }

        /// <summary>
        /// This test considers the possibility of the number of payments (p) value being wrong
        /// In case of something like this the code is expected to fail, since at some point a decimal will be attempted to be parsed as an int
        /// This test passes if the code fails, since the code should not work under such conditions
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.FormatException), "Input String was not in a correct format")]
        public void WrongPValueTest()
        {
            string fileName = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"Files\\expenses-wrong-n.txt");

            splittingBill.ReadInput(fileName);
        }

        /// <summary>
        /// This test considers the possibility of there being an empty line within the input file
        /// The code is designed to not have any empty lines
        /// In case of something like this the code is expected to fail, since at some point the code will attempt to parse for int or decimal
        /// This test passes if the code fails, since the code should not work under such conditions
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.FormatException), "Input String was not in a correct format")]
        public void EmptyLineTest()
        {
            string fileName = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, @"Files\\expenses-empty-line.txt");

            splittingBill.ReadInput(fileName);
        }


        /// <summary>
        /// This method reads the output file and makes a list of every line of string within the file
        /// </summary>
        /// <param name="fileName">the name of the file to be read</param>
        /// <returns>returns the list of every line of string within the file</returns>
        public List<String> ReadOutput(string fileName)
        {
            List<String> outputValues = new List<string>();
            string line;
            StreamReader file = new StreamReader(fileName + ".out");

            while (!file.EndOfStream)
            {
                line = file.ReadLine();
                outputValues.Add(line);
            }

            return outputValues;
        }
    }
}
