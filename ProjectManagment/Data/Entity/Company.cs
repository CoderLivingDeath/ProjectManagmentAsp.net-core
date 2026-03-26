using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagment.Data.Entity
{
    [Table("companies")]
    public class Company
    {
        [Key]
        public required Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Name { get; set; }

        [InverseProperty("CustomerCompany")]
        public virtual ICollection<Project>? ProjectsAsCustomer { get; set; }

        [InverseProperty("ExecuterCompany")]
        public virtual ICollection<Project>? ProjectsAsExecuter { get; set; }
    }
}
