using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace DgCore.DgEnitity
{
    public class Invoice : Entity<int>, IFullAudited
    {
        //Add-Migration init
        //    Update-Database init
           [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int? Id { get; set; }
        //public int UserID { get; set; }
        //发票批号
        public uint InvoiceserialNo { get; set; } 
        public uint InvoiceNo { get; set; } //发票编号



        public int BranchID { get; set; }
        public string BranchName { get; set; }
        #region  其他  标识
        public uint InvoiceStateID { get; set; }
        public uint InvoiceBillNo { get; set; }

        public uint Sorting { get; set; }
        //public string ID { get; set; }

        public bool IsCandidate { get; set; }
        public uint Version { get; set; }
        public uint BillCheck { get; set; }
        public string Claime_Type { get; set; }
        public string Claim_Name { get; set; }


        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        #endregion

         

        #region

        #endregion

          



        #region  下半身
        public decimal Total { get; set; }
        public string Price_Plus_Taxes { get; set; }
        public decimal Arabic_Numbers { get; set; }

        public string Payee { get; set; }
        public string Review { get; set; }
        public string Drawer { get; set; }
        public string The_Seller { get; set; }

        #endregion





        #region   客户 购买方纳税信息

        public string CompanyName { get; set; }
        public string Taxpayer_identification_number { get; set; }
        public string Registered_address { get; set; }
        public string Primary_Tel { get; set; }
        public string Opening_bank { get; set; }
        public string Bank_account_number { get; set; }
        #endregion



        #region  我们 瑞庆方纳税信息
        public string MyCompanyName { get; set; }
        public string MyTaxpayer_identification_number { get; set; }
        public string MyRegistered_address { get; set; }
        public string MyPrimary_Tel { get; set; }
        public string MyOpening_bank { get; set; }
        public string MyBank_account_number { get; set; }

        #endregion








    }
}
