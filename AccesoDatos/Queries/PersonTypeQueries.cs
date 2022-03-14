using AccesoDatos.Mer;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace AccesoDatos.Queries
{
    public class PersonTypeQueries
    {
        private SGCISEntities _objBdContext;

        public PersonTypeQueries()
        {
            _objBdContext = new SGCISEntities();
        }

        public List<BEPersonType> GetPersonTypeList()
        {
            var personTypeList = _objBdContext.PersonType.ToList();
            return MaptoBE(personTypeList);
        }

        private List<BEPersonType> MaptoBE(List<PersonType> personTypeEntity)
        {
            List<BEPersonType> personTypes = new List<BEPersonType>();
            foreach (PersonType personType in personTypeEntity)
            {
                personTypes.Add(new BEPersonType()
                {
                    PersonTypeId = personType.PersonTypeId,
                    PersonTypeDescription = personType.PersonTypeDescription
                });
            }

            return personTypes;
        }
    }
}
