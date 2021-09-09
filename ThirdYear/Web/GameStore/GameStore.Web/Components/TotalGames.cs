using GameStore.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Components
{
    public class TotalGames : ViewComponent
    {
        private readonly IGameService _gameService;

        public TotalGames(IGameService gameService)
        {
            _gameService = gameService;
        }

        public IViewComponentResult Invoke()
        {
            int totalNumbersOfGames = _gameService.GetCountGames();

            return Content(totalNumbersOfGames.ToString());
        }
    }
}
