using System;
using System.Collections.Generic;
using GameStore.Domain.Enums;
using GameStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infrastructure.Data.Helpers
{
    public static class DbInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var genreStrategy = new Genre { Id = GuidString.Generate(), Name = "Strategy" };
            var genreRPG = new Genre { Id = GuidString.Generate(), Name = "RPG" };
            var genreSports = new Genre { Id = GuidString.Generate(), Name = "Sports" };
            var genreRaces = new Genre { Id = GuidString.Generate(), Name = "Races" };
            var genreAction = new Genre { Id = GuidString.Generate(), Name = "Adventure" };
            var genrePuzzleSkill = new Genre { Id = GuidString.Generate(), Name = "Puzzle and Skill" };
            var genreMisc = new Genre { Id = GuidString.Generate(), Name = "Misc" };
            var genreRTS = new Genre { Id = GuidString.Generate(), Name = "RTS", ParentGenreId = genreStrategy.Id };
            var genreTBS = new Genre { Id = GuidString.Generate(), Name = "TBS", ParentGenreId = genreStrategy.Id };
            var genreRally = new Genre { Id = GuidString.Generate(), Name = "Rally", ParentGenreId = genreRaces.Id };
            var genreArcade = new Genre { Id = GuidString.Generate(), Name = "Arcade", ParentGenreId = genreRaces.Id };
            var genreFormula = new Genre { Id = GuidString.Generate(), Name = "Formula", ParentGenreId = genreRaces.Id };
            var genreOffroad = new Genre { Id = GuidString.Generate(), Name = "Off-road", ParentGenreId = genreRaces.Id };
            var genreFPS = new Genre { Id = GuidString.Generate(), Name = "FPS", ParentGenreId = genreAction.Id };
            var genreTPS = new Genre { Id = GuidString.Generate(), Name = "TPS", ParentGenreId = genreAction.Id };

            modelBuilder.Entity<Genre>()
                .HasData(genreRTS, genreTBS, genreRally, genreArcade, genreFormula, genreOffroad, genreFPS, genreTPS, genreStrategy, genreRPG, genreSports, genreRaces, genreAction, genrePuzzleSkill, genreMisc);

            var platformTypeMobile = new Platform { Id = GuidString.Generate(), Name = "mobile" };
            var platformTypeBrowser = new Platform { Id = GuidString.Generate(), Name = "browser" };
            var platformTypeDesktop = new Platform { Id = GuidString.Generate(), Name = "desktop" };
            var platformTypeConsole = new Platform { Id = GuidString.Generate(), Name = "console" };

            modelBuilder.Entity<Platform>()
                .HasData(platformTypeMobile, platformTypeBrowser, platformTypeDesktop, platformTypeConsole);

            var valve = new Publisher { Id = GuidString.Generate(), CompanyName = "valve", HomePage = "https://www.valvesoftware.com/en/", Description = "American computer games company" };
            var osulazer = new Publisher { Id = GuidString.Generate(), CompanyName = "osu lazer", HomePage = "https://osu.ppy.sh/home", Description = "Created OSU framework" };
            var naughtyDog = new Publisher { Id = GuidString.Generate(), CompanyName = "Naughty Dog", HomePage = "https://www.naughtydog.com/", Description = "is an American first-party video game developer based in Santa Monica" };
            var electronicArts = new Publisher { Id = GuidString.Generate(), CompanyName = "Electronic Arts", HomePage = "https://www.ea.com/", Description = "is an American video game company headquartered in Redwood City, California." };
            var blizzard = new Publisher { Id = GuidString.Generate(), CompanyName = "Blizzard ", HomePage = "https://www.blizzard.com/en-us/", Description = "is an American video game developer and publisher based in Irvine, California." };
            var ubisoft = new Publisher { Id = GuidString.Generate(), CompanyName = "Ubisoft ", HomePage = "https://www.ubisoft.com/ru-ru/", Description = "is a French video game company headquartered in Montreuil with several development studios across the world." };
            var rockstarGames = new Publisher { Id = GuidString.Generate(), CompanyName = "Rockstar Games ", HomePage = "https://www.rockstargames.com/", Description = "is a French video game company headquartered in Montreuil with several development studios across the world." };

            var test = new Game { Id = GuidString.Generate(), Name = "test", Description = "test", Key = "test", AmountViews = 100, UploadDate = DateTime.Now, PublishedDate = DateTime.Now, Price = 1800, Discount = 10, IsDiscounted = true, Discontinued = false, UnitsInStock = 15 };
            var dota = new Game { Id = GuidString.Generate(), Name = "Dota 2", Description = "dont play this game", AmountViews = 120, UploadDate = DateTime.Now, PublishedDate = new DateTime(2019, 10, 14), Key = "dota", Price = 1, Discontinued = false, UnitsInStock = 180, PublisherId = valve.Id };
            var csgo = new Game { Id = GuidString.Generate(), Name = "CS:GO", Description = "a lot of cheats", AmountViews = 150, UploadDate = DateTime.Now, PublishedDate = new DateTime(2019, 8, 10), Key = "csgo", Price = 499, Discount = 5, IsDiscounted = true, Discontinued = false, UnitsInStock = 130, PublisherId = valve.Id };
            var osu = new Game { Id = GuidString.Generate(), Name = "Osu!", Description = "Tu tu ty tu", AmountViews = 175, UploadDate = DateTime.Now, PublishedDate = new DateTime(2019, 5, 15), Key = "osu", Price = 20, Discontinued = false, UnitsInStock = 30, PublisherId = osulazer.Id };

            modelBuilder.Entity<Game>()
                .HasData(test, dota, csgo, osu);

            modelBuilder.Entity<Publisher>()
                .HasData(valve, osulazer, naughtyDog, electronicArts, blizzard, ubisoft, rockstarGames);

            var gameGenretest = new GameGenre { GameId = test.Id, GenreId = genreStrategy.Id };
            var gameGenredota = new GameGenre { GameId = dota.Id, GenreId = genreStrategy.Id };
            var gameGenredotaTBS = new GameGenre { GameId = dota.Id, GenreId = genreTBS.Id };
            var gameGenrecsgo = new GameGenre { GameId = csgo.Id, GenreId = genreAction.Id };
            var gameGenrecsgoFPS = new GameGenre { GameId = csgo.Id, GenreId = genreFPS.Id };
            var gameGenreosu = new GameGenre { GameId = osu.Id, GenreId = genreMisc.Id };

            modelBuilder.Entity<GameGenre>()
                .HasData(gameGenretest, gameGenredota, gameGenredotaTBS, gameGenrecsgo, gameGenrecsgoFPS, gameGenreosu);

            var gamePlatformtest = new GamePlatform { GameId = test.Id, PlatformId = platformTypeMobile.Id };
            var gamePlatformdota = new GamePlatform { GameId = dota.Id, PlatformId = platformTypeDesktop.Id };
            var gamePlatformcsgo = new GamePlatform { GameId = csgo.Id, PlatformId = platformTypeDesktop.Id };
            var gamePlatformosu = new GamePlatform { GameId = osu.Id, PlatformId = platformTypeConsole.Id };

            modelBuilder.Entity<GamePlatform>()
                .HasData(gamePlatformtest, gamePlatformdota, gamePlatformcsgo, gamePlatformosu);

            var comment1 = new Comment
            {
                Id = GuidString.Generate(),
                Name = "user 1",
                Body = "game is good",
                GameId = test.Id,
            };
            var comment11 = new Comment
            {
                Id = GuidString.Generate(),
                Name = "user 2",
                Body = "No game is fc",
                GameId = test.Id,
                ParentCommentId = comment1.Id,
            };
            var comment111 = new Comment
            {
                Id = GuidString.Generate(),
                Name = "user 3",
                Body = "you're right",
                GameId = test.Id,
                ParentCommentId = comment11.Id,
            };
            var comment111_1 = new Comment
            {
                Id = GuidString.Generate(),
                Name = "user 4",
                Body = "yep, right",
                GameId = test.Id,
                ParentCommentId = comment11.Id,
            };
            var comment1111 = new Comment
            {
                Id = GuidString.Generate(),
                Name = "user 5",
                IsQuoted = true,
                Body = "NOOOOOOOO, game is cool",
                GameId = test.Id,
                ParentCommentId = comment111.Id,
            };
            var comment1_1 = new Comment
            {
                Id = GuidString.Generate(),
                Name = "user 6",
                Body = "a lot of bugs",
                GameId = test.Id,
                ParentCommentId = comment1.Id,
            };
            var comment1_11 = new Comment
            {
                Id = GuidString.Generate(),
                Name = "user 7",
                Body = "i never seen",
                IsQuoted = true,
                UserId = "1",
                GameId = test.Id,
                ParentCommentId = comment1_1.Id,
            };
            modelBuilder.Entity<Comment>()
               .HasData(comment1, comment11, comment111, comment1_1, comment1_11, comment111_1, comment1111);
 
            var order = new Order
            {
                Id = GuidString.Generate(),
                Status = OrderStatus.Opened,
                CustomerId = "1",
                OrderDate = DateTime.Now,
            };
            var dotaOrderDetails = new OrderDetails
            {
                Id = GuidString.Generate(),
                GameId = dota.Id,
                Discount = dota.Discount,
                Price = dota.Price,
                Quantity = 2,
                OrderId = order.Id,
            };
            var csgoOrderDetails = new OrderDetails
            {
                Id = GuidString.Generate(),
                GameId = csgo.Id,
                Discount = csgo.Discount,
                Price = csgo.Price,
                Quantity = 1,
                OrderId = order.Id,
            };

            modelBuilder.Entity<Order>()
                .HasData(order);

            modelBuilder.Entity<OrderDetails>()
                .HasData(dotaOrderDetails, csgoOrderDetails);

            var listGenres = new List<Genre> { genreRTS, genreTBS, genreRally, genreArcade, genreFormula, genreOffroad, genreFPS, genreTPS, genreStrategy, genreRPG, genreSports, genreRaces, genreAction, genrePuzzleSkill, genreMisc };
            var listPublishers = new List<Publisher> { valve, osulazer, naughtyDog, electronicArts, blizzard, ubisoft, rockstarGames };
            var listPlatforms = new List<Platform> { platformTypeMobile, platformTypeBrowser, platformTypeDesktop, platformTypeConsole };

            var gameGenerator = new GameGenerator(listGenres, listPublishers, listPlatforms);
            var games = gameGenerator.GenerateGames(150);

            modelBuilder.Entity<Game>().HasData(games);

            modelBuilder.Entity<GameGenre>().HasData(gameGenerator.ListGameGenres);

            modelBuilder.Entity<GamePlatform>().HasData(gameGenerator.ListGamePlatforms);
        }

        private static class GuidString
        {
            public static string Generate()
            {
                return Guid.NewGuid().ToString();
            }
        }
    }
}
