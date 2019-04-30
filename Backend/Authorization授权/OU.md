```js
  "ConnectionStrings": {
    "Default": "User ID=postgres;Password=wsx1001;Host=localhost;Port=5432;Database=ERMDbVue;Pooling=true;",
    "Default2": "Server=localhost; Database=ERMDbVue; Trusted_Connection=True;"
  },
```

工具 - > NuGet软件包管理器 - >软件包管理器控制台 
//创建模型的初始表 
**Add-Migration InitialCreate** 
//将新迁移应用于数据库 
**Update-Database**





组织	人员	角色	权限








```csharp
 public class DgOrganizationUnit : Entity<int>
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Level { get; set; }
        public int Sorting { get; set; }
        public object[] ExtendedAttributes { get; set; }
        public string Operator { get; set; }
        public string RecordCreateTime { get; set; }
        public string RecordUpdateTime { get; set; }

        public int RecordStatus { get; set; }


        public DgOrganizationUnit Parent { get; set; }
        public long? ParentId { get; set; }
        public DgOrganizationUnit[] Children { get; set; }
        public string description { get; set; }
        public bool isActive { get; set; }

    }
}


```

严重性	代码	说明	项目	文件	行	禁止显示状态
警告	CS0108	“DgOrganizationUnit.CodeUnitLength”隐藏继承的成员“OrganizationUnit.CodeUnitLength”。如果是有意隐藏，请使用关键字 new。	DG.ERM.Core	E:\DgHub\Dg.ERM\source\DG.ERMVue\4.6.0\aspnet-core\src\DG.ERM.Core\DgOrganizationUnits\DgOrganizationUnit.cs	30	活动的

