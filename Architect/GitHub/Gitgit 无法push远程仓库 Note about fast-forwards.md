# git 无法push远程仓库 Note about fast-forwards

https://blog.csdn.net/zwkkkk1/article/details/82699387

```
$ git pull origin master --allow-unrelated-histories
【解决方案】
```





2018年09月14日 10:57:52

zwkkkk1

阅读数：1041

 								标签： 																[git																](http://so.csdn.net/so/search/s.do?q=git&t=blog) 							更多

 								个人分类： 																[环境配置																](https://blog.csdn.net/zwkkkk1/article/category/7186657) 							

 														

​                   					 							 					                   					 					版权声明：本文为博主原创文章，未经博主允许不得转载。					https://blog.csdn.net/zwkkkk1/article/details/82699387				

### 问题

push远程仓库时，经常报出下面的错误，导致操作失败，让我们来看看怎么解决。

```shell
To github.com:zwkkkk1/chatroom.git
 ! [rejected]        master -> master (non-fast-forward)
error: failed to push some refs to 'git@github.com:zwkkkk1/chatroom.git'
hint: Updates were rejected because the tip of your current branch is behind
hint: its remote counterpart. Integrate the remote changes (e.g.
hint: 'git pull ...') before pushing again.
hint: See the 'Note about fast-forwards' in 'git push --help' for details.
1234567
```

错误：`non-fast-forward`
 远程仓库：`origin`
 远程分支：`master`
 本地分支：`master`

### 解决方案

Git 已经提示我们，先用 `git pull` 把最新的内容从远程分支(`origin/master`)拉下来，然后在本地 `merge`，解决 `conflict`，再 `push`。

不过，在 `git pull` 时，还有其他的错误，我们分别看看可能出现的错误。

#### fatal: refusing to merge unrelated histories

  此项错误是由于本地仓库和远程有不同的开始点，也就是两个仓库没有共同的 commit 出现的无法提交。这里我们需要用到 `--allow-unrelated-histories`。也就是我们的 pull 命令改为下面这样的：

```shell
git pull origin master --allow-unrelated-histories
1
```

如果设置了默认分支，可以这样写

```shell
git pull --allow-unrelated-histories
1
```

#### There is no tracking information for the current branch.

完整报错代码可能是这样的：

```shell
There is no tracking information for the current branch.
Please specify which branch you want to merge with.
See git-pull(1) for details.

    git pull <remote> <branch>

If you wish to set tracking information for this branch you can do so with:

    git branch --set-upstream-to=origin/<branch> master
123456789
```

原因是没有指定本地 `master` 分支和远程 `origin/master` 的连接，这里根据提示：

```shell
git branch --set-upstream-to=origin/master master
1
```

#### 产生冲突

  `pull` 还可能产生 `conflict`，这里需要自己手动解决冲突再 `merge`，这里不过多介绍。

成功 `git pull` 之后，然后就可以成功 `git push` 了~~







其他

https://blog.csdn.net/renfufei/article/details/41648061





https://blog.csdn.net/lesaqiu/article/details/49834295