 		[mongodb数据库设计原则](https://www.cnblogs.com/lifeone/p/4863247.html) 	

1.一对很少  one-to-few  可以采用内嵌文档 

person集合中

{

name:'张三',

age:20,

address:[

{country:"中国"，province:"山西省"，city:"长治市"}，

{country:"中国"，province:"山西省"，city:"太原市"}

]

}

​    优点：不需要单独执行一条语句去获取内嵌的内容

​    缺点：法把这些内嵌文档当做单独的实体去访问

​    适用场合：一对很少且不需要单独访问内嵌内容

2.一对许多（但并不是很多） one-to-many  中间引用

person集合

{

_id:ObjectID(12个字节组成)

name:"张三"

age:23

}

人员组集合

{

name:"一组"，

persons:[

ObjectID("aaaaa"),

ObjectID("AAABBB")

.....

]

}

适用场合：一对多且多的一端内容因为各种理由需要单独存在的情况下可以通过数组的方式引用多的一方的。

3.一对非常多 one-to-squillions  父级引用（mongodb每个文档的最大16M）

company集合

{

_id:ObjectID("company01")

name:"可为时代"

}

员工集合

{

name:"张三",

age:23,

company:ObjectID("company01")

}

适用场合：一对非常多的情况下，请将一的那端引用嵌入进多的一端对象中。

4.双向关联  在one端和many端同时保存对方的引用

person集合

{

_id:ObjectID("person01"),

name:"张三",

age:23,

group:ObjectID("group01")

}

group集合

{

_id:ObjectID("group01"),

name:"研发一组",

persons:[

ObjectID("person01")

ObjectID("person02")

]

}

优点：具有一对多的所有优点，同时在多的一方，可以很快找到少的一方

缺点：更新时需要同时更新两个集合中的引用，不能使用原子性

5.反范式

反范式Many-<one ：冗余mony端的数据到one端即在one的一方保存mony的引用集合

反范式noe -<many :冗余one端的数据到many端即在many的一方保存one的引用

使用场合：读比较高，更新比较少的情况（没有原子性）

 

7.总的设计原则

a.优先考虑内嵌，除非有什么迫不得已的原因。

b.需要单独访问一个对象，那这个对象就不适合被内嵌到其他对象中。

c.数组不应该无限制增长。如果many端有数百个文档对象就不要去内嵌他们可以采用引用ObjectID的方案；如果有数千个文档对象，那么就不要内嵌ObjectID的数组。该采取哪些方案取决于数组的大小。

d.在进行反范式设计时请先确认读写比。一个几乎不更改只是读取的字段才适合冗余到其他对象中。

 







​         [好文要顶](javascript:void(0);)             [关注我](javascript:void(0);)     [收藏该文](javascript:void(0);)     [![img](assets/icon_weibo_24.png)](javascript:void(0);)     [![img](assets/wechat.png)](javascript:void(0);) 

![img](assets/sample_face.gif)

​             [YL10000](http://home.cnblogs.com/u/lifeone/)
​             [关注 - 2](http://home.cnblogs.com/u/lifeone/followees)
​             [粉丝 - 2](http://home.cnblogs.com/u/lifeone/followers)         





​                 [+加关注](javascript:void(0);)     

​         1     

​         0     



​     



[« ](https://www.cnblogs.com/lifeone/p/4863234.html) 上一篇：[activiti学习总结](https://www.cnblogs.com/lifeone/p/4863234.html)
[» ](https://www.cnblogs.com/lifeone/p/4863259.html) 下一篇：[mongodb学习笔记](https://www.cnblogs.com/lifeone/p/4863259.html)