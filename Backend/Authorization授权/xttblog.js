
use xttblog //进入 xttblog 数据库
 
db.createRole(
    { 
    role:"testrole",//角色名称
    privileges: [ // 权限集
        {
            resource: //资源 
                            {
                                    db:"jhr", //创建的testrole角色具有对xttblog库的操作权限，具体权限建actions
                                    collection:"" //xttblog库下对应的集合名.如果为""表示所有集合
                            },
                        actions: [ "find", "insert", "remove","update" ] //角色可进行的操作，注意这里是一个数组
                }
        ],
        roles: [] // 是否继承其他的角色，如果指定了其他角色那么新创建的角色自动继承对应其他角色的所有权限，该参数必须显示指定
    }
)
{ 
    role: "testrole", //角色名称
    privileges: [ // 权限集
        { //资源 
            resource: {
                                    db: "jhr", //创建的testrole角色具有对xttblog库的操作权限，具体权限建actions
                                    collection: "" //xttblog库下对应的集合名.如果为""表示所有集合
            },
                        actions: [
                "find",
                "insert",
                "remove",
                "update"
            ] //角色可进行的操作，注意这里是一个数组
        }
    ],
        roles: [] // 是否继承其他的角色，如果指定了其他角色那么新创建的角色自动继承对应其他角色的所有权限，该参数必须显示指定
}


// 指定内建角色来创建用户
db.createUser(
    {
        user:'mongo', // 用户名
        pwd:'123',    // 密码
        roles:[
            {
                role:'root',// 通过指定内建角色root 来创建用户
                db:'admin'  // 指定角色对应的认证数据库，内建角色通常认证库为admin
            }
        ]
    }
);
// 指定自定义角色来创建用户，这里是在admin下创建的用户故认证库也是admin
db.createUser(
    { 
        user:"mongo",// 用户名
        pwd:"123",   // 密码
        roles:["testrole"] //通过指定用户自定义角色来创建用户,注意这里是数组
    }
)