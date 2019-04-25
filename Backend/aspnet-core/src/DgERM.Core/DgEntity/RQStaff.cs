using System;
using System.Collections.Generic;
using System.Text;

using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;


namespace DgCore.DgEnitity
{


    public class RQStaffs : Entity, IFullAudited
    {


        public virtual bool IsEmailConfirmed { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string Password { get; set; }


        public virtual int? GroupID { get; set; }
        public virtual string GroupName { get; set; }

        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string FullName { get { return this.Name + " " + this.Surname; } }



        public RQStaffs()
        {
            CreationTime = DateTime.Now;
        }

        #region Entity---->IFullAudited

        #region 数据版本号
        public int VersionCode { get; set; }
        #endregion

        #region   激活/未激活
        public bool IsActive { get; set; }
        //一些实体需要标记为激活的或未激活的。    
        //            这样，你就可以根据实体的激活或者未激活状态来采取行动。
        //            你可以实现IPassivable接口来达到目的。该接口定义了IsActive属性。 
        #endregion
        //public interface IAudited : ICreationAudited, IModificationAudited
        #region 创建者Creator 
        //public interface ICreationAudited : IHasCreationTime
        public long? CreatorUserId{get;set;}
        public DateTime CreationTime{get;set;}

        #endregion

        #region  历史修改
        //public interface IModificationAudited : IHasModificationTime
        public long? LastModifierUserId{get;set;}
        public DateTime? LastModificationTime{get;set;}

        #endregion

        #region  软删除
        //public interface IDeletionAudited : ISoftDelete
        //软删除是将一个实体标记为已删除的通常使用的模式，而不是直接从数据库中删除。
        //    比如，你可能不想从数据库中硬删除一个User，因为它可能关联其他的表
        public long? DeleterUserId{get;set;}
        public DateTime? DeletionTime{get;set;}
        public bool IsDeleted{get;set;}

        #endregion

        #endregion
    }
}