using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DgCore.DgEnitity
{
    public class BillInfo : Entity<long>, ICreationAudited, IDeletionAudited, IModificationAudited
    {
      
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None )]  //取消主键自增长，否则出现显示主键inseret 出错
        public override long Id { get; set; }
   
        public int BillNo { get; set; }

        [DefaultValue(false)]
        public bool IsCandidate { get; set; }

        [DefaultValue(1)]
        public int Version { get; set; }

        public string SendBranchID { get; set; }
        [DefaultValue(false)]
        public bool BillCheck { get; set; }
        public int BillStateID { get; set; }

        public long BalanceNo { get; set; }
        public string ExpressBillNo { get; set; }
        public string ExpressNo { get; set; }
        public string Secondary_contact { get; set; }
        public string Secondary_Tel { get; set; }
        public string MerchandiserName { get; set; }
        public long MerchandiserId { get; set; }
        public string CompanyAbbreviation { get; set; }

        public string ShipperCompanyName { get; set; }
        public string ShipperAccount_No { get; set; }
        public string ShipperName { get; set; }
        public string ShipperTel { get; set; }
        public string ShipperPostCode { get; set; }
        public string ShipperCity { get; set; }
        public string ShipperProvince { get; set; }
        public string ShipperAddress { get; set; }

        public string ReceiversCompanyName { get; set; }
        public string ReceiversAccount_No { get; set; }
        public string ReceiversName { get; set; }
        public string ReceivingTel { get; set; }
        public string ReceivingPostCode { get; set; }
        public string ReceivingCity { get; set; }
        public string ReceivingProvince { get; set; }
        public string ReceivingAddress { get; set; }
        public string Totalnumberofpackages { get; set; }
        public string Totalweight { get; set; }
        public string Volume { get; set; }
        public string Measurementrules { get; set; }

        public string Volume_weight { get; set; }
        public string Chargeableweight { get; set; }

        public bool Has_return { get; set; }
        public string SERVICELEVEL { get; set; }
        public string TransportationMode { get; set; }
        public string Receivingdates  { get; set; }
        public string CONTENT { get; set; }

        public string Value { get; set; }
        public bool HasInsured { get; set; }


        public string OTHER { get; set; } 
		

        //支付方  WHO
        public string PaymentType { get; set; }

        public string CHARGES { get; set; } 



        #region 应付

        public string The_cost { get; set; }
        public string TRANSPORTATION2 { get; set; } 
        public string Remark2 { get; set; } 
		
		public string Distribution2 { get; set; }
        public string Delivery2 { get; set; }
        public string Transfer2 { get; set; }
        public string Packing2 { get; set; }
        public string Pallet2 { get; set; } 
        public string Upstairs2 { get; set; } 
        public string OTHER2 { get; set; }


        //public string TOTAL_CHARGES2 { get; set; }

        #endregion


        //-支付方式 



        public bool isPacking { get; set; }
        public bool isUpstairs { get; set; }


        //是否启用税点计算
        public bool isTax { get; set; } 
        public int Tax_point { get; set; } 

        #region 应收

        public string TRANSPORTATION { get; set; } 
        public string Remark { get; set; }
		
		public string Distribution { get; set; }
        public string Delivery { get; set; }
        public string Transfer { get; set; }
        public string Packing { get; set; }
        public string Pallet { get; set; } 
        public string Upstairs { get; set; }
		
		
        public string Other_fees { get; set; }//!!!!!!!暂无
        public string TOTAL_CHARGES { get; set; }
        #endregion 
		
        public string BillImgUrl { get; set; }


        #region  系统软判断
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }


        #endregion

        public void UpBillCheckToTrue()
        {
            BillCheck = true;           
        }
        public void UpVersion()
        {
            Version = Version + 1;
        }
        public void ChangeIsCandidate()
        {
            IsCandidate = true;
        }

    }

}
