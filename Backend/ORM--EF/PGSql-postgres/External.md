### 3.1.5 IEntity 接口

事实上 Entity 实现了 IEntity 接口（Entity<TPrimaryKey> 实现了 IEntity<TPrimaryKey>接口）。如果你不想从 Entity 类派生，你能直接的实现这些接口。 其他实体类也可以实现相应的接口。但是不建议你用这种方式。除非你有一个很好的理由不从 Entity 类派生。

### 3.1.6 IExtendableObject 接口

在Abp中有一个接口 **IExtendableObject**，可以轻松的将 **任意name-value数据** 关联到一个实体。如下是一个简单的实体类：

```csharp
public class Person : Entity, IExtendableObject
{
    public string Name { get; set; }

    public string ExtensionData { get; set; }

    public Person(string name)
    {
        Name = name;
    }
}
```

在 **IExtendableObject** 接口中仅定定义了一个字符串属性：**ExtensionData**，该属性用来存储 **JSON** 格式的 **name-value** 对象。如下所示：

```csharp
var person = new Person("John");

person.SetData("RandomValue", RandomHelper.GetRandom(1, 1000));
person.SetData("CustomData", new MyCustomObject { Value1 = 42, Value2 = "forty-two" });
```

我们可以使用 **SetData** 方法来设置任意类型的值。如果代码是上面示例所示的话，那么 **ExtensionData** 的值将会是：

```json
{"CustomData":{"Value1":42,"Value2":"forty-two"},"RandomValue":178}
```

我们可以使用 **GetData** 方法来取得任意值：

```csharp
var randomValue = person.GetData<int>("RandomValue");
var customData = person.GetData<MyCustomObject>("CustomData");
```

在某些情况下(当你需要动态的添加额外数据到实体的时候)，这个技术是非常有用的。正常情况下，应该使用正规的属性。如同这样动态使用是类型不安全且明确的。