using RobotApp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotApp.Models
{
    public class RobotParser
    {
        public RobotParser(string[] file)
        {
            string gridDefinition = file.SingleOrDefault(line => line.Contains("GRID") && line.Contains("x"));
            RobotGrid = new(gridDefinition);

            InitialiseJourneys(file);

            CheckObstaclesBeforeJourneys(file);
            InitialiseObstacles(file);
        }

        public Grid RobotGrid { get; set; }

        public IEnumerable<RobotJourney> RobotJourneys { get; set; }

        public IEnumerable<Obstacle> RobotObstacles { get; set; }


        private void InitialiseJourneys(string[] file)
        {
            const int journeyListSize = 3;
            var journeys = file
                .Where(line => !(line.Contains("GRID") && line.Contains("x")) && !line.Contains("OBSTACLE") && !string.IsNullOrWhiteSpace(line))
                .ToSubLists(journeyListSize);

            RobotJourneys = journeys.Select(journey => new RobotJourney(journey, RobotGrid));
        }

        private void InitialiseObstacles(string[] file)
        {
            RobotObstacles = file
                .Where(line => line.Contains("OBSTACLE"))
                .Select(obstacle => new Obstacle(obstacle, RobotGrid));
        }

        private void CheckObstaclesBeforeJourneys(string[] file)
        {
            string startOfFirstJourney = file.FirstOrDefault(line =>
                !(line.Contains("GRID") && line.Contains("x")) &&
                !line.Contains("OBSTACLE") &&
                !string.IsNullOrWhiteSpace(line));

            int journeyFirstIndex = Array.FindIndex(file, row => row.Equals(startOfFirstJourney));
            int obstacleLastIndex = Array.FindLastIndex(file, row => row.Contains("OBSTACLE"));

            if (obstacleLastIndex > journeyFirstIndex)
            {
                throw new FormatException("Please make sure all obstacles are declared before the start of any journeys");
            }
        }
    }
}
