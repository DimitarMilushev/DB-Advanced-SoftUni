namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AddFriendCommand
    {
        // AddFriend <username1> <username2>
        public static string Execute(string[] data)
        {
            string firstUsername = data[0];
            string secondUsername = data[1];

            using (var db = new PhotoShareContext())
            {
                var firstUser = db.Users.FirstOrDefault(x => x.Username == firstUsername);
                var secondUser = db.Users.FirstOrDefault(x => x.Username == secondUsername);

                if (firstUser == null)
                    throw new ArgumentException($"{firstUsername} not found!");
                if (secondUser == null)
                    throw new ArgumentException($"{secondUsername} not found!");

                if (firstUser.FriendsAdded == secondUser || firstUser.AddedAsFriendBy == secondUser)
                    throw new InvalidOperationException
                        ($"{secondUsername} is already a friend to {firstUsername}");

                firstUser.FriendsAdded.Add(new Friendship { User = firstUser, Friend = secondUser });

                db.SaveChanges();
            }

            return $"Friend {secondUsername} added to {firstUsername}";
        }
    }
}