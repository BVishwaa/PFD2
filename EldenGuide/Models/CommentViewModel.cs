namespace EldenGuide.Models
{
    public class CommentViewModel
    {
        public IEnumerable<Comment> Comments { get; set; }
        public Comment SingleComment { get; set; }
        public int ThreadID { get; set; }
    }
}
