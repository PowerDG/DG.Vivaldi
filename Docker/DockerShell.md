## 我们可以使用以下两种方式在Windows环境下使用docker：

https://www.cnblogs.com/moashen/p/8067612.html



### 1. 直接安装：

```
Docker支持直接安装并运行在windows环境下，但对Windows的版本和xu虚拟化服务等有着一定的要求。
而hyper-v服务会对VM等虚拟机的正常服务造成影响，因此不建议此种方法（血泪教训）。
```

### 2. 使用Docker Toolbox：

```
除了可以避免方法1出现的问题外，还可以支持集群环境的搭建。
```

方法1安装过程较为简单，但要注意Hyper-V的支持是否开启，
过程中可能会重启下面。下文主要对
方法2做一些说明。

##### 1. 阿里云提供的国内开源镜像站点
因为Docker Toolbox是存放在Docker公司网站上，国内的用户下载非常慢，所以从阿里云提供的国内开源镜像站点上下载：
​     <https://mirrors.aliyun.com/docker-toolbox/windows/docker-toolbox/>

#####  2. Docker Quickstart   Terminal 
安装过程会安装VirtualBox虚拟机、Kitematic及Git。安装完成Toolbox后会有一个Docker Quickstart   Terminal的快捷方式，双击运行如果报错，注意是否已经需要关闭Hyper-V服务，使用Hyper-V时VirtualBox无法用64位的虚拟机。运行Docker  Quickstart  Terminal会在Virtualbox中创建一个叫做default的虚拟机，等待命令完成Docker虚拟机配置（运行至waiting for  an IP的命令时，可能会有点慢，耐心等待即可）。运行完成时，就可以在PowerShell中使用docker命令了。

###    3.与直装版不同
使用过程中Docker Toolbox与直装版不同的几点： (1) Docker Toolbox运行容器内的服务后默认使用default虚拟机所分到的ip， 而直装版使用的是本机ip。 (2) Docker Toolbox使用挂载命令时，将Windows的目录挂载到default虚拟机，然后使用 ssh 登录到 docker 宿主机，
`$ docker-machine ssh default`
 然后再虚拟机中运行相关容器；直装版再获取相关目录挂载权限后即可挂载。

######   命令开始default虚拟机 

​    
​    ```powershell
​    docker-machine start default
​    ```

###### **命令停止default虚拟机**

    ```powershell
    docker-machine stop  default
    ```

######  **然后重启default** 

    ```powershell
    docker-machine restart default
    ```

### 4. 镜像加速:





#####  更改镜像仓库地址

　　docker默认的镜像仓库地址是https://hub.docker.com/，但国内的下载速度太过缓慢，最好使用国内镜像，比如阿里云。

　　首先需要注册阿里云账号，然后在https://cr.console.aliyun.com/页面的左侧可以看到【镜像加速器】选项
　　![](https://images2018.cnblogs.com/blog/691999/201805/691999-20180512153033712-520562256.png)

在powershell或cmd中通过 【docker-machine ssh default】命令登录虚拟机（或使用设置好的XShell连接），执行如下命令：

```powershell
`sudo sed -i ``"s|EXTRA_ARGS='|EXTRA_ARGS='--registry-mirror=加速地址 |g"` `/``var``/lib/boot2docker/profile`



https://anuzyij8.mirror.aliyuncs.com

sudo sed -i ``"s|EXTRA_ARGS='|EXTRA_ARGS='--registry-mirror=加速地址 |g"` `/``var``/lib/boot2docker/profile`

```

###### 1登录

docker-machine ssh default       

```
docker-machine ssh default // 先进入虚拟机，default 是默认的虚拟机名称
sudo vi /var/lib/boot2docker/profile // 编辑这个文件，添加镜像源 --registry-mirror https://registry.docker-cn.com
```

$ sudo sed -i 
"s|EXTRA_ARGS='|EXTRA_ARGS='--registry-mirror=https://anuzyij8.mirror.aliyuncs.com |g" 
/var/lib/boot2docker/profile       



###### 2镜像

`sudo sed -i "s|EXTRA_ARGS='|EXTRA_ARGS='--registry-mirror=https://anuzyij8.mirror.aliyuncs.com |g" /var/lib/boot2docker/profile`



➜  ~ docker-machine ssh default "echo 'EXTRA_ARGS=\"--registry-mirror=<https://anuzyij8.mirror.aliyuncs.com> \"' | sudo tee -a /var/lib/boot2docker/profile"
EXTRA_ARGS="--registry-mirror=https://anuzyij8.mirror.aliyuncs.com"
➜  ~ docker-machine restart default



```
➜  ~ docker-machine ssh default "echo 'EXTRA_ARGS=\"--registry-mirror=https://yourcode.mirror.aliyuncs.com\"' | sudo tee -a /var/lib/boot2docker/profile"
EXTRA_ARGS="--registry-mirror=https://xxx.mirror.aliyuncs.com"
➜  ~ docker-machine restart default
```

###### 5退出

$ exit       

```
sudo /etc/init.d/docker restart // 重启 docker 进程
exit // 退出虚拟机
docker info // 看一下镜像源是否设置成功（是否有刚刚设置的 --registry-mirror 这一行）
docker pull nginx // 现在可以愉快地拉取`nginx`镜像了
```

###### 6重启

$ docker-machine restart default

docker-machine restart default



 

https://cr.console.aliyun.com/cn-hangzhou/mirrors

https://anuzyij8.mirror.aliyuncs.com