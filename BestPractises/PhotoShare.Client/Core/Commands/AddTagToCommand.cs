namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AddTagToCommand 
    {
        // AddTagTo <albumName> <tag>
        public static string Execute(string[] data)
        {
            string album = data[0];
            string tag = data[1];

            using (var db = new PhotoShareContext())
            {
                var givenAlbum = db.Albums.FirstOrDefault(x => x.Name == album);
                var givenTag = db.Tags.FirstOrDefault(x => x.Name == album);

                if (givenAlbum == null || givenTag == null)
                    throw new ArgumentException("Either tag or album do not exist!");

                givenAlbum.AlbumTags.Add(new AlbumTag { Album = givenAlbum, Tag = givenTag });
                db.SaveChanges();
            }

            return $"Tag {tag} added to {album}!";
        }
    }
}
