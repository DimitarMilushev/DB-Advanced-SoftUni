namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using System;
    using System.Linq;

    public class ModifyUserCommand
    {
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public static string Execute(string[] data)
        {
            string username = data[0];
            string property = data[1];
            string value = data[2];

            using (var db = new PhotoShareContext())
            {
                var user = db.Users
                    .FirstOrDefault(x => x.Username == username);

                if (user == null)
                    throw new ArgumentException($"User {username} not found!");

                string exceptionMesssage = $"Value {value} not valid" + Environment.NewLine;

                switch (property)
                {
                    case "Password":
                        {
                            if (!value.Any(char.IsLower)
                                || !value.Any(char.IsDigit))
                                throw new ArgumentException(exceptionMesssage
                                    + "Invalid Password");

                            user.Password = value;
                            break;
                        }
                    case "BornTown":
                    case "CurrentTown":
                        {
                            var newTown = db.Towns
                                .FirstOrDefault(x => x.Name == value);

                            if (newTown == null)
                                throw new ArgumentException(exceptionMesssage
                                    + $"Town {value} not found!");

                            if (property == "BornTown")
                                user.BornTown = newTown;
                            else
                                user.CurrentTown = newTown;

                            break;
                        }
                    default:
                        throw new ArgumentException($"Property {property} not supported!");
                }

                db.SaveChanges();
            }
            return $"User {username} {property} is {value}.";
        }
    }
}