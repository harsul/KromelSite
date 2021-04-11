using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KromelSite.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Display(Name = "Baslik")]
        public string Baslik { get; set; }
        [Display(Name = "Tanitim")]
        public string ResimTanimi { get; set; }
        [Display(Name = "Resim Yolu")]
        public string ResimYolu { get; set; }
    }
}
