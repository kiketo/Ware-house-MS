using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Services;
using WHMS.Services.Contracts;
using WHMS.Services.DatabaseServices;
using WHMSData.Models;

namespace WHMS.Commands
{
    public class ImportTownsFromJsonCommand : ICommand
    {
        IJSONImportService dbService;

        public ImportTownsFromJsonCommand(IJSONImportService dbService)
        {
            this.dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
        }

        public string Execute(IReadOnlyList<string> parameters)
        {
            string jsonFileName;
            string jsonPath = "..\\..\\..\\..\\WHMSData\\DatabaseArchiveInJSON";



            jsonFileName = "Towns.json";
            var towns = File.ReadAllText($"{jsonPath}\\{jsonFileName}");
            var townsJson = JsonConvert.DeserializeObject<Town[]>(towns);
            dbService.ImportTowns(townsJson);

            return "Successfully Imported Towns!";
            //private void LoadJsonInDB(ModelBuilder modelBuilder)
            //{
            //    try
            //    {
            //        var actorsAsJson = File.ReadAllText(@"..\MovieDb.Data\Jsons\actors.json");
            //        var directorsAsJson = File.ReadAllText(@"..\MovieDb.Data\Jsons\directors.json");
            //        var genresAsJson = File.ReadAllText(@"..\MovieDb.Data\Jsons\genres.json");
            //        var moviesAsJson = File.ReadAllText(@"..\MovieDb.Data\Jsons\movies.json");
            //        var moviesActorsAsJson = File.ReadAllText(@"..\MovieDb.Data\Jsons\moviesactors.json");
            //        var moviesGenresAsJson = File.ReadAllText(@"..\MovieDb.Data\Jsons\moviesgenres.json");
            //        var previeswAsJson = File.ReadAllText(@"..\MovieDb.Data\Jsons\previews.json");


            //        var actors = JsonConvert.DeserializeObject<Actor[]>(actorsAsJson);
            //        var directors = JsonConvert.DeserializeObject<Director[]>(directorsAsJson);
            //        var genres = JsonConvert.DeserializeObject<Genre[]>(genresAsJson);
            //        var movies = JsonConvert.DeserializeObject<Movie[]>(moviesAsJson);
            //        var moviesActors = JsonConvert.DeserializeObject<MoviesActors[]>(moviesActorsAsJson);
            //        var moviesGenres = JsonConvert.DeserializeObject<MoviesGenres[]>(moviesGenresAsJson);
            //        var previews = JsonConvert.DeserializeObject<Preview[]>(previeswAsJson);


            //        modelBuilder.Entity<Actor>().HasData(actors);
            //        modelBuilder.Entity<Director>().HasData(directors);
            //        modelBuilder.Entity<Genre>().HasData(genres);
            //        modelBuilder.Entity<Movie>().HasData(movies);
            //        modelBuilder.Entity<MoviesActors>().HasData(moviesActors);
            //        modelBuilder.Entity<MoviesGenres>().HasData(moviesGenres);
            //        modelBuilder.Entity<Preview>().HasData(previews);
            //    }
            //    catch (Exception)
            //    {
            //        return;
            //    }
            //}




            //jsonFileName = "Towns.json";

            //this.databaseService.PushTowns((new DatabaseJSON<Town>()).Read(jsonPath, jsonFileName));

            jsonFileName = "Addresses.json";

            //this.databaseService.PushAddresses((new DatabaseJSON<Address>()).Read(jsonPath, jsonFileName));

            //jsonFileName = "Categories.json";
            //(new DatabaseJSON<Category>()).Read(jsonPath, jsonFileName);

            //jsonFileName = "Orders.json";
            //(new DatabaseJSON<Order>()).Read(jsonPath, jsonFileName);

            //jsonFileName = "Partners.json";
            //(new DatabaseJSON<Partner>()).Read(jsonPath, jsonFileName);

            //jsonFileName = "Products.json";
            //(new DatabaseJSON<Product>()).Read(jsonPath, jsonFileName);

            //jsonFileName = "Towns.json";
            ////(new DatabaseJSON<Town>()).Read(jsonPath, jsonFileName);

            //this.databaseService.PushTowns((new DatabaseJSON<Town>()).Read(jsonPath, jsonFileName));

            //jsonFileName = "Units.json";
            //(new DatabaseJSON<Unit>()).Read(jsonPath, jsonFileName);

            //jsonFileName = "Warehouse.json";
            //(new DatabaseJSON<Warehouse>()).Read(jsonPath, jsonFileName);

            return "Data transfered from JSON";
        }
    }
}