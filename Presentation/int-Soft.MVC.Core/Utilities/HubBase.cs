using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using intSoft.MVC.Core.Extensions;
using Microsoft.AspNet.SignalR;

namespace intSoft.MVC.Core.Utilities
{
    public abstract class HubBase : Hub
    {
        protected static readonly Dictionary<Guid, List<string>> Connections = new Dictionary<Guid, List<string>>();

        public override Task OnConnected()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                var userId = Context.User.Identity.GetUserGuid();
                if (Connections.ContainsKey(Context.User.Identity.GetUserGuid()))
                {
                    Connections[userId].Add(Context.ConnectionId);
                }
                else
                {
                    Connections.Add(userId, new List<string> {Context.ConnectionId});
                }
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                var userId = Context.User.Identity.GetUserGuid();
                Connections[userId].Remove(Context.ConnectionId);
                if (!Connections[userId].Any())
                {
                    Connections.Remove(userId);                    
                }
            }
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                var userId = Context.User.Identity.GetUserGuid();
                if (Connections.ContainsKey(Context.User.Identity.GetUserGuid()))
                {
                    Connections[userId].Add(Context.ConnectionId);
                }
                else
                {
                    Connections.Add(userId, new List<string> { Context.ConnectionId });
                }
            }
            return base.OnReconnected();
        }
    }
}