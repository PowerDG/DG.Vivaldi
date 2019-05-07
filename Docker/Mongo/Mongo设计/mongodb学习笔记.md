 		[mongodb学习笔记](https://www.cnblogs.com/lifeone/p/4863259.html) 	

Mongodb学习

安装级配置windows随机启动

  1.下载mongodb,并解压到D:\database\mongodb\mongo

  2.配置环境变量

​     1.新建环境变量MONGO_HOME=D:\database\mongodb\mongo\bin

​     2.加入path变量%MONGO_HOME%;

  3.配置数据库安装和日志文件夹

​     1.在D:\database\mongodb文件夹下添加logs和data文件夹

​     2.在logs文件夹下添加mongodb.log文件

  4.配置windows开机启动

​     在命令行中输入mongod --dbpath D:\database\mongodb\data --logpath=D:\database\mongodb\logs\mongodb.log --install

  5.测试是否安装成功

​     在命令行中输入mongo如何出现下面的提示表示安装成功

基本操作

  添加操作 db.person.insert({"name":"tom","age":20})

  查找操作

​     查找全部  db.person.find()

​     条件查询  db.person.find({"name":"tom"})

​         \>,>=,<,<=,!=,=分别对应"$gt", "$gte", "$lt", "$lte", "$ne", "没有特殊关键字"

​             \>:db.person.find({"age",{$gt:20}})

​               <:db.person.find({"age",{$lt:20}})

​               =:db.person.find({"age",20})

​         and,or,in,notIn分别对应"无关键字“, "$or", "$in"，"$nin"

​              and:db.person.find({"name":"tom","age":20})

​              or: db.person.find({$or:[{"name":"tom"},{"name":"zhangsan"}]})

​              in: db.person.find({"name":{$in:["zhangsan","tom"]}})

​              notIn: db.person.find({"name",{$nin:["zhangsan","tom"]}})

​     正则表达式  db.person.find({"name":/^j/,"name":/e$/})

​     $where   db.person.find({$where:function(){return this.name=="tom"}})

​     查询person集合中的记录数   db.person.count()

  修改操作

​     整体修改 db.person.update({"name","tom"},{"name":"tom1","age":30})

​     局部修改  

​         $inc增加increase的缩写  db.person.update({"name","tom"},{$inc:{"age":20}})  //修改后tom.age=40

​         $set设置  db.person.update({"name":"tom"},{$set:{"age":20}})   //修改后tom.age=20

inserUpdate模式

​    如果根据第一个查询条件找不到相应数据就插入一条新数据 

db.person.update({"name":"tom1"},{$set:{"age":40}},true)

批量修改

​    mongodb默认如果根据一个查询条件找到要修改的数据不止一条，会默认只修改一条，可以设置第四个参数为true进行批量修改

删除操作  db.person.remove({"name":"tom1"})

聚合操作

count 总记录数  db.person.count()

条件中记录数  db.person.count({"age":{$gt:30}})

distinct 不带条件的去重  db.person.distinct("name")

带条件的去重  db.person.distinct("name",{"age":{$gt:30}})

group  基本分组

db.person.group({"key":{"name":true},"initial":{"person":[]},"reduce":function(doc,out){out.person.push(doc.name+":"+doc.age)}})

 

显示每个分组中的个数

db.person.group({"key":{"age":true},"initial":{"person":[]},"reduce":function(doc,out){out.person.push(doc.name+":"+doc.age)},"finalize":function(out){out.count=out.person.length}})

 

带有条件的分组

db.person.group({"key":{"age":true},"initial":{"person":[]},"reduce":function(doc,out){out.person.push(doc.name+":"+doc.age)},"finalize":function(out){out.count=out.person.length},"condition":{"age":{$gt:30}}})

 

mapReduce

第一步：创建映射函数

var map=function(){    emit(this.age,{count:1}) }     //分组参数

第二步：创建简化函数

var reduce=function(key,value){    var result={count:0};    for(var i=0;i<value.length;i++){ result.count+=value[i].count;    }    return result; }

第三步：执行的函数

db.person.mapReduce(map,reduce,{"out":"collection"})

 

索引操作

建立索引

db.person.ensureIndex({"name":1})    //给name字段添加索引  1代表升序排列，-1代表降序排列    默认建立的索引都不是唯一索引

建立唯一索引

db.person.ensureIndex({"name":1},{"unique":true})

建立组合索引

db.person.ensureIndex({"name":1,"age":-1})

获取集合下的索引索引

db.person.getIndexes()

删除索引

db.person.dropIndex({"name":1})