using Google.Cloud.Firestore;

namespace EldenGuide.Models
{
    [FirestoreData]
    public class Event
    {
        [FirestoreProperty]
        public int EventID { get; set; }
        [FirestoreProperty]
        public string EventName { get; set; }
        [FirestoreProperty]
        public string Details { get; set; }
        [FirestoreProperty]
        public string EventPhoto { get; set; }
    }
}
