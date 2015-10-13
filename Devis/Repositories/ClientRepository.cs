using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devis.Models;

namespace Devis.Repositories
{
    public class ClientRepository
    {
        internal List<Client> GetAll()
        {
            using (Entities context = new Entities())
            {
                return context.Clients.ToList();
            }
        }

        internal Client Get(int clientId)
        {
            using (Entities context = new Entities())
            {
                return context.Clients.First(x => x.Id == clientId);
            }
        }
    }
}
