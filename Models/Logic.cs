using System;
using System.Net.WebSockets;
using System.Linq;
using System.Text.RegularExpressions;

namespace apiforapp.Models
{
    public class Logic
    {
        public static List<Food> SelectFoods(List<Food> foodItems, double targetCalories, double targetProtein, double targetFat, double targetCarbs, int count,double acceptableError)
        {
            var calo = targetCalories;
            var listfood = foodItems;
            List<Food> selectedFoods = new List<Food>();

            for (int i = 0; i < foodItems.Count; i++)
            {
                Food selectedFood = FindMostSimilarFood(foodItems, targetCalories, targetProtein, targetFat, targetCarbs);

                var _targetCalories = targetCalories; 
                if (selectedFood != null)
                {
                    if (selectedFood.Calories <= targetCalories * acceptableError)
                    {
                        targetCalories -= selectedFood.Calories;
                        targetProtein -= selectedFood.Protein;
                        targetFat -= selectedFood.Fat;
                        targetCarbs -= selectedFood.Carbohydrates;
                        selectedFoods.Add(selectedFood);
                        foodItems.Remove(selectedFood);
                    }
                    if (targetCalories <= 0 ||targetCarbs<=0||targetFat<=0||targetProtein<=0)
                        break;
                }
            }
            selectedFoods = selectedFoods
                             .OrderByDescending(x => x.Calories)
                             .ThenByDescending(x => x.Protein)
                             .ThenByDescending(x => x.Carbohydrates)
                             .ThenByDescending(x => x.Fat)
                             .Take(count)
                             .ToList();
            foodItems = CleanFood(listfood, selectedFoods);
            return selectedFoods;
        }

        private static Food FindMostSimilarFood(List<Food> foodItems, double targetCalories, double targetProtein, double targetFat, double targetCarbs)
        {
            Food mostSimilarFood = null;
            double highestSimilarity = double.MinValue;

            Random random = new Random();

            for (int i = 0; i < foodItems.Count; i++)
            {
                double similarity = CalculateCosineSimilarity(foodItems[i], random.Next((int)(targetCalories*0.9),(int)(targetCalories*1.1)), random.Next((int)(targetProtein * 0.9), (int)(targetProtein * 1.1)) , random.Next((int)(targetFat * 0.9), (int)(targetFat * 1.1)) , random.Next((int)(targetCarbs * 0.9), (int)(targetCarbs * 1.1)) );

                if (similarity > highestSimilarity)
                {
                    highestSimilarity = similarity;
                    mostSimilarFood = foodItems[i];
                }
            }
            return mostSimilarFood;
        }

        private static double CalculateCosineSimilarity(Food food, double targetCalories, double targetProtein, double targetFat, double targetCarbs)
        {
            double dotProduct = food.Calories * targetCalories +
                                food.Protein * targetProtein +
                                food.Fat * targetFat +
                                food.Carbohydrates * targetCarbs;

            double normFood = Math.Sqrt(food.Calories * food.Calories +
                                        food.Protein * food.Protein +
                                        food.Fat * food.Fat +
                                        food.Carbohydrates * food.Carbohydrates);

            double normTarget = Math.Sqrt(targetCalories * targetCalories +
                                          targetProtein * targetProtein +
                                          targetFat * targetFat +
                                          targetCarbs * targetCarbs);

            if (normFood == 0 || normTarget == 0)
                return 0;

            return dotProduct / (normFood * normTarget);
        }

        private static List<Food> CleanFood(List<Food> foodList, List<Food> foodsRecommend) 
        {
            foreach(Food food in foodsRecommend)
            {
                foodList.Remove(food);
            }

            return foodList;
        }
        public double calCalo(List<Food> lsfood)
        {
            double calo = 0;
            foreach (Food food in lsfood)
            {
                calo = calo + food.Calories;
            }
            return Math.Round(calo,2);
        }
        public double calCarbs(List<Food> lsfood)
        {
            double carbs = 0;
            foreach (Food food in lsfood)
            {
                carbs = carbs + food.Carbohydrates;
            }
            return Math.Round(carbs,2);
        }
        public double calFat(List<Food> lsfood)
        {
            double fat = 0;
            foreach (Food food in lsfood)
            {
                fat = fat + food.Fat;
            }
            return Math.Round(fat,2);
        }
        public double calProtein(List<Food> lsfood)
        {
            double protein = 0;
            foreach (Food food in lsfood)
            {
                protein = protein + food.Protein;
            }
            return Math.Round(protein,2);
        }

    }
}
