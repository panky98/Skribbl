using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProjekatContext _context;

        //Repositories
        private IRecRepository recRepository;
        private IKategorijaRepository kategorijaRepository;
        private IRecPoKategorijiRepository recPoKategorijiRepository;

        public UnitOfWork(ProjekatContext projekatContext)
        {
            this._context = projekatContext;
        }

        #region PropsForRepositories
        public IRecRepository RecRepository
        {
            get
            {
                if (recRepository == null)
                    recRepository = new RecRepository(_context);
                return recRepository;
            }
        }
        public IKategorijaRepository KategorijaRepository
        {
            get
            {
                if (kategorijaRepository == null)
                    kategorijaRepository = new KategorijaRepository(_context);
                return kategorijaRepository;
            }
        }

        public IRecPoKategorijiRepository RecPoKategorijiRepository
        {
            get
            {
                if (recPoKategorijiRepository == null)
                    recPoKategorijiRepository = new RecPoKategorijiRepository(_context);
                return recPoKategorijiRepository;
            }
        }
        #endregion

        public void Commit()
        {
            this._context.SaveChanges();
        }

        public void Rollback()
        {
            this._context.Dispose();
        }
    }
}
