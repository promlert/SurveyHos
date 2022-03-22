using OrchardCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Module.Models
{
    public class IpModel : Entity
    {
        public int Id { set; get; }
        [Required(ErrorMessage = "Ip is required.")]
        public string Ip { set; get; }
        [Required(ErrorMessage = "Station is required.")]
        public string Station { set; get; }
        public string CreateBy { set; get; }
        public DateTime? CreateDate { set; get; }
    }
}
