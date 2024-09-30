using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccessLayer.Models
{
    public class Department
    {
        public int Id { get; set; } //pk
        [Range(0,500)]
        public int Code { get; set; }
        [Required(ErrorMessage ="name is required")]
        public string Name { get; set; }
        [Display(Name = "Created at")]
        public DateTime MyProperty { get; set; }
        public ICollection<Employee>? Employees { get; set; } = new List<Employee>();
    }
}
