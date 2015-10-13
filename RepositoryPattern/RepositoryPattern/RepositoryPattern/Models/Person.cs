using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Models
{
    /// <summary>
    /// Ceci est la seule classe de mon modèle
    /// </summary>
    public class Person
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public  int Age { get; set; }
    }
}
