using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagment.Data.Entity
{
    [Table("employees")]
    public class Employee
    {
        [Key]
        public required Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Surname { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Email { get; set; }

        [Column("company_id")]
        public required Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        public List<Position> Positions { get; set; } = new List<Position>();
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
