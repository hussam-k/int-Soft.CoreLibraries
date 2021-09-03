using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using IntSoft.DAL.Common;
using IntSoft.DAL.RepositoriesBase;
using Microsoft.AspNet.SignalR.Client;

namespace intSoft.MVC.Core.Utilities
{
    public class NotificationService<T>
        where T : class, IEntity
    {
        public void Notify(T model)
        {
            var repository = DependencyResolver.Current.GetService<Repository<T>>();
            repository.Save(model);
            var hub = DependencyResolver.Current.GetService<INotificationHub<T>>();
            hub.Notify(model);
        }

        public void NotifyRemote(T model, string serverUrl, string hubName, string hubMethod)
        {
            var repository = DependencyResolver.Current.GetService<Repository<T>>();
            repository.Save(model);
            var hubConnection = new HubConnection(serverUrl.Trim())
            {
                Credentials = CredentialCache.DefaultNetworkCredentials,
                TransportConnectTimeout = TimeSpan.FromSeconds(60),
                DeadlockErrorTimeout = TimeSpan.FromSeconds(60)
            };
            var hubProxy = hubConnection.CreateHubProxy(hubName.Trim());

            if (hubConnection.State == ConnectionState.Disconnected)
                hubConnection.Start().Wait();
            hubProxy.Invoke(hubMethod.Trim(), model.Id).Wait();
        }
    }
}