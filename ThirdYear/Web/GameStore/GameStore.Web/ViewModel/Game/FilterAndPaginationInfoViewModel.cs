using GameStore.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.ViewModel.Game
{
    public class FilterAndPaginationInfoViewModel
    {
        public FilterGamesViewModel Filter { get; set; }

        [BindProperty(BinderType = typeof(DefaultPaginationModelBinder))]
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}
