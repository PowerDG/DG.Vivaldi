# Docker run 命令的使用方法

2015年10月12日 17:44:01 					[代码独裁者](https://me.csdn.net/wongson) 						阅读数：50147 	 

https://blog.csdn.net/wongson/article/details/49077353										

---

#  Docker run 命令的使用方法

 注意，本文基于最新的Docker 1.4文档翻译。

 Docker会在隔离的容器中运行进程。当运行 

```
docker run
```

命令时，Docker会启动一个进程，并为这个进程分配其独占的文件系统、网络资源和以此进程为根进程的进程组。在容器启动时，

镜像

可能已经定义了要运行的二进制文件、暴露的网络端口等，但是用户可以通过

```
docker
 run
```

命令重新定义（译者注：docker run可以控制一个容器运行时的行为，它可以覆盖

```
docker build
```

在构建镜像时的一些默认配置），这也是为什么

```
run
```

命令相比于其它命令有如此多的参数的原因。

####  命令格式

 最基本的

```
docker run
```

命令的格式如下：

```
$ sudo docker run [OPTIONS] IMAGE[:TAG] [COMMAND] [ARG...]
```

 如果需要查看[OPTIONS]的详细使用说明，请参考Docker关于OPTIONS的

章节

。这里仅简要介绍Run所使用到的参数。OPTIONS总起来说可以分为两类：

1. 设置运行方式： 
    - 决定容器的运行方式，前台执行还是后台执行；
    - 设置containerID；
    - 设置网络参数；
    - 设置容器的CPU和内存参数；- 设置权限和LXC参数；
2. 设置镜像的默认资源，也就是说用户可以使用该命令来覆盖在镜像构建时的一些默认配置。

```
docker run [OPTIONS]
```

可以让用户完全控制容器的生命周期，并允许用户覆盖执行

```
docker
 build
```

时所设定的参数，甚至也可以修改本身由Docker所控制的内核级参数。

####  Operator exclusive options

 当执行

```
docker run
```

时可以设置以下参数：

- Detached vs Foreground 
    - Detached (-d)- Foreground
- Container Identification 
    - Name (--name)- PID Equivalent
- IPC Setting
- Network Settings
- Clean Up (--rm)
- Runtime Constraints on CPU and Memory
- Runtime Privilege, Linux Capabilities, and LXC Configuration

 接下来我们依次进行介绍。

Detached vs foreground

 当我们启动一个容器时，首先需要确定这个容器是运行在前台还是运行在后台。

```
-d=false: Detached mode: Run container in the background, print new container id
```

Detached (-d)

 如果在

```
docker run
```

后面追加

```
-d=true
```

或者

```
-d
```

，那么容器将会运行在后台模式。此时所有I/O数据只能通过网络资源或者共享卷组来进行交互。因为容器不再监听你执行

```
docker
 run
```

的这个终端命令行窗口。但你可以通过执行

```
docker attach
```

来重新附着到该容器的回话中。需要注意的是，容器运行在后台模式下，是不能使用

```
--rm
```

选项的。

Foregroud

 在前台模式下（不指定

```
-d
```

参数即可），Docker会在容器中启动进程，同时将当前的命令行窗口附着到容器的标准输入、标准输出和标准错误中。也就是说容器中所有的输出都可以在当前窗口中看到。甚至它都可以虚拟出一个TTY窗口，来执行信号中断。这一切都是可以配置的：

```
-a=[]          　　　　 : Attach to `STDIN`, `STDOUT` and/or `STDERR`
-t=false        　　  : Allocate a pseudo-tty
--sig-proxy=true　: Proxify all received signal to the process (non-TTY mode only)
-i=false        　　  : Keep STDIN open even if not attached
```

 如果在执行run命令时没有指定

```
-a
```

参数，那么Docker默认会挂载所有标准数据流，包括输入输出和错误，你可以单独指定挂载哪个标准流。

```
$ sudo docker run -a stdin -a stdout -i -t ubuntu /bin/bash
```

 如果要进行交互式操作（例如Shell脚本），那我们必须使用

```
-i -t
```

参数同容器进行数据交互。但是当通过管道同容器进行交互时，就不需要使用

```
-t
```

参数，例如下面的命令：

```
echo test | docker run -i busybox cat
```

容器识别

Name（--name）

 可以通过三种方式为容器命名：

 \1. 使用UUID长命名（"f78375b1c487e03c9438c729345e54db9d20cfa2ac1fc3494b6eb60872e74778"）

 \2. 使用UUID短命令（"f78375b1c487"）

 \3. 使用Name("evil_ptolemy")

 这个UUID标示是由Docker deamon生成的。如果你在执行

```
docker run
```

时没有指定

```
--name
```

，那么deamon会自动生成一个随机字符串UUID。但是对于一个容器来说有个name会非常方便，当你需要连接其它容器时或者类似需要区分其它容器时，使用容器名称可以简化操作。无论容器运行在前台或者后台，这个名字都是有效的。

PID equivalent

 如果在使用Docker时有自动化的需求，你可以将containerID输出到指定的文件中（PIDfile），类似于某些应用程序将自身ID输出到文件中，方便后续脚本操作。

```
--cidfile="": Write the container ID to the file
```

Image[:tag]

 当一个镜像的名称不足以分辨这个镜像所代表的含义时，你可以通过tag将版本信息添加到run命令中，以执行特定版本的镜像。例如: 

```
docker run
 ubuntu:14.04
```

IPC Settings

 默认情况下，所有容器都开启了IPC命名空间。

```
--ipc=""  : Set the IPC mode for the container,
        'container:<name|id>': reuses another container's IPC namespace
        'host': use the host's IPC namespace inside the container
```

 IPC（POSIX/SysV IPC）命名空间提供了相互隔离的命名共享内存、信号灯变量和消息队列。

 共享内存可以提高进程数据的交互速度。共享内存一般用在数据库和高性能应用（C/OpenMPI、C++/using boost libraries）上或者金融服务上。如果需要容器中部署上述类型的应用，那么就应该在多个容器直接使用共享内存了。

Network settings

 默认情况下，所有的容器都开启了网络接口，同时可以接受任何外部的数据请求。

```
--dns=[]         : Set custom dns servers for the container
--net="bridge"   : Set the Network mode for the container
                          'bridge': creates a new network stack for the container on the docker bridge
                          'none': no networking for this container
                          'container:<name|id>': reuses another container network stack
                          'host': use the host network stack inside the container
--add-host=""    : Add a line to /etc/hosts (host:IP)
--mac-address="" : Sets the container's Ethernet device's MAC address
```

 你可以通过

```
docker run --net none
```

来关闭网络接口，此时将关闭所有网络数据的输入输出，你只能通过STDIN、STDOUT或者files来完成I/O操作。默认情况下，容器使用主机的DNS设置，你也可以通过

```
--dns
```

来覆盖容器内的DNS设置。同时Docker为容器默认生成一个MAC地址，你可以通过

```
--mac-address
 12:34:56:78:9a:bc
```

来设置你自己的MAC地址。

 Docker支持的网络模式有：

- none。关闭容器内的网络连接
- bridge。通过veth接口来连接容器，默认配置。
- host。允许容器使用host的网络堆栈信息。 注意：这种方式将允许容器访问host中类似D-BUS之类的系统服务，所以认为是不安全的。
- container。使用另外一个容器的网络堆栈信息。 　　
    **None模式**

 将网络模式设置为none时，这个容器将不允许访问任何外部

router

。这个容器内部只会有一个loopback接口，而且不存在任何可以访问

外部网络

的router。

Bridge模式

 Docker默认会将容器设置为bridge模式。此时在主机上面将会存在一个docker0的网络接口，同时会针对容器创建一对veth接口。其中一个veth接口是在主机充当网卡桥接作用，另外一个veth接口存在于容器的命名空间中，并且指向容器的loopback。Docker会自动给这个容器分配一个IP，并且将容器内的数据通过桥接转发到外部。

Host模式

 当网络模式设置为host时，这个容器将完全共享host的网络堆栈。host所有的网络接口将完全对容器开放。容器的主机名也会存在于主机的hostname中。这时，容器所有对外暴露的端口和对其它容器的连接，将完全失效。

Container模式

 当网络模式设置为Container时，这个容器将完全复用另外一个容器的网络堆栈。同时使用时这个容器的名称必须要符合下面的格式：--net container:<name|id>.

 比如当前有一个绑定了本地地址localhost的Redis容器。如果另外一个容器需要复用这个网络堆栈，则需要如下操作：

```
$ sudo docker run -d --name redis example/redis --bind 127.0.0.1
$ # use the redis container's network stack to access localhost
$ sudo docker run --rm -ti --net container:redis example/redis-cli -h 127.0.0.1
```

管理/etc/hosts

 /etc/hosts文件中会包含容器的hostname信息，我们也可以使用

```
--add-host
```

这个参数来动态添加/etc/hosts中的数据。

```
$ /docker run -ti --add-host db-static:86.75.30.9 ubuntu cat /etc/hosts
172.17.0.22     09d03f76bf2c
fe00::0         ip6-localnet
ff00::0         ip6-mcastprefix
ff02::1         ip6-allnodes
ff02::2         ip6-allrouters
127.0.0.1       localhost
::1             localhost ip6-localhost ip6-loopback
86.75.30.9      db-static
```

Clean up (--rm)

 默认情况下，每个容器在退出时，它的文件系统也会保存下来，这样一方面调试会方便些，因为你可以通过查看日志等方式来确定最终状态。另外一方面，你也可以保存容器所产生的数据。但是当你仅仅需要短暂的运行一个容器，并且这些数据不需要保存，你可能就希望Docker能在容器结束时自动清理其所产生的数据。

 这个时候你就需要--rm这个参数了。 

注意：--rm 和 -d不能共用！

```
--rm=false: Automatically remove the container when it exits (incompatible with -d)
```

Security configuration

```
--security-opt="label:user:USER"   : Set the label user for the container
--security-opt="label:role:ROLE"   : Set the label role for the container
--security-opt="label:type:TYPE"   : Set the label type for the container
--security-opt="label:level:LEVEL" : Set the label level for the container
--security-opt="label:disable"     : Turn off label confinement for the container
--secutity-opt="apparmor:PROFILE"  : Set the apparmor profile to be applied  to the container
```

 你可以通过

```
--security-opt
```

修改容器默认的schema标签。比如说，对于一个MLS系统来说（译者注：MLS应该是指Multiple  Listing System），你可以指定MCS/MLS级别。使用下面的命令可以在不同的容器间分享内容：

```
#docker run --security-opt label:level:s0:c100,c200 -i -t fedora bash
```

 如果是MLS系统，则使用下面的命令：

```
# docker run --security-opt label:level:TopSecret -i -t rhel7 bash
```

 使用下面的命令可以在容器内禁用安全策略：

```
# docker run --security-opt label:disable -i -t fedora bash
```

 如果你需要在容器内执行更为严格的安全策略，那么你可以为这个容器指定一个策略替代，比如你可以使用下面的命令来指定容器只监听Apache端口：

```
# docker run --security-opt label:type:svirt_apache_t -i -t centos bash
```

 注意：此时，你的主机环境中必须存在一个名为svirt_apache_t的安全策略。

Runtime constraints on CPU and memory

 下面的参数可以用来调整容器内的性能。

```
-m="": Memory limit (format: <number><optional unit>, where unit = b, k, m or g)
-c=0 : CPU shares (relative weight)
```

 通过

```
docker run -m
```

可以调整容器所使用的内存资源。如果主机支持swap内存，那么可以使用

```
-m
```

可以设定比主机物理内存还大的值。

 同样，通过

```
-c
```

可以调整容器的CPU优先级。默认情况下，所有的容器拥有相同的CPU优先级和CPU调度周期，但你可以通过Docker来通知内核给予某个或某几个容器更多的CPU计算周期。

 比如，我们使用

```
-c
```

或者

```
--cpu-shares
 =0
```

启动了C0、C1、C2三个容器，使用-c/--cpu-shares=512启动了C3容器。这时，C0、C1、C2可以100%的使用CPU资源（1024），但C3只能使用50%的CPU资源（512）。如果这个主机的操作系统是时序调度类型的，每个CPU时间片是100微秒，那么C0、C1、C2将完全使用掉这100微秒，而C3只能使用50微秒。

Runtime privilege, Linux capabilities, and LXC configuration

```
--cap-add: Add Linux capabilities
--cap-drop: Drop Linux capabilities
--privileged=false: Give extended privileges to this container
--device=[]: Allows you to run devices inside the container without the --privileged flag.
--lxc-conf=[]: (lxc exec-driver only) Add custom lxc options --lxc-conf="lxc.cgroup.cpuset.cpus = 0,1"
```

 默认情况下，Docker的容器是没有特权的，例如不能在容器中再启动一个容器。这是因为默认情况下容器是不能访问任何其它设备的。但是通过"privileged"，容器就拥有了访问任何其它设备的权限。

 当操作者执行

```
docker run --privileged
```

时，Docker将拥有访问主机所有设备的权限，同时Docker也会在apparmor或者

selinux

做一些设置，使容器可以容易的访问那些运行在容器外部的设备。你可以访问

Docker博客

来获取更多关于--privileged的用法。

 同时，你也可以限制容器只能访问一些指定的设备。下面的命令将允许容器只访问一些特定设备：

```
$ sudo docker run --device=/dev/snd:/dev/snd ...
```

 　　默认情况下，容器拥有对设备的读、写、创建设备文件的权限。使用

```
:rwm
```

来配合

```
--device
```

，你可以控制这些权限。

```
　$ sudo docker run --device=/dev/sda:/dev/xvdc --rm -it ubuntu fdisk  /dev/xvdc

Command (m for help): q
$ sudo docker run --device=/dev/sda:/dev/xvdc:r --rm -it ubuntu fdisk  /dev/xvdc
You will not be able to write the partition table.

Command (m for help): q

$ sudo docker run --device=/dev/sda:/dev/xvdc:w --rm -it ubuntu fdisk  /dev/xvdc
    crash....

$ sudo docker run --device=/dev/sda:/dev/xvdc:m --rm -it ubuntu fdisk  /dev/xvdc
fdisk: unable to open /dev/xvdc: Operation not permitted
```

 使用

```
--cap-add
```

和

```
--cap-drop
```

，配合

```
--privileged
```

，你可以更细致的控制人哦怒气。默认使用这两个参数的情况下，容器拥有一系列的内核修改权限，这两个参数都支持

```
all
```

值，如果你想让某个容器拥有除了MKNOD之外的所有内核权限，那么可以执行下面的命令：

```
$ sudo docker run --cap-add=ALL --cap-drop=MKNOD ...
```

 如果需要修改网络接口数据，那么就建议使用

```
--cap-add=NET_ADMIN
```

，而不是使用

```
--privileged
```

。

```
$ docker run -t -i --rm  ubuntu:14.04 ip link add dummy0 type dummy
RTNETLINK answers: Operation not permitted
$ docker run -t -i --rm --cap-add=NET_ADMIN ubuntu:14.04 ip link add dummy0 type dummy
```

 如果要挂载一个FUSE文件系统，那么就需要

```
--cap-add
```

和

```
--device
```

了。

```
$ docker run --rm -it --cap-add SYS_ADMIN sshfs sshfs sven@10.10.10.20:/home/sven /mnt
fuse: failed to open /dev/fuse: Operation not permitted
$ docker run --rm -it --device /dev/fuse sshfs sshfs sven@10.10.10.20:/home/sven /mnt
fusermount: mount failed: Operation not permitted
$ docker run --rm -it --cap-add SYS_ADMIN --device /dev/fuse sshfs
# sshfs sven@10.10.10.20:/home/sven /mnt
The authenticity of host '10.10.10.20 (10.10.10.20)' can't be established.
ECDSA key fingerprint is 25:34:85:75:25:b0:17:46:05:19:04:93:b5:dd:5f:c6.
Are you sure you want to continue connecting (yes/no)? yes
sven@10.10.10.20's password:
root@30aa0cfaf1b5:/# ls -la /mnt/src/docker
total 1516
drwxrwxr-x 1 1000 1000   4096 Dec  4 06:08 .
drwxrwxr-x 1 1000 1000   4096 Dec  4 11:46 ..
-rw-rw-r-- 1 1000 1000     16 Oct  8 00:09 .dockerignore
-rwxrwxr-x 1 1000 1000    464 Oct  8 00:09 .drone.yml
drwxrwxr-x 1 1000 1000   4096 Dec  4 06:11 .git
-rw-rw-r-- 1 1000 1000    461 Dec  4 06:08 .gitignore
```

 如果Docker守护进程在启动时选择了

```
lxc
```

 lxc-driver（

```
docker
 -d --exec-driver=lxc
```

），那么就可以使用

```
--lxc-conf
```

来设定LXC参数。但需要注意的是，未来主机上的Docker  deamon有可能不会使用LXC，所以这些参数有可能会包含一些没有实现的配置功能。这意味着，用户在操作这些参数时必须要十分熟悉LXC。

 特别注意：当你使用

```
--lxc-conf
```

修改容器参数后，Docker deamon将不再管理这些参数，那么用户必须自行进行管理。比如说，你使用

```
--lxc-conf
```

修改了容器的IP地址，那么在/etc/hosts里面是不会自动体现的，需要你自行维护。

Overriding Dockerfile image defaults

 　　当开发者使用

Dockerfile

进行build或者使用commit提交容器时，开发人员可以设定一些镜像默认参数。

 这些参数中，有四个是无法被覆盖的：FROM、MAINTAINER、RUN和ADD，其余参数都可以通过

```
docker run
```

进行覆盖。我们将介绍如何对这些参数进行覆盖。

- CMD (Default Command or Options)
- ENTRYPOINT (Default Command to Execute at Runtime)
- EXPOSE (Incoming Ports)
- ENV (Environment Variables)
- VOLUME (Shared Filesystems)
- USER
- WORKDIR 　　

CMD (default command or options)

```
$ sudo docker run [OPTIONS] IMAGE[:TAG] [COMMAND] [ARG...]
```

 这个命令中的COMMAND部分是可选的。因为这个IMAGE在build时，开发人员可能已经设定了默认执行的命令。作为操作人员，你可以使用上面命令中新的command来覆盖旧的command。

 如果镜像中设定了ENTRYPOINT，那么命令中的CMD也可以作为参数追加到ENTRYPOINT中。

ENTRYPOINT (default command to execute at runtime)

```
--entrypoint="": Overwrite the default entrypoint set by the image
```

 这个ENTRYPOINT和COMMAND类似，它指定了当容器执行时，需要启动哪些进程。相对COMMAND而言，ENTRYPOINT是很难进行覆盖的，这个ENTRYPOINT可以让容器设定默认启动行为，所以当容器启动时，你可以执行任何一个二进制可执行程序。你也可以通过COMMAND为ENTRYPOINT传递参数。但当你需要在容器中执行其它进程时，你就可以指定其它ENTRYPOINT了。

 下面就是一个例子，容器可以在启动时自动执行Shell，然后启动其它进程。

```
$ sudo docker run -i -t --entrypoint /bin/bash example/redis
#or two examples of how to pass more parameters to that ENTRYPOINT:
$ sudo docker run -i -t --entrypoint /bin/bash example/redis -c ls -l
$ sudo docker run -i -t --entrypoint /usr/bin/redis-cli example/redis --help
```

EXPOSE (incoming ports)

 　　Dockefile在网络方面除了提供一个EXPOSE之外，没有提供其它选项。下面这些参数可以覆盖Dockefile的expose默认值：

```
--expose=[]: Expose a port or a range of ports from the container
        without publishing it to your host
-P=false   : Publish all exposed ports to the host interfaces
-p=[]      : Publish a container᾿s port to the host (format:
         ip:hostPort:containerPort | ip::containerPort |
         hostPort:containerPort | containerPort)
         (use 'docker port' to see the actual mapping)
--link=""  : Add link to another container (name:alias)
```

 　　--expose可以让容器接受外部传入的数据。容器内监听的端口不需要和外部主机的端口相同。比如说在容器内部，一个HTTP服务监听在80端口，对应外部主机的端口就可能是49880.

 　　如果使用

```
-p
```

或者

```
-P
```

，那么容器会开放部分端口到主机，只要对方可以连接到主机，就可以连接到容器内部。当使用

```
-P
```

时，Docker会在主机中随机从49153  和65535之间查找一个未被占用的端口绑定到容器。你可以使用

```
docker port
```

来查找这个随机绑定端口。

 当你使用

```
--link
```

方式时，作为客户端的容器可以通过私有网络形式访问到这个容器。同时Docker会在客户端的容器中设定一些环境变量来记录绑定的IP和PORT。

ENV (environment variables)

```
HOME    Set based on the value of USER
HOSTNAME    The hostname associated with the container
PATH    Includes popular directories, such as :
/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin
TERM    xterm if the container is allocated a psuedo-TTY
```

 当容器启动时，会自动在容器中初始化这些变量。

 操作人员可以通过

```
-e
```

来设定任意的环境变量，甚至覆盖已经存在的环境变量，或者是在Dockerfile中通过ENV设定的环境变量。

```
$ sudo docker run -e "deep=purple" --rm ubuntu /bin/bash -c export
declare -x HOME="/"
declare -x HOSTNAME="85bc26a0e200"
declare -x OLDPWD
declare -x PATH="/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin"
declare -x PWD="/"
declare -x SHLVL="1"
declare -x container="lxc"
declare -x deep="purple"
```

 操作人员可以通过

```
-h
```

来设定hostname。也可以使用"--link name:alias"来设定环境变量，当使用

```
--link
```

后，Docker将根据后面提供的IP和PORT信息来连接服务端容器。下面就是使用redis的例子：

```
# Start the service container, named redis-name
$ sudo docker run -d --name redis-name dockerfiles/redis
4241164edf6f5aca5b0e9e4c9eccd899b0b8080c64c0cd26efe02166c73208f3

# The redis-name container exposed port 6379
$ sudo docker ps
CONTAINER ID        IMAGE                      COMMAND                CREATED             STATUS              PORTS               NAMES
4241164edf6f        $ dockerfiles/redis:latest   /redis-stable/src/re   5 seconds ago       Up 4 seconds        6379/tcp            redis-name

# Note that there are no public ports exposed since we didn᾿t use -p or -P
$ sudo docker port 4241164edf6f 6379
2014/01/25 00:55:38 Error: No public port '6379' published for 4241164edf6f
```

 你使用

```
--link
```

后，就可以获取到关于Redis容器的相关信息。

```
$ sudo docker run --rm --link redis-name:redis_alias --entrypoint /bin/bash dockerfiles/redis -c export
declare -x HOME="/"
declare -x HOSTNAME="acda7f7b1cdc"
declare -x OLDPWD
declare -x PATH="/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin"
declare -x PWD="/"
declare -x REDIS_ALIAS_NAME="/distracted_wright/redis"
declare -x REDIS_ALIAS_PORT="tcp://172.17.0.32:6379"
declare -x REDIS_ALIAS_PORT_6379_TCP="tcp://172.17.0.32:6379"
declare -x REDIS_ALIAS_PORT_6379_TCP_ADDR="172.17.0.32"
declare -x REDIS_ALIAS_PORT_6379_TCP_PORT="6379"
declare -x REDIS_ALIAS_PORT_6379_TCP_PROTO="tcp"
declare -x SHLVL="1"
declare -x container="lxc"
#And we can use that information to connect from another container as a client:
$ sudo docker run -i -t --rm --link redis-name:redis_alias --entrypoint /bin/bash dockerfiles/redis -c '/redis-stable/src/redis-cli -h $REDIS_ALIAS_PORT_6379_TCP_ADDR -p $REDIS_ALIAS_PORT_6379_TCP_PORT'
172.17.0.32:6379>
```

 Docker也会将这个alias的IP地址写入到/etc/hosts文件中。然后你就可以通过别名来访问link后的容器。

```
$ sudo docker run -d --name servicename busybox sleep 30
$ sudo docker run -i -t --link servicename:servicealias busybox ping -c 1 servicealias
```

 如果你重启了源容器（servicename），相关联的容器也会同步更新/etc/hosts。

VOLUME (shared filesystems)

```
-v=[]: Create a bind mount with: [host-dir]:[container-dir]:[rw|ro].
   If "container-dir" is missing, then docker creates a new volume.
--volumes-from="": Mount all volumes from the given container(s)
```

 关于volume参数，可以在

Managing data in containers

查看详细说明，需要注意的是开发人员可以在Dockerfile中设定多个volume，但是只能由运维人员设置容器直接的volume访问。

USER

 容器中默认的用户是root，但是开发人员创建新的用户之后，这些新用户也是可以使用的。开发人员可以通过Dockerfile的USER设定默认的用户，并通过"-u "来覆盖这些参数。

WORKDIR

 容器中默认的工作目录是根目录（/）。开发人员可以通过Dockerfile的WORKDIR来设定默认工作目录，操作人员可以通过"-w"来覆盖默认的工作目录。