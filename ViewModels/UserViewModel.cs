using apiforapp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace apiforapp.ViewModels
{
    public class UserViewModel
    {
        public int idUser { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public double weigh { get; set; }
        public double heigh { get; set; }
        public bool gender { get; set; }
        public int age { get; set; }
        public string avatar { get; set; }
        public string name { get; set; }
        public int idStatebody { get; set; }
        public int idRole { get; set; }
        public double bmi { get; set; }
        public double bmr { get; set; }
        public double tdee { get; set; }

    }
}
