using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.ViewModel.Comment;

namespace GameStore.Web.ViewModel.Game
{
    public class GameViewModel
    {
        public string Id { get; set; }

        [RegularExpression(@"^[A-Za-z0-9_-]{3,20}", ErrorMessage = "KeyExpression")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(20)]
        public string Key { get; set; }

        [Required(ErrorMessage = "Required")]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Existent")]
        [Range(0, 100000)]
        public decimal Price { get; set; }

        public float Discount { get; set; }

        public bool IsDiscounted { get; set; }

        [Required(ErrorMessage = "Existent")]
        [Range(0, 100000)]
        public short UnitsInStock { get; set; }

        public bool Discontinued { get; set; }

        public DateTime PublishedDate { get; set; }

        public DateTime UploadDate { get; set; }

        public int AmountViews { get; set; }

        public string CompanyName { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        public ICollection<string> Genres { get; set; }

        [Required(ErrorMessage = "Game has no platforms")]
        [MinLength(1)]
        public ICollection<string> Platforms { get; set; }
    }
}
