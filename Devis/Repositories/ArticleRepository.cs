using Devis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devis.Repositories
{
    public class ArticleRepository : IRepositoryGeneric<QuoteArticle>
    {
        public ICollection<QuoteArticle> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(QuoteArticle obj)
        {
            using (Entities context = new Entities())
            {
                QuoteArticle dbArticle = context.Articles.First(x => x.Id == obj.Id);
                dbArticle.Quantity = obj.Quantity;
                dbArticle.Price = obj.Price;
                dbArticle.Unit = obj.Unit;
                dbArticle.Disbursed = obj.Disbursed;
                context.SaveChanges();
            }
        }
    }
}
