using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagment.Data.Entity
{
    [Table("projects")]
    public class Project
    {
        [Key]
        public required Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Name { get; set; }

        [Required]
        public required int Priority { get; set; }

        [Required]
        public required DateOnly StartDate { get; set; }

        [Required]
        public required DateOnly EndDate { get; set; }

        [Column("company_customer_id")]
        public required Guid CompanyCustomerId { get; set; }

        [Column("company_executer_id")]
        public required Guid CompanyExecuterId { get; set; }

        [ForeignKey("CompanyCustomerId")]
        public Company? CustomerCompany { get; set; }

        [ForeignKey("CompanyExecuterId")]
        public Company? ExecuterCompany { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
