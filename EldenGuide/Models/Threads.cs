using Google.Cloud.Firestore;
using System.Threading.Tasks;

namespace EldenGuide.Models
{
    [FirestoreData]
    public class Threads
    {
        [FirestoreProperty]
        public int ThreadID { get; set; }
        [FirestoreProperty]
        public string Title { get; set; }
        [FirestoreProperty]
        public string Category { get; set; }
        [FirestoreProperty]
        public string Description { get; set; }
        [FirestoreProperty]
        public string DatePosted { get; set; }
        [FirestoreProperty]
        public string Username { get; set; }
    }
}
