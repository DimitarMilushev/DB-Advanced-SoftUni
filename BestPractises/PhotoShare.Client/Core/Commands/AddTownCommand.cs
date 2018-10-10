namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Data;
    using System.Linq;
    using System;

    public class AddTownCommand
    {
        // AddTown <townName> <countryName>
        public static string Execute(string[] data)
        {
            string townName = data[0];
            string country = data[1];

            using (var db = new PhotoShareContext())
            {
                var checkTown = db.Towns
                    .FirstOrDefault(x => x.Name == townName);

                if (checkTown != null)
                    throw new ArgumentException($"Town {townName} was already added!");
            }

            using (PhotoShareContext context = new PhotoShareContext())
            {
                Town town = new Town
                {
                    Name = townName,
                    Country = country
                };

                context.Towns.Add(town);
                context.SaveChanges();

                return $"Town {townName} was added successfully!";
            }
        }
    }
}
