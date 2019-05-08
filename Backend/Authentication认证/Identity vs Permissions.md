# 标识与权限

## [身份与权限](https://leastprivilege.com/2016/12/16/identity-vs-permissions/)

我们经常看到人们误用IdghtyServer作为授权/权限管理系统。这很麻烦-原因如下。

IdghtyServer(因此得名)非常擅长在系统中的所有应用程序中为您的用户提供稳定的身份。对于身份，我指的是不可改变的身份(至少在会话的一生中是这样)-典型的例子是用户id(也就是主题id)、名称、部门、电子邮件地址、客户id等等。

IdghtyServer不太适合于让客户端或API知道该用户可以做什么-例如创建客户记录、删除表、读取某个文档等-…

这本质上并不是IdghtyServer的一个弱点-但是IdghtyServer是一个令牌服务，而且声明，特别是令牌不是传输此类信息的特别好的媒介，这是一个事实。以下是几个原因：

- 声明应该用来建模用户的身份，而不是权限。
- 声明通常是简单的字符串？您通常需要更复杂的东西来建模授权信息或权限。
- 用户的权限通常是不同的，这取决于它所使用的客户端或API-将它们全部放入单个标识或访问令牌中是令人困惑的，并且会导致问题。相同的权限甚至可能有不同的含义，这取决于谁在使用它。
- 权限可以在会话的生命周期内更改，但是获得新令牌的唯一方法是往返到令牌服务。这通常需要一些不可取的UI交互。
- 权限和业务逻辑经常重叠-您希望在哪里划定界限？
- 唯一确切了解当前操作的授权需求的方是实际发生的代码令牌服务只能提供粗粒度信息。
- 你想把你的代币放小些。浏览器URL长度限制和带宽通常是限制因素
- 最后但并非最不重要的是，在令牌上添加一个声明是很容易的。很难移除一个。你永远不知道是否有人已经很依赖它了。您添加到令牌中的每一个声明都应该被仔细检查。

换句话说-将权限和授权数据保留在令牌之外。当您接近实际需要该信息的资源时，将授权信息添加到上下文中。即使如此，使用声明(Microsoft服务和框架将您推向这个方向)来建模权限还是很有诱惑力的-记住一个简单的字符串是一个非常有限的数据结构。现代编程语言有更好的构造。

**角色呢？**这是一个很常见的问题。角色是身份和授权之间的灰色地带。我的经验法则是，如果一个角色是用户身份的一个基本部分，并且对您系统的每个部分都感兴趣-角色成员身份不会或不经常改变-那么它就是象征性地提出要求的候选人。例如*客户*VS*雇员*-或*有耐心的*VS*医生*VS*护士*.

角色的其他每一种用法-特别是如果角色成员资格基于所使用的客户端或API而有所不同，那么它是纯授权数据，应该避免。如果您意识到用户的角色数量很高-或者越来越多-请避免将它们放入令牌中。

**结语**设计一个清晰的身份和权限分离(这只是身份验证和授权的重新迭代)。获取授权数据，尽可能接近需要它的代码-只有在那里，您才能做出真正需要的明智决定。

我还经常会遇到这样的问题：如果我们有类似于身份验证的灵活的授权解决方案，那么现在的答案是否定的。但我觉得2017年将是我们最终解决授权问题的一年。继续关注！







---



 					 						[leastprivilege.com](https://leastprivilege.com/) 					 				

Dominick Baier on Identity & Access Control

 							![img](assets/cropped-20110730-img_3322-edit.jpg) 						

[Skip to content](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#content)

- [Home](https://leastprivilege.com/)
- [Training](https://leastprivilege.com/training/)
- [Open Source](https://leastprivilege.com/open-source/)
- [Pluralsight](https://leastprivilege.com/pluralsight/)
- [Publications](https://leastprivilege.com/publications/)
- [Archive](https://leastprivilege.com/archive/)
- [About](https://leastprivilege.com/about/)
- [Photography](https://leastprivilege.com/photography/)

[← Optimizing Identity Tokens for size](https://leastprivilege.com/2016/12/14/optimizing-identity-tokens-for-size/)

[IdentityServer4 is now OpenID Certified →](https://leastprivilege.com/2016/12/20/identityserver4-is-now-openid-certified/)

## [Identity vs Permissions](https://leastprivilege.com/2016/12/16/identity-vs-permissions/)

 						Posted on [December 16, 2016](https://leastprivilege.com/2016/12/16/identity-vs-permissions/)						by [Dominick Baier](https://leastprivilege.com/author/dominickbaier/) 					

We often see people misusing IdentityServer as an authorization/permission management system. This is troublesome – here’s why.

IdentityServer (hence the name) is really good at providing a stable  identity for your users across all applications in your system. And with  identity I mean immutable identity (at least for the lifetime of the  session) – typical examples would be a user id (aka the subject id), a  name, department, email address, customer id etc…

IdentityServer is not so well suited for for letting clients or APIs  know what this user is allowed to do – e.g. create a customer record,  delete a table, read a certain document etc…

And this is not inherently a weakness of IdentityServer – but  IdentityServer is a token service, and it’s a fact that claims and  especially tokens are not a particularly good medium for transporting  such information. Here are a couple of reasons:

- Claims are supposed to model the identity of a user, not permissions
- Claims are typically simple strings – you often want something more  sophisticated to model authorization information or permissions
- Permissions of a user are often different depending which client or  API it is using – putting them all into a single identity or access  token is confusing and leads to problems. The same permission might even  have a different meaning depending on who is consuming it
- Permissions can change over the life time of a session, but the only  way to get a new token is to make a roundtrip to the token service.  This often requires some UI interaction which is not preferable
- Permissions and business logic often overlap – where do you want to draw the line?
- The only party that knows exactly about the authorization  requirements of the current operation is the actual code where it  happens – the token service can only provide coarse grained information
- You want to keep your tokens small. Browser URL length restrictions and bandwidth are often limiting factors
- And last but not least – it is easy to add a claim to a token. It is  very hard to remove one. You never know if somebody already took a hard  dependency on it. Every single claim you add to a token should be  scrutinized.

In other words – keep permissions and authorization data out of your  tokens. Add the authorization information to your context once you get  closer to the resource that actually needs the information. And even  then, it is tempting to model permissions using claims (the Microsoft  services and frameworks kind of push you into that direction) – keep in  mind that a simple string is a very limiting data structure. Modern  programming languages have much better constructs than that.

**What about roles?** That’s a very common question. Roles are a bit of a grey area  between identity and authorization. My rule of thumb is that if a role  is a fundamental part of the user identity that is of interest to every  part of your system – and role membership does not or not frequently  change – it is a candidate for a claim in a token. Examples could be *Customer* vs *Employee* – or *Patient* vs *Doctor* vs *Nurse*.

Every other usage of roles – especially if the role membership would  be different based on the client or API being used, it’s pure  authorization data and should be avoided. If you realize that the number  of roles of a user is high – or growing – avoid putting them into the  token.

**Conclusion** Design for a clean separation of identity and permissions  (which is just a re-iteration of authentication vs authorization).  Acquire authorization data as close as possible to the code that needs  it – only there you can make an informed decision what you really need.

I also often get the question if we have a similar flexible solution  to authorization as we have with IdentityServer for authentication – and  the answer is – right now – no. But I have the feeling that 2017 will  be our year to finally tackle the authorization problem. Stay tuned!

### Share this:

---



### 对45项答复*身份与权限*

1. ![img](assets/387cbe23a237e3277b591464b407ddf8.jpeg)

   吉普13

   说：

   [2016年12月16日11：44](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-82978)

   我理解关于获取授权数据尽可能接近操作的建议，即用户单击“Add”按钮，在Add操作的API中，在执行任何工作之前，检查当前用户是否有权采取该操作，如果没有，则返回适当的HTTP代码。

   但是，当您想要根据与用户关联的权限在应用程序中显示/隐藏UI元素时，会发生什么情况呢？也就是说，如果不允许当前用户执行该操作，您可能希望禁用Add按钮，或将其完全隐藏。历史上，我们使用添加到令牌中的声明来控制用户身份验证后屏幕上显示/隐藏元素。你对这方面有什么看法？谢谢!

   [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=82978#respond)

   - ![img](assets/75681814fbbb90c9224ea5ed0f8324ee.jpeg)

     [多米尼克·拜尔](https://dominickbaier.wordpress.com/)

     说：

     [2016年12月16日11：47](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-82979)

     编写一个将信息返回给UI的服务。基于用户的身份，可能基于更多的上下文-但最重要的是，以一种格式对UI最有用。

     [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=82979#respond)

     - ![img](https://1.gravatar.com/avatar/75681814fbbb90c9224ea5ed0f8324ee?s=90&d=identicon&r=G)

       [多米尼克·拜尔](https://dominickbaier.wordpress.com/)

       说：

       [2016年12月16日11：48](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-82980)

       从某种意义上说，这与我的其他建议完全一致-如果您将ui视为“资源”…的话。

       

     - ![img](assets/eHOgbKqK_normal.jpeg)

       [Al Zaudtke(@AlZaudtke)](http://twitter.com/AlZaudtke)

       说：

       [2017年1月18日19：00](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-83805)

       该服务是身份服务器应用程序或应用程序/API的一部分吗？我提出了任意一个论点，但目前倾向于在IdiceServerApp中使用一个新的端点。

       

     - ![img](https://1.gravatar.com/avatar/75681814fbbb90c9224ea5ed0f8324ee?s=90&d=identicon&r=G)

       [多米尼克·拜尔](https://dominickbaier.wordpress.com/)

       说：

       [2017年01月19日12：52](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-83825)

       海事组织并不重要-这是一项服务。我看不出把它放到身份服务器应用tbh中有什么好处。

       

2. ![img](assets/bf3e89a04c0c269b8ad988f10950061d.jpeg)

   [应用](http://gravatar.com/applicita)

   说：

   [2016年12月16日20：00](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-82987)

   我希望我早在2012年就知道了，我花了一段时间才得出同样的结论，当时我说我疯了！

   [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=82987#respond)

3. ![img](assets/330b102adb0d04738c0fa3b561b78be3.png)

   [费利克斯](http://www.rabinovich.org/drupal/node/6)

   说：

   [2016年12月17日07：01](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-82996)

   您是否认为授权解决方案将处理ASP.NET实体存储中类似于组的“角色”？从更广泛的意义上讲，Group可能有类似于用户的声明，而用户将有一些简单的方法来聚合他们所属的组的声明。然后，受保护的安全可以将所需的索赔与用户由于属于组而拥有的索赔相匹配。现在对我来说，这是最难解决的…问题
   谢谢

   [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=82996#respond)

   - ![img](https://1.gravatar.com/avatar/75681814fbbb90c9224ea5ed0f8324ee?s=90&d=identicon&r=G)

     [多米尼克·拜尔](https://dominickbaier.wordpress.com/)

     说：

     [2016年12月17日13时14分](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-83000)

     我同意-这是一个很难解决的问题。

     [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=83000#respond)

4. ![img](assets/728f54725409566d840dfaabca262476.jpeg)

   [去尘螨](http://codematrixblog.wordpress.com/)

   说：

   [2016年12月17日，下午3：30](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-83003)

   你会觉得这很明显吗？你的建议是正确的建议，不符合SRP(单一责任原则)。

   [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=83003#respond)

   - ![img](assets/Yr3RNlop_normal.png)

     [Infiniform(@Infiniform)](http://twitter.com/Infiniforms)

     说：

     [2017年01月10日13时55分](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-83549)

     很明显，当你知道你在做什么的时候，但是当你在学习的时候很容易感到困惑。

     [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=83549#respond)

5. ![img](assets/21ba493ef796c119a26abedc9944e16c.png)

   [迭戈](https://www.iberodev.com/)

   说：

   [2016年12月18日22：53](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-83035)

   我们也遇到过类似的困境，尽管我同意你所说的，把这些担忧明确地分开，这就是我们所做的(对于我们的场景，它有一些好处，不利之处并不那么重要)。

   我们的身份服务器保存身份详细信息，如姓名、电子邮件、道布等。但我们也决定将“授权”细节伪装成“部门”身份(称之为“角色”？)。
   例如，作为身份的一部分，我们有如下内容：
   “部门”：[“只读”、“编辑”、“管理员”]

   这似乎是将授权与身份混合起来，但我们认为这实际上与说：
   “部门”：[“部门A”、“部门B”、“部门C”]
   因为用户拥有部门“只读”这一事实不会说任何话，除非您将其与http谓词和资源试图访问的上下文放在一起，而且这种情况仍然只发生在资源的API端。

   通过包含这些角色，我们发现的最大好处是，在客户端，我们还可以基于这些令牌的“标识”进行授权，以便显示或隐藏某些UI元素，而无需对API进行额外调用，以确定是否授权该标识查看某些UI元素。例如，如果id_Token具有“admins”部门，则UI将在浏览器中显示管理部分，因为它“信任”它刚才请求的令牌，或者如果找不到“编辑器”部门，则在某些区域隐藏编辑按钮。

   然后，在API级别，我们有规则说，例如，如果Access_Token没有“admins”部门，那么某个资源上的帖子将是未经授权的。

   不利之处在于，你提到的那些“身份-潜移默化”的变化，以及第三方客户可能有旧的“身份”的可能性。我们决定违反这种关注点分离，因为客户端应用程序也是我们的应用程序，我们每5分钟就悄悄的更新令牌(我们可以忍受这5分钟的不正确身份)。我知道，如果我们决定将我们的资源暴露给第三方公司，这可能会成为一个问题。

   另一个缺点是令牌的大小。所以我同意你所有的观点，但这是一条灰色的线：)

   [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=83035#respond)

   - ![img](assets/f98a528ddddd725c6709af1a5bd022d0.png)

     塔马斯·F ldesi

     说：

     [2017年01月21日下午1时30分](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-83869)

     你从哪里得到访问令牌的？我从Identity Server 3获得的用户设置为Self，因此使用它访问另一个API似乎是不对的。还是将id令牌作为访问令牌发送？

     [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=83869#respond)

6. ![img](assets/f3e1063ed50a82ad024f3ff73f1e1897.png)

   马克

   说：

   [2017年01月9日05：02](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-83509)

   “但我觉得2017年将是我们最终解决授权问题的一年”这句话真的让我的兴趣达到了顶峰。当超越RBAC时，这可能是一个非常复杂的问题。我遇到的大多数复杂项目都需要用户和资源(Abac)的一些属性来确定授权访问，但这可能意味着硬编码规则，这可能是一个问题，也可能不是问题。

   您是否完全按照ABAC、XACML或其他方式思考呢？

   [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=83509#respond)

   - ![img](https://1.gravatar.com/avatar/75681814fbbb90c9224ea5ed0f8324ee?s=90&d=identicon&r=G)

     [多米尼克·拜尔](https://dominickbaier.wordpress.com/)

     说：

     [2017年01月9日10：06](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-83515)

     我会写更多关于这一点的，一旦我们接近了，对不起；)

     [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=83515#respond)

     - ![img](assets/6a67bb8695ead4d97e3d1cf363cb77c8.png)

       朱利安

       说：

       [2017年11月16日18时34分](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-91881)

       2017年几乎完成…关于这个话题有什么要分享的消息吗？不想施加任何压力，只是觉得，而且仍然感到非常兴奋，终于看到了这个问题上的一些东西，这一直是一个问题，对任何一个系统永远。祝贺您在过去几年中围绕身份/身份验证所做的所有工作，这些工作帮助了成千上万的人，包括：-)

       

7. ![img](https://0.gravatar.com/avatar/f98a528ddddd725c6709af1a5bd022d0?s=90&d=identicon&r=G)

   塔马斯·F ldesi

   说：

   [2017年01月21日13时25分](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-83868)

   我一直试图建立一个架构，它涉及一个纯基于浏览器的客户端、一个API和一个安全令牌服务。在这里读文档：<https://identityserver.github.io/Documentation/docsv2/overview/bigPicture.html>建议Identity Server是在一次往返过程中进行身份验证和授权的良好候选服务器-但在本文中，您似乎说的是相反的…？
   链接的文档让我尝试使用IS访问令牌来保护我的API，虽然它看起来不对，但是听众就是，只是举一个问题。

   [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=83868#respond)

   - ![img](https://1.gravatar.com/avatar/75681814fbbb90c9224ea5ed0f8324ee?s=90&d=identicon&r=G)

     [多米尼克·拜尔](https://dominickbaier.wordpress.com/)

     说：

     [2017年01月22日13时14分](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-83885)

     IdghtyServer既支持id_Tokens(身份验证)，也支持访问令牌(访问API)-有些人还称它为客户端(粗粒度)授权。
     本文讨论(更细粒度的)用户授权。

     受众是“发行者名称+/Resources”-或者IOW-“由该发行者保护的所有资源”-对于更细粒度的受众-使用范围声明。

     [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=83885#respond)

     - ![img](https://0.gravatar.com/avatar/f98a528ddddd725c6709af1a5bd022d0?s=90&d=identicon&r=G)

       塔马斯·F ldesi

       说：

       [2017年01月22日20：33](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-83895)

       好的，谢谢你的澄清-对我来说，这篇文章听起来好像我根本不应该使用授权，但我想使用它来授予对某个Web API的访问作为一个整体是可以的。

       而且，我不知道什么/资源意味着什么，但这样我就少了一个问号：)

       

   - ![img](https://1.gravatar.com/avatar/75681814fbbb90c9224ea5ed0f8324ee?s=90&d=identicon&r=G)

     [多米尼克·拜尔](https://dominickbaier.wordpress.com/)

     说：

     [2017年01月22日13时14分](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-83886)

     “只想说出一个问题”-你还有什么其他的问题吗？

     对于身份服务器的问题，最好的地方是GitHub问题跟踪器。

     [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=83886#respond)

8. ![img](assets/89e828e01487ed6b8f1376115f9c090b.jpeg)

   安东尼

   说：

   [2017年2月2日14时21分](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-84109)

   “AspNetRoleClaims”表(ASP.NET核心标识EntityFramework实现的一部分)使用声明描述的权限不应该使用或仅仅是“AspNetClaims”建模吗？(即基于索赔的丰富模型)。

   我所在的组织通常将所有权限管理委托给一个服务台。理想的情况是服务台用户可以打开一个管理应用程序来管理用户、角色和权限(角色实际上是组织中的一组权限模板)。他们不想为用户和角色打开一个应用程序，然后单独打开一个应用程序来管理权限。因此，对用户权限的描述和关联可以在更高级别(例如在ASP.NET核心标识EntityFramework实现中)管理，但实现应该在较低级别(在需要授权的应用程序中)。然后，这将倾向于创建权限&RolePerports表，以避免使用基于声明的表。这篇博客文章谈到了一个类似的场景：<http://benjamincollins.com/blog/practical-permission-based-authorization-in-asp-net-core/>

   女士们似乎确实把你推到了以索赔为基础的路线上。这似乎是一个简单的选择，但不是一个很好的适合你说的。期待你在2017年解决作者(z/s)问题，但我可能已经实现了一些东西！

   [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=84109#respond)

9. ![img](assets/56dfce1dcb38efb546018072943c1590.png)

   [穆罕默德](http://gravatar.com/benaichouchem)

   说：

   [2017年2月2日14时47分](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-84110)

   (刚开始使用IdServer)

   我的困惑来自于这样一个事实：客户端有作用域(“添加”、“删除”等…)。所以我希望我可以和一个用户做类似的工作。最后的作用域将是客户机+用户的交集。

   不确定这是否能让人感觉到：-)

   [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=84110#respond)

10. ![img](assets/9e4853446771f472109e6944025c6791.png)

    马特

    说：

    [2017年3月11日16时51分](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-84993)

    再来一次。关于使用索赔建模权限的问题-Microsoft当然不仅仅推动您这样做，他们明确表示，索赔可以/应该用于授权：<https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims>.

    我发现很难不同意他们在上面说的话，这并没有打破关注点的分离-IDP只是返回身份信息，API正在使用它认为合适的方式。在我看来，关于cookie大小和令牌过期的问题是分开的，我们无法避免它们，但它们是实际的问题，不应该影响概念。

    显然，必须由API来做出特定于其自身需求的授权决策。我也同意，在许多情况下，单个API很可能有更多关于用户的信息，这些信息存储在IDP中是没有意义的，但在一个简单的案例中，我认为API授权访问的任何问题都是基于声称IDP提供的内容的。

    [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=84993#respond)

    - ![img](https://1.gravatar.com/avatar/75681814fbbb90c9224ea5ed0f8324ee?s=90&d=identicon&r=G)

      [多米尼克·拜尔](https://dominickbaier.wordpress.com/)

      说：

      [2017年3月11日21：23](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-85000)

      我没有说“不要使用授权声明”-如果您的授权是基于身份数据的，那是完全没有问题的。

      我是说，声明应该被用来模拟身份，而不是权限。

      [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=85000#respond)

      - ![img](assets/bf54a595d772931e2cec3e47a9017cc7.png)

        吉米

        说：

        [2017年5月29日14时59分](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-87096)

        所以让我们说清楚：

        一项价值为“1970年1月1日”的“出生日期”索赔绝对没有问题。因为它塑造了“身份”。

        价值为“true”的“夜总会访问”的说法是错误的。因为它模拟了“权限”。

        如果你的生态系统有150个不同的客户想要决定用户是否可以进入夜总会，他们应该根据用户的身份自行决定，特别是使用“出生日期”声明。

        在合法进入夜总会的年龄为18岁的国家经营的客户可能会产生与在合法年龄为21岁的国家经营的另一客户不同的结果。如果我们对权限进行了建模，那么一个单独的“夜店访问”就会一直存在问题。

        对，是这样?

        

      - ![img](https://1.gravatar.com/avatar/75681814fbbb90c9224ea5ed0f8324ee?s=90&d=identicon&r=G)

        [多米尼克·拜尔](https://dominickbaier.wordpress.com/)

        说：

        [2017年5月29日15时34分](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-87098)

        是的-类似于你的护照-它包含了出生日期-你可以在许多国家使用它。当你被允许喝酒或进入酒吧时，确切的政策是在当地处理的。

        

      - ![img](assets/e756128fb34c1e74a2a41a2fc709c33d.png)

        吉米

        说：

        [2017年5月30日10：05](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-87134)

        你好，多米尼克，谢谢你在我之前的评论中的回应。我有点困惑，因为在我看来，这篇文章与官方文件中的一个例子相矛盾。在我看来，官方文档中的以下代码似乎是模型*权限*而不是*标识*：

        <http://docs.identityserver.io/en/release/topics/resources.html>

        /此API定义了两个作用域
        范围=
        {
        新范围()
        {
        name=“api 2.ull_access”，
        displayName=“完全访问API 2”，
        },
        新范围
        {
        name=“api2.read_only”，
        displayName=“对API 2的只读访问”
        }
        }

        在我看来，这两个范围就像“权限”。如果API 2想要允许完全访问或不允许访问，这是API 2的内部业务，不应该是身份的一部分。换句话说，在本例中，IdghtyServer似乎携带(和控制！)有关API 2权限的信息。至少我就是这么解释的。请你详细说明一下好吗？

        通过阅读和(我想)理解你的帖子，我希望在身份上有一个简单的“API 2”范围，而不关心“完全访问”或“只读”，因为这是API 2的事情，这与身份本身无关。

        此外，感谢你，布罗克和其他贡献者为一个伟大的项目*和*一个伟大的文件！

        

      - ![img](https://1.gravatar.com/avatar/75681814fbbb90c9224ea5ed0f8324ee?s=90&d=identicon&r=G)

        [多米尼克·拜尔](https://dominickbaier.wordpress.com/)

        说：

        [2017年5月30日11：15](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-87136)

        作用域与用户权限无关-它们是关于用户授予客户端的访问权限。

        我知道这有点让人费解-但一旦你明白了，就完全有意义了；)

        也许读一下这个：<https://www.amazon.com/OAuth-2-Action-Justin-Richer/dp/161729327X/ref=sr_1_1?ie=UTF8&qid=1496135709&sr=8-1&keywords=oauth2+in+action>

        

11. ![img](assets/40a9029c3a560ba2fb99dff0f549eca3.png)

    拉斯托吉

    说：

    [2017年4月15日07：37](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-85641)

    我有点糊涂。我使用角色来控制我的API中的所有权限，使用授权属性。我也使用引用标记。如果访问令牌没有角色声明，API如何知道用户是否被授权访问控制器？再次点击DB来检查用户的角色吗？如果API没有访问用户数据库的权限，该怎么办？

    [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=85641#respond)

    - ![img](https://1.gravatar.com/avatar/75681814fbbb90c9224ea5ed0f8324ee?s=90&d=identicon&r=G)

      [多米尼克·拜尔](https://dominickbaier.wordpress.com/)

      说：

      [2017年4月21日08：32](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-85766)

      我想我已经在我的帖子里说过了。

      您的API应该可以访问数据存储区，在那里它可以找到其授权信息。

      如果你的系统很简单，你只需要几个角色，也许你就可以忽略我的建议-你会后悔以后当复杂性上升的时候。

      [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=85766#respond)

      - ![img](assets/30857e5b1602866208f3e0339e901e91.jpeg)

        [CodeRevver](http://michaelclark.tech/)

        说：

        [2017年6月13日00：14](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-87607)

        你好多米尼克

        我喜欢这个职位。我正试图接受如何在我的应用程序中构造权限，并且有很多相互矛盾的建议。

        不过，我有一个与上述问题有关的问题。

        现在，我可以将100个操作/权限分配给一个角色，一个用户分配给该角色。很容易检查我试图执行的操作(“CreateUser”)是否对我的任何角色都可用，因此允许我访问。是什么使这种方法不如一个完全独立的不使用角色的实现呢？

        另外，你会建议什么样的实现？我能想到的唯一件事就是做上面的几乎相同的事情，远离标识DB。

        例如，这些表：权限、用户组。

        然后，客户授权属性将查看与您正在执行的操作一起执行的允许id是否包含在与您关联的任何组上-与角色几乎完全相同，除非没有声明。

        

      - ![img](https://1.gravatar.com/avatar/75681814fbbb90c9224ea5ed0f8324ee?s=90&d=identicon&r=G)

        [多米尼克·拜尔](https://dominickbaier.wordpress.com/)

        说：

        [2017年6月20日09：23](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-87802)

        从技术上讲，您可以通过多种方式实现这一点。我认为我的主要信息是“不要将身份和权限混为一谈-特别是在安全令牌中”。

        

12. ![img](assets/e51cd7dd258cb105f1bbb5f6e9d414dc.png)

    范埃尔堡

    说：

    [2017年9月4日22：22](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-89835)

    关于身份的部分非常有意义，但真正让我困惑的是IdghtyServer中角色的存在。我并不是说这是授权信息，但我看不出用户是如何在整个系统中被标记为“客户”或“雇员”的。

    索赔同样重要。“客户ID”或“员工ID”如何成为“整个”系统的用户声明？

    如果我想在未来的多个未知应用程序(不限于一个组织)中使用IdghtyServer，那么在某个时候，用户既是“客户”，也是“雇员”，并且可能有多个“员工ID”声明。而且，除非我弄错了，否则没有与客户端应用程序的链接。难道不应该在角色(和声明)表中有一个鉴别器来维护角色并将声明添加到特定的应用程序中吗？还是不应该像这样使用IdghtyServer？

    但是，虽然添加一个鉴别器可能有效，但它似乎并不是解决“角色”问题的正确方法。所以我想到，最好不要使用角色。因为使用它们没有真正的好处。

    因此，在阅读了您的文章(一遍又一遍)并阅读了有关通过授权保护用户数据的内容之后，我今天得出了这样的结论：更好的方法是使用“规则”。因为我认为授权实际上是一组规则，而不是从“授权上下文”中读到的内容。

    使用策略和授权处理程序，我可以实现接近源的高级授权。业务定义规则(或至少为重要部分)，并具有可用于授权的信息，如部门、功能，甚至角色。同时，可以从身份声明中读取访问信息。我不知道要实施这个计划有多难。

    [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=89835#respond)

13. ![img](https://1.gravatar.com/avatar/75681814fbbb90c9224ea5ed0f8324ee?s=90&d=identicon&r=G)

    [多米尼克·拜尔](https://dominickbaier.wordpress.com/)

    说：

    [2017年11月17日09：58](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-91898)

    我们将在一月份在NDC发布消息。还将在…之后不久发表博客请继续收看；)

    [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=91898#respond)

14. ![img](assets/58cdf96ff752f05e7d7a8adc687be7f7.png)

    [Tor Hovland(@torhovland)](http://twitter.com/torhovland)

    说：

    [2018年1月8日下午4：00](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-93224)

    嗨!听到您将在伦敦NDC宣布一些事情，我非常兴奋，自从NDC奥斯陆以来，我一直在急切地等待关于授权的消息。

    我有一个问题想听听你的想法。你说角色是身份和授权之间的灰色地带。我有一个类似的灰色区域：多租户SaaS组合中的服务订阅。几乎所有的服务都需要知道给定用户是否是每个URL请求的租户的成员，以及这些租户是否实际订阅了所请求的服务。您认为可以让IdSrv发出声明，列出用户所属的租户列表，以及每个租户订阅的服务标识符吗？

    如果是的话，您是否也会说，可以包含用户获得的3种通用访问级别中的哪一种：管理员、读/写和读？如果一个服务需要更具体或更细粒度的服务，它需要自己处理它，但是对于大约80%的服务来说，这个基本集将涵盖授权需求，然后他们就不需要查询另一个服务了。

    [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=93224#respond)

    - ![img](https://1.gravatar.com/avatar/75681814fbbb90c9224ea5ed0f8324ee?s=90&d=identicon&r=G)

      [多米尼克·拜尔](https://dominickbaier.wordpress.com/)

      说：

      [2018年01月10日09：15](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-93242)

      嘿,

      我的直觉是：租户列表是好的-服务标识符是应用逻辑。

      [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=93242#respond)

15. ![img](https://i0.wp.com/graph.facebook.com/v2.9/833214620202297/picture?q=type%3Dlarge%26_md5%3D56e72afbee936ba6e465aa045f5b973a&resize=40%2C40)

    [肯·维纳林](https://www.facebook.com/app_scoped_user_id/833214620202297/)

    说：

    [2018年4月5日02时16分](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-94962)

    您知道我在哪里可以找到一个使用OAuth作为授权/权限管理系统的框架吗？是否有可能为此扩展IdghtyServer-如果是的话，有人这样做了吗？我需要一个api权限管理工具，在这个工具中，用户可以授予对特定范围的访问权，还可以检查和撤销第三方应用程序的权限。(就像Facebook的同意书屏幕，您可以在该屏幕之后更改权限)<https://www.facebook.com/settings?tab=applications>)

    [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=94962#respond)

    - ![img](https://1.gravatar.com/avatar/75681814fbbb90c9224ea5ed0f8324ee?s=90&d=identicon&r=G)

      [多米尼克·拜尔](https://dominickbaier.wordpress.com/)

      说：

      [2018年4月5日07：55](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-94970)

      \>用户可以授予对特定作用域的访问权，也可以检查和撤销第三方应用程序的权限。

      这正是OAuth的目的所在-而IdghtyServer正是为此而构建的。

      [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=94970#respond)

16. ![img](assets/710ac361eae6bcaf57593d7bd4150261.png)

    瑞恩

    说：

    [2018年7月7日01：10](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-97864)

    你好多米尼克，谢谢你的这篇文章。我经常提到它。

    我很清楚作用域和用户权限之间的区别(前者代表授权客户机/令牌做什么，后者代表授权用户做什么)。

    然而，我只想知道一个实际的含义。假设我有管理员和使用者使用相同的身份系统，我有一个名为“Reports”的范围，表示对某些报告端点/API的访问。管理员可以授予具有“Reports”作用域的客户端访问权限，并且客户机可以调用这些API。到目前为止还不错。

    如果使用者尝试使用“报告”作用域授予对同一客户端的访问权限，即使用者根本无法访问的范围/功能，该怎么办？允许用户将该范围授权给客户似乎是错误的。该用户所允许的作用域应尽量限制在他/她被授权的范围内。但是，在高级别上，这将是一个用户授权问题(现在IDP中)。

    我怀疑这不是一个独特的问题，对于国内流离失所者具有不同的用户群体。非常感谢你的意见。

    [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=97864#respond)

    - ![img](https://1.gravatar.com/avatar/75681814fbbb90c9224ea5ed0f8324ee?s=90&d=identicon&r=G)

      [多米尼克·拜尔](https://dominickbaier.wordpress.com/)

      说：

      [2018年7月7日12：10](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-97907)

      是的，你是对的-我对你没有很好的回答。它绝对不在协议的范围之内。

      [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=97907#respond)

      - ![img](https://1.gravatar.com/avatar/710ac361eae6bcaf57593d7bd4150261?s=90&d=identicon&r=G)

        瑞恩

        说：

        [2018年7月7日16：10](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-97918)

        啊，好吧，我很感激你的坦率。我能想到的最好的方法是基于一个简单的“组成员”到范围映射在IDP中实现一些粗粒度的授权规则，然后保持应用程序中包含的所有细粒度用户权限(基于指定的角色)。这只足以使国内流离失所者明智地工作，但不足以损害你在上面提到的原则。

        感谢您的参与，我们在社会上，您的专业知识是高度重视的！

        

17. ![img](assets/165a4bb57bd14c69106345c0d4dd6d18.png)

    [博特克莱](http://citysweepbelfast.wordpress.com/)

    说：

    [2018年7月13日12：25](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-98246)

    你好多米尼克
    感谢这篇文章和迄今为止在Identity Server上所做的所有工作。

    是否有基于非索赔的授权方法的基本实施的例子？在声明中，我已经得到了我需要存储的所有内容，但是，由于索赔变得太大，没有多久就遇到了431(请求头字段大到大)。

    我已经浏览了GitHub上的示例，但似乎找不到任何涉及此场景的内容。

    谢谢

    [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=98246#respond)

    - ![img](https://1.gravatar.com/avatar/75681814fbbb90c9224ea5ed0f8324ee?s=90&d=identicon&r=G)

      [多米尼克·拜尔](https://dominickbaier.wordpress.com/)

      说：

      [2018年7月14日09：22](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-98261)

      看这里

      <https://policyserver.io/>

      [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=98261#respond)

18. ![img](assets/8Aonuthh_normal.jpg)

    [Bbone(@bbonecode)](http://twitter.com/bbonecode)

    说：

    [2018年12月11日11：21](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-104245)

    你好多米尼克
    首先，感谢您的文章和您关于身份和授权的所有文章。
    我想我理解身份和许可之间的区别。
    你认为将许可与资源联系起来是个好主意吗？目标将是在特定范围(在访问令牌中定义的API)中向用户授予对特定资源(业务实体：客户端、商人)的权限。
    我看到了您介绍Policy Server的视频，但我不知道如何将Policy Server用于这种情况，或者我跨越了授权和业务需求之间的界限？
    谢谢

    [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=104245#respond)

    - ![img](https://1.gravatar.com/avatar/75681814fbbb90c9224ea5ed0f8324ee?s=90&d=identicon&r=G)

      [多米尼克·拜尔](https://dominickbaier.wordpress.com/)

      说：

      [2018年12月17日15时42分](https://leastprivilege.com/2016/12/16/identity-vs-permissions/#comment-104589)

      将访问控制链接到资源是一种常见的做法-与NTFS的做法相比。您可能需要某种继承规则。同时也考虑到孤儿条目。

      PolicyServer可以帮你做到这一点。

      [答复](https://leastprivilege.com/2016/12/16/identity-vs-permissions/?replytocom=104589#respond)