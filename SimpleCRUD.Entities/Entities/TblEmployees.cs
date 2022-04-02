using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleCRUD.Entities
{
    [Table("TblEmployees")]
    public class TblEmployees
    {
        [Key]
        public int EmployeeId { get; set; }
        public int UserId { get; set; }
        public DateTime Doj { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatededDate { get; set; }

    }
}
