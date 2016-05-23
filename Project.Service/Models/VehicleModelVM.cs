using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class VehicleModelVM
    {
        public int Id { get; set; }
        public int MakeId { get; set; }
        public string MakeName { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}
