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

