### 1下载安装git for windows (msysgit)客户端，安装略

下载地址： https://gitforwindows.org/

### 2下载安装可视化工具 tortoisegit  ，安装略

下载地址：https://tortoisegit.org/download/

### 3本地 生成ssh公私钥 如下：

鼠标右键，单击 Git Bash Here , 进去git的命令台，输入如下：

ssh-keygen -t rsa -C  1049365046@qq.com

---------------------
作者：不知道取啥昵称 
来源：CSDN 
原文：https://blog.csdn.net/winy_lm/article/details/80452590 
版权声明：本文为博主原创文章，转载请附上博文链接！











`                                                                                
PowerDGTMS@DESKTOP-F44Q079 MINGW64 ~                                            
$ ssh-keygen -t rsa -C  1049365046@qq.com                                       
Generating public/private rsa key pair.                                         
Enter file in which to save the key (/c/Users/PowerDGTMS/.ssh/id_rsa):          
Created directory '/c/Users/PowerDGTMS/.ssh'.                                   
Enter passphrase (empty for no passphrase):                                     
Enter same passphrase again:                                                    
Your identification has been saved in /c/Users/PowerDGTMS/.ssh/id_rsa.          
Your public key has been saved in /c/Users/PowerDGTMS/.ssh/id_rsa.pub.          
The key fingerprint is:                                                         
SHA256:qvfbv6boXK+VCKp6wBNETULNBlwrFZUyrGYTPWr4dY4 1049365046@qq.com            
The key's randomart image is:                                                   
+---[RSA 2048]----+                                                             
| +=X=o..         |                                                             
|  +o@..          |                                                             
| o.=.+           |                                                             
|. O.. .          |                                                             
| * + +  S        |                                                             
|  = E .o . . .   |                                                             
|   o  o   o o    |                                                             
|    .o.. + o.    |                                                             
|  .oo. o*.+=+.   |                                                             
+----[SHA256]-----+                                                             `