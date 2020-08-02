# [docker 配置国内镜像源 linux/mac/windows](https://www.jianshu.com/p/9fce6e583669)



$ docker --version

docker --version

Docker version 18.03.0-ce, build 0520e24302





建议加入其他国内镜像



```
https://docker.mirrors.ustc.edu.cn
https://hub-mirror.c.163.com
```

### Docker Toolbox

（不推荐使用 docker toolbox，建议使用新的 docker for mac 及 docker for windows 以在这两种平台运行 docker ）
 请确认你的 Docker Toolbox 已经启动，并执行下列命令（请将 **加速地址** 替换为在[加速器](https://link.jianshu.com?t=https://www.daocloud.io/mirror#accelerator-doc)页面获取的专属地址）

```
docker-machine ssh default sudo sed -i "s|EXTRA_ARGS='|EXTRA_ARGS='--registry-mirror=https://docker.mirrors.ustc.edu.cn |g" /var/lib/boot2docker/profile
exit	
docker-machine restart default
```

作者：极客圈

docker-machine ssh defaultsudo sed -i "s|EXTRA_ARGS='|EXTRA_ARGS='--registry-mirror=加速地址 |g" /var/lib/boot2docker/profileexitdocker-machine restart defaul

链接：https://www.jianshu.com/p/9fce6e583669

来源：简书

简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。