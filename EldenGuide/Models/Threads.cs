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
        public string Category { get; set; }
        public string Description { get; set; }
    }
}
