
#代码生成器(ABP Code Power Tools )使用说明文档

**52ABP官方网站：[http://www.52abp.com](http://www.52abp.com)**

>欢迎您使用 ABP Code Power Tools ，.net core 版本。
开发代码生成器的初衷是为了让大家专注于业务开发，
而基础设施的地方，由代码生成器实现，节约大家的实现。
实现提高效率、共赢的局面。

欢迎到：[Github地址](https://github.com/52ABP/52ABP.CodeGenerator) 提供您的脑洞，
如果合理的我会实现哦~

# 使用说明:

**配置Automapper** :

复制以下代码到Application层下的：ERMApplicationModule.cs
中的 PreInitialize 方法中:

```
// 自定义类型映射
// 如果没有这一段就把这一段复制上去
Configuration.Modules.AbpAutoMapper().Configurators.Add(configuration =>
{
    // ....

    // 只需要复制这一段
ExtendInfoMapper.CreateMappings(configuration);

    // ....
});

```

**配置权限功能**  :

如果你生成了**权限功能**。复制以下代码到 ERMApplicationModule.cs
中的 PreInitialize 方法中:

```
Configuration.Authorization.Providers.Add<ExtendInfoAuthorizationProvider>();

```

**EntityFramework功能配置**:

可以在```DbContext```中增加：

 ```
public DbSet<ExtendInfo>  ExtendInfos { get; set; }

 ```

在方法```OnModelCreating```中添加

```
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExtendInfoCfg());
        }

```


**多语言配置**  

.Core 层下 Localization->SourceFiles 中

```

<text name="TenantId"  value="TenantId"></text>
<text name="ParentId"  value="ParentId"></text>
<text name="Super_Type"  value="Super_Type"></text>
<text name="EntityTypeFullName"  value="EntityTypeFullName"></text>
<text name="Name"  value="Name"></text>
<text name="Value"  value="Value"></text>
<text name="DataTypeName"  value="DataTypeName"></text>
<text name="Description"  value="Description"></text>
<text name="DisplayName"  value="DisplayName"></text>
<text name="CreatorUserId"  value="CreatorUserId"></text>
<text name="CreationTime"  value="CreationTime"></text>
<text name="IsDeleted"  value="IsDeleted"></text>


<text name="ExtendInfo" value=""></text><text name="QueryExtendInfo"  value="查询"></text><text name="CreateExtendInfo"  value="添加"></text><text name="EditExtendInfo"  value="编辑"></text><text name="DeleteExtendInfo"  value="删除"></text><text name="BatchDeleteExtendInfo" value="批量删除"></text><text name="ExportExtendInfo"  value="导出"></text>                             

```




 **路线图**

todo: 目前优先完成SPA 以angular 为主，
如果你有想法我替你实现前端生成的代码块。
那么请到github 贴出你的代码段。
我感兴趣的话，会配合你的。

[https://github.com/52ABP/52ABP.CodeGenerator](https://github.com/52ABP/52ABP.CodeGenerator) 提供您的脑洞，

已完成：
- [x ]SPA版本的前端

待办：
- [ ]暂时搞不定注释，后期想办法
- [ ]菜单栏问题，如果是MPA版本
- [ ]MPA版本的前端
## 广告

52ABP官方网站：[http://www.52abp.com](http://www.52abp.com)

代码生成器帮助文档：[http://www.cnblogs.com/wer-ltm/p/8445682.html](http://www.cnblogs.com/wer-ltm/p/8445682.html)

【ABP代码生成器交流群】：104390185（收费）
[![52ABP .NET CORE 实战群](http://pub.idqqimg.com/wpa/images/group.png)](http://shang.qq.com/wpa/qunwpa?idkey=3f301fa3101d3201c391aba77803b523fcc53e59d0c68e6eeb9a79336c366d92)

【52ABP .NET CORE 实战群】：633751348 (免费)
[![52ABP .NET CORE 实战群](http://pub.idqqimg.com/wpa/images/group.png)](https://jq.qq.com/?_wv=1027&k=5pWtBvu)
