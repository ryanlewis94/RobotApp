using System;
using System.Threading.Tasks;
using RobotApp.Models;
using RobotApp.Services.FileService;

namespace RobotApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please provide a single valid file path.");
                return;
            }

            string filePath = args[0];

            var fileService = new FileService();

            try
            {
                string[] file = await fileService.ReadFileAsync(filePath);

                RobotParser robotParser = new RobotParser(file);

                foreach (RobotJourney journey in robotParser.RobotJourneys)
                {
                    journey.BeginJourney(robotParser.RobotGrid, robotParser.RobotObstacles);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}
