namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CreateAlbumCommand
    {
        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(string[] data)
        {
            string username = data[0];
            string albumTitle = data[1];
            string bgColor = data[2];
            string[] tags = data.Skip(3).ToArray();
            List<Tag> albumTags = new List<Tag>();

            using (var db = new PhotoShareContext())
            {
                User user = db.Users.FirstOrDefault(x => x.Username == username);

                if (user == null)
                    throw new ArgumentException($"User {username} not found!");

                if (db.Albums.Any(x => x.Name == albumTitle))
                    throw new ArgumentException($"Album {albumTitle} exists!");

                Color color;             
                if (!Enum.TryParse(bgColor, out color))
                    throw new ArgumentException($"Color {bgColor} not found!");

                foreach(var tag in tags)
                {
                    if (!db.Tags.Any(x => x.Name == tag))
                        throw new ArgumentException("Invalid tags!");

                    albumTags.Add(db.Tags.First(x => x.Name == tag));
                }

                Album newAlbum = new Album
                {
                    Name = albumTitle,
                    BackgroundColor = (Color)Enum.Parse(typeof(Color), bgColor)
                };

                albumTags.ToList()
                .ForEach(at => db.AlbumTags.Add(new AlbumTag { Album = newAlbum, Tag = at }));

                db.AlbumRoles.Add(new AlbumRole
                {
                    Album = newAlbum,
                    User = user,
                    Role = Role.Owner
                });

                db.SaveChanges();
            }

            return $"Album {albumTitle} successfully created!";
        }
    }
}