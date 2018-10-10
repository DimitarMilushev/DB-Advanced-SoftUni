namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using Data;

    public class DeleteUserCommand
    {
        // DeleteUser <username>
        public static string Execute(string[] data)
        {
            string username = data[0];
            using (PhotoShareContext context = new PhotoShareContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                if (user.IsDeleted.Value)
                    throw new InvalidOperationException($"User {username} already deleted!");

                user.IsDeleted = true;
                // TODO: Delete User by username (only mark him as inactive)
                context.SaveChanges();

                return $"User {username} was deleted successfully!";
            }
        }
    }
}