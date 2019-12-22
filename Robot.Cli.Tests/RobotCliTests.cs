using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot;
using Robot.Cli;

namespace Robot.Cli.Tests
{
    [TestClass]
    public class RobotCliTests
    {
        MemoryStream _inputStream;
        MemoryStream _outputStream;
        MemoryStream _errorStream;

        TextReader _inputReader;
        TextWriter _outputWriter;
        TextWriter _errorWriter;

        TextInterface _textInterface;
        Robot _robot;
        

        [TestCleanup]
        public void Cleanup()
        {
            _inputReader?.Dispose();
            _outputWriter?.Dispose();
            _errorWriter?.Dispose();
            _inputStream?.Dispose();
            _outputStream?.Dispose();
            _errorStream?.Dispose();
        }

        void Run(string input)
        {
            _inputStream = new MemoryStream(Encoding.ASCII.GetBytes(input));
            _outputStream = new MemoryStream();
            _errorStream = new MemoryStream();
            
            _inputReader = new StreamReader(_inputStream);
            _outputWriter = new StreamWriter(_outputStream);
            _errorWriter = new StreamWriter(_errorStream);

            _textInterface = new TextInterface(_inputReader, _outputWriter, _errorWriter);
            
            _robot = new Robot(new World(5, 5));

            _textInterface.Run(_robot);

            _outputWriter.Flush();
            _errorWriter.Flush();
            _outputStream.Flush();
            _errorStream.Flush();
        }

        string[] StreamToLines(MemoryStream stream)
        {
            var text = Encoding.ASCII.GetString(stream.ToArray());
            var lines = from line in Regex.Split(text, "\r\n|\r|\n")
                        where !string.IsNullOrWhiteSpace(line)
                        select line;
            return lines.ToArray();
        }

        string[] GetErrors() => StreamToLines(_errorStream);
        string[] GetOutput() => StreamToLines(_outputStream);

        [TestMethod]
        public void TestMoveBeforePlaceIsIgnored()
        {
            Run("MOVE\nREPORT");
            Assert.AreEqual(0, GetErrors().Length);
            Assert.AreEqual(0, GetOutput().Length);
        }

        [TestMethod]
        public void TestLeftBeforePlaceIsIgnored()
        {
            Run("LEFT\nREPORT");
            Assert.AreEqual(0, GetErrors().Length);
            Assert.AreEqual(0, GetOutput().Length);
        }

        [TestMethod]
        public void TestRightBeforePlaceIsIgnored()
        {
            Run("RIGHT\nREPORT");
            Assert.AreEqual(0, GetErrors().Length);
            Assert.AreEqual(0, GetOutput().Length);
        }

        [TestMethod]
        public void TestReportBeforePlaceIsIgnored()
        {
            Run("REPORT");
            Assert.AreEqual(0, GetErrors().Length);
            Assert.AreEqual(0, GetOutput().Length);
        }

        [TestMethod]
        public void TestPlaceWithInvalidX()
        {
            Run("PLACE A,1,North\nREPORT");
            var errors = GetErrors();
            Assert.AreEqual(1, errors.Length);
            Assert.AreEqual("Unrecognised command: PLACE A,1,North", errors[0]);
            Assert.AreEqual(0, GetOutput().Length);
        }

        [TestMethod]
        public void TestPlaceWithInvalidY()
        {
            Run("PLACE 1,B,North\nREPORT");
            var errors = GetErrors();
            Assert.AreEqual(1, errors.Length);
            Assert.AreEqual("Unrecognised command: PLACE 1,B,North", errors[0]);
            Assert.AreEqual(0, GetOutput().Length);
        }

        [TestMethod]
        public void TestPlaceWithInvalidDirection()
        {
            Run("PLACE 1,1,NorthEast\nREPORT");
            var errors = GetErrors();
            Assert.AreEqual(1, errors.Length);
            Assert.AreEqual("Unrecognised command: PLACE 1,1,NorthEast", errors[0]);
            Assert.AreEqual(0, GetOutput().Length);
        }

        [TestMethod]
        public void TestPlace()
        {
            Run("PLACE 1,1,North\nREPORT");
            Assert.AreEqual(0, GetErrors().Length);
            var output = GetOutput();
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual("X=1 Y=1 North", output[0]);
        }

        [TestMethod]
        public void TestInvalidPlace()
        {
            Run("PLACE 10,1,North\nREPORT");
            Assert.AreEqual(0, GetErrors().Length);
            Assert.AreEqual(0, GetOutput().Length);
        }

        [TestMethod]
        public void TestValidMove()
        {
            Run("PLACE 1,1,North\nMOVE\nREPORT");
            Assert.AreEqual(0, GetErrors().Length);
            var output = GetOutput();
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual("X=1 Y=2 North", output[0]);
        }     

        [TestMethod]
        public void TestInvalidMove()
        {
            Run("PLACE 1,0,South\nMOVE\nREPORT");
            Assert.AreEqual(0, GetErrors().Length);
            var output = GetOutput();
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual("X=1 Y=0 South", output[0]);
        }

        [TestMethod]
        public void TestLeft()
        {
            Run("PLACE 1,1,North\nLEFT\nREPORT");
            Assert.AreEqual(0, GetErrors().Length);
            var output = GetOutput();
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual("X=1 Y=1 West", output[0]);
        }  

        [TestMethod]
        public void TestRight()
        {
            Run("PLACE 1,1,North\nRIGHT\nREPORT");
            Assert.AreEqual(0, GetErrors().Length);
            var output = GetOutput();
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual("X=1 Y=1 East", output[0]);
        }          
    }
}
