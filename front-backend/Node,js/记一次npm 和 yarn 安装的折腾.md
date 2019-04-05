# 记一次npm 和 yarn 安装的折腾

​             ![96](assets/a83dec29-99c6-498d-ad3c-faa4fe6f3508.jpg) 

​             [QingMings](https://www.jianshu.com/u/e7977ea5130d)                          

​                                                    0.1                                                 2019.01.23 10:55               字数 419             阅读 611评论 0喜欢 2

环境

| 环境       | 版本号   |
| ---------- | -------- |
| windows 10 | 1809     |
| nodejs     | v10.15.0 |
| npm        | 6.4.1    |
| yarn       | 1.12.3   |

问题描述： npm 和  yarn 的一些缓存和全局安装的包，默认都会在C盘存储，这个对于C盘的宝贵空间来说，实在是不能忍啊。

于是乎，百度了如何改变`npm`默认的缓存位置

在CMD命令行中执行

```
#1.改变npm 全局安装位置
npm config set prefix "你的磁盘路径"
#这里是我的路径
npm config set prefix "D:\appCache\nodejs\node_global"
#2. 改变 npm 缓存位置
npm config set cache "你的磁盘路径"
#这里是我的路径
npm config set cache "D:\appCache\nodejs\node_cache"
```

然后配置一下系统环境变量

将 `D:\appCache\nodejs\node_global` 和 `D:\appCache\nodejs\node_global\node_modules` 这两个添加到你的系统环境变量中。

Yarn 的安装： 在官网下载 安装包。

修改全局安装位置 和 缓存位置

在CMD命令行中执行

```
#1.改变 yarn 全局安装位置
yarn config  set global-folder "你的磁盘路径"
#2.然后你会在你的用户目录找到 `.yarnrc` 的文件，打开它，找到 `global-folder` ，改为 `--global-folder`
#这里是我的路径
yarn config  set global-folder "D:\appCache\yarn\global"
#2. 改变 yarn 缓存位置
yarn config cache-folder "你的磁盘路径"
#这里是我的路径
yarn config cache-folder "D:\appCache\yarn\cache"
```

在我们使用 全局安装 包的时候，会在 “D:\appCache\yarn\global” 下 生成 `node_modules\.bin` 目录

我们需要将 `D:\appCache\yarn\global\node_modules\.bin` 整个目录 添加到系统环境变量中去，否则通过yarn 添加的全局包 在cmd 中是找不到的。

检查当前yarn 的 bin的 位置

```
yarn global bin
```

检查当前 yarn 的 全局安装位置

```
yarn global dir
```

------

写在最后

为什么说折腾呢

因为我之前只 是改了 npm 和yarn的 全局安装位置和缓存位置，没有添加系统环境变量

然后在全局安装 vue-cli3 的时候，就蛋疼了，开始是报 powershell 脚本不能执行的问题。然后又是报 目录错误的问题，然后又是找不到命令。

当我把这些坑踩完，距离我刚开始装vue-cli3已经过去一个半小时了，都快忘记自己一开始要干啥了。

小礼物走一走，来简书关注我