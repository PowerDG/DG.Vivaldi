<template>
  <a-list itemLayout="vertical" size="large" :pagination="pagination" :dataSource="entitylistData">
    
    
    
    
    
    <div slot="footer">
      <b>ant design vue</b> footer part
    </div>
    <a-list-item  style="hight:360px"
     slot="renderItem" slot-scope="item, index" key="item.title">
<div class="listDetail"
        style="float:left; "
        >


      <img
        slot="extra"
        width="272"
        alt="logo"
        src="https://gw.alipayobjects.com/zos/rmsportal/mqaQswcyDLcXyDKnZfES.png"
      />
        </div>
  <div>
      <a-list-item-meta :description="item.name">
        <a slot="title" :href="item.href">{{item.order}}</a>
        <!-- <a-avatar slot="avatar" :src="item.avatar" /> -->
      </a-list-item-meta>
      【{{item.resource}}】---{{item.lastBookReview}}

     <template slot="actions" v-for="{type, text} in actions">
        <span :key="type">
          <a-icon :type="type" style="margin-right: 8px" />
          {{text}}
        </span>
      </template>
       </div>
    </a-list-item>
  </a-list>
</template>



<script>
import { reqGetBookPaged } from "../../request/api";
const listData = [];
for (let i = 0; i < 23; i++) {
  listData.push({
    href: "https://vue.ant.design/",
    title: `ant design vue part ${i}`,
    avatar: "https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png",
    description:
      "Ant Design, a design language for background applications, is refined by Ant UED Team.",
    content:
      "We supply a series of design principles, practical patterns and high quality design resources (Sketch and Axure), to help people create their product prototypes beautifully and efficiently."
  });
}

export default {
  data() {
    return {
      listData,

      entitylistData: [],
    //   pagination: {
    //     onChange: page => {
    //       console.log(page);
    //     },
    //     pageSize: 3
    //   },

                  formTitle:"添加事项",
            queryType:'all',
            // columns,
            current:2,
            fundData:[],
            cacheData:[],
            remain: 0,
            pagination:{
              pageSizeOptions: ['2','3','10', '20', '30', '40', '50'],
              defaultCurrent:2,
              current: this.pageCurrent,
              showSizeChanger:true,
              pageSize:2,
              total: 0,
              showQuickJumper:true,
              onChange:(page,pageSize)=>this.changePage(page,pageSize),
              showTotal:(total)=>"共 "+total+" 条",
              onShowSizeChange: (current,pageSize)=>this.onShowSizeChange(current,pageSize),
            },                      
      actions: [
        { type: "star-o", text: "156" },
        { type: "like-o", text: "156" },
        { type: "message", text: "2" }
      ]
    };
  },
  methods: {
    getData(page, pageSize) {
      // this.$axios.get("PartyService.Host/Fund/GetTotalFunds",
      reqGetBookPaged({
        params: {
          MaxResultCount: pageSize,
          SkipCount: pageSize * (page - 1),
          QueryType: this.queryType
        }
      })
        .then(response => {

            
          console.log("--", page);
          console.log("--", pageSize);
          console.log("--", response);
          var items = response.result.items;
          for (let i = 0; i < items.length; i++) {
            items[i].order = pageSize * (page - 1) + i + 1;
          }

          console.log("in items", items);
          this.entitylistData = items;

          console.log("in entitylistData", this.entitylistData);
          this.pagination.total = response.result.length;
        })
        .catch(function(error) {
          console.info(error);
        });
    },
     activityFilter(e) {
      console.log(e.target.value);
    },
    changePage(page, pageSize) {
      console.log("page: ", page);
      console.log("pageSize: ", pageSize);
      this.current = page;
      this.pagination.current = page;
      this.pagination.pageSize = pageSize;
      this.getData(this.current, this.pagination.pageSize);
    },
    onShowSizeChange(current, pageSize) {
      this.pagination.pageSize = pageSize;
      //  this.current=current;
      console.log("onShowSizeChange: ", current, pageSize);
      this.getData(this.current, this.pagination.pageSize);
    },
    onChange(pageNumber) {
      console.log("current: ", pageNumber.current);
      this.pagination.current = pageNumber.current;
      console.log("pagezie: ", this.pagination.pageSize);
      console.log(
        "skip: ",
        this.pagination.pageSize * (this.pagination.current - 1)
      );
    },
    handleChange(value, key, column) {
      console.log("handleChange", value);
      console.log("handleChange", key);
      console.log("handleChange", column);
      const newData = [...this.entitylistData];
      const target = newData.filter(item => key === item.id)[0];
      if (target) {
        target[column] = value;
        this.data = newData;
      }
    },
  },

  mounted() {
    console.log("in mouont");
    // console.log(" mouont",this.current,this.pagination.pageSize)
    // this.getRemainMoney();
    // this.getData(this.current,this.pagination.pageSize);

    this.getData(this.current, this.pagination.pageSize);
  }
};
</script>
<style>
</style>
