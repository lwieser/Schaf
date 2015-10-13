using System.Collections.Generic;
using RepositoryPattern.Models;

namespace RepositoryPattern.Repositories
{
    public interface IPersonRepository
    {
        /// <summary>
        /// Obtient la liste des personnes
        /// </summary>
        ICollection<Person> Persons { get; }

        /// <summary>
        /// Delete a person
        /// </summary>
        /// <returns>true if the person is deleted</returns>
        bool Delete(Person p);
    }
}