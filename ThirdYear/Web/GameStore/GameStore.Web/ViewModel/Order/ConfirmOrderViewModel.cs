using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModel.Order
{
    public class ConfirmOrderViewModel
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z]{3,100}", ErrorMessage = "Holders Name is invalid")]
        public string CardHoldersName { get; set; }

        [Required]
        [RegularExpression(@"^[0-9^\s]{3,20}", ErrorMessage = "Number is invalid")]
        public string CardNumber { get; set; }

        [Required]
        public string ExpirationDate { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{3}", ErrorMessage = "CVV is invalid")]
        public int CardVerificationValue { get; set; }
    }
}
