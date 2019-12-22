using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Robot.Cli
{
    // PLACE X,Y,F
    // MOVE
    // LEFT
    // RIGHT
    // REPORT
    public class TextInterface
    {
        readonly TextReader _inputReader;
        readonly TextWriter _outputWriter;
        readonly TextWriter _errorWriter;
      
        public TextInterface(TextReader inputReader, TextWriter outputWriter, TextWriter errorWriter)
        {
            _inputReader = inputReader;
            _outputWriter = outputWriter;
            _errorWriter = errorWriter;
        }

        public void Run(Robot robot)
        {
            while (true) {
                var line = _inputReader.ReadLine();
                if (line is null) {
                    break;
                }
                ProcessCommand(line, robot);
            }
        }

        static readonly Regex PlaceRegex = new Regex(@"PLACE (\d+),(\d+),(NORTH|SOUTH|EAST|WEST)$", RegexOptions.IgnoreCase);

        void ProcessCommand(string rawLine, Robot robot)
        {
            var line = rawLine.Trim().ToUpper();

            if (line == "MOVE") {
                robot.Move();
                return;
            }

            if (line == "LEFT") {
                robot.Left();
                return;
            }

            if (line == "RIGHT") {
                robot.Right();
                return;
            }

            if (line == "REPORT") {
                if (robot.Report() is Position position) {
                    _outputWriter.WriteLine(position);
                }
                return;    
            }

            var match = PlaceRegex.Match(line);

            if (!match.Success) {
                _errorWriter.WriteLine("Unrecognised command: " + rawLine);    
                return;
            }

            var xInput = match.Groups[1].ToString();
            var yInput = match.Groups[2].ToString();
            var directionInput = match.Groups[3].ToString();

            if (!uint.TryParse(xInput, out var x)) {
                _errorWriter.WriteLine($"Invalid value for X: {xInput}");
                return;
            }

            if (!uint.TryParse(yInput, out var y)) {
                _errorWriter.WriteLine($"Invalid value for X: {yInput}");
                return;
            }

            if (!Enum.TryParse<Direction>(directionInput, ignoreCase:true, out var direction)) {
                _errorWriter.WriteLine($"Invalid direction: {directionInput}");
                return;
            }  

            robot.Place(new Position(new Coordinate(x, y), direction));
        }
    }
}