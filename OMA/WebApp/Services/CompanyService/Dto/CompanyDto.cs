using System.ComponentModel.DataAnnotations;

namespace WebApp.Services.CompanyService.Dto
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalAddress { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        public string ContactNumber { get; set; }
    }
}
