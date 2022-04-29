using System.ServiceModel;

namespace MTS_CRM.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ILoginService
    {
        [OperationContract]
        public bool LoginCheck(string username, string passwword);


        // TODO: Add your service operations here
    }

}
