using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataRooms.Entity
{
    public class RoleMaster
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}
