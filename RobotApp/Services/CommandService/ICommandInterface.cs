using RobotApp.Enums;
using RobotApp.Models;

namespace RobotApp.Services.CommandService
{
    public interface ICommandInterface
    {
        Position ExecuteCommand(Position currentPosition, CommandEnum command);
    }
}
