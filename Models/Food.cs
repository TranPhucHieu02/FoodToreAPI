using System.ComponentModel.DataAnnotations;

namespace apiforapp.Models
{
   
    public class Food
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Carbohydrates { get; set; }
        public double Fat { get; set; }
        public string ImageUrl { get; set; }
        public string RecipeInstructions { get; set; }
        public string RecipeIngredientParts { get; set; }


    }
}
