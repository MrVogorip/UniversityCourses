using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Domain.Enums;
using GameStore.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.ViewModel.Game
{
    public class FilterGamesViewModel
    {
        [FromQuery]
        [BindProperty(BinderType = typeof(CommaSeparatedModelBinder))]
        public ICollection<string> SelectedGenres { get; set; }

        public ICollection<string> AllGenres { get; set; }

        [FromQuery]
        [BindProperty(BinderType = typeof(CommaSeparatedModelBinder))]
        public ICollection<string> SelectedPlatforms { get; set; }

        public ICollection<string> AllPlatforms { get; set; }

        [FromQuery]
        [BindProperty(BinderType = typeof(CommaSeparatedModelBinder))]
        public ICollection<string> SelectedPublishers { get; set; }

        public ICollection<string> AllPublishers { get; set; }

        [FromQuery]
        public SortingCriterion SortingCriterion { get; set; }

        [FromQuery]
        public decimal? PriceFrom { get; set; }

        [FromQuery]
        public decimal? PriceTo { get; set; }

        public DateIssue DateIssue { get; set; }

        [FromQuery]
        [MinLength(3)]
        public string GameName { get; set; }

        public bool IsFilter { get; set; }
    }
}
