

解压Nginx

更改conf	且覆盖

\nginx-1.15.8\conf \ [nginx.conf](C:\Deployment\Dependencies\Setup Resource\nginx-1.15.8\conf\nginx.conf) 

# include /etc/nginx/conf.d/*.conf;
```js
# include /etc/nginx/sites-enabled/*;

server {
    listen 8090;
    # 接口服务的IP地址
    server_name localhost;
    charset utf-8;
    access_log off;
    # ElecManageSystem-应用文件夹名称 app-index.html页面所在文件夹
    root E:/DgWrok/lifeScreen2019/Web_Vue/app;
    #vue 路由
    location / {
         try_files $uri $uri/ @router;
         index index.html;
     }

    location @router {
        rewrite ^.*$ /index.html last;
    }
}
```


更改js到自己端口号

> CMD  ipconfig查看IP

start nginx

nginx  -s quit

