





```
docker images // 查看所有镜像
docker container ls // 查看正在运行的容器，辛辛苦苦敲了这几个单词却没有显示容器怎么办？
docker container ls -a // 可以带上 -a 参数，列出所有的容器，此时可以看到刚才的 hello-world 容器了，因为它运行完就退出了
docker rm -f container CONTAINER ID // 删除容器
docker rmi IMAGE ID // 删除镜像
```