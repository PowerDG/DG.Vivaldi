##	 TortoiseGit安装与配置

https://blog.csdn.net/renfufei/article/details/41647937



##	TortoiseGit 初次连接github 使用步骤

https://blog.csdn.net/winy_lm/article/details/80452590



###	**本地 生成ssh公私钥**

 $ ssh-keygen -t rsa -C  1049365046@qq.com

```



git config --global user.email "you@example.com"



```



> GitHub配置
>
> git config --global user.email "1049365046@qq.com"
> git config --global user.name "PowerDg"



```bash

Administrator@RESEARCH-WSX MINGW64 ~
$ ssh-keygen -t rsa -C  1049365046@qq.com
Generating public/private rsa key pair.
Enter file in which to save the key (/c/Users/Administrator/.ssh/id_rsa):
Enter passphrase (empty for no passphrase):
Enter same passphrase again:
Your identification has been saved in /c/Users/Administrator/.ssh/id_rsa.
Your public key has been saved in /c/Users/Administrator/.ssh/id_rsa.pub.
The key fingerprint is:
SHA256:0UxUD79nCG8JzeeESfRM2MFslReYDWbWuwT5dB7/z4c 1049365046@qq.com
The key's randomart image is:
+---[RSA 2048]----+
|         .o.o*%=B|
|         +  +%./+|
|        . o o %.@|
|         .   + @o|
|        S     B *|
|             . +.|
|               o.|
|              E +|
|                .|
+----[SHA256]-----+

Administrator@RESEARCH-WSX MINGW64 ~

```

###	注册到SSH keys

```



ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABAQCya0MlliwfDFEVWh6lza/KzxMqSm5QwFfTNH6dFF/tQ9yVaTmoRIFperE08btXbHV+u/a5ndxvOWy23Ufm0ntRufcDMl1gZVbVlPjO0PlbFKIiLvWxtfH4YXYJU6hTJlif/wgdW/qIMLihecBG2ZE8WkuTFCT8pZUAnbRUYoa+ge1qwiyGN8KGgD4aefwOYotuW+J8GfKReHLRuZAQMD+vhmuXxCv6nn4yRtLf4jBXodtbI/jKa1edrXMLyJbiI5nqMnFytLfaSR1eFXV2n+JUXbX2p0N+tRuKUnqAdu5VtNGLX+SEPP3KEkxanv1KKMS+BoLB2ONSYO1gCDrdXQDH 1049365046@qq.com


```



