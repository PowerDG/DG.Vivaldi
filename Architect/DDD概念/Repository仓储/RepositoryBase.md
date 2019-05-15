asp.net core 实现一个简单的仓储

https://blog.csdn.net/li123128/article/details/78684542

2017年12月01日 11:06:15 li123128 阅读数：1363
asp.net core 实现一个简单的仓储
　　
　　一直有自己写个框架的想法,但是一直没有行动起来,最近比较闲,正好可以开工了.
　　
　　现在已经完成了两部分.1.一个简单仓储,实现使用的是ef 2.IOC部分,这里是把内置的ioc替换成了aotofac,这部分感觉还是有一点缺陷的.下面说
　　
　　仓储部分
　　
　　这里主要是接口是实现,目前使用ef实现了仓储的接口.看一下代码


```csharp 
　　
　　public interface IRepository<TEntity, TPrimaryKey>
　　
　　where TEntity : class
　　
　　{
　　
　　#region Select/Get/Query
　　
　　IQueryable<TEntity> GetAll();
　　
　　IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);
　　
　　List<TEntity> GetAllList();
　　
　　Task<List<TEntity>> GetAllListAsync();
　　
　　List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);
　　
　　Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);
　　
　　T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);
　　
　　TEntity Get(TPrimaryKey id);
　　
　　Task<TEntity> GetAsync(TPrimaryKey id);
　　
　　TEntity Single(Expression<Func<TEntity, bool>> predicate);
　　
　　Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);
　　
　　TEntity FirstOrDefault(TPrimaryKey id);
　　
　　Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);
　　
　　TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
　　
　　Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
　　
　　TEntity Load(TPrimaryKey id);
　　
　　#endregion
　　
　　#region Insert
　　
　　TEntity Insert(TEntity entity);
　　
　　Task<TEntity> InsertAsync(TEntity entity);
　　
　　#endregion
　　
　　#region Update
　　
　　TEntity Update(TEntity entity);
　　
　　Task<TEntity> UpdateAsync(TEntity entity);
　　
　　TEntity Update(TPrimaryKey id, www.dfgj157.com Action<TEntity> updateAction);
　　
　　Task<TEntity> UpdateAsync(TPrimaryKey www.dfgjyl.cn/  id, Func<TEntity, Task> updateAction);
　　
　　#endregion
　　
　　#region Delete
　　
　　void Delete(TEntity entity);
　　
　　Task DeleteAsync(TEntity entity);
　　
　　void Delete(TPrimaryKey www.qinlinyu.cn  id);
　　
　　Task DeleteAsync(TPrimaryKey id);
　　
　　void Delete(Expression<Func<TEntity, bool>> predicate);
　　
　　Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
　　
　　#endregion
　　
　　#region Aggregates
　　
　　int Count();
　　
　　Task<int> CountAsync(www.yinfenyule.com );
　　
　　int Count(Expression<Func<TEntity, bool>> predicate);
　　
　　Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
　　
　　long LongCount();
　　
　　Task<long> LongCountAsync();
　　
　　long LongCount(Expression<Func<TEntity, bool>> predicate);
　　
　　Task<long> LongCountAsync(www.thd178.com/ Expression<Func<TEntity, bool>> predicate);
　　
　　#endregion
　　
　　}
　　
　　下面是实现的部分代码,代码比较占版面,就不贴全了.
　　
　　public abstract class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
　　
　　where TEntity : class
　　
　　{
　　
　　public abstract IQueryable<TEntity> GetAll();
　　
　　public abstract IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);
　　
　　public virtual List<TEntity> GetAllList()
　　
　　{
　　
　　return GetAll().ToList();
　　
　　}
　　
　　public virtual async Task<List<TEntity>> GetAllListAsync()
　　
　　{
　　
　　return await Task.FromResult(GetAllList());
　　
　　}
　　
　　}
　　
　　public class EfRepositoryBase<TDbContext, TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey>
　　
　　where TEntity : class
　　
　　where TDbContext : DbContext
　　
　　{
　　
　　public virtual TDbContext Context { private set; get; }
　　
　　public virtual DbSet<TEntity> Table => Context.Set<TEntity>();
　　
　　public EfRepositoryBase(TDbContext context)
　　
　　{
　　
　　Context = context;
　　
　　}
　　
　　public override IQueryable<TEntity> GetAll()
　　
　　{
　　
　　return Table;
　　
　　}
　　
　　public override IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
　　
　　{
　　
　　if (propertySelectors == null)
　　
　　{
　　
　　return GetAll();
　　
　　}
　　
　　var linq = GetAll();
　　
　　foreach (var item in propertySelectors)
　　
　　{
　　
　　linq = linq.Include(item);
　　
　　}
　　
　　return linq;
　　
　　}
　　
　　}
　　
　　注意看EfRepositoryBase继承了RepositoryBase,而RepositoryBase实现了IRepository.这里的RepositoryBase是所有实现的基类.GetAllList虚方法直接调用了抽象方法GetAll,这样在EfRepositoryBase中就可以减少很多代码了.
　　
　　这里有个坑 EfRepositoryBase 是不能直接注册到IOC中的,因为EfRepositoryBase和IRepository的泛型参数个数不一致,ioc不能找到多出的一个泛型的值.使用仓储的时候继承EfRepositoryBase把dbcontext传进去就好了
　　
　　public class TestRepository<TEntity, TPrimaryKey> : EfRepositoryBase<TestContext, TEntity, TPrimaryKey> where TEntity : class
　　
　　{
　　
　　public TestRepository(TestContext context)
　　
　　: base(context)
　　
　　{
　　
　　}
　　
　　}
　　
　　IOC部分
　　
　　asp.net core 微软提供了一个简单的IOC,但是接口比较少,替换成我们熟悉的ioc框架就方便多了. asp.net core 也有很方便的替换ioc的方法.简单说就是修改ConfigureServices方法的返回值为IServiceProvider.我使用了autofac,下面看代码.
　　
　　public IServiceProvider ConfigureServices(IServiceCollection services)
　　
　　{
　　
　　services.AddMvc();
　　
　　return services.AddLuna<AutofacModule>();
　　
　　}
　　
　　public static IServiceProvider AddLuna<TModule>([NotNull]this IServiceCollection services)
　　
　　where TModule : IModule, new()
　　
　　{
　　
　　var builder = new ContainerBuilder();
　　
　　builder.Populate(services);
　　
　　builder.RegisterModule<TModule>();
　　
　　return new AutofacServiceProvider(builder.Build());
　　
　　}
　　
　　public class AutofacModule : Module
　　
　　{
　　
　　protected override void Load(ContainerBuilder builder)
　　
　　{
　　
　　builder.RegisterType<TestContext>();
　　
　　builder.RegisterGeneric(typeof(TestRepository<,>)).As(typeof(IRepository<,>))
　　
　　.InstancePerLifetimeScope();
　　
　　}
　　
　　}
　　
　　这里的Module和IModule是autofac的,功能已经实现了,但是作为框架来说直接暴露了autofac的东西显然是不合适的,下一步要实现一个框架自身的模块加载方式.




```

