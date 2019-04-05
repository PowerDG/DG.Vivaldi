##       MySQL知识体系——存储引擎                                                                             

https://my.oschina.net/zbnb/blog/3030725#comments)

​                                 [MyISAM](https://my.oschina.net/zbnb?q=MyISAM)[MySQL](https://my.oschina.net/zbnb?q=MySQL)[InnoDB](https://my.oschina.net/zbnb?q=InnoDB)                            

[开发十年，就只剩下这套架构体系了！ >>> ](https://my.oschina.net/u/3985214/blog/3018099?tdsourcetag=s_pcqq_aiomsg)  ![img](assets/hot3-1554464358014.png)



# 你需要get的小点

- 存储引擎是MySQL有别于其他数据库管理系统的最大特色。
- MySQL中的数据用各种不同的“技术”存储在文件（或者内存）中。每一种“技术”都使用不同的存储机制、索引技巧、锁定水平并且最终提供广泛的不同的功能和能力。这些不同的技术以及配套的相关功能在MySQL中被称作**存储引擎**(也称作**表类型：\***储引擎的使用级别是数据表)。
- 对于存储引擎的选择，往往是由业务决定的。
- **全文索引**是指对char、varchar和text中的每个词（停用词除外）建立倒排序索引。
  - 停用词（stopword）也叫停止词/字
  - 在全文索引中，如果一个词被认为是太普通或者太没价值，那么它将会被搜索索引和搜索查询忽略
  - 查看停用词 SELECT * FROM information_schema.INNODB_FT_DEFAULT_STOPWORD;



# 存储引擎（本文简述InnoDB和MyIsam）

1. **MyIsam**
2. **InnoDB**
3. Memory
4. Blackhole
5. CSV
6. Performance_Schema
7. Archive
8. Federated 
9. Mrg_Myisam



# InnoDB（默认）



## 特点

1. 支持事务（提供了对数据库ACID事务的支持）
2. 行级锁定（锁的粒度更小，*如果在执行一个SQL语句时MySQL不能确定要扫描的范围，InnoDB表同样会锁全表）
3. 支持外键约束
4. 实现了SQL标准的四种隔离级别
5. 索引和数据在一起，数据文件本身就是主键索引文件，这样的索引被称为“聚簇索引”
6. 不支持FULLTEXT类型的索引，但是innodb可以使用sphinx插件支持全文索引，并且效果更好
7. MySQL运行时Innodb会在内存中建立缓冲池，用于缓冲数据和索引
8. 不保存表的行数（当SELECT COUNT(*) FROM TABLE时需要扫描全表）



## 最佳实践（以下内容来自官方https://dev.mysql.com/doc/refman/8.0/en/innodb-best-practices.html）

- 指定最频繁查询的字段为每个表的[主键](https://dev.mysql.com/doc/refman/8.0/en/glossary.html#glos_primary_key)，如果没有明显的主键，则指定[自动增量](https://dev.mysql.com/doc/refman/8.0/en/glossary.html#glos_auto_increment)值。

​        *这里涉及到InnoDB的索引知识，聚集索引**（clustered index）和辅助索引（secondary index）（具体详见“MySQL知识体系——索引”一文）。一般来说，聚集索引就是按照表中主键的顺序构建一颗 B+ 树，并在叶节点中存放表中的行记录数据。*

- 对于具有关联关系的表使用[连接](https://dev.mysql.com/doc/refman/8.0/en/glossary.html#glos_join)。要获得快速连接性能，请在连接列上定义 [外键](https://dev.mysql.com/doc/refman/8.0/en/glossary.html#glos_foreign_key)，并在每个表中声明具有相同数据类型的列。添加外键可确保对引用的列建立索引，从而提高性能。外键还会将删除或更新传播到所有受影响的表，如果父表中不存在相应的ID，则会阻止在子表中插入数据。
- 关闭[自动提交](https://dev.mysql.com/doc/refman/8.0/en/glossary.html#glos_autocommit)。每秒提交数百次会限制性能（受存储设备写入速度的限制）。

​        *InnoDB的AUTOCOMMIT默认是打开的，即每条SQL语句会默认被封装成一个事务，自动提交，这样会影响速度，所以最好是把多条SQL语句显示放在begin和commit之间，组成一个事务去提交。*

- `把相关的`[DML](https://dev.mysql.com/doc/refman/8.0/en/glossary.html#glos_dml) 操作（数据操作语言，它是对表记录的操作(增、删、改)）组合在一起，然后用`START TRANSACTION（开启事务）`和 `COMMIT（提交）`包裹起来。这样可以避免过于频繁地提交，和继续数小时而不提交的大批量 [`INSERT`](https://dev.mysql.com/doc/refman/8.0/en/insert.html)， [`UPDATE`](https://dev.mysql.com/doc/refman/8.0/en/update.html)或者`DELETE 。`

- 不使用[`LOCK TABLES`](https://dev.mysql.com/doc/refman/8.0/en/lock-tables.html) 语句。`InnoDB`可以同时处理多个会话，同时读取和写入同一个表，而不会牺牲可靠性或高性能。要获得对一组行的独占写访问权，请使用 [`SELECT ... FOR UPDATE`](https://dev.mysql.com/doc/refman/8.0/en/innodb-locking-reads.html)语法仅锁定要更新的行。

- 启用 [`innodb_file_per_table`](https://dev.mysql.com/doc/refman/8.0/en/innodb-parameters.html#sysvar_innodb_file_per_table)选项或使用通用表空间将表的数据和索引放入单独的文件中，而不是 [系统表空间](https://dev.mysql.com/doc/refman/8.0/en/glossary.html#glos_system_tablespace)。

  [`innodb_file_per_table`](https://dev.mysql.com/doc/refman/8.0/en/innodb-parameters.html#sysvar_innodb_file_per_table) 默认启用。

- 评估您的数据和访问模式是否受益于`InnoDB`表或页面 [压缩](https://dev.mysql.com/doc/refman/8.0/en/glossary.html#glos_compression)功能。您可以在不牺牲读/写功能的情况下压缩`InnoDB`表。

- 打开 [`--sql_mode=NO_ENGINE_SUBSTITUTION`](https://dev.mysql.com/doc/refman/8.0/en/server-system-variables.html#sysvar_sql_mode) 选项，防止 在[`CREATE TABLE`](https://dev.mysql.com/doc/refman/8.0/en/create-table.html)的 `ENGINE=` 子句中指定的引擎有问题时，使用其他存储引擎创建表。



# MyIsam



## 特点

1. 不支持事务
2. 只支持表级锁，用户在操作myisam表时，select，update，delete，insert语句都会给表自动加锁
3. 不支持外键约束
4. 索引和数据分离，天生非聚簇索引，最多有一个unique的性质
5. 支持FULLTEXT类型的索引（但是不支持中文分词，必须由使用者分词后加入空格再写到数据表里，而且少于4个汉字的词会和停用词一样被忽略掉）
6. 内置了一个计数器（当SELECT COUNT(*) FROM TABLE时直接从计数器中读取）



## 最佳实践

1. 设置合适的索引(缓存机制)
2. 调整读写优先权限、根据业务需求、确保重要操作更有执行权限
3. 启用延时插入(尽量批量插入、降低写的频率)
4. 写数据的时候、顺序操作、让insert数据都写入到尾部、减少阻塞
5. 分解大的时间长的操作、降低单个操作的阻塞时间
6. 降低并发数(减少数据库的访问、高并发场景的话、可以使用队列机制)
7. 对于静态更新不频繁的数据库数据、充分利用Query Cache或者Memcached缓存服务、极大可能的提高访问效率
8. count的时候、只有count(*)会直接返回行数、才是效率最高的；带有where条件的count都需要进行全部数据的访问
9. 配置主从数据库的时候，可以采用主数据库使用InnoDB，从数据库使用MyISAM，进行读写分离



# 总结 

​    在MySQL知识体系中，存储引擎独占鳌头，作为第一章节是十分履顺的，而对于Java后端开发人员来说，我觉得以上知识点基本够用了。存储引擎其实包含了MySQL三大知识点：索引、事务和锁。以后随着我们的修行，对于存储引擎的认识也会更加深入。