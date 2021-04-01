using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repository
{
    public interface ISobaRepository : IRepository<Soba>
    {
        void DeleteAllByCategoryId(int idKateg);
        IList<Soba> GetAllRoomByCategoryId(int idKateg);

        void CloseRoomById(int idSobe);
    }
}
