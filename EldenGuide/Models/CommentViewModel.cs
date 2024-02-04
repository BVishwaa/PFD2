namespace EldenGuide.Models
{
    public class CommentViewModel
    {
        //public IEnumerable<Comment> Comments { get; set; }
        public Comment SingleComment { get; set; }
        public IEnumerable<Threads> Threads { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

        public CommentViewModel()
        {
            Threads = new List<Threads>();
            Comments = new List<Comment>();
        }
    }
}
