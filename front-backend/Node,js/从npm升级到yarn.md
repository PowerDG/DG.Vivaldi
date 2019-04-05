# [从npm升级到yarn](https://blog.csdn.net/kucoll/article/details/79890176)

 																				2018年04月11日 00:32:22 					[kucoll](https://me.csdn.net/kucoll) 						阅读数：5750 						 																															

 									

​                   					 							 					                   					 					版权声明：原创作品，允许转载，转载时请务必以超链接形式标明文章 原始出处 、作者信息和本声明。否则将追究法律责任。					https://blog.csdn.net/kucoll/article/details/79890176				

```html
npm install -g yarn
```

yarn默认安装源地址使用以下命令查看（https://registry.yarnpkg.com）

```html
yarn config get registry
```

切换yarn安装源为淘宝

```html
yarn config set registry 'https://registry.npm.taobao.org'
```



### 使用

1. 初始化某个项目

   ```
   npm init
   
   
   
   yarn init
   ```

2. 默认的安装依赖操作

   ```sql
   npm install/link
   
   
   
   yarn install/link
   ```

3. 安装某个依赖，并且默认保存到package

   ```sql
   npm install xxx —save
   
   
   
   yarn add xxx
   ```

4. 移除某个依赖项目

   ```
   npm uninstall xxx —save
   
   
   
   yarn remove xxx
   ```

5. 安装某个开发时依赖项目

   ```
   npm install --save-dev xxx 
   
   
   
   yarn add xxx —dev
   ```

6. 更新某个依赖项目

   ```sql
   npm update --save xxx
   
   
   
   yarn upgrade xxx
   ```

7. 安装某个全局依赖项目

   ```sql
   npm install -g xxx 
   
   
   
   yarn global add xxx
   ```

8. 发布/登录/登出，一系列NPM Registry操作

   ```
   npm publish/login/logout
   
   
   
   yarn publish/login/logout
   ```

9. 运行某个命令

   ```
   npm run/test
   
   
   
   yarn run/test
   ```