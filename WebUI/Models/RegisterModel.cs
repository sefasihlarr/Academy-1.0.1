using EntityLayer.Concrete;
using System.ComponentModel.DataAnnotations;


namespace WebUI.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [StringLength(12, MinimumLength = 11, ErrorMessage = "Tc Numarası 11 karakter olmalıdır")]
        public string TcNumber { get; set; }


        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string SurName { get; set; }


        public int ClassId { get; set; }
        public Class Class { get; set; }


        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "En az 5 karakter ve Büyük Küçük harf içermelidir")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

    }
}
