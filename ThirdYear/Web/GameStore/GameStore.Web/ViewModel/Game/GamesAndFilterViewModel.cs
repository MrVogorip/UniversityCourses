using System.Collections.Generic;

namespace GameStore.Web.ViewModel.Game
{
    public class GamesAndFilterViewModel
    {
        public ICollection<GameViewModel> Games { get; set; }

        public FilterGamesViewModel Filter { get; set; }

        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}
