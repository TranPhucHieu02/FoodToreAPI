using apiforapp.Models;

namespace apiforapp.ViewModels
{
    public class DailyMenuView
    {
        public List<Food> Breakfast { get; set; }
        public List<Food> Lunch { get; set; }
        public List<Food> Dinner { get; set; }
    }
}
