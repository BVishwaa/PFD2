using Google.Cloud.Firestore;

namespace EldenGuide.Models
{
    [FirestoreData]
    public class Videocall
    {
        [FirestoreProperty]
        public string URL { get; set; }

        [FirestoreProperty]
        public string Title { get; set; }

        [FirestoreProperty]
        public string DateCreated { get; set; }
    }
}
