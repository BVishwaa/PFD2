using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EldenGuide.Hubs
{
    public class ChatHub : Hub
    {
        private static List<RoomInfo> activeRooms = new List<RoomInfo>();

        // Define a class to hold room information
        public class RoomInfo
        {
            public string RoomName { get; set; }
            public bool IsAdminJoined { get; set; }
        }

        // Method for users to create a room
        public void CreateRoom()
        {
            // Generate a unique group name (e.g., a GUID)
            string groupName = Guid.NewGuid().ToString();

            // Add the room to the list of available rooms
            activeRooms.Add(new RoomInfo
            {
                RoomName = groupName,
                IsAdminJoined = false // Initially, no admin has joined
            });

            // Create the group
            Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            // Send updates to admin clients
            UpdateAdminsAboutAvailableRooms();

            Console.WriteLine("Created");
            Console.WriteLine(string.Join(", ", activeRooms));
        }

        // Method for admins to get the list of available rooms
        public IEnumerable<RoomInfo> GetAvailableRooms()
        {
            return activeRooms;
        }

        // Method for admins to join a room
        public void AdminJoinRoom(string roomName)
        {
            // Find the room and mark it as joined by admin
            var room = activeRooms.FirstOrDefault(r => r.RoomName == roomName);
            if (room != null)
            {
                room.IsAdminJoined = true;

                // Add admin to the group for the room
                Groups.AddToGroupAsync(Context.ConnectionId, roomName);

                // Send updates to admin clients
                UpdateAdminsAboutAvailableRooms();
            }
        }

        public void AdminJoin(string roomName)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
        }

        public async Task SendMessage(string user, string message)
        {
            Clients.All.SendAsync("ReceiveMessage", user, message);
            Console.WriteLine("test");
        }

        // Helper method to send available rooms to admin clients
        private void UpdateAdminsAboutAvailableRooms()
        {
            Clients.Group("Admins").SendAsync("AvailableRooms", activeRooms);
        }
    }
}
