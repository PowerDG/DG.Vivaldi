using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Linq;
using Abp.Linq.Extensions;
using Abp.Extensions;
using Abp.UI;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using DgCore.DgEnitity;
using Abp.Domain.Uow;

namespace DgCore.DgEnitity
{
    public class BillInfoManager : DomainService
    {
      
        private readonly IRepository<BillInfo, long> _BillInfoRepository;
        public BillInfoManager(IRepository<BillInfo, long> BillInfoRepository)
        {
            _BillInfoRepository = BillInfoRepository;
          
        }

        [UnitOfWork]
        public BillInfo UP(BillInfo task  )
        {

            var task_old = _BillInfoRepository.FirstOrDefault(t => t.Id == task.Id);
            task_old.ChangeIsCandidate();
            _BillInfoRepository.Update(task_old);

            task.Id = Snowflake.Instance().GetId();
            task.UpVersion();
            var result=_BillInfoRepository.Insert(task);
            CurrentUnitOfWork.SaveChanges();
            return result;
        }

        public IList<BillInfo> GETALL()
        {
            var task = _BillInfoRepository.GetAll()//.Where(t =>t.IsCandidate == false)
               .OrderBy(t =>t.IsCandidate)
               .OrderBy(t=>t.Version)
               .OrderBy(t=>t.CreationTime)
               .ToList();
            return task;
        }
    }
}
