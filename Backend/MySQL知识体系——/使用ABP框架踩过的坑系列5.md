【melo.EntityFrameworkCore.MySql】 



完成以上步骤，从MSSQL切换到了Mysql, 
大功告成！理想是这样的，事实上也大部分可行，但会报一些莫名其妙的错误：约束错误！这些问题，一度让我很沮丧，甚至想放弃mysql, 
迫于linux下装MSSQL的恐怖，还是坚持用mysql, 
后在baidu的帮助下，找到了原因：Microsoft.entityframeworkcore.mysql 
有问题，必须用melo.EntityFrameworkCore.MySql