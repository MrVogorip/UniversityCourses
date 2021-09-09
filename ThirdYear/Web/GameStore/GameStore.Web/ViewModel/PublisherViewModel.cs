using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModel
{
    public class PublisherViewModel
    {
        [Required]
        public string CompanyName { get; set; }

        public string Description { get; set; }

        public string HomePage { get; set; }
    }
}
