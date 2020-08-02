## [sql boolean类型](https://www.cnblogs.com/nucdy/p/5783515.html) 		

[关于 MySQL 的 boolean 和 tinyint(1)](http://www.cnblogs.com/xiaochaohuashengmi/archive/2011/08/25/2153011.html)

boolean类型
MYSQL保存BOOLEAN值时用1代表TRUE,0代表FALSE，boolean在MySQL里的类型为tinyint(1),
MySQL里有四个常量：true,false,TRUE,FALSE,它们分别代表1,0,1,0，
mysql> select true,false,TRUE,FALSE;
+------+-------+------+-------+
| TRUE | FALSE | TRUE | FALSE |
+------+-------+------+-------+
|    1 |     0 |    1 |     0 |
+------+-------+------+-------+
可以如下插入boolean值：insert into [xxxx(xx)] values(true)，当然也可以values(1);
举例如下：
mysql> alter table test add isOk boolean;
Query OK
mysql> desc test;
+-------+-------------+------+-----+---------+----------------+
| Field | Type        | Null | Key | Default | Extra          |
+-------+-------------+------+-----+---------+----------------+
| id    | int(11)     | NO   | PRI | NULL    | auto_increment |
| isOk  | tinyint(1)  | YES  |     | NULL    |                |
+-------+-------------+------+-----+---------+----------------+
mysql> insert into test(isOk) values(true);
Query OK
mysql> select isOk from test ;
+------+
| isOk |
+------+
|    1 |
+------+
=================

 MySQL没有boolean类型。这也是比较奇怪的现象。例：

create table xs (    id int primary key,    bl boolean )

这样是可以创建成功，但查看一下建表后的语句，就会发现，mysql把它替换成tinyint(1)。也就是说mysql把boolean=tinyInt了，但POJO类要定义成什么类型呢？

因为惯性思维，在java类中也把它定义成type。然后在Struts中使用<s:check/>标签。这就产生一个严重的问题了。<s:check>是boolean，而POJO去定义成byte。这样数据永远也无法提交，被struts的intercept拦截掉了。解决办法是在POJO类中定义成boolean，在mysql中定义成tinyint(1)。 

 

\------

 TINYINT(1) or ENUM( 'true' , 'false')

\-------

 

 

# [关于 MySQL 的 boolean 和 tinyint(1)](http://www.cnblogs.com/xiaochaohuashengmi/archive/2011/08/25/2153011.html)

一、oracle本身没有boolean类型，就是说跟数据库相关的类型中不包括boolean，一般采用number（1）和char（1）来实现。

所  以”You   cannot   insert   the   values   TRUE and   FALSE   into   a    database   column.  Also,   you   cannot   select   or   fetch    column   values into   a   BOOLEAN   variable.“

plsql为了实现结构化编程，支持了boolean类   型，所以可能会出现的问题是一个存储过程或者函数的返回参数是boolean型的，但在call这个procedure的时候，无法注册boolean类  型的返回参数，执行时会抛出”参数类型不对“的exception，解决的方案就是把boolean的参数用别的类型代替。

不太明白oracle为什么不支持 boolean类型。

二、  一直被Oracle中没有boolean类型困扰，网上有两种解决方案，一是用Number(1)，二是用Char(1)，各有所长，个人比较喜欢用  Number方式解决，原因很简单，因为是从C语言开始的，这符合C语言的习惯。前几天有幸咨询到Oracle方面的顾问，他们提供的解决方案是用  Char(1)实现boolean，但也有需要注意的地方，原话如下：  如果是特定boolean类型情况下，Char(1）是比Number(1)更好的选择，因为前者所用的存储空间会比后者少，但这二者在查询时存储空间的  节省会提供查效率，但是要注意的是用Char(1)的时候不能让这个字段可以为空，必须有缺省，否则查询效率会降低

三、

PL/SQL 中有boolean类型，有null类型

PL/SQL中是有Boolean类型的，只能取2个值：true和false；

存储过程中的变量也支持boolean型；

但数据库中没有boolean类型。

四、存储过程中：

declare 
v1 boolean;
begin 
v1:=1>2;
if(v1)then 
     dbms_output.put_line('true

');

else 
    dbms_output.put_line('false

');

end if;
end; 

打印：false

\----------------------------------------

declare 
v1 boolean;
begin 
v1:=1>2;
dbms_output.put_line(v1);
end;

会报错。

运行时得到错误信息：调用'PUT_LINE' 时参数个数或类型错误。这是因为在脚本中不能直接打印boolean类型的值。


五：

oracle 没有boolean，mysql用bit(1)而oracle可以用char(1) check(...(0,1))字段，

如：

create table a ( a char(1) check (a in(0,1)))

然后JDBC用getBoolean()可以返回正确的结果。

JDBC我用ojdbc14.jar

ps：以上内容均引自网络，请尊重原作者，这里仅为学习。

 

 

[Oracle sql语句中不支持boolean类型（decode&case）](http://blog.csdn.net/t0nsha/article/details/7828538)

SQL> show err;
Errors for FUNCTION IS1GT0:

LINE/COL ERROR
-------- -----------------------------------------------------------------
5/3      PL/SQL: Statement ignored
5/10     PLS-00306: wrong number or types of arguments in call to 'DECODE'
SQL>

case完美通过：
SQL> CREATE OR REPLACE FUNCTION is1gt0
  RETURN VARCHAR2
IS
BEGIN
  RETURN CASE 1 > 0
    WHEN TRUE
      THEN 'true'
    ELSE 'false'
  END;
END;
/

Function created.

SQL> show err;
No errors.
SQL> select is1gt0 from dual;

IS1GT0
\--------------------------------------------------------------------------------
true

SQL>


小结：
\1. Oracle sql语句中不支持boolean类型；
\2. decode是oracle独有的；而case是标准sql，mysql和sqlserver也可以使用，而且case还能把boolean转换输出。

 

  

分类: [数据库](https://www.cnblogs.com/nucdy/category/773796.html)