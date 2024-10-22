using System.ComponentModel.DataAnnotations;

namespace TemDeTudo.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Sellers Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BithDate { get; set; }

        [Range(1400, 50000, ErrorMessage = "Invalid sallary")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public Decimal Salary { get; set; }
        
        public int DepartmentId { get; set; }
        public Department Department{ get; set; }

        public List<SalesRecord> Sales { get; set; } = new List<SalesRecord>();
    }
}
