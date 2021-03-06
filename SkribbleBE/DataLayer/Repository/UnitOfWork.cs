﻿using System;
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

        private IPotezRepository potezRepository;
        private ITokIgreRepository tokIgreRepository;
        private ITokIgrePoKorisnikuRepository tokIgrePoKorisnikuRepository;

        private IKorisnikRepository korisnikRepository;
        private ISobaRepository sobaRepository;
        private IKorisniciPoSobiRepository korisniciPoSobiRepository;

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

        public IPotezRepository PotezRepository
        {
            get
            {
                if (potezRepository == null)
                    potezRepository = new PotezRepository(_context);
                return potezRepository;
            }
        }

        public ITokIgreRepository TokIgreRepository
        {
            get
            {
                if (tokIgreRepository == null)
                    tokIgreRepository = new TokIgreRepository(_context);
                return tokIgreRepository;
            }
        }

        public ITokIgrePoKorisnikuRepository TokIgrePoKorisnikuRepository
        {
            get
            {
                if (tokIgrePoKorisnikuRepository == null)
                    tokIgrePoKorisnikuRepository = new TokIgrePoKorisnikuRepository(_context);
                return tokIgrePoKorisnikuRepository;
            }
        }
        public IKorisnikRepository KorisnikRepository
        {
            get
            {
                if (korisnikRepository == null)
                    korisnikRepository = new KorisnikRepository(_context);
                return korisnikRepository;
            }
        }
        public ISobaRepository SobaRepository
        {
            get
            {
                if (sobaRepository == null)
                    sobaRepository = new SobaRepository(_context);
                return sobaRepository;
            }
        }
        public IKorisniciPoSobiRepository KorisniciPoSobiRepository
        {
            get
            {
                if (korisniciPoSobiRepository == null)
                    korisniciPoSobiRepository = new KorisniciPoSobiRepository(_context);
                return korisniciPoSobiRepository;
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
