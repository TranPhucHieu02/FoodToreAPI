using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace apiforapp.Models
{
    public class User
    {
        [Key]
        public int idUser { get; set; }
        public string name { get; set; }
        [EmailAddress]
        public string emailAddress { get; set; }
        public string password { get; set; }
        [AllowNull]
        public double weigh { get; set; }
        [AllowNull]
        public double heigh { get; set; }
        [AllowNull]
        public bool gender { get; set; }
        [AllowNull]
        public int age { get; set; }
        [AllowNull]
        public string avatar { get; set; }
        //public int isActive { get; set; }
        [ForeignKey("Role")]
        public int idRole { get; set; }
        [JsonIgnore]
        public virtual Role? Role { get; set; }
    }
}
