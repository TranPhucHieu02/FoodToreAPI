using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace apiforapp.Models
{
    public partial class Role
    {
        public Role()
        {
            User = new List<User>();
        }
        [Key]
        public int idRole { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        [JsonIgnore]
        public virtual ICollection<User>? User { get; set; }
    }
}
