using System.ComponentModel.DataAnnotations.Schema;

namespace HrPortal3.Models
{
    public class IntervieweeCreateModel
    {
        public string Name { get; set; }

        public string Contact { get; set; }

        public IFormFile Resume { get; set; }
        public DateTime InterviewDate { get; set; }
        public int PanelId { get; set; }
        public int PostId { get; set; }

        public List<string> UserId { get; set; }

    }
}
