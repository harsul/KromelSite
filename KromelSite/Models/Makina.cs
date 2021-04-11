using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KromelSite.Models
{
    public class Makina
    {
        public int Id { get; set; }
        [Display(Name = "Makina Adi")]
        public string MakinaAdi { get; set; }
        [Display(Name = "Makina Tanitimi")]
        public string MakinaTanitim { get; set; }
        [Display(Name = "Makina Grubu")]
        public int UrunGruplariID { get; set; }
        [Display(Name = "Resim")]
        public string ResimYolu { get; set; }

        public UrunGruplari UrunGruplari { get; set; }

        [Display(Name = "Resim File")]
        [NotMapped]
        public IFormFile ResimFile { get; set; }
    }
}
