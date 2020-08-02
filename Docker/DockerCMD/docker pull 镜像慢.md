[docker 解决下载镜像慢](https://blog.csdn.net/dounine/article/details/53028974)

使用docker pull 镜像这个超级慢，因为docker的hub是在国外的，所以是特别慢的，有什么办法可以解决这个问题么？答案肯定是有的，我们可以使用docker的代理的解决这个问题，大家要自己准备一个可以使用的http代理地扯 原文。
代理设置

此文使用的系统是fedora

创建一个docker service目录

mkdir /etc/systemd/system/docker.service.d

    1

创建 /etc/systemd/system/docker.service.d/http-proxy.conf 文件，把代理地扯写入文件中

vim /etc/systemd/system/docker.service.d/http-proxy.conf

    1

文件内容如下

[Service]
Environment="HTTP_PROXY=http://代理ip:端口"

    1
    2

让配置文件生效

systemctl daemon-reload

    1

重启docker

systemctl restart docker.service

    1

尝试pull一个镜像看看吧

docker pull jenkins
--------------------- 
作者：dounine 
来源：CSDN 
原文：https://blog.csdn.net/dounine/article/details/53028974 
版权声明：本文为博主原创文章，转载请附上博文链接！