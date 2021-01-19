using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repository
{
    public interface ITokIgrePoKorisnikuRepository :IRepository<TokIgrePoKorisniku>
    {
        IList<TokIgrePoKorisniku> VratiTokIgrePoKorisnikuZaTokIgre(int idTokaIgre);
    }
}
