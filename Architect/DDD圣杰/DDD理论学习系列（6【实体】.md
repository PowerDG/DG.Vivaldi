# DDD理论学习系列（6）-- 实体

​             ![96](https://upload.jianshu.io/users/upload_avatars/2799767/0b0f3fb5-f8b9-4bf4-ac1d-b94468c2e1c8.jpg?imageMogr2/auto-orient/strip|imageView2/1/w/96/h/96) 

​             [圣杰](https://www.jianshu.com/u/39ec0e6b1844)                          

​                                2017.06.11 15:39*               字数 2040             阅读 2969评论 0喜欢 11赞赏 1

> [DDD理论学习系列——案例及目录](https://www.jianshu.com/p/6e2917551e63)

------

# 1.引言

实体对应的英语单词为Entity。提到实体，你可能立马就想到了代码中定义的实体类。在使用一些ORM框架时，比如Entity  Framework，实体作为直接反映数据库表结构的对象，就更尤为重要。特别是当我们使用EF Code  First时，我们首先要做的就是实体类的设计。在DDD中，实体作为领域建模的工具之一，也是十分重要的概念。

但DDD中的实体和我们以往开发中定义的实体是同一个概念吗？
 不完全是。在以往未实施DDD的项目中，我们习惯于将关注点放在数据上，而非领域上。这也就说明了为什么我们在软件开发过程中会首先做数据库的设计，进而根据数据库表结构设计相应的实体对象，这样的实体对象是数据模型转换的结果。
 在DDD中，实体作为一个领域概念，在设计实体时，我们将从领域出发。

# 2.DDD中的实体

DDD中要求实体是唯一的且可持续变化的。意思是说在实体的生命周期内，无论其如何变化，其仍旧是同一个实体。唯一性由唯一的身份标识来决定的。可变性也正反映了实体本身的状态和行为。

# 3. 唯一标识

举个例子，在有双胞胎的家庭里，家人都可以快速分辨开来。这得益于家人对双胞胎性格和外貌的区分。然而邻居却不能，只能通过名字来区分。上小学后，学校里尽然有重名的，这时候就要取外号区分了。上大学后，要坐火车去学校，买票时就要用身份证号来区分了。

针对这个例子，如果我们要抽象出一个User实体，要如何定义其唯一标识呢？
 其中性格、外貌、昵称、身份证号都可以作为User实体的属性，在某些场景下某个属性就可以对对象进行区分。但为了确保标识的稳定性，我们只能将身份证号设为唯一身份标识。

## 3.1.唯一标识的类型

唯一标识的类型在不同的场景又有不同的要求。
 主要可以分为有意义和无意义两种。
 在一个简单的应用程序里，一个int类型的自增Id就可以作为唯一标识。优点就是占用空间小，查询速度快。
 而在一些业务当中，要求唯一标识有意义，通过唯一标识就能识别出一些基本信息，比如支付宝的交易号，其中就包含了日期和用户ID。这种就属于字符串类型的标识，这就对唯一标识的生成提出了挑战。
 在一些复杂的业务流程中，对唯一标识没有要求，我们可以使用GUID类型来生成唯一标识，很显然GUID占用空间就毕竟大，且不利于查询。

## 3.2.唯一标识的生成时机

有某些场景下，唯一标识的生成时机也各不相同，主要分为即时生成和延迟生成。
 即时生成，即在持久化实体之前，先申请唯一标识，再更新到数据库。
 延迟生成，即在持久化实体之后。

## 3.3.委派标识和领域标识

基于领域实体概念分析确定的唯一身份标识，我们可以称为领域实体标识。
 而在有些ORM工具，比如Hibernate、EF，它们有自己的方式来处理对象的身份标识。它们倾向于使用数据库提供的机制，比如使用一个数值序列来生成识。在ORM中，委派标识表现为int或long类型的实体属性，来作为数据库的主键。很显然，委派标识是为了迎合ORM而创建的，且委派标识和领域实体标识无任何关系。

那既然ORM需要委派标识，我们就可以创建一个实体基类来统一指定委派标识。而这个实体基类又被称为层超类型。

### 3.3.1.实现层超类型

首先定义层超类型接口：

```
public interface IEntity
{
}

public interface IEntity<TPrimaryKey> : IEntity
{
    TPrimaryKey Id { get; set; }
}
```

通过定义泛型接口，以支持自定义主键类型。

实现层超类型：

```
    public class Entity : Entity<int>, IEntity
    {
    }

    public class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TPrimaryKey>))
            {
                return false;
            }

            //Same instances must be considered as equal
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (Entity<TPrimaryKey>) obj;
            //Must have a IS-A relation of types or must be same type
            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.GetTypeInfo().IsAssignableFrom(typeOfOther) && !typeOfOther.GetTypeInfo().IsAssignableFrom(typeOfThis))
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            return !(left == right);
        }
    }
```

可以看到默认的委托标识为int类型。我们重写了Equals，GetHashCode方法，以及==和!=两个操作符。

通过这样一种方式，我们进行约定，所有的实体必须继承自`Entity`，即可实现委托标识的统一定义。

# 4.可变性

解决了实体的唯一身份标识问题后，我们就可以保证其生命周期中的连续性，不管其如何变化。

那可变性说的是什么呢？可变性是实体的状态和行为。
 而实体的状态和行为就要对具体的业务模型加以分析，提炼出通用语言，再基于通用语言来抽象成实体对应的属性或方法。

> 我们拿订单环节来举例说明：
>  当顾客从购物车点击结算时创建订单，初始状态为未支付状态，支付成功后切换到正常状态，此时可对订单做发货处理并置为已发货状态。当顾客签收后，将订单关闭。

从以上的通用语言的描述中（在通用语言的术语中，名词用于给概念命名，形容词用于描述这些概念，而动词则表示可以完成的操作。）
 我们可以提取订单的相关状态和行为：

- 订单状态：未支付、正常、已发货、关闭。针对状态，我们需定义一个状态属性即可。
- 订单的行为：支付、发货和关闭。针对行为，我们可以在实体中定义方法或创建单独的领域服务来处理。

实体既然存在状态和行为，就必然会与事件有所牵连。比如订单支付成功后，需要知会商家发货。这时我们就要追踪订单状态的变化，而追踪变化最实用的方法就是领域事件。关于领域事件，我们后续再讲。

# 5.实体的验证

验证的目的是为了检查模型的正确性和有效性。检查的对象可以为某个属性，也可以是整个对象，或是多个对象的组合。针对验证的方式，不一而足，根据需要可自行发挥。

# 6.总结

实体作为领域建模的工具之一，唯一的身份标识是实体最基本的特征，其次是可变性。唯一身份标识和可变性也是用来区分实体和值对象的主要特征。

为了正确建立实体模型，我们需要将关注点从数据转向领域，从业务模型中提炼通用语言，再基于通用语言分析其状态和行为。

所以，我们可以认为：**实体 = 唯一身份标识 + 可变性【状态（属性） + 行为（方法或领域事件或领域服务）】**

喜欢就很好

- [![img](https://upload.jianshu.io/users/upload_avatars/547945/93dee5148009?imageMogr2/auto-orient/strip|imageView2/1/w/40/h/40)](https://www.jianshu.com/u/4792fd5daf2e)

​                      DDD理论学习 

​           © 著作权归作者所有         

​           举报文章         

​             [               ![96](https://upload.jianshu.io/users/upload_avatars/2799767/0b0f3fb5-f8b9-4bf4-ac1d-b94468c2e1c8.jpg?imageMogr2/auto-orient/strip|imageView2/1/w/96/h/96) ](https://www.jianshu.com/u/39ec0e6b1844)            

圣杰



写了 195594 字，被 2917 人关注，获得了 2134 个喜欢