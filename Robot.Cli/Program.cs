using System;

namespace Robot.Cli
{
    class Program
    {
        static void Main(string[] _)
        {
            try {
                var robot = new Robot(new World(5, 5));
                var cli = new TextInterface(Console.In, Console.Out, Console.Error);
                cli.Run(robot);
            }   
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }     
        }
    }
}
