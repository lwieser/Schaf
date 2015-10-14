using Devis.Models;
using Devis.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devis.Repositories
{
    public class RepositoryGeneric<T> :  IRepositoryGeneric<T> where T : class
    { 
        public ICollection<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
