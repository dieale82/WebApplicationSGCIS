using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Models;
using AccesoDatos.Queries;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
	private PersonQueries _personQueries;
	private PersonTypeQueries _personTypeQueries;

	public Service()
	{
		_personQueries = new PersonQueries();
		_personTypeQueries = new PersonTypeQueries();
	}

	public List<BEPerson> GetPersons()
	{
		return _personQueries.GetPersonList();
	}

	public List<BEPersonType> GetPersonTypes()
	{	
		return _personTypeQueries.GetPersonTypeList();
	}

	public BEAnswer DeletePerson(int personId)
	{
		return _personQueries.DeletePerson(personId);
	}

	public BEAnswer UpdatePerson(BEPerson person)
	{
		return _personQueries.UpdatePerson(person);
	}

	public BEAnswer CreatePerson(BEPerson person)
	{
		return _personQueries.CreatePerson(person);
	}
}
