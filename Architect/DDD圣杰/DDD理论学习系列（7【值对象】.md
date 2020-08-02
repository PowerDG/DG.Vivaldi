# DDD理论学习系列（7）-- 值对象

​             ![96](https://upload.jianshu.io/users/upload_avatars/2799767/0b0f3fb5-f8b9-4bf4-ac1d-b94468c2e1c8.jpg?imageMogr2/auto-orient/strip|imageView2/1/w/96/h/96) 

​             [圣杰](https://www.jianshu.com/u/39ec0e6b1844)                          

​                                2017.06.14 21:53*               字数 2738             阅读 2815评论 18喜欢 12

> [DDD理论学习系列——案例及目录](https://www.jianshu.com/p/6e2917551e63)

------



![img](https://upload-images.jianshu.io/upload_images/2799767-73475b1895af68a5.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/789)

# 1.引言

提到值对象，我们可能立马就想到值类型和引用类型。而在C#中，值类型的代表是strut和enum，引用类型的代表是class、interface、delegate等。值类型和引用类型的区别，大家肯定都知道，值类型分配在栈上，引用类型分配在堆上。
 那是不是值类型对应的就是值对象，引用类型对应的就是实体吗？很抱歉，不是的。

值对象我们要分开来看，其包含两个词：值和对象。值是什么？比如，数字（1、2、3.14），字符串（“hello  world”、“DDD”），金额（￥50、$50），地址（深圳市南山区科技园）它们都是一个值，这个值有什么特点呢，固定不变，表述一个具体的概念。对象又是什么？一切皆为对象，是对现实世界的抽象，用来描述一个具体的事物。那**值对象=值+对象=将一个值用对象的方式进行表述，来表达一个具体的固定不变的概念**。

所以了解值对象，我们关键要抓住关键字——**值**。

# 2.值的特征

1就是代表数字1，“Hello  DDD”就是一个固定字符串，“￥50”就是表示人民币50元。假设你手上有一沓钞票，我们去超市购物的时候，很显然我们会根据面额去付款，不会拿20元当50元花，也不会把美元当人民币花，毕竟￥50≠$50。那对于钞票来说，我们怎么识别它们，无非就是钞票上印刷的数字面额和货币单位。你可能会说了，每张钞票上都印有编号，就算同样面额的毛爷爷，那它也不一样。这个陈述，我竟然无言以对。但我只想问你，你平时购物付款，是用编号识别面额的啊？编号显然是银行关心的事，与我们无关。
 我们这里提到的数字面额、货币单位和编号，除此之外还有发行日期，其实都是钞票的基本特征，在coding中我们会根据场景选择性的对某些特征以属性的形式加以抽象。而在我们日常消费的场景下，显然编号和发行日期这两个特征我们可以直接忽略不计。

从上面这个例子我们可用总结出值的特征：

1. 表示一个具体的概念
2. 通过值的属性对其识别
3. 属性判等
4. 固定不变

# 3.案例分析

> 购物网站都会维护客户收货地址信息来进行发货处理，一个地址信息一般主要包含省份、城市、区县、街道、邮政编码信息。

如果要让我们设计，我们肯定噼里啪啦就把代码写下来了：

```
    /// <summary>
    /// 地址
    /// </summary>
    public class Address {

        /// <summary>
        ///Id
        /// </summary>
        public int AddressId{ get; private set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; private set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// 区县
        /// </summary>
        public string County { get; private set; }

        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; private set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string Zip { get; private set; }
    }
}
```

很简单的类，我想你在没了解DDD值对像之前肯定会这样写，这并不奇怪，我之前也是这样设计的，为了将Address映射到数据库，我们需要定义一个AddressId作为主键映射，这是数据建模的结果。那在DDD中应该如何设计？别急，我们一步一步的分析。

首先，我们要问自己一个问题，地址是什么？广东省深圳市南山区高新科技园中区一路 邮政编码:  518057（腾讯大厦），它就是一个标准的地址，表述的是一个具体的不变的位置信息。它不会随着时间而变化，它包含了地址所需要的完整属性（省份、城市、区县、街道、邮政编码）信息。所以，地址是一个值。

按照我们现在的设计，如果有多个所处腾讯大厦的注册用户，我们数据库将存在多条相同的地址信息（只是Id不同）。但Id不同，就不是同一个地址吗？我们在做发货处理的时候，难道会因为Id不同，而将货物发往不同的地方吗？很显然不是的。这也再次论证了地址是一个值的事实。

那我们如何抽象设计这个地址呢，让其具有值的特征？
 我们一条一条的来进行分析。

1. 表示一个具体的概念
    我们上面设计的Address类，也能表示出地址这个概念。
2. 通过值的属性对其识别
    也就是不需要唯一标识，删去我们设计的AddressId即可。
3. 属性判等
    重写Equals方法，比较属性判断。
4. 固定不变
    就是通过构造函数来初始化，所有属性均不提供修改入口。

修改后的Address如下：

```
   /// <summary>
/// 地址
/// </summary>
public class Address
{
    /// <summary>
    /// 省份
    /// </summary>
    public string Province { get; private set; }

    /// <summary>
    /// 城市
    /// </summary>
    public string City { get; private set; }

    /// <summary>
    /// 区县
    /// </summary>
    public string County { get; private set; }

    /// <summary>
    /// 街道
    /// </summary>
    public string Street { get; private set; }

    /// <summary>
    /// 邮政编码
    /// </summary>
    public string Zip { get; private set; }

    public Address(string province, string city,
        string county, string street, string zip)
    {
        this.Province = province;
        this.City = city;
        this.County = county;
        this.Street = street;
        this.Zip = zip;
    }

    public override bool Equals(object obj)
    {
        bool isEqual = false;
        if (obj != null && this.GetType() == obj.GetType())
        {
            var that = obj as Address;
            isEqual = this.Province == that.Province
                && this.City == that.City
                && this.County == that.County 
                && this.Street == that.Street 
                && this.Zip == that.Zip;
        }
        return isEqual;
    }

    public override int GetHashCode()
    {
        return this.ToString().GetHashCode();
    }

    public override string ToString()
    {
        string address = $"{this.Province}{this.City}" +
            $"{this.County}{this.Street}({this.Zip})";
        return address;
    }
}
```

至此，我们的`Address`就具有了值的特征，我们可以直接使用`Address address = new Address("广东省", "深圳市", "南山区", "高新科技园中区一路 ", "518057");)`来表示一个具体的通过属性识别的不可变的位置概念。在DDD中，我们称这个`Address`为值对象。读到这里，你可能会觉得值对象也不过如此，也可能会有一堆问题，但请稍安勿躁，我们继续讲解。

# 4.DDD中的值对象

通过上面对值的特征分析，结合实际的案例，我们设计出了一个`Address`这个值对象。那在DDD中对值对象又是怎样描述的呢？

## 4.1.值对象的特征

咱们来看看《实现领域驱动设计》上是如何定义的吧：

> - 描述了领域中的一件东西

- 不可变的
- 将不同的相关属性组合成了一个概念整体
- 当度量和描述改变时，可以用另外一个值对象予以替换
- 可以和其他值对象进行相等性比较
- 不会对协作对象造成副作用

由此可见，值对象包含了值所具有的全部特征。

另外有一点：个人认为值对象不会孤立的存在，它有其所属。比如我们所说的地址，它是一个客观存在。没有一个具体的上下文语境，它就仅仅是一个字符串。只有在某个具体的领域下，才有其实质意义，比如客户收货地址、售后地址。

## 4.2.值对象的问题

说到问题，你可能想到的第一个问题就是持久化的问题。是的，值对象没有标识列如何存储数据库呢？
 当下比较流行使用ORM持久化机制，使用ORM将每个类映射到一张数据库表，再将每个属性映射到数据库表中的列会增加程序的复杂性。那如何使用ORM持久化来避免这一问题呢？

1. 单个值对象
    上面我们提到值对象不会孤立存在，所以我们可以将值对象中的属性作为所属实体/聚合根的数据列来存储（比如，我们可以将收货地址的属性映射到客户实体中）。这样做就会导致数据表列数增多，但是能够优化查询性能，因为不需要联表查询。
2. 多个值对像序列化到单个列
    当每个客户仅允许维护一个收货地址时，我们用上面的方式没有问题。但很显然一个客户可以有多个收货地址。这个时候我们该怎么持久化值对象集合呢？不可能把值对象集合的每个元素映射到外层的实体表中，但是创建多个表又增加复杂性，所以一个变态的方法是使用**序列化大对象**模式。把一个集合序列化后塞到外层实体表的某一列中，是有点匪夷所思。而且数据库的列宽是有限制的，且不方便查询。但似乎也带来一个好处，大大简化了系统的设计（不用设计多列分别存储了）。
3. 使用数据库实体保存多个值对像
    使用**层超类型**来赋予值对象一个委派标识，以数据库实体的形式保存值对象。（关于层超类型，可参考我上一篇文章，这里不作赘述。）

你可能会觉得第3个方法好，因为其更符合传统的设计方式，但其并非DDD推崇的一种方式，因为层超类型让值对象有了实体的影子。在进行持久化设计的时候，我们要谨记**根据领域模型来设计数据模型，而不是根据数据模型来设计领域模型**。

## 4.3.值对象的作用

通过上面的分析介绍，我们可以体会到值对象带来的以下好处：

- 符合通用语言，更简单明了的表达简单业务概念。
- 提升系统性能。
- 简化设计，减少不必要的数据库表设计。

# 5.建模值对象

值对象作为领域建模工具之一，有其存在的意义。领域中，并不是每一个事物都必须有一个唯一身份标识，对于某些对象，我们更关心它是什么而无需关心它是哪个。所以建模值对象，我们关键要结合通用语言的表述看其是否有**值的含义和特征**。

# 6. 总结

如果非要对值对象进行总结的话，我希望你记住我开头的那句话：
 **值对象=值+对象=将一个值用对象的方式进行表述，来表达一个具体的固定不变的概念**。
 仔细揣摩，定有收获。

------

> 参考资料
>  [应用程序框架实战十六:DDD分层架构之值对象（介绍篇）](https://link.jianshu.com?t=http://www.cnblogs.com/xiadao521/p/4121861.html)
>  [DDD领域驱动设计（二） 之 值对象](https://link.jianshu.com?t=http://www.cnblogs.com/fan-yuan/p/3513867.html)
>  [值对象的威力](https://link.jianshu.com?t=http://michael-j.net/2016/10/18/%E5%80%BC%E5%AF%B9%E8%B1%A1%E7%9A%84%E5%A8%81%E5%8A%9B/)

喜欢就很好



​                      DDD理论学习 

​           © 著作权归作者所有         

​           举报文章         

​             [               ![96](https://upload.jianshu.io/users/upload_avatars/2799767/0b0f3fb5-f8b9-4bf4-ac1d-b94468c2e1c8.jpg?imageMogr2/auto-orient/strip|imageView2/1/w/96/h/96) ](https://www.jianshu.com/u/39ec0e6b1844)            

圣杰



写了 195594 字，被 2917 人关注，获得了 2134 个喜欢