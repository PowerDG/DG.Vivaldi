安装 ant

$ npm install ant-design-vue --save

或者 $ yarn add ant-design-vue

```
npm install babel-plugin-import --save-dev
```

main.js添加 

```
import Antd from 'ant-design-vue'
import 'ant-design-vue/dist/antd.css'

Vue.use(Antd)
```



# Vue学习笔记

```html
<!DOCTYPE html>
<html>
<head>
<meta charset="utf-8">
<title>Vue 测试实例 - 菜鸟教程(runoob.com)</title>
<script src="https://cdn.staticfile.org/vue/2.2.2/vue.min.js"></script>
<style>
		.static {
	width: 100px;
	height: 50px;
	
}
	.size {
	width: 200px;
	height: 50px;
	
}
.active {

	background: green;
}
</style>
</head>
<body>
<div id="app">
  <div class="static" v-bind:class="{ active: isActive,size:reSize }">test</div>
<div v-bind:style="{'color': color, 'fontSize': fontSize + 'px'}">wenben<div>
	</div>
<div style="color:#AAAAAA">wenben<div>
	</div>
<script>
new Vue({
  el: '#app',
  data: {
    isActive: true,
	reSize: false,
	color:'#AAAAAA' ,
	fontSize:22,
    books:[
        {name:'vue.js 实战'},
        {name:'javascript 实战'}
    ],
    date: new Date()
  },
    created: function(){
        
    },
    mounted: function(){
      var this = this;//声明一个变量指向Vue 实例this ，保证作用域一致
        this.timer= setinterval(function() {
        this.date= new Date();//修改数据date
        } f 1000);  
    },
    beforeDestroy: function(){
        if (this.timer) {
            clearinterval(this.timer); 
            //在Vue 实例销毁前，清除我们的定时器
        }
    }
})
</script>
</body>
</html>
```



## 属性值可以进行简单的运算

{{number}}

isOK ? '确定':'取消'

过滤：格式化

{{date| formatDate}}

filters:{

​	formatDate: function(value){

​	}

}

## v-bind

v-bind可以用：简化

```
<img v-bind : src="imgUrl">
<img :src="imgUrl">
<div :class="{'active': isActive}">
class可以与普通class共存，还可以添加多个
<div :class="{'active': isActive， 'error':isError}">

 <span slot="operateMoney"  slot-scope="operateMoney" :class="{'red':operateMoney<0,'green':operateMoney>=0}" >
        {{operateMoney}}</span>
```



## button

v-on 可以用 @简化

```
<button v-on:click="handleClose">隐藏</button>
<button @click="handleClose">隐藏</button>
methods:{
	handleColse: function(){
		this.show= !this.show;
	}
}
```

## computed

```
 computed:{
          color: function () {
            return this.remain<0? 'red':'green';
          }
          fullName:{
          	//getter
          	get:function(){}
          	//setter
          	set:function(newValue){}
          }
      }
      
```

可以依赖其他的计算属性，页面上还可以用其他实例的data

具有缓存性质，依赖的数据发生变化时，才会重新取值

## methods

```
 {{color()}}
 
 methods:{
          color: function () {
            return this.remain<0? 'red':'green';
          }
      }
```

每次渲染都重新计算



## vue script:

```vue
import Fund from "@/components/party/Fund";

export default {
      name: "PartyPage",
      components:{Activity, Fund},
      data(){
        return {
          partymodule:"Activity",
          state:{
            current: 'mail'
          }
        }
        }
       ,
      methods:{
          menuclick()
            {

            }
      }

}
```

组件引用

```
components:{ActivityShow,ActivityPhoto},
```



## **v-cloak** 的用法

```
<div v-cloak>
  {{ message }}
</div>
```

css 添加

[v-cloak] {
  display: none;
}

```css
[v-cloak] {
  display:none !important;
}
```

## v-show  v-if

```
<p v-show="status === 1">当status 为1 时显示该行</p>
```

​    v-if真正渲染，根据表达式适当的销毁和重建元素

## v-for



## click

```
＜ ！一阻止单击事件冒泡一〉
<a @click.stop=”handle "></a>
〈！一提交事件不再重载页面一〉
<form @submit.prevent="handle” ></ form>
〈！一修饰符可以串联一〉
<a @click.stop.prevent=” handle ” ></a>
〈！一只有修饰符一〉
<form @submit . prevent></form>
〈！一添加事件侦听器时使用事件捕获模式一〉
<div @click . capture=”handle ”> ... </div>
〈！一只当事件在该元素本身（而不是子元素） 触发时触发回调一〉
<div @click.self=” handle ”> ... </div>
＜ ！一只触发一次，组件同样适用一〉
<div @click.once=” handle ”> ... </div>
<!--只有在keyCode 是13 时调用vm.submit()-->
<input @keyup.13 ＝"submit">

全局定义按键名字  Vue.config.keyCode.f1=112;
@keyup.f1
提供的别名：.enter  .tab .delete删除和退格  .esc .space .up
.down .left .right  .ctrl .alt .shift .meta 
组合例如：
<! -- Shift + S -- >
<input @keyup.shift . 83 =” handleSave”>
< !-- Ctrl + Click -- >
<div @click.ctrl=” doSomething”> Do something</div>
```

# 5 数组更新

push() pop() shift() unshift() splice() sort() reverse()



不会更新的操作 返回一个新的数组

filter()  concat()  slice()



## v-model

可以用 @input 可以实时更新

```
<textarea v-model="text" placeholder="input" ></textarea>
<textarea @input="handleInput" placeholder="input" ></textarea>

  data: {
	text:""
	}
	methods:{
		handleInput:function(e){ this.text= e.target.value;}
	}
	
	
	form.getFieldValue('password')
```





# code

```
//console.log(this.$route.path);  //当前路由

  // this.$router.ref({ path:this.$route.path});
  // this.$router.go(0);//刷新当前页，
  // this.$router.push({ path:'/Party/Fund'  })
```



表单

初始值

```
  <a-form-item>
          <a-input
            v-model="loginForm.password"
            v-decorator="[ 'password',
                      { initialValue: '123', 
                       rules: [{ required: true, message: '请输入用户名!' }] }
                      ]"
            type="password"
            placeholder="密码">
            <a-icon slot="prefix" type="lock" style="color: rgba(0,0,0,.25)" />
          </a-input>
        </a-form-item>
        
        Email
         <a-input
            v-decorator="[
          'email',
          {
            rules: [{
              type: 'email', message: 'The input is not valid E-mail!',
            }, {
              required: true, message: 'Please input your E-mail!',
            }]
          }
        ]"
          />
```





npm install --save vuex

npm install vuex-persistedstate  --save



## 7 组件

```
Vue.component ( 'my-component', {
  template:'<div>这里是组件的内容</div>'}) ;
```

组件中data声明数据，不会共享，如果想共享，就在外层定义，data里引用。

共享例子：

```
var data ={ counter:0};
Vue.component ( 'my-component', {
  template:'<div>这里是组件的内容 {{counter}}</div>',
  data:function(){
  	return data;
  }
  }) ;
```

### 7.2传递数据

1. props 父给子模块传数据

传递：

```
 <activity-photo
              :activityPhotos="item.partyPhotoDtos"
              :party-id="item.id"
              @refreshPhoto="handlerefreshPhoto"  //接收子组件事件
            ></activity-photo>
```

接收：

props:['message']

```
 export default {
    props:['activityPhotos','partyId'],
    data () {
      return {
        partyPhotoCreate:false,
        destroyOnClose:true,
        id:this.partyId  //获取之后就可以修改了
      }
    },
    computed:{
       acitivityId:{
         return '000'+this.partyId;
       }
    },
    methods:{
        handlerefreshPhoto() {
      		console.log("refreshPhoto");
      		this.getData(this.pagination.current, this.pagination.pageSize);
    	},
    }
```

props 类型可以是  String, Number,Boolean,Object, Array, Function



emit 子模块给父组件发送事件, $on()来监听子组件事件

```
this.$emit('refreshPhoto')  //发送事件

@refreshPhoto="handlerefreshPhoto"  //接收子组件事件
```

### 7.3 非父子组件通信

```

```



7.4 slot 分发内容

86

