namespace HrPortal3.Models
{
    public class IntervieweeEditModel:IntervieweeCreateModel
    {
        public int IntervieweeId { get; set; }

        public string ExistingResumePath { get; set; }

    }
}
