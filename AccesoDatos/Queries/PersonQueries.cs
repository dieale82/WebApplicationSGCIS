using AccesoDatos.Mer;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace AccesoDatos.Queries
{
    public class PersonQueries
    {
        private SGCISEntities _objBdContext;

        public PersonQueries()
        {
            _objBdContext = new SGCISEntities();
        }

        public List<BEPerson> GetPersonList()
        {
            var personList = (from person in _objBdContext.Person
                                     join personType in _objBdContext.PersonType on person.PersonTypeId equals personType.PersonTypeId
                                     select new BEPerson
                                     {
                                         PersonId = person.PersonId,
                                         PersonTypeId = person.PersonTypeId,
                                         PersonName = person.PersonName,
                                         PersonAge = (int)person.PersonAge,
                                         PersonTypeDescription = personType.PersonTypeDescription,
                                     }).ToList();
            return personList;
        }

        public BEAnswer DeletePerson(int personId)
        {
            try {
                var personToDelete = _objBdContext.Person.Where(x => x.PersonId.Equals(personId)).FirstOrDefault();
                _objBdContext.Person.Remove(personToDelete);
                _objBdContext.SaveChanges();

                return new BEAnswer()
                {
                    answerTypeId = (int)AnswerTypes.Succesful,
                    rowId = personId,
                    message = "Person deleted"
                };
            }
            catch {
                return new BEAnswer()
                {
                    answerTypeId = (int)AnswerTypes.Error,
                    rowId = personId,
                    message = "Internal error while deleting person"
                };
            }            
        }

        public BEAnswer UpdatePerson(BEPerson person)
        {
            try
            {
                var personToUpdate = _objBdContext.Person.Where(x => x.PersonId.Equals(person.PersonId)).FirstOrDefault();
                personToUpdate.PersonName = person.PersonName;
                personToUpdate.PersonTypeId = person.PersonTypeId;
                personToUpdate.PersonAge = person.PersonAge;
                _objBdContext.SaveChanges();

                return new BEAnswer()
                {
                    answerTypeId = (int)AnswerTypes.Succesful,
                    rowId = person.PersonId,
                    message = "Person updated"
                };
            }
            catch
            {
                return new BEAnswer()
                {
                    answerTypeId = (int)AnswerTypes.Error,
                    rowId = person.PersonId,
                    message = "Internal error while updating person"
                };
            }
        }

        public BEAnswer CreatePerson(BEPerson person)
        {
            try
            {
                Person personToAdd = MapToEntity(person);
                _objBdContext.Person.Add(personToAdd);
                _objBdContext.SaveChanges();

                return new BEAnswer()
                {
                    answerTypeId = (int)AnswerTypes.Succesful,
                    rowId = personToAdd.PersonId,
                    message = "Person added"
                };
            }
            catch
            {
                return new BEAnswer()
                {
                    answerTypeId = (int)AnswerTypes.Error,
                    rowId = person.PersonId,
                    message = "Internal error while adding person"
                };
            }
        }

        private Person MapToEntity(BEPerson person)
        {
            return new Person()
            {
                PersonTypeId = person.PersonTypeId,
                PersonName = person.PersonName,
                PersonAge = person.PersonAge,
            };
        }
    }
}
