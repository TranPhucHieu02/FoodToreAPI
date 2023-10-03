using apiforapp.Models;
using apiforapp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace apiforapp.Api
{
    [Route("api/[controller]")]
    public class HealthController : Controller
    {
        private readonly ApplicationDbcontext _db;
        private readonly ILogger<HealthController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HealthController(ILogger<HealthController> logger, ApplicationDbcontext db, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("menu")]
        public IActionResult RecommendMenu(double age, double weigh, double heigh, bool gender, int activity, int target, int colorMain, int taste, int ActivityDen, int EmotionSen) // Chỉ số Body Mass Index
        {
            double bmi = weigh / (heigh / 100 * heigh / 100);     //Chỉ số Khối Lượng Cơ Thể (Body Mass Index - BMI)
            double bfp, tdee = 0;                                             //Chỉ số Tỷ lệ Mỡ Cơ Thể (Body Fat Percentage)
            double oldAsDouble = age;
            if (gender)
            {
                bfp = 1.2 * bmi + 0.23 * oldAsDouble - 16.2;
            }
            else
            {
                bfp = 1.2 * bmi + 0.23 * oldAsDouble - 5.4;
            }
            double lbm = weigh - (weigh * bfp / 100);
            double bmr = 370 + (21.6 * lbm);
            double calo = bmr * 1.2;

            List<double> otherOne = new List<double> { 1.2, 1.375, 1.55, 1.725, 1.9 };
            switch (activity)
            {
                case 0:
                    tdee = otherOne[activity] * bmr;
                    break;
                case 1:
                    tdee = otherOne[activity] * bmr;
                    break;
                case 2:
                    tdee = otherOne[activity] * bmr;
                    break;
                case 3:
                    tdee = otherOne[activity] * bmr;
                    break;
                case 4:
                    tdee = otherOne[activity] * bmr;
                    break;
                case 5:
                    tdee = otherOne[activity] * bmr;
                    break;
            }

            List<double> otherTwo = new List<double> { 1.2, 1, 0.9, 0.8 };
            switch (target)
            {
                case 0:
                    tdee = otherTwo[target] * tdee;
                    break;
                case 1:
                    tdee = otherTwo[target] * tdee;
                    break;
                case 2:
                    tdee = otherTwo[target] * tdee;
                    break;
                case 3:
                    tdee = otherTwo[target] * tdee;
                    break;

            }
            // Tính lứa tuổi , giá trị giới tính
            int valueAge, valueGender;

            if (age < 18)
            {
                valueAge = 0;
            }
            else if (18 <= age && age < 39)
            {
                valueAge = 1;
            }
            else
            {
                valueAge = 2;
            }

            if (gender)
            {
                valueGender = 1;
            }
            else
            {
                valueGender = 2;
            }


            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string jsonPath = Path.Combine(contentRootPath, "wwwroot", "Content", "jsonAlth.txt");
            string jsonData = System.IO.File.ReadAllText(jsonPath);
            List<Nutrient> lnutr = JsonConvert.DeserializeObject<List<Nutrient>>(jsonData);
            double getValue = lnutr[1].Emotion;


            double proteinRecommand = lnutr[0].Age * valueAge + lnutr[0].Color_main * colorMain + lnutr[0].Gender * valueGender + 1 * lnutr[0].intercept + lnutr[0].Emotion * EmotionSen + lnutr[0].Activity_Sensitive * ActivityDen + lnutr[0].Taste * taste;
            double cacbonhydratrecommand = lnutr[1].Age * valueAge + lnutr[1].Color_main * colorMain + lnutr[1].Gender * valueGender + 1 * lnutr[1].intercept + lnutr[1].Emotion * EmotionSen + lnutr[1].Activity_Sensitive * ActivityDen + lnutr[1].Taste * taste;
            double fatRecommand = lnutr[2].Age * valueAge + lnutr[2].Color_main * colorMain + lnutr[2].Gender * valueGender + 1 * lnutr[2].intercept + lnutr[2].Emotion * EmotionSen + lnutr[2].Activity_Sensitive * ActivityDen + lnutr[2].Taste * taste;

            double sumNutri = proteinRecommand + cacbonhydratrecommand + fatRecommand;


            // Tính lượng dinh dưỡng cho mỗi bữa
            double totalCaloriesPerDay = tdee;
            double totalProteinPerDay = tdee * proteinRecommand / sumNutri / 4;
            double totalCarbsPerDay = tdee * cacbonhydratrecommand / sumNutri / 4;
            double totalFatPerDay = tdee * fatRecommand / sumNutri / 9;

            double breakfastCalories = totalCaloriesPerDay * (2.0 / 10.0);
            double lunchCalories = totalCaloriesPerDay * (5.0 / 10.0);
            double dinnerCalories = totalCaloriesPerDay * (3.0 / 10.0);

            double breakfastProtein = totalProteinPerDay * (2.0 / 10.0);
            double lunchProtein = totalProteinPerDay * (5.0 / 10.0);
            double dinnerProtein = totalProteinPerDay * (3.0 / 10.0);

            double breakfastFat = totalFatPerDay * (2.0 / 10.0);
            double lunchFat = totalFatPerDay * (5.0 / 10.0);
            double dinnerFat = totalFatPerDay * (3.0 / 10.0);

            double breakfastCarbs = totalCarbsPerDay * (2.0 / 10.0);
            double lunchCarbs = totalCarbsPerDay * (5.0 / 10.0);
            double dinnerCarbs = totalCarbsPerDay * (3.0 / 10.0);

            var menus = new
            {
                Monday = new DailyMenuView(),
                Tuesday = new DailyMenuView(),
                Wednesday = new DailyMenuView(),
                Thursday = new DailyMenuView(),
                Friday = new DailyMenuView(),
                Saturday = new DailyMenuView(),
                Sunday = new DailyMenuView()
            };



            List<Food> foodItems = _db.Foods.ToList();
            int numberOfMealsPerDay = 3;
            double acceptableError = 1.5;
            Logic logic = new Logic();
            for (int i = 0; i < 7; i++)
            {
                List<Food> breakfastFoods = Logic.SelectFoods(foodItems, breakfastCalories, breakfastProtein, breakfastFat, breakfastCarbs, numberOfMealsPerDay, acceptableError);
                Console.WriteLine("Ngày " + i + " sang:  " +"\tCalo = " + logic.calCalo(breakfastFoods) + "\tProtein = "+logic.calProtein(breakfastFoods)+"\tCarbs = "+logic.calCarbs(breakfastFoods)+"\tFat = " +logic.calFat(breakfastFoods));
                List<Food> lunchFoods = Logic.SelectFoods(foodItems, lunchCalories, lunchProtein, lunchFat, lunchCarbs, numberOfMealsPerDay, acceptableError);
                Console.WriteLine("Ngày " + i + " trua:  " + "\tCalo = " + logic.calCalo(lunchFoods) + "\tProtein = " + logic.calProtein(lunchFoods) + "\tCarbs = " + logic.calCarbs(lunchFoods) + "\tFat = " + logic.calFat(lunchFoods));
                List<Food> dinnerFoods = Logic.SelectFoods(foodItems, dinnerCalories, dinnerProtein, dinnerFat, dinnerCarbs, numberOfMealsPerDay, acceptableError);
                Console.WriteLine("Ngày " + i + " toi:  " + "\tCalo = " + logic.calCalo(dinnerFoods) + "\tProtein = " + logic.calProtein(dinnerFoods) + "\tCarbs = " + logic.calCarbs(dinnerFoods) + "\tFat = " + logic.calFat(dinnerFoods));

                if (foodItems.Count <= 3)
                {
                    foodItems = _db.Foods.ToList();
                }

                switch (i)
                {
                    case 0:
                        menus.Monday.Breakfast = breakfastFoods;
                        menus.Monday.Lunch = lunchFoods;
                        menus.Monday.Dinner = dinnerFoods;
                        break;

                    case 1:
                        menus.Tuesday.Breakfast = breakfastFoods;
                        menus.Tuesday.Lunch = lunchFoods;
                        menus.Tuesday.Dinner = dinnerFoods;
                        break;

                    case 2:
                        menus.Wednesday.Breakfast = breakfastFoods;
                        menus.Wednesday.Lunch = lunchFoods;
                        menus.Wednesday.Dinner = dinnerFoods;
                        break;

                    case 3:
                        menus.Thursday.Breakfast = breakfastFoods;
                        menus.Thursday.Lunch = lunchFoods;
                        menus.Thursday.Dinner = dinnerFoods;
                        break;

                    case 4:
                        menus.Friday.Breakfast = breakfastFoods;
                        menus.Friday.Lunch = lunchFoods;
                        menus.Friday.Dinner = dinnerFoods;
                        break;

                    case 5:
                        menus.Saturday.Breakfast = breakfastFoods;
                        menus.Saturday.Lunch = lunchFoods;
                        menus.Saturday.Dinner = dinnerFoods;
                        break;

                    case 6:
                        menus.Sunday.Breakfast = breakfastFoods;
                        menus.Sunday.Lunch = lunchFoods;
                        menus.Sunday.Dinner = dinnerFoods;
                        break;
                }
            }
            // Tình trạng người dùng
            string bmiStatus = "", targetStatus = "";
            if (bmi < 18.5)
            {
                bmiStatus = "Thiếu cân";
            }
            else if (18.5 <= bmi && bmi <= 24)
            {
                bmiStatus = "Bình thường";
            }
            else
            {
                bmiStatus = "Thừa cân";
            }
            // Mục tiêu người dùng
            if (target == 0)
            {
                targetStatus = "Tăng cân";
            }
            else if (target == 1)
            {
                targetStatus = "Duy trì thể trạng";
            }
            else if (target == 2)
            {
                targetStatus = "Giảm cân chậm";
            }
            else if (target == 3)
            {
                targetStatus = "Giảm cân nhanh";
            }
            var data = new
            {
                Bmi = bmi,
                BmiStatus = bmiStatus,
                Bmr = bmr,
                Calo = calo,
                TargetStatus = targetStatus,
                TargetCalo = tdee,
                Protein_ngay = tdee * proteinRecommand / sumNutri / 4,
                Carbs_ngay = tdee * cacbonhydratrecommand / sumNutri / 4,
                Fat_ngay = tdee * fatRecommand / sumNutri / 4,
                Menus = menus,

            };
            return Ok(data);

        }
        
    }
}