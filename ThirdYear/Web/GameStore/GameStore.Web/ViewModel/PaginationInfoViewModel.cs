using GameStore.Domain.Enums;

namespace GameStore.Web.ViewModel
{
    public class PaginationInfoViewModel
    {
        public PaginItemQuantity PaginItemQuantity { get; set; }

        public int NumberCurrentPage { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }
    }
}
