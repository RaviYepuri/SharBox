using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataRooms.Entity
{
    public class ItemTracker
    {
        [Key]
        public int Id { get; set; }
        public int DataRoomId { get; set; }
        public string DataRoomName { get; set; }
        public string ItemName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string ModifierName { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? DeletedBy { get; set; }
        public string DeletorName { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
