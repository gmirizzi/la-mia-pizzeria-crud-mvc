using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
	[Table("ingredients")]
	public class Ingredient
	{
		[Key]
		public int IngredientId { get; set; }
		[Required]
		[StringLength(20)]
		public string Name { get; set; }
		public List<Pizza> PizzasList { get; set; }
	}
}
