using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaViewModel
    {
        public Pizza Pizza { get; set; }
        public List<string> SelectedIngredients { get; set; }
    }
}
