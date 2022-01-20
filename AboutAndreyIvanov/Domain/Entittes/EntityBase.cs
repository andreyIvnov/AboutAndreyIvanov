using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutAndreyIvanov.Domain.Entittes
{
    public abstract class EntityBase
    {
        protected EntityBase() => DateAdded = DateTime.UtcNow;
        [Required]
        public Guid Id { get; set; }
        
        [Display(Name ="Name (title)")]
        public virtual string  Title { get; set; }
        public virtual string Subtitle { get; set; }
        public virtual string Text { get; set; }
        public virtual string TitleImagePath { get; set; }
        public string MetaTitle{ get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public DateTime DateAdded { get; set; }

    }
}
