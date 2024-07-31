using RobotApp.Enums;
using RobotApp.Models;
using System;
using System.Linq;

namespace RobotApp.Services.CommandService
{
    public class CommandService : ICommandInterface
    {
        public Position ExecuteCommand(Position currentPosition, CommandEnum command)
        {
            if (command == CommandEnum.F)
            {
                MoveForward(currentPosition);
                return currentPosition;
            }

            Rotate(currentPosition, command);
            return currentPosition;
        }

        private void MoveForward(Position currentPosition)
        {
            switch (currentPosition.Direction)
            {
                case DirectionEnum.N:
                    currentPosition.Y++;
                    break;
                case DirectionEnum.E:
                    currentPosition.X++;
                    break;
                case DirectionEnum.S:
                    currentPosition.Y--;
                    break;
                case DirectionEnum.W:
                    currentPosition.X--;
                    break;
            }
        }

        private void Rotate(Position currentPosition, CommandEnum command)
        {
            var direction = currentPosition.Direction + (int)command;
            
            if (Enum.IsDefined(typeof(DirectionEnum), direction))
            {
                currentPosition.Direction = direction;
            }

            if ((int)direction == -1)
            {
                currentPosition.Direction = Enum.GetValues(typeof(DirectionEnum)).Cast<DirectionEnum>().Max();
            }

            if ((int)direction == 4)
            {
                currentPosition.Direction = Enum.GetValues(typeof(DirectionEnum)).Cast<DirectionEnum>().Min();
            }
        }
    }
}
