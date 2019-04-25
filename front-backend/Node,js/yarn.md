# [node 中 安装  yarn](https://www.cnblogs.com/xiangsj/p/8083094.html) 		

Yarn是Facebook最近发布的一款依赖包安装工具。Yarn是一个新的快速安全可信赖的可以替代NPM的依赖管理工具

 

**快速安装**

```
//在NPM 中安装
npm install -g yarn


yarn config get registry
https://registry.yarnpkg.com

yarn config set registry 'https://registry.npm.taobao.org'

npm install -g cnpm --registry=https://registry.npm.taobao.org
```

 

**MacOS**

在Mac上安装比较方便，使用初始化脚本即可

  `curl -o- -L https:``//yarnpkg``.com``/install``.sh | ``bash`  

**Linux**

Po主自己的机器是Ubuntu,安装比较简单 

输入命令

  `sudo` `apt-key adv --keyserver pgp.mit.edu --recv D101F7899D41F3C3 ` `echo` `"deb http://dl.yarnpkg.com/debian/ stable main"` `| ``sudo` `tee` `/etc/apt/sources``.list.d``/yarn``.list`      

然后输入安装命令就行啦：

  `sudo` `apt-get update && ``sudo` `apt-get ``install` `yarn`      

**windows**

windows 下需要下载msi文件 ，下载地址：<https://yarnpkg.com/latest.msi>

 

 

关于安装，你可以去官网查看到更多资料 <https://yarnpkg.com/en/docs/install>

[![img](http://files.jb51.net/file_images/article/201610/20161025104429259.png?2016925104441)](http://files.jb51.net/file_images/article/201610/20161025104429259.png?2016925104441)

安装完成后，你可以测试下自己的版本

  `yarn --version`      

**PS:** 如果抛出错误`yarn: command not found `,你可以去这里找下解决方法，应该都可以解决的

**开始使用**

我们新建一个文件夹yarn测试下

输入命令: `yarn init`

![img](http://files.jb51.net/file_images/article/201610/20161025104523188.png?2016925104531)

一路enter下去就行

然后我们试着加一些依赖：

  `yarn add gulp-``less`      

如果加入具体版本可以后面写上`@0.x.x` 这样子

![img](http://files.jb51.net/file_images/article/201610/20161025104608734.png?2016925104616)

Po主试着装了三个gulp插件，这个时候package.json里面是这个样子的：

![img](http://files.jb51.net/file_images/article/201610/20161025104632533.png?2016925104640)

如果你要移除的话，可以使用`yarn remove package_name `比如：

  `yarn remove gulp-``less`      

升级更新某个依赖可以使用这个:

  `yarn upgrade [package]`      

**总结**

总之安装和使用都挺方便的，注意本地`node version >=4.0安`装时会提示的。安装速度也挺快的，目前自己还没有publish过，不过打算稍后publish尝试下。如同FB声称，快速，可靠，安全。yarn是开源的，随着关注和使用的人越来越多，yarn会变得更好，相信也会有部分工程师使用yarn而放弃npm 。