using System;
using System.Collections.Generic;
using GameStore.Domain.Models;

namespace GameStore.Infrastructure.Data.Helpers
{
    public class GameGenerator
    {
        private readonly List<Genre> _listGenres;
        private readonly List<Publisher> _listPublishers;
        private readonly List<Platform> _listPlatforms;
        private readonly List<string> _gameNamesPart1;
        private readonly List<string> _gameNamesPart2;
        private readonly List<string> _prepositions;
        private readonly Random _random;

        public GameGenerator(List<Genre> listGenres, List<Publisher> listPublishers, List<Platform> listPlatforms)
        {
            _random = new Random();
            _listGenres = listGenres;
            _listPublishers = listPublishers;
            _listPlatforms = listPlatforms;
            ListGameGenres = new List<GameGenre>();
            ListGamePlatforms = new List<GamePlatform>();
            _gameNamesPart1 = GetGameNamesPart("adamuadinaadjudajibiakajealideapacharingarlamaroyaasdadatsarauletbaasebadlabahalbarafbarudbawbubegaubekribezinbhalabishrbitembizumbluanbodzabubutbutlocarvecaspecendocicauconolcorrecotascuparcymrudaspedayoudazaldedapdehamdesaidesendigamdjugudoguidoriedulskdykesedraledseleirupeisamekataelmeremmesernulesasieuelnfansefesetfriesfulgagagnegalozgazangeeragerligjotagolpagqoragrgargriffgulaggurgygverehajrihamouharthhosumhottehuihehulayhuwuniadoriloniipilairiboitotojazanjbalajebgajengajoccajokakjubeyjuchujulipkadaokamadkapbakawalkawgakbetukealekehkikittsklumpkpahakrekikungukwemelabuylaijilamimlebanlempelesislodoglonbilovetlullulupismammimanakmaxeymbotombumamekremeradmerakmeurammemomoeramollemondomulgemultomuzuanaifindjernelmsngoldnjerunkuzinkweonomsonouannymphohongouteroyabaozadapagyipalliporteprkosquihirahadrahanramzeriojaromeoruavasapwesekershifosihansinopsomoastrumtakuatanjitantitartataubatigastoedotuyomusigavaltuveliewagarxiazeyarfiyarriyopilyuhusywezuzauruzeglazegyozhimaziradzomdozurfizurin");
            _gameNamesPart2 = GetGameNamesPart("abekactuacuaafatagnaaleaamagamedanifanogaranartaarviatesawasayayayimbaaybalabanibapabautbefebemabhosbigabigubizebobiboqoboribresbruzbugtbuugcarecevochadcliocoalcodocorocotacumucuridaapdavadavidjomdoveedlaegonekbyelanenteeruhetahetalevjefafafoyofuatfuhefunkgambgiyagningoilhehoheilheydhofthubaiazuigbajagajaqrjedajisojobajongjortkanekanukawmkipikitfkodakopokpemkreokuillaonlauclaynlegoletileviliaplopelowiloyaluftlysemarlmburmeermimamoiamonsmortnaicnajrnalundjanecknogenokhnublogisohitohrtohwaomidorzyotaooutiovedpadaparppasipehsperopiasprexqakaqasaqimoraiuraubrecirijkrooiruhasaassamisaursejsshaisimisirisokisonisooisortspesstubsudasususuvitadatagatagotahttaostaouteeltelztheltinktiyetobotrentriatubotunaubiauhreuradurgoutsouturvicavustwagawagiwaxiwoldxavexisiyaifyaluyeheyeriyewuyookyuguzerkzinzzujiabekactuacuaafatagnaaleaamagamedanifanogaranartaarviatesawasayayayimbaaybalabanibapabautbefebemabhosbigabigubizebobiboqoboribresbruzbugtbuugcarecevochadcliocoalcodocorocotacumucuridaapdavadavidjomdoveedlaegonekbyelanenteeruhetahetalevjefafafoyofuatfuhefunkgambgiyagningoilhehoheilheydhofthubaiazuigbajagajaqrjedajisojobajongjortkanekanukawmkipikitfkodakopokpemkreokuillaonlauclaynlegoletileviliaplopelowiloyaluftlysemarlmburmeermimamoiamonsmortnaicnajrnalundjanecknogenokhnublogisohitohrtohwaomidorzyotaooutiovedpadaparppasipehsperopiasprexqakaqasaqimoraiuraubrecirijkrooiruhasaassamisaursejsshaisimisirisokisonisooisortspesstubsudasususuvitadatagatagotahttaostaouteeltelztheltinktiyetobotrentriatubotunaubiauhreuradurgoutsouturvicavustwagawagiwaxiwoldxavexisiyaifyaluyeheyeriyewuyookyuguzerkzinzzuji");
            _prepositions = new List<string> { " at ", " on ", " in ", " of ", " for ", " to " };
        }

        public List<GameGenre> ListGameGenres { get; }

        public List<GamePlatform> ListGamePlatforms { get; }

        public List<Game> GenerateGames(int countGames)
        {
            List<Game> games = new List<Game>();
            for (int i = 0; i < countGames; i++)
            {
                var game = GenerateGame();
                games.Add(game);

                var gameGenre1 = GenerateGameGenre(game, 0, _listGenres.Count / 2);
                var gameGenre2 = GenerateGameGenre(game, _listGenres.Count / 2, _listGenres.Count);
                ListGameGenres.Add(gameGenre1);
                ListGameGenres.Add(gameGenre2);

                var gamePlatform1 = GenerateGamePlatform(game, 0, _listPlatforms.Count / 2);
                var gamePlatform2 = GenerateGamePlatform(game, _listPlatforms.Count / 2, _listPlatforms.Count);
                ListGamePlatforms.Add(gamePlatform1);
                ListGamePlatforms.Add(gamePlatform2);
            }

            return games;
        }

        private Game GenerateGame()
        {
            int partOfGame = _random.Next(0, 100) < 60 ? 1 : _random.Next(2, 5);
            string nameGameNames1 = _gameNamesPart1[_random.Next(0, _gameNamesPart1.Count)];
            string nameGameNames2 = _gameNamesPart2[_random.Next(0, _gameNamesPart2.Count)];
            string gameKey = nameGameNames1 + nameGameNames2;
            string union = _random.Next(0, 100) < 15 ? " " : _prepositions[_random.Next(0, _prepositions.Count)];
            string gameName = UppercaseFirstLetter(nameGameNames1) + union + UppercaseFirstLetter(nameGameNames2);
            if (partOfGame != 1)
            {
                gameKey += partOfGame;
                gameName += " " + partOfGame;
            }

            var game = new Game()
            {
                Id = Guid.NewGuid().ToString(),
                Key = gameKey,
                Name = gameName,
                Description = GenerateDescription(),
                UnitsInStock = (short)_random.Next(1, 128),
                Price = _random.Next(0, 15000),
                IsDiscounted = _random.Next(0, 100) < 50,
                Discount = _random.Next(0, 15),
                AmountViews = _random.Next(2, 1337),
                PublishedDate = DateTime.Now,
                UploadDate = new DateTime(_random.Next(2000, 2020), _random.Next(1, 12), _random.Next(1, 28)),
                Discontinued = _random.Next(0, 100) < 12,
                IsDeleted = _random.Next(0, 100) < 2,
                PublisherId = _listPublishers[_random.Next(0, _listPublishers.Count)].Id,
            };

            return game;
        }

        private GameGenre GenerateGameGenre(Game game, int l, int r)
        {
            var gameGenre = new GameGenre
            {
                GameId = game.Id,
                GenreId = _listGenres[_random.Next(l, r)].Id,
            };

            return gameGenre;
        }

        private GamePlatform GenerateGamePlatform(Game game, int l, int r)
        {
            var gamePlatform = new GamePlatform
            {
                GameId = game.Id,
                PlatformId = _listPlatforms[_random.Next(l, r)].Id,
            };

            return gamePlatform;
        }

        private List<string> GetGameNamesPart(string str)
        {
            List<string> list = new List<string>();
            int i = 0;
            int splitRange = 4;
            while (i < str.Length - 1)
            {
                list.Add(str.Substring(i, splitRange));
                i += splitRange;
            }

            return list;
        }

        private string UppercaseFirstLetter(string value)
        {
            char[] array = value.ToCharArray();
            array[0] = char.ToUpper(array[0]);
            return new string(array);
        }

        private string GenerateDescription()
        {
            string description = string.Empty;
            for (int i = 0; i < 500; i++)
            {
                description += (char)_random.Next(101, 132);
            }

            return description;
        }
    }
}
