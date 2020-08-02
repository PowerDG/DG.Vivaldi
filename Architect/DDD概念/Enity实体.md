



实体类型


### 非自增型
```csharp
namespace RQCore.RQEnitity
{
    public class T_ExpressType:Entity<int>
    {
      
        public new int?Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ExpressNo { get; set; }
        
        
     
```

```csharp
   // 插入前判断是否重复No
           public string InsertExpress(SearchT_ExpressTypeDto dto)
        {
            var task = _T_ExpressTypeRepository
            .FirstOrDefault(t => t.ExpressNo == dto.ExpressNo);
            if(task!=null)
            {
                return "快递商编码不能重复，请重新设置！！";
            }
            T_ExpressType Express = new T_ExpressType();
            try
            {
                Express.ExpressName = dto.ExpressName;
                Express.ExpressNo = dto.ExpressNo;
                Express.IsDefaultShow = bool.Parse(dto.IsDefaultShow.ToString());
                Express.Remark = dto.Remark;
                //Express = Mapper.Map<T_ExpressType>(dto);
            }
            catch (Exception e)
            {             
                //throw new Exception("Error!!!!!");
            }
          
            _T_ExpressTypeRepository.Insert(Express); 
            return "新增成功";
        }
```





### 自增型

```csharp
namespace RQCore.RQEnitity
{
    public class InvoiceInfo : Entity<int>, IFullAudited
    {
        //Add-Migration init
        //    Update-Database init
           [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int? Id { get; set; }
```

```csharp
       [UnitOfWork]
        public int? CreateMissionQ(InvoiceInfoDto input)
        {
            var task = Mapper.Map<InvoiceInfo>(input);
            task.Id = null;
            if (task != null)
            {
                var result = _InvoiceRepository.Insert(task);
                CurrentUnitOfWork.SaveChanges();

                return result.Id;
            }
            else
            { return 0; }
        }
```





###  没标明



````csharp
namespace RQCore.RQEnitity
{
    public class DepartmentInfo : Entity, ICreationAudited, IDeletionAudited
    {
        public new int? Id { get; set; }
        [Key]
        public int DepartmentID { get; set; }
````

```csharp
        public int CreateMissionQ(DepartmentInfoDto input)
        {
            var task = Mapper.Map<DepartmentInfo>(input);
            if (task != null)
            {
                int result = _userRepository.InsertAndGetId(task);
                return result;
            }
            else
            { return 0; }
        }
```

