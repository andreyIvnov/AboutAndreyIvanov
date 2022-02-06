using System.ComponentModel.DataAnnotations;

namespace AboutAndreyIvanov.Domain.Entittes
{
    public class TextField :EntityBase
    {
        [Required]
        public string CodeWord { get; set; }

        [Display(Name = "Page name (Title)")]
        public override string Title { get; set; } = "Page Information";

        [Display(Name = "Content of page")]
        public override string Text { get; set; } = "Content is filled by Administrator";
    }
}
