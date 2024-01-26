using Google.Cloud.Firestore;

namespace EldenGuide.Models
{
    [FirestoreData]
    public class Comment
    {
        [FirestoreProperty]
        public int CommentID { get; set; }
        [FirestoreProperty]
        public string CommentText { get; set; }
        [FirestoreProperty]
        public string Username { get; set; }
        [FirestoreProperty]
        public string DateCommented { get; set; }
    }
}
