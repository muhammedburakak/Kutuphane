using System;
using System.ComponentModel.DataAnnotations;

namespace KutuphaneProgrami.Data.Model
{
    public class OduncKitap: BaseEntity
    {
        [Required]
        public int KitapId { get; set; }

        [Required]
        public int UyeId { get; set; }

        [Required]
        public DateTime AlisTarihi { get; set; }

        [Required]
        public DateTime GetirecegiTarihi { get; set; }

        public DateTime? GetirdigiTarih { get; set; }

        public virtual Uye Uye { get; set; }

        public virtual Kitap Kitap { get; set; }
    }
}
