using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTS_CRM.MODEL
{
    public class ChatHub : Hub
    {
        private static List<EmployeeChat> ConnectedEmp = new List<EmployeeChat>();
        private static List<Message> Messages = new List<Message>();

        public void Connect(string userName)
        {
            var id = Context.ConnectionId;
            if (ConnectedEmp.Count(x => x.ConnID == id) == 0)
            {
                ConnectedEmp.Add(new EmployeeChat { ConnID = id, Username = userName });
                Clients.Caller.onConnected(id, userName, ConnectedEmp, Messages);
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }

        }
        private void AddMessageinCache(string userName, string message)
        {
            Messages.Add(new Message { Username = userName, Messaging = message });

            if (Messages.Count > 100)
                Messages.RemoveAt(0);
        }
        public void SendMessageToAll(string userName, string message)
        {

            AddMessageinCache(userName, message);
            Clients.All.messageReceived(userName, message);

        }
        public void SendPrivateMessage(string toEmpId, string message)
        {

            string fromEmpId = Context.ConnectionId;

            var toEmployee = ConnectedEmp.FirstOrDefault(x => x.ConnID == toEmpId);
            var fromEmployee = ConnectedEmp.FirstOrDefault(x => x.ConnID == fromEmpId);

            if (toEmployee != null && fromEmployee != null)
            {
                // send to 
                Clients.Client(toEmpId).sendPrivateMessage(fromEmpId, fromEmployee.Username, message);

                // send to caller user
                Clients.Caller.sendPrivateMessage(toEmpId, fromEmployee.Username, message);
            }

        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var item = ConnectedEmp.FirstOrDefault(x => x.ConnID == Context.ConnectionId);
            if (item != null)
            {
                ConnectedEmp.Remove(item);

                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Username);

            }
            return base.OnDisconnected(stopCalled);
        }
    }
}
