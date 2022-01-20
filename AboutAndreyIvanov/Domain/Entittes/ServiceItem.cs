using System.ComponentModel.DataAnnotations;

namespace AboutAndreyIvanov.Domain.Entittes
{
    public class ServiceItem: EntityBase
    {
        [Required(ErrorMessage = "Fill the service name")]
        [Display(Name = "Service name")]
        public override string Title { get ; set; }

        [Display(Name = "Subtitle of Service")]
        public override string Subtitle { get ; set ; }

        [Display(Name = "Full title of service")]
        public override string Text { get ; set ; }
    }
}
