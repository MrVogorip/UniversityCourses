using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Mime;
using AutoMapper;
using GameStore.Application.Filters;
using GameStore.Application.Filters.Games;
using GameStore.Domain.Enums;
using GameStore.Domain.Interfaces.Services;
using GameStore.Domain.Models;
using GameStore.Web.Helpers;
using GameStore.Web.ViewModel;
using GameStore.Web.ViewModel.Game;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IGenreService _genreService;
        private readonly IPlatformService _platformService;
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public GameController(
            IGameService gameService,
            IGenreService genreService,
            IPlatformService platformService,
            IPublisherService publisherService,
            IMapper mapper)
        {
            _gameService = gameService;
            _genreService = genreService;
            _platformService = platformService;
            _publisherService = publisherService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/games/new")]
        public ActionResult CreateGame()
        {
            var createGameViewModel = new CreateGameViewModel
            {
                Platforms = _platformService.GetNames(),
                Genres = _genreService.GetNames(),
                CompanyNames = _publisherService.GetNames(),
            };

            return View(createGameViewModel);
        }

        [HttpPost]
        [Route("/games/new")]
        public ActionResult CreateGame(GameViewModel gameViewModel)
        {
            if (_gameService.IsExist(gameViewModel.Key))
            {
                ModelState.AddModelError("Key", "Game with such a key already exists");
            }

            if (!ModelState.IsValid)
            {
                var createGameViewModel = _mapper.Map<CreateGameViewModel>(gameViewModel);
                createGameViewModel.Platforms = _platformService.GetNames();
                createGameViewModel.Genres = _genreService.GetNames();
                createGameViewModel.CompanyNames = _publisherService.GetNames();

                return View(createGameViewModel);
            }

            var game = _mapper.Map<Game>(gameViewModel);

            _gameService.Create(game, gameViewModel.Platforms, gameViewModel.Genres, gameViewModel.CompanyName);

            return RedirectToAction(nameof(GameController.GetGames));
        }

        [HttpPost]
        [Route("/games/update")]
        public ActionResult EditGame([FromBody] GameViewModel gameViewModel)
        {
            if (gameViewModel is null)
            {
                return RedirectToAction(ResourceNames.Methods.BadRequest, ResourceNames.Classes.Error);
            }

            var game = _mapper.Map<Game>(gameViewModel);
            _gameService.Edit(game);

            return Ok("Game was successfully updated");
        }

        [HttpGet]
        [Route("/game/{key}")]
        public ActionResult GetGameDetails(string key)
        {
            if (AssertGameExists(key))
            {
                return RedirectToAction(ResourceNames.Methods.NotFound, ResourceNames.Classes.Error);
            }

            var game = _gameService.GetByKey(key);
            var gameViewModel = _mapper.Map<GameViewModel>(game);

            return View(gameViewModel);
        }

        [HttpGet]
        [Route("/")]
        [Route("/games")]
        public ActionResult GetGames([FromQuery] FilterAndPaginationInfoViewModel filterAndPaginationInfoViewModel)
        {
            filterAndPaginationInfoViewModel.Filter ??= new FilterGamesViewModel();
            filterAndPaginationInfoViewModel.Filter.AllGenres = _genreService.GetNames();
            filterAndPaginationInfoViewModel.Filter.AllPlatforms = _platformService.GetNames();
            filterAndPaginationInfoViewModel.Filter.AllPublishers = _publisherService.GetNames();

            var expression = GetExpressionFilter(filterAndPaginationInfoViewModel.Filter);
            filterAndPaginationInfoViewModel.PaginationInfo.TotalItems = _gameService.GetCountGames(expression);
            filterAndPaginationInfoViewModel.PaginationInfo.TotalPages = CalculateNumberPages(filterAndPaginationInfoViewModel.PaginationInfo);
            if (filterAndPaginationInfoViewModel.Filter.IsFilter)
            {
                filterAndPaginationInfoViewModel.PaginationInfo.NumberCurrentPage = 1;
            }

            var games = _gameService.GetAll(
                expression,
                filterAndPaginationInfoViewModel.Filter.SortingCriterion,
                filterAndPaginationInfoViewModel.PaginationInfo.PaginItemQuantity,
                filterAndPaginationInfoViewModel.PaginationInfo.NumberCurrentPage);
            var gamesViewModel = _mapper.Map<ICollection<GameViewModel>>(games);

            var gamesAndFilterViewModel = new GamesAndFilterViewModel
            {
                Filter = filterAndPaginationInfoViewModel.Filter,
                PaginationInfo = filterAndPaginationInfoViewModel.PaginationInfo,
                Games = gamesViewModel,
            };

            return View(gamesAndFilterViewModel);
        }

        [HttpPost]
        [Route("/games/remove/{gameKey}")]
        public ActionResult DeleteGame(string gameKey)
        {
            if (AssertGameExists(gameKey))
            {
                return RedirectToAction(ResourceNames.Methods.NotFound, ResourceNames.Classes.Error);
            }

            _gameService.Delete(gameKey);

            return RedirectToAction(nameof(GameController.GetGames));
        }

        [HttpGet]
        [Route("/game/{gameKey}/download")]
        [ResponseCache(Duration = 60)]
        public ActionResult Download(string gameKey)
        {
            if (AssertGameExists(gameKey))
            {
                return RedirectToAction(ResourceNames.Methods.NotFound, ResourceNames.Classes.Error);
            }

            var file = _gameService.GetFileForDownload(gameKey);

            return File(file, MediaTypeNames.Application.Octet);
        }

        private bool AssertGameExists(string gameKey)
        {
            return string.IsNullOrEmpty(gameKey) || !_gameService.IsExist(gameKey);
        }

        private Expression<Func<Game, bool>> GetExpressionFilter(FilterGamesViewModel filterGamesViewModel)
        {
            var expression = new GamePipeline()
                .Register(new GameNameFilter(filterGamesViewModel.GameName))
                .Register(new GenreFilter(filterGamesViewModel.SelectedGenres))
                .Register(new PlatformFilter(filterGamesViewModel.SelectedPlatforms))
                .Register(new PublisherFilter(filterGamesViewModel.SelectedPublishers))
                .Register(new PriceFilter(filterGamesViewModel.PriceFrom, filterGamesViewModel.PriceTo))
                .Register(new PublishedDateFilter(filterGamesViewModel.DateIssue))
                .Process(x => true);

            return expression;
        }

        private int CalculateNumberPages(PaginationInfoViewModel paginationInfoViewModel)
        {
            return paginationInfoViewModel.PaginItemQuantity == PaginItemQuantity.All ? 1 :
                (int)Math.Ceiling(paginationInfoViewModel.TotalItems / (decimal)paginationInfoViewModel.PaginItemQuantity);
        }
    }
}
