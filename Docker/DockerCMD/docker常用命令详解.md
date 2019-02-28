# docker常用命令详解

2016年07月11日 11:35:54

permike

阅读数：80143---

---

https://blog.csdn.net/permike/article/details/51879578

---

个人分类： 																[docke																](https://blog.csdn.net/permike/article/category/6289903) 							

 														

 本文只记录docker命令在大部分情境下的使用，如果想了解每一个选项的细节，请参考官方文档，这里只作为自己以后的备忘记录下来。

 根据自己的理解，总的来说分为以下几种：

- 容器生命周期管理 — `docker [run|start|stop|restart|kill|rm|pause|unpause]`
- 容器操作运维 — `docker [ps|inspect|top|attach|events|logs|wait|export|port]`
- 容器rootfs命令 — `docker [commit|cp|diff]`
- 镜像仓库 — `docker [login|pull|push|search]`
- 本地镜像管理 — `docker [images|rmi|tag|build|history|save|import]`
- 其他命令 — `docker [info|version]`

 看一个变迁图
![img](https://segmentfault.com/img/bVdlRG)

###  1. 列出机器上的镜像（images）

```
# docker images 



REPOSITORY               TAG             IMAGE ID        CREATED         VIRTUAL SIZE



ubuntu                   14.10           2185fd50e2ca    13 days ago     236.9 MB



…
```

 其中我们可以根据REPOSITORY来判断这个镜像是来自哪个[服务器](https://www.baidu.com/s?wd=%E6%9C%8D%E5%8A%A1%E5%99%A8&tn=24004469_oem_dg&rsv_dl=gh_pl_sl_csd)，如果没有 / 则表示官方镜像，类似于`username/repos_name`表示Github的个人公共库，类似于`regsistory.example.com:5000/repos_name`则表示的是私服。
 IMAGE ID列其实是缩写，要显示完整则带上`--no-trunc`选项

###  2. 在docker index中搜索image（search）

 `Usage: docker search TERM`

```
# docker search seanlo



NAME                DESCRIPTION           STARS     OFFICIAL   AUTOMATED



seanloook/centos6   sean's docker repos         0
```

 搜索的范围是官方镜像和所有个人公共镜像。NAME列的 / 后面是仓库的名字。

###  3. 从docker registry server 中下拉image或repository（pull）

 `Usage: docker pull [OPTIONS] NAME[:TAG]`

```
# docker pull centos
```

 上面的命令需要注意，在docker  v1.2版本以前，会下载官方镜像的centos仓库里的所有镜像，而从v.13开始官方文档里的说明变了：will pull the  centos:latest image, its intermediate layers and any aliases of the same  id，也就是只会下载tag为latest的镜像（以及同一images id的其他tag）。
 也可以明确指定具体的镜像：

```
# docker pull centos:centos6
```

 当然也可以从某个人的公共仓库（包括自己是私人仓库）拉取，形如`docker pull username/repository<:tag_name>` ：

```
# docker pull seanlook/centos:centos6
```

 如果你没有网络，或者从其他私服获取镜像，形如`docker pull registry.domain.com:5000/repos:<tag_name>`

```
# docker pull dl.dockerpool.com:5000/mongo:latest
```

###  4. 推送一个image或repository到registry（push）

 与上面的pull对应，可以推送到Docker Hub的Public、Private以及私服，但不能推送到Top Level Repository。

```
# docker push seanlook/mongo



# docker push registry.tp-link.net:5000/mongo:2014-10-27
```

 registry.tp-link.net也可以写成IP，172.29.88.222。
 在repository不存在的情况下，命令行下push上去的会为我们创建为私有库，然而通过浏览器创建的默认为公共库。

###  5. 从image启动一个container（run）

 `docker run`命令首先会从特定的image创之上create一层可写的container，然后通过start命令来启动它。停止的container可以重新启动并保留原来的修改。run命令启动参数有很多，以下是一些常规使用说明，更多部分请参考<http://www.cnphp6.com/archives/24899>
 当利用 docker run 来创建容器时，Docker 在后台运行的标准操作包括：

- 检查本地是否存在指定的镜像，不存在就从公有仓库下载
- 利用镜像创建并启动一个容器
- 分配一个文件系统，并在只读的镜像层外面挂载一层可读写层
- 从宿主主机配置的网桥接口中桥接一个虚拟接口到容器中去
- 从地址池配置一个 ip 地址给容器
- 执行用户指定的应用程序
- 执行完毕后容器被终止

 `Usage: docker run [OPTIONS] IMAGE [COMMAND] [ARG...]`

####  5.1 使用image创建container并执行相应命令，然后停止

```
# docker run ubuntu echo "hello world"



hello word
```

 这是最简单的方式，跟在本地直接执行`echo 'hello world'` 几乎感觉不出任何区别，而实际上它会从本地ubuntu:latest镜像启动到一个容器，并执行打印命令后退出（`docker  ps -l`可查看）。需要注意的是，默认有一个`--rm=true`参数，即完成操作后停止容器并从文件系统移除。因为Docker的容器实在太轻量级了，很多时候用户都是随时删除和新创建容器。
 容器启动后会自动随机生成一个`CONTAINER ID`，这个ID在后面commit命令后可以变为`IMAGE  ID`

####  使用image创建container并进入交互模式, login shell是/bin/bash

```
# docker run -i -t --name mytest centos:centos6 /bin/bash



bash-4.1#
```

 上面的`--name`参数可以指定启动后的容器名字，如果不指定则docker会帮我们取一个名字。镜像`centos:centos6`也可以用`IMAGE  ID` (68edf809afe7) 代替），并且会启动一个[伪终端](https://www.baidu.com/s?wd=%E4%BC%AA%E7%BB%88%E7%AB%AF&tn=24004469_oem_dg&rsv_dl=gh_pl_sl_csd)，但通过ps或top命令我们却只能看到一两个进程，因为容器的核心是所执行的应用程序，所需要的资源都是应用程序运行所必需的，除此之外，并没有其它的资源，可见Docker对资源的利用率极高。此时使用exit或Ctrl+D退出后，这个容器也就消失了（消失后的容器并没有完全删除？）
 （那么多个TAG不同而IMAGE ID相同的的镜像究竟会运行以哪一个TAG启动呢

####  5.2 运行出一个container放到后台运行

```sql
# docker run -d ubuntu /bin/sh -c "while true; do echo hello world; sleep 2; done"



ae60c4b642058fefcc61ada85a610914bed9f5df0e2aa147100eab85cea785dc
```

 它将直接把启动的container挂起放在后台运行（这才叫saas），并且会输出一个`CONTAINER ID`，通过`docker  ps`可以看到这个容器的信息，可在container外面查看它的输出`docker logs ae60c4b64205`，也可以通过`docker  attach ae60c4b64205`连接到这个正在运行的终端，此时在`Ctrl+C`退出container就消失了，按ctrl-p  ctrl-q可以退出到宿主机，而保持container仍然在运行
 另外，如果-d启动但后面的命令执行完就结束了，如`/bin/bash`、`echo  test`，则container做完该做的时候依然会终止。而且-d不能与--rm同时使用
 可以通过这种方式来运行[memcached](https://www.baidu.com/s?wd=memcached&tn=24004469_oem_dg&rsv_dl=gh_pl_sl_csd)、apache等。

####  5.3 映射host到container的端口和目录

 映射主机到容器的端口是很有用的，比如在container中运行memcached，端口为11211，运行容器的host可以连接container的  internel_ip:11211 访问，如果有从其他主机访问memcached需求那就可以通过-p选项，形如`-p  <host_port:contain_port>`，存在以下几种写法：

```css
-p 11211:11211 这个即是默认情况下，绑定主机所有网卡（0.0.0.0）的11211端口到容器的11211端口上



-p 127.0.0.1:11211:11211 只绑定localhost这个接口的11211端口



-p 127.0.0.1::5000



-p 127.0.0.1:80:8080
```

 目录映射其实是“绑定挂载”host的路径到container的目录，这对于内外传送文件比较方便，在搭建私服那一节，为了避免私服container停止以后保存的images不被删除，就要把提交的images保存到挂载的主机目录下。使用比较简单，`-v  <host_path:container_path>`，绑定多个目录时再加`-v`。

```
-v /tmp/docker:/tmp/docker
```

 另外在两个container之间建立联系可用`--link`，详见高级部分或[官方文档](http://docs.docker.com/v1.1/reference/commandline/cli/#run)。
 下面是一个例子：

```
# docker run --name nginx_test \



> -v /tmp/docker:/usr/share/nginx/html:ro \



> -p 80:80 -d \



> nginx:1.7.6
```

 在主机的/tmp/docker下建立index.html，就可以通过`http://localhost:80/`或`http://host-ip:80`访问了。

###  6. 将一个container固化为一个新的image（commit）

 当我们在制作自己的镜像的时候，会在container中安装一些工具、修改配置，如果不做commit保存起来，那么container停止以后再启动，这些更改就消失了。
`docker commit <container> [repo:tag]`
 后面的repo:tag可选
 只能提交正在运行的container，即通过`docker ps`可以看见的容器，

```
查看刚运行过的容器



# docker ps -l



CONTAINER ID   IMAGE     COMMAND      CREATED       STATUS        PORTS   NAMES



c9fdf26326c9   nginx:1   nginx -g..   3 hours ago   Exited (0)..     nginx_test



 



启动一个已存在的容器（run是从image新建容器后再启动），以下也可以使用docker start nginx_test代替  



[root@hostname docker]# docker start c9fdf26326c9



c9fdf26326c9



 



 



docker run -i -t --sig-proxy=false 21ffe545748baf /bin/bash



nginx服务没有启动



 



 



# docker commit -m "some tools installed" fcbd0a5348ca seanlook/ubuntu:14.10_tutorial



fe022762070b09866eaab47bc943ccb796e53f3f416abf3f2327481b446a9503
```

 -a "seanlook7@gmail.com"
 请注意，当你反复去commit一个容器的时候，每次都会得到一个新的`IMAGE ID`，假如后面的`repository:tag`没有变，通过`docker  images`可以看到，之前提交的那份镜像的`repository:tag`就会变成`<none>:<none>`，所以尽量避免反复提交。
 另外，观察以下几点:

- commit container只会pause住容器，这是为了保证容器文件系统的一致性，但不会stop。如果你要对这个容器继续做其他修改：
    - 你可以重新提交得到新image2，删除次新的image1
    - 也可以关闭容器用新image1启动，继续修改，提交image2后删除image1
    - 当然这样会很痛苦，所以一般是采用`Dockerfile`来`build`得到最终image，参考[]
- 虽然产生了一个新的image，并且你可以看到大小有100MB，但从commit过程很快就可以知道实际上它并没有独立占用100MB的硬盘空间，而只是在旧镜像的基础上修改，它们共享大部分公共的“片”。

下



###  1. 开启/停止/重启container（start/stop/restart）

 容器可以通过`run`新建一个来运行，也可以重新`start`已经停止的container，但`start`不能够再指定容器启动时运行的指令，因为docker只能有一个前台进程。
 容器stop（或`Ctrl+D`）时，会在保存当前容器的状态之后退出，下次start时保有上次关闭时更改。而且每次进入`attach`进去的界面是一样的，与第一次run启动或commit提交的时刻相同。

```ruby
CONTAINER_ID=$(docker start <containner_id>)



docker stop $CONTAINER_ID



docker restart $CONTAINER_ID
```

 关于这几个命令可以通过一个完整的实例使用：[docker如何创建一个运行后台进程的容器并同时提供shell终端](http://segmentfault.com/blog/seanlook/1190000000755980)。

###  2. 连接到正在运行中的container（attach）

 要`attach`上去的容器必须正在运行，可以同时连接上同一个container来共享屏幕（与`screen`命令的attach类似）。
 官方文档中说`attach`后可以通过`CTRL-C`来detach，但实际上经过我的测试，如果container当前在运行bash，`CTRL-C`自然是当前行的输入，没有退出；如果container当前正在前台运行进程，如输出nginx的access.log日志，`CTRL-C`不仅会导致退出容器，而且还stop了。这不是我们想要的，detach的意思按理应该是脱离容器终端，但容器依然运行。好在`attach`是可以带上`--sig-proxy=false`来确保`CTRL-D`或`CTRL-C`不会关闭容器。

```
# docker attach --sig-proxy=false $CONTAINER_ID
```

###  3. 查看image或container的底层信息（inspect）

 `inspect`的对象可以是image、运行中的container和停止的container。

```
查看容器的内部IP



# docker inspect --format='{{.NetworkSettings.IPAddress}}' $CONTAINER_ID



172.17.42.35
```

###  4. 删除一个或多个container、image（rm、rmi）

 你可能在使用过程中会`build`或`commit`许多镜像，无用的镜像需要删除。但删除这些镜像是有一些条件的：

- 同一个`IMAGE ID`可能会有多个`TAG`（可能还在不同的仓库），首先你要根据这些  image names 来删除标签，当删除最后一个tag的时候就会自动删除镜像；
- 承上，如果要删除的多个`IMAGE NAME`在同一个`REPOSITORY`，可以通过`docker  rmi <image_id>`来同时删除剩下的`TAG`；若在不同Repo则还是需要手动逐个删除`TAG`；
- 还存在由这个镜像启动的container时（即便已经停止），也无法删除镜像；

 TO-DO
 如何查看镜像与容器的依存关系

 **删除容器**
`docker rm <container_id/contaner_name>`

```javascript
删除所有停止的容器



docker rm $(docker ps -a -q)
```

 **删除镜像**
`docker rmi <image_id/image_name ...>`
 下面是一个完整的示例：

```
# docker images            <==



ubuntu            13.10        195eb90b5349       4 months ago       184.6 MB



ubuntu            saucy        195eb90b5349       4 months ago       184.6 MB



seanlook/ubuntu   rm_test      195eb90b5349       4 months ago       184.6 MB



 



使用195eb90b5349启动、停止一个容器后，删除这个镜像



# docker rmi 195eb90b5349



Error response from daemon: Conflict, cannot delete image 195eb90b5349 because it is 



tagged in multiple repositories, use -f to force



2014/11/04 14:19:00 Error: failed to remove one or more images



 



删除seanlook仓库中的tag     <==



# docker rmi seanlook/ubuntu:rm_test



Untagged: seanlook/ubuntu:rm_test



 



现在删除镜像，还会由于container的存在不能rmi



# docker rmi 195eb90b5349



Error response from daemon: Conflict, cannot delete 195eb90b5349 because the 



 container eef3648a6e77 is using it, use -f to force



2014/11/04 14:24:15 Error: failed to remove one or more images



 



先删除由这个镜像启动的容器    <==



# docker rm eef3648a6e77



 



删除镜像                    <==



# docker rmi 195eb90b5349



Deleted: 195eb90b534950d334188c3627f860fbdf898e224d8a0a11ec54ff453175e081



Deleted: 209ea56fda6dc2fb013e4d1e40cb678b2af91d1b54a71529f7df0bd867adc961



Deleted: 0f4aac48388f5d65a725ccf8e7caada42f136026c566528a5ee9b02467dac90a



Deleted: fae16849ebe23b48f2bedcc08aaabd45408c62b531ffd8d3088592043d5e7364



Deleted: f127542f0b6191e99bb015b672f5cf48fa79d974784ac8090b11aeac184eaaff
```

 注意，上面的删除过程我所举的例子比较特殊——镜像被tag在多个仓库，也有启动过的容器。按照`<==`指示的顺序进行即可。

###  5. docker build 使用此配置生成新的image

 `build`命令可以从`Dockerfile`和上下文来创建镜像：
`docker build [OPTIONS] PATH | URL | -`
 上面的`PATH`或`URL`中的文件被称作上下文，build  image的过程会先把这些文件传送到docker的服务端来进行的。
 如果`PATH`直接就是一个单独的`Dockerfile`文件则可以不需要上下文；如果`URL`是一个Git仓库地址，那么创建image的过程中会自动`git  clone`一份到本机的临时目录，它就成为了本次build的上下文。无论指定的`PATH`是什么，`Dockerfile`是至关重要的，请参考[Dockerfile  Reference](http://docs.docker.com/reference/builder/)。
 请看下面的例子：

```sql
# cat Dockerfile 



FROM seanlook/nginx:bash_vim



EXPOSE 80



ENTRYPOINT /usr/sbin/nginx -c /etc/nginx/nginx.conf && /bin/bash



 



# docker build -t seanlook/nginx:bash_vim_Df .



Sending build context to Docker daemon 73.45 MB



Sending build context to Docker daemon 



Step 0 : FROM seanlook/nginx:bash_vim



 ---> aa8516fa0bb7



Step 1 : EXPOSE 80



 ---> Using cache



 ---> fece07e2b515



Step 2 : ENTRYPOINT /usr/sbin/nginx -c /etc/nginx/nginx.conf && /bin/bash



 ---> Running in e08963fd5afb



 ---> d9bbd13f5066



Removing intermediate container e08963fd5afb



Successfully built d9bbd13f5066
```

 上面的`PATH`为`.`，所以在当前目录下的所有文件（不包括`.dockerignore`中的）将会被`tar`打包并传送到`docker  daemon`（一般在本机），从输出我们可以到`Sending build context...`，最后有个`Removing  intermediate container`的过程，可以通过`--rm=false`来保留容器。
 TO-DO
`docker build github.com/creack/docker-firefox`失败。

###  6. 给镜像打上标签（tag）

 tag的作用主要有两点：一是为镜像起一个容易理解的名字，二是可以通过`docker tag`来重新指定镜像的仓库，这样在`push`时自动提交到仓库。

```
将同一IMAGE_ID的所有tag，合并为一个新的



# docker tag 195eb90b5349 seanlook/ubuntu:rm_test



 



新建一个tag，保留旧的那条记录



# docker tag Registry/Repos:Tag New_Registry/New_Repos:New_Tag
```

###  7. 查看容器的信息container（ps）

 `docker ps`命令可以查看容器的`CONTAINER  ID`、`NAME`、`IMAGE  NAME`、端口开启及绑定、容器启动后执行的`COMMNAD`。经常通过`ps`来找到`CONTAINER_ID`。
`docker ps` 默认显示当前正在运行中的container
`docker ps -a` 查看包括已经停止的所有容器
`docker ps -l` 显示最新启动的一个容器（包括已停止的）

###  8. 查看容器中正在运行的进程（top）

 容器运行时不一定有`/bin/bash`终端来交互执行top命令，查看container中正在运行的进程，况且还不一定有`top`命令，这是`docker  top <container_id/container_name>`就很有用了。实际上在host上使用`ps -ef|grep docker`也可以看到一组类似的进程信息，把container里的进程看成是host上启动docker的子进程就对了。

###  9. 其他命令

 docker还有一些如`login`、`cp`、`logs`、`export`、`import`、`load`、`kill`等不是很常用的命令，比较简单，请参考官网。

###  参考

- [Official Command Line Reference](http://docs.docker.com/v1.1/reference/commandline/cli/)
- [docker中文指南cli-widuu翻译](http://www.widuu.com/docker/)
- [Docker —— 从入门到实践](http://www.dockerpool.com/static/books/docker_practice/)
- [Docker基础与高级](http://17173ops.com/2014/10/13/docker%E5%9F%BA%E7%A1%80%E4%B8%8E%E9%AB%98%E7%BA%A7.shtml)
- 