namespace HrPortal3.Models
{
    public class IntervieweeIndexModel
    {
        public int IntervieweeId { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string ResumeFile { get; set; }
        public DateTime InterviewDate { get; set; }
        public int PanelId { get; set; }
        public int PostId { get; set; }

        public string PanelName { get; set; }
        public string PostName { get; set; }
        public string UserName { get; set; }    
 
    }
}
