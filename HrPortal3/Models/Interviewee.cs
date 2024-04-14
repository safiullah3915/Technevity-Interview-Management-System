using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HrPortal3.Models
{
    public class Interviewee
    {
        [Key]
        public int IntervieweeId { get; set; }

        public string Name { get; set; }

        public string Contact { get; set; }

        public string ResumeFile { get; set; }
        public DateTime InterviewDate { get; set; }
        [ForeignKey("Panel")]
        public int PanelId { get; set; }
       
        [ForeignKey("Post")]
        public int PostId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }

    }
}
