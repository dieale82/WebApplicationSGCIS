using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Models;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{

	[OperationContract]
	List<BEPersonType> GetPersonTypes();

	[OperationContract]
	List<BEPerson> GetPersons();

	[OperationContract]
	BEAnswer DeletePerson(int personId);

	[OperationContract]
	BEAnswer UpdatePerson(BEPerson person);

	[OperationContract]
	BEAnswer CreatePerson(BEPerson person);
}

