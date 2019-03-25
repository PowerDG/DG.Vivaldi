# RabbitMQ 安装

一般来说安装 RabbitMQ 之前要安装 Erlang ，可以去[Erlang官网](https://link.jianshu.com?t=http://www.erlang.org/downloads)下载。接着去[RabbitMQ官网](https://link.jianshu.com?t=https://www.rabbitmq.com/download.html)下载安装包，之后解压缩即可。根据操作系统不同官网提供了相应的安装说明：[Windows](https://link.jianshu.com?t=http://www.rabbitmq.com/install-windows.html)、[Debian / Ubuntu](https://link.jianshu.com?t=http://www.rabbitmq.com/install-debian.html)、[RPM-based Linux](https://link.jianshu.com?t=http://www.rabbitmq.com/install-rpm.html)、[Mac](https://link.jianshu.com?t=http://www.rabbitmq.com/install-standalone-mac.html)

如果是Mac 用户，个人推荐使用 HomeBrew 来安装，安装前要先更新 brew：

```
brew update
```

接着安装 rabbitmq 服务器：

```
brew install rabbitmq
```

这样 RabbitMQ 就安装好了，安装过程中会自动其所依赖的 Erlang 。

# RabbitMQ 运行和管理

1. 启动
    启动很简单，找到安装后的 RabbitMQ 所在目录下的 sbin 目录，可以看到该目录下有6个以 rabbitmq 开头的可执行文件，直接执行 rabbitmq-server 即可，下面将 RabbitMQ 的安装位置以 . 代替，启动命令就是：

```
./sbin/rabbitmq-server
```

启动正常的话会看到一些启动过程信息和最后的 completed with 7 plugins，这也说明启动的时候默认加载了7个插件。



![img](https:////upload-images.jianshu.io/upload_images/5015984-1392cdc83b0d8341.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1000/format/webp)

正常启动

1. 后台启动
    如果想让 RabbitMQ 以守护程序的方式在后台运行，可以在启动的时候加上 -detached 参数：

```
./sbin/rabbitmq-server -detached
```

1. 查询服务器状态
    sbin 目录下有个特别重要的文件叫 rabbitmqctl ，它提供了 RabbitMQ 管理需要的几乎一站式解决方案，绝大部分的运维命令它都可以提供。
    查询 RabbitMQ 服务器的状态信息可以用参数 status ：

```
./sbin/rabbitmqctl status
```

该命令将输出服务器的很多信息，比如 RabbitMQ 和 Erlang 的版本、OS 名称、内存等等

1. 关闭 RabbitMQ 节点
    我们知道 RabbitMQ 是用 Erlang 语言写的，在Erlang 中有两个概念：节点和应用程序。节点就是 Erlang 虚拟机的每个实例，而多个 Erlang 应用程序可以运行在同一个节点之上。节点之间可以进行本地通信（不管他们是不是运行在同一台服务器之上）。比如一个运行在节点A上的应用程序可以调用节点B上应用程序的方法，就好像调用本地函数一样。如果应用程序由于某些原因奔溃，Erlang 节点会自动尝试重启应用程序。
    如果要关闭整个 RabbitMQ 节点可以用参数 stop ：

```
./sbin/rabbitmqctl stop
```

它会和本地节点通信并指示其干净的关闭，也可以指定关闭不同的节点，包括远程节点，只需要传入参数 -n ：

```
./sbin/rabbitmqctl -n rabbit@server.example.com stop 
```

-n node 默认 node 名称是 rabbit@server ，如果你的主机名是 [server.example.com](https://link.jianshu.com?t=http://server.example.com) ，那么 node 名称就是 [rabbit@server.example.com](https://link.jianshu.com?t=mailto:rabbit@server.example.com) 。

1. 关闭 RabbitMQ 应用程序
    如果只想关闭应用程序，同时保持 Erlang 节点运行则可以用 stop_app：

```
./sbin/rabbitmqctl stop_app
```

这个命令在后面要讲的集群模式中将会很有用。

1. 启动 RabbitMQ 应用程序

```
./sbin/rabbitmqctl start_app
```

1. 重置 RabbitMQ 节点

```
./sbin/rabbitmqctl reset
```

该命令将清除所有的队列。

1. 查看已声明的队列

```
./sbin/rabbitmqctl list_queues
```

1. 查看交换器

```
./sbin/rabbitmqctl list_exchanges
```

该命令还可以附加参数，比如列出交换器的名称、类型、是否持久化、是否自动删除：

```
./sbin/rabbitmqctl list_exchanges name type durable auto_delete
```

1. 查看绑定

```
./sbin/rabbitmqctl list_bindings
```

作者：预流

链接：https://www.jianshu.com/p/79ca08116d57/

来源：简书

简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。

---

## [**常用相关命令**](https://blog.51cto.com/ttxsgoto/1857931)

```bash
添加用户：
rabbitmqctl add_user abc abc

添加权限：
rabbitmqctl set_permissions -p "/" abc ".*" ".*" ".*"

设置用户标签：
rabbitmqctl set_user_tags abc administrator

删除用户：
rabbitmqctl delete_user guest

修改密码：
rabbitmqctl change_password   username  newpassword

list_users
add_vhost   vhostpath
rabbitmqctl list_user_permissions abc  
list_queues 
list_exchanges
list_bindings
```