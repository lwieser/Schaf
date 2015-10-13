using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryPattern.Models;

namespace RepositoryPattern.Repositories
{
    /// <summary>
    /// Son rôle est : de charger et de mettre à jour mes données en mémoire ()
    /// </summary>
    public class MemoryPersonRepository : IPersonRepository
    {
        // Ma liste initiale de personnes
        private List<Person> _persons = new List<Person>
        {
            new Person {Id = 0, Age = 30, Nom = "Jean Noel"},
            new Person {Id = 1, Age = 35, Nom = "Naude"}
        };


        /// <summary>
        /// Obtient la liste des personnes
        /// </summary>
        public ICollection<Person> Persons
        {
            get
            {
                return  new Collection<Person>(_persons);
            }
        }


        /// <summary>
        /// Delete a person
        /// </summary>
        /// <returns>true if the person is deleted</returns>
        public bool Delete(Person p)
        {
            var deleted = false;

            if (p != null)
                deleted = _persons.Remove(p);

            return deleted;
        }
    }
}
