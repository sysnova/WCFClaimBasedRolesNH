using sysnova.Domain.Entities;
using sysnova.Infrastructure.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace sysnova.Services.CRUDService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string[] GetProducts(int value);

        [OperationContract]
        [FaultContract(typeof(GlobalErrorDetails))]
        string[] GetCategories(int value);

        [OperationContract]
        string DoAdd();

        [OperationContract]
        string DoAddCloud();

        [OperationContract]
        string DoAddHelpDesk();
        // TODO: Add your service operations here
    }
    
}
