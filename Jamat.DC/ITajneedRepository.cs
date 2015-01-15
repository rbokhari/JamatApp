﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.EntityFramework;

namespace Jamat.DC
{
    public interface ITajneedRepository
    {
        Task<IQueryable<Tajneed>> GetTajneedList();

        Task<Int32> GetTajneedCount();

        IQueryable<TajneedCount> GetTajneedAuxilaryCount();
        IQueryable<TajneedCount> GetTajneedRegionCount();

        IQueryable<TajneedCount> GetTajneedNationalityCount();

        IQueryable<TajneedCount> GetTajneedWassiyatCount();

        IQueryable<Tajneed> GetTajneed(int id);

        bool Save();

        bool AddTajneed(Tajneed newTajneed);

        bool UpdateTajneed(Tajneed updateTajneed);

        bool AddIncome(TajneedIncome newIncome);

        bool UpdateIncome(TajneedIncome updateIncome);

    }

    public class TajneedCount
    {
        public Int32 CountId;
        public string CountName;
        public Int32 CountTotal;
        public string CountColor;
    }

}
