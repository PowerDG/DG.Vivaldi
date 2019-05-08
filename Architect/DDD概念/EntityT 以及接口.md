要定义实体，可以从Entity、Entity<T&>、IEntity和IEntity<T>等类或接口中派生。这4个类或接口中，Entity派生于Entity<int>、 IEntity和IEntity<T>，使用整型作为实体的主键；Entity<T>是接口IEntity<T>的实现，也就是已经为你实现了接口的功能，不再需要自己去实现接口功能。从这4个类或接口的定义来看，一般情况下，我们从Entity类或Entity<T>类派生实体类就行，如果有特殊需求，就从接口中派生。

在定义实体类时，还可以为实体类添加以下常用接口用来实现一些常用功能：

    IHasCreationTime：为实体添加CreationTime属性，用来记录实体的创建时间
    IHasDeletionTime：为实体添加DeletionTime属性，用来记录实体的删除时间，这个只有在使用软删除的时候才有效。如果不是使用软删除，记录都删除了，这个字段没有任何意义。
    IHasModificationTime：为实体添加LastModificationTime属性，用来记录实体的最后修改时间
    
    ICreationAudited ：在IHasCreationTime的基础上添加CreatorUserId属性，用来记录创建实体的用户的Id
    IDeletionAudited：在IHasDeletionTime的基础上添加DeleterUserId属性，用来记录删除实体的用户的Id
    IModificationAudited：在IHasModificationTime的基础上添加LastModifierUserId属性，用来记录最后修改实体的用户的Id
    
    IAudited：ICreationAudited和IModificationAudited的合体，主要用于非软删除的情景
    IFullAudited：IAudited和IDeletionAudited的合体，主要用于软删除的情景
    ISoftDelete：为实体添加IsDeleted属性，用于判断实体是否已经被删除，主要用于软删除的情景
    
    IPassivable：为实体添加IsActive属性，用于判断实体是否处于活跃状态
    IMayHaveTenant：为实体添加TenantId属性，用于指定实体所属的租户。该属性允许值为null，也就是可以指定租户，也可以不指定
    IMustHaveTenant：该接口与IMayHaveTenant接口的主要区别是，必须指定租户
    IExtendableObject：为实体添加ExtensionData属性，用于存储JSON格式的数据。在实体中可通过SetData方法来设置存储的数据，通过GetData来获取存储的数据
---------------------
作者：上将军 
来源：CSDN 
原文：https://blog.csdn.net/tianxiaode/article/details/78880387 
版权声明：本文为博主原创文章，转载请附上博文链接！