using System.ComponentModel.DataAnnotations;

namespace Microservices.Microservices.CouponAPI.Models
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public double DiscountAmount { get; set; }
        public int MinimunAmount { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
