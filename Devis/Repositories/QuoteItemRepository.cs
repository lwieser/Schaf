using Devis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devis.Repositories
{
    public class QuoteItemRepository : RepositoryGeneric<QuoteItem>
    {
        public override void Update(QuoteItem obj)
        {
            using (Entities context = new Entities())
            {
                QuotePackage package = obj as QuotePackage;
                QuoteArticle article = obj as QuoteArticle;
                QuoteEntry entry = obj as QuoteEntry;

                if (package != null)
                {
                    context.Packages.Attach(package);
                }
                else if(article != null)
                {
                    context.Articles.Attach(article);
                }
                else if (entry != null)
                {
                    context.Entries.Attach(entry);
                }

                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
