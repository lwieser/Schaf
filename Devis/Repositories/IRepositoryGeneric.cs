using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devis.Repositories
{
    interface IRepositoryGeneric<T> where T : class
    {
        ICollection<T> GetAll();
    }
}
