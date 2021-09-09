using System;
using System.Threading.Tasks;
using GameStore.Domain.Enums;
using GameStore.Web.ViewModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GameStore.Web.Helpers
{
    public class DefaultPaginationModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var numberCurrentPageString = bindingContext.ValueProvider.GetValue("PaginationInfo.NumberCurrentPage");
            var paginItemQuantityString = bindingContext.ValueProvider.GetValue("PaginationInfo.PaginItemQuantity");

            if (numberCurrentPageString == ValueProviderResult.None || paginItemQuantityString == ValueProviderResult.None)
            {
                var paginationInfoViewModelDefault = new PaginationInfoViewModel
                {
                    NumberCurrentPage = 1,
                    PaginItemQuantity = PaginItemQuantity.Ten,
                };

                bindingContext.Result = ModelBindingResult.Success(paginationInfoViewModelDefault);
            }
            else
            {
                Enum.TryParse<PaginItemQuantity>(paginItemQuantityString.FirstValue, out var paginItemQuantity);

                var paginationInfoViewModel = new PaginationInfoViewModel
                {
                    NumberCurrentPage = int.Parse(numberCurrentPageString.FirstValue),
                    PaginItemQuantity = paginItemQuantity,
                };

                bindingContext.Result = ModelBindingResult.Success(paginationInfoViewModel);
            }

            return Task.CompletedTask;
        }
    }
}
