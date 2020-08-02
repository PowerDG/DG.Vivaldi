SELECT
(case when a.colorder=1 then d.name else '' end) as 表名,--如果表名相同就返回空
a.colorder as 字段序号,
a.name as 字段名,
(case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end) as 标识,
(case when (SELECT count(*) FROM sysobjects--查询主键
WHERE (name in
(SELECT name FROM sysindexes
WHERE (id = a.id) AND (indid in
(SELECT indid FROM sysindexkeys
WHERE (id = a.id) AND (colid in
(SELECT colid FROM syscolumns
WHERE (id = a.id) AND (name = a.name))
)))))
AND (xtype = 'PK'))>0 then '√' else '' end) as 主键,--查询主键END
b.name as 类型,
a.length as 占用字节数,
COLUMNPROPERTY(a.id,a.name,'PRECISION') as 长度,
isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0) as 小数位数,
(case when a.isnullable=1 then 'y'else '' end) as 允许空,
isnull(e.text,'') as 默认值,
isnull(g.[value],'') AS 字段说明
FROM syscolumns a left join systypes b
on a.xtype=b.xusertype
inner join sysobjects d
on a.id=d.id and d.xtype='U' and d.name<>'dtproperties'
left join syscomments e
on a.cdefault=e.id
left join sys.extended_properties g
on a.id=g.major_id AND a.colid = g.minor_id
order by a.id,a.colorder 