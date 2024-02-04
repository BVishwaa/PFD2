using EldenGuide.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Google.Cloud.Firestore.V1.StructuredAggregationQuery.Types.Aggregation.Types;

namespace EldenGuide.Hubs
{
    public class ChatHub : Hub
    {
        private static List<RoomInfo> activeRooms = new List<RoomInfo>();

        public class RoomInfo
        {
            public string RoomName { get; set; }
            public bool IsAdminJoined { get; set; }
            public string user { get; set; }
        }



        public void CreateRoom(string groupName, string creator)
        {
            activeRooms.Add(new RoomInfo
            {
                RoomName = groupName,
                IsAdminJoined = false,
                user = creator
            });

            Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            UpdateAdminsAboutAvailableRooms();

            Console.WriteLine("Created");
            Console.WriteLine(string.Join(", ", activeRooms));
        }


        public void AdminJoinRoom(string roomName)
        {            
            var room = activeRooms.FirstOrDefault(r => r.RoomName == roomName);
            if (room != null)
            {
                room.IsAdminJoined = true;
                Groups.AddToGroupAsync(Context.ConnectionId, roomName);

                UpdateAdminsAboutAvailableRooms();
            }
        }

        public void AdminJoin(string roomName)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
        }

        public async Task SendMessage(string sender, string message, string selectedRoom)
        {
            await Clients.Group(selectedRoom).SendAsync("ReceiveMessage", sender, message);
            Console.WriteLine("test");
        }

        private void UpdateAdminsAboutAvailableRooms()
        {
            Clients.Group("Admins").SendAsync("AvailableRooms", activeRooms);
        }


        public void LeaveChat(string currentUser)
        {
            Console.WriteLine($"{currentUser}");
            var room = activeRooms.LastOrDefault(r => r.user == currentUser);
            if (room != null)
            {
                Console.WriteLine("Success");
                activeRooms.Remove(room);
                UpdateAdminsAboutAvailableRooms();
            }
        }

        public void GetRooms()
        {
            UpdateAdminsAboutAvailableRooms();
        }
    }
}
