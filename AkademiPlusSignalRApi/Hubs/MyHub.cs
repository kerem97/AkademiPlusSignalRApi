using AkademiPlusSignalRApi.DAL;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AkademiPlusSignalRApi.Hubs
{
    public class MyHub : Hub
    {
        //Kişileri tutacak olan liste
        public static List<string> Names { get; set; } = new List<string>();
        // Anlık olarak bağlı olan client sayısını gösterecek property
        public int ClientCount { get; set; } = 0;

        // Bir odada bulunabilecek kişi sayısı
        public static int RoomCount { get; set; } = 5;

        private readonly Context _context;

        public MyHub(Context context)
        {
            _context = context;
        }
        public async Task sendname(string name)
        {
            Names.Add(name);
            await Clients.All.SendAsync("receivername", name);
        }

        public async override  Task OnConnectedAsync()
        {
            ClientCount++;
            await Clients.All.SendAsync("receiveclientcount", ClientCount);
        }

        public async override  Task OnDisconnectedAsync(Exception exception)
        {
            ClientCount--;
            await Clients.All.SendAsync("receiveclientcount", ClientCount);
        }
    }
}
