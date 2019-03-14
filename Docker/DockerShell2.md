

https://cr.console.aliyun.com/cn-hangzhou/mirrors


```powershell

docker-machine create --engine-registry-mirror=https://anuzyij8.mirror.aliyuncs.com -d virtualbox default
```





```powershell
docker-machine -s "H:\docker" create --engine-registry-mirror=https://vf29u5xi.mirror.aliyuncs.com -d virtualbox default
```



## myShell：

E:\DgDocker\dokcer

```powershell
docker-machine -s "E:\DgDocker\dokcer" create --engine-registry-mirror=https://anuzyij8.mirror.aliyuncs.com -d virtualbox default
```





## notepad .bash_profile

PowerDg@DESKTOP-9FMKQ28 MINGW64 ~
$  notepad .bash_profile

然后，在空白处输入（因为我想将以后的镜像都安装到H盘，此处可以修改你喜欢的盘符）：

```powershell
export MACHINE_STORAGE_PATH='H:\docker'
```



libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: cHRM chunk does not match sRGB
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile
libpng warning: iCCP: known incorrect sRGB profile



## docker-machine -s

PowerDg@DESKTOP-9FMKQ28 MINGW64 ~
$ docker-machine -s "E:\DgDocker\dokcer" create --engine-registry-mirror=https:/                                                                                                                /anuzyij8.mirror.aliyuncs.com -d virtualbox default
Docker machine "default" already exists

PowerDg@DESKTOP-9FMKQ28 MINGW64 ~
$ docker-machine -s "E:\DgDocker\dokcer" create --engine-registry-mirror=https:/                                                                                                                /anuzyij8.mirror.aliyuncs.com -d virtualbox ede
Running pre-create checks...
Creating machine...
(ede) Copying E:\DgDocker\dokcer\cache\boot2docker.iso to E:\DgDocker\dokcer\mac                                                                                                                hines\ede\boot2docker.iso...
(ede) Creating VirtualBox VM...
(ede) Creating SSH key...
(ede) Starting the VM...
(ede) Check network to re-create if needed...
(ede) Windows might ask for the permission to configure a dhcp server. Sometimes                                                                                                                , such confirmation window is minimized in the taskbar.
(ede) Waiting for an IP...



Waiting for machine to be running, this may take a few minutes...
Detecting operating system of created instance...
Waiting for SSH to be available...
Detecting the provisioner...
Provisioning with boot2docker...
Copying certs to the local machine directory...
Copying certs to the remote machine...
Setting Docker configuration on the remote daemon...
Checking connection to Docker...
Docker is up and running!
To see how to connect your Docker Client to the Docker Engine running on this vi                                                                                                                rtual machine, run: C:\Program Files\Docker Toolbox\docker-machine.exe env ede