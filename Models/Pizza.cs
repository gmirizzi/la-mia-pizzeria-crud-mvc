using la_mia_pizzeria_static.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    [Table("pizzas")]
    public class Pizza
    {
        public Pizza()
        {
    
        }

        [Key]
        public int PizzaId { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(25, ErrorMessage = "Il nome non può avere più di 25 caratteri")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [FiveWordsValidationAttribute]
        [Column(TypeName = "text")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Url]
        public string ImgPath { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Price { get; set; }
		public int? CategoryId { get; set; }
		public Category? Category { get; set; }
        public List<Ingredient>? IngredientsList { get; set; }
	}
}
