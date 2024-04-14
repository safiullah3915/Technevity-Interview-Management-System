using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrPortal3.Models
{
    public class Panel
    {
        [Key]
        public int PanelId { get; set; }

        public string PanelName { get; set; }
      
      
    }
}
