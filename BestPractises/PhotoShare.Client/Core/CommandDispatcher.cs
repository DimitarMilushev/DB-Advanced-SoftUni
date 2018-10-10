namespace PhotoShare.Client.Core
{
    using PhotoShare.Client.Core.Commands;
    using System;
    using System.Linq;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            string command = commandParameters[0];

            commandParameters = commandParameters.Skip(1).ToArray()`;

            switch (command)
            {
                case "RegisterUser":
                    return RegisterUserCommand.Execute(commandParameters);

                case "AddTown":
                    return AddTownCommand.Execute(commandParameters);

                case "ModifyUser":
                    return ModifyUserCommand.Execute(commandParameters);

                case "DeleteUser":
                    return DeleteUserCommand.Execute(commandParameters);

                case "AddTag":
                    return AddTagCommand.Execute(commandParameters);

                default:
                    throw new InvalidOperationException($"Command {command} not valid!");
            }
        }
    }
}
