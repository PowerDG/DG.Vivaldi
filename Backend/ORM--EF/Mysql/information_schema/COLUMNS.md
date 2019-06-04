# MySQL中表的字段信息查询--information_schema.COLUMNS

​                                                   2018年03月14日 17:20:12           [lkforce](https://me.csdn.net/lkforce)           阅读数：4177                                                                  

​                   

​                                                                         版权声明：本文为博主瞎抄的文章，未经博主允许可以随意转载。          https://blog.csdn.net/lkforce/article/details/79557482        

MySQL的information_schema库中有个COLUMNS表，里面记录了mysql所有库中所有表的字段信息，该表信息如下：

 ![img](assets/2018031417144511)

COLUMNS表的每一条记录都对应了数据库中某个表的某个字段，该表记录了如下信息：

#### TABLE_CATALOG

MySQL官方文档中说，这个字段值永远是def，但没写这个字段是干嘛用的。

网上有把这个叫表限定符的，有叫登记目录的。作用疑似是和其他种类的数据库做区分。

#### TABLE_SCHEMA

表格所属的库。

#### TABLE_NAME

表名

#### COLUMN_NAME

字段名

#### ORDINAL_POSITION

字段标识。

其实就是字段编号，从1开始往后排。

#### COLUMN_DEFAULT

字段默认值。

#### IS_NULLABLE

字段是否可以是NULL。

该列记录的值是YES或者NO。

#### DATA_TYPE

数据类型。

里面的值是字符串，比如varchar，float，int。

#### CHARACTER_MAXIMUM_LENGTH

字段的最大字符数。

假如字段设置为varchar(50)，那么这一列记录的值就是50。

该列只适用于二进制数据，字符，文本，图像数据。其他类型数据比如int，float，datetime等，在该列显示为NULL。

#### CHARACTER_OCTET_LENGTH

字段的最大字节数。

和最大字符数一样，只适用于二进制数据，字符，文本，图像数据，其他类型显示为NULL。

和最大字符数的数值有比例关系，和字符集有关。比如UTF8类型的表，最大字节数就是最大字符数的3倍。

#### NUMERIC_PRECISION

数字精度。

适用于各种数字类型比如int，float之类的。

如果字段设置为int(10)，那么在该列保存的数值是9，少一位，还没有研究原因。

如果字段设置为float(10,3)，那么在该列报错的数值是10。

非数字类型显示为在该列NULL。

#### NUMERIC_SCALE

小数位数。

和数字精度一样，适用于各种数字类型比如int，float之类。

如果字段设置为int(10)，那么在该列保存的数值是0，代表没有小数。

如果字段设置为float(10,3)，那么在该列报错的数值是3。

非数字类型显示为在该列NULL。

#### DATETIME_PRECISION

datetime类型和SQL-92interval类型数据库的子类型代码。

我本地datetime类型的字段在该列显示为0。

其他类型显示为NULL。

#### CHARACTER_SET_NAME

字段字符集名称。比如utf8。

### COLLATION_NAME

字符集排序规则。

比如utf8_general_ci，是不区分大小写一种排序规则。utf8_general_cs，是区分大小写的排序规则。

#### COLUMN_TYPE

字段类型。比如float(9,3)，varchar(50)。

#### COLUMN_KEY

索引类型。

可包含的值有PRI，代表主键，UNI，代表唯一键，MUL，可重复。

#### EXTRA

其他信息。

比如主键的auto_increment。

#### PRIVILEGES

权限

多个权限用逗号隔开，比如 select,insert,update,references

#### COLUMN_COMMENT

字段注释

#### GENERATION_EXPRESSION

组合字段的公式。

组合字段的介绍可以参考以下文章：

<http://blog.csdn.net/lkforce/article/details/79557373>