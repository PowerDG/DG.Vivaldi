<template>
<div >
  <div >
    <div class="float-left" style="margin-top: 20px">
      <a-radio-group  :value="filterType" buttonStyle="solid" @change="activityFilter">
        <a-radio-button class="radio-button" value="all">所有图书</a-radio-button>
        <a-radio-button class="radio-button" value="high">可借图书</a-radio-button>
      </a-radio-group>
      <a-input-search
        placeholder="书名或作者"
        style="width: 250px"
        v-model="filterText"
        @search="onSearch"
        enterButton="搜索"
      />

    </div>
       <div class="float-right" >
      <a-button type="primary"  @click="showDrawer" >
        <a-icon type="plus" />抽屉自测
      </a-button>
    </div>
    <div class="float-right" >
      <a-button type="primary"  @click="showDrawer" v-if="bookCreate">
        <a-icon type="plus" />添加新书
      </a-button>
    </div>
  </div>
  <div>
    <a-drawer
      title="图书详情"
      :width="720"
      @close="onClose"
      :visible="createDrawerVisible"
      :wrapStyle="{height: 'calc(100% - 108px)',overflow: 'auto',paddingBottom: '108px'}"
    >
      <a-form :form="detailForm" layout="vertical" hideRequiredMark>
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item label="书名">
              <a-input
                v-decorator="['name', {
                  rules: [{ required: true, message: '请输入书名' }]
                }]"
                placeholder="请输入书名"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item label="Url">
              <a-input
                v-decorator="['url', {
                  rules: [{ required: true, message: 'please enter url' }]
                }]"
                style="width: 100%"
                addonBefore="http://"
                addonAfter=".com"
                placeholder="please enter url"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item label="Owner">
              <a-select
                v-decorator="['owner', {
                  rules: [{ required: true, message: 'Please select an owner' }]
                }]"
                placeholder="Please a-s an owner"
              >
                <a-select-option value="xiao">Xiaoxiao Fu</a-select-option>
                <a-select-option value="mao">Maomao Zhou</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item label="Type">
              <a-select
                v-decorator="['type', {
                  rules: [{ required: true, message: 'Please choose the type' }]
                }]"
                placeholder="Please choose the type"
              >
                <a-select-option value="private">Private</a-select-option>
                <a-select-option value="public">Public</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item label="Approver">
              <a-select
                v-decorator="['approver', {
                  rules: [{ required: true, message: 'Please choose the approver' }]
                }]"
                placeholder="Please choose the approver"
              >
                <a-select-option value="jack">Jack Ma</a-select-option>
                <a-select-option value="tom">Tom Liu</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item label="DateTime">
              <a-date-picker
                v-decorator="['dateTime', {
                  rules: [{ required: true, message: 'Please choose the dateTime' }]
                }]"
                style="width: 100%"
                :getPopupContainer="trigger => trigger.parentNode"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="16">
          <a-col :span="24">
            <a-form-item label="Description">
              <a-textarea
                v-decorator="['description', {
                  rules: [{ required: true, message: 'Please enter url description' }]
                }]"
                :rows="4"
                placeholder="please enter url description"
              />
            </a-form-item>
          </a-col>
        </a-row>
      </a-form>
      <div
        :style="{
          position: 'absolute',
          left: 0,
          bottom: 0,
          width: '100%',
          borderTop: '1px solid #e9e9e9',
          padding: '10px 16px',
          background: '#fff',
          textAlign: 'right',
        }"
      >
        <a-button
          :style="{marginRight: '8px'}"
          @click="onClose"
        >
          Cancel
        </a-button>
        <a-button @click="onClose" type="primary">Submit</a-button>
      </div>
    </a-drawer>
  </div>
  <div >
    <a-list
      size="large"
      :grid="{ gutter: 16, xs: 1, sm: 1, md: 2, lg: 3, xl: 1, xxl: 1 }"
      :pagination="pagination"
      :dataSource="entitylistData"
    >
      <a-list-item  key="item.title" slot="renderItem" slot-scope="item, index">
        <div class="listDetail">
          <div class="list-col"><span>{{item.order}}</span></div>
          <div class="list-col">
            <div class="text-wrapper" style="width:220px">
              <a >《{{item.name}}》</a>
            </div>
          </div>
          <div class="list-col" style="width:180px">
            <img
              slot="extra"
              height="105"
              alt="logo"
              src="https://gw.alipayobjects.com/zos/rmsportal/mqaQswcyDLcXyDKnZfES.png"
            />
          </div>
          <div class="list-col" style="width:120px">
            <div class="text-wrapper" style="">
              <span>{{item.author}}</span>
            </div>
          </div>
          <div class="list-col" style=" width:80px">
            <span>
              {{item.entryTime.toString().substr(0,10)}}
              <!-- {{formatDate(new Date(item.entryTime ), "yyyy-MM-dd hh:mm")}} -->
            </span>
          </div>
          <div class="list-col" style="width:150px">
            <div class="text-wrapper">
              <a-rate v-model="averageScore" />
              <span class="ant-rate-text">{{likeLevel}}</span>
            </div>
          </div>
          <div class="list-col-text" style="width:150px">
            <div class="text-wrapper" style="max-width:150px">{{item.lastBookReview}}</div>
          </div>
          <div class="list-col-text" style="width:50px">
            <div class="text-wrapper" style="max-width:150px">借阅人</div>
          </div>
          <div class="list-col-text" style="width:150px">
            <div class="text-wrapper" style="max-width:150px">

            </div>
          </div>
        </div>
      </a-list-item>
    </a-list>
  </div>
</div>
</template>


<script>
import { reqGetBookPaged } from "../../request/api";

import { formatDate } from "../../fiters/common.js";
const listData = [];

export default {
  data() {
    return {
      //permission
      bookCreate: true,
      bookUpdate: true,
      bookDelete: true,


likeLevel:0,
      filterType:'all',
      filterText:'',
      listData,
      form: this.$form.createForm(this), 
      formComments: this.$form.createForm(this),
      detailForm: this.$form.createForm(this),





      createDrawerVisible: false,
      bookDetail:[

      ],


      entityEditForm:{

        name: "",
        author: "",
        resource: "",
        photo: "",
        photoHd: "",
      },

      entitylistData: [
     ],
      formTitle: "添加事项",
      queryType: "all",
      // columns,
      current: 1,
      fundData: [],
      cacheData: [],
      remain: 0,
      pagination: {
        pageSizeOptions: ["10", "20", "30", "40", "50"],
        defaultCurrent: 1,
        current: 1,
        showSizeChanger: true,
        pageSize: 20,
        total: 0,
        showQuickJumper: true,
        position: "bottom",
        onChange: (page, pageSize) => this.changePage(page, pageSize),
        showTotal: total => "共 " + total + " 条",
        onShowSizeChange: (current, pageSize) =>
          this.onShowSizeChange(current, pageSize)
      },
      actions: [
        { type: "star-o", text: "156" },
        { type: "like-o", text: "156" },
        { type: "message", text: "2" }
      ],
    };
  },
  methods: {
    getData(page, pageSize, queryType='all') {
      let _this=this;
      reqGetBookPaged({params: {
          MaxResultCount: pageSize,
          SkipCount: pageSize * (page - 1),
          QueryType: queryType,
          filterText: this.filterText
        }
      })
        .then(response => {
          var items = response.result.items;
          for (let i = 0; i < items.length; i++) {
            items[i].order = pageSize * (page - 1) + i + 1;
            items[i].photo =this.webConfig.ImageBaseUrl +  items[i].photo;
            items[i].photoHd =this.webConfig.ImageBaseUrl +  items[i].photoHd;
          }
          _this.entitylistData = items;
          console.log("in entitylistData", this.entitylistData);
          _this.pagination.total = response.result.totalCount;
        })
        .catch(function(error) {
          console.info(error);
          _this.entitylistData=[];
          _this.pagination.total = 0;
        });
    },
//#endregion

    activityFilter(e) {
      console.log(e.target.value);
    },
    changePage(page, pageSize) {
      this.pagination.current = page;
      this.pagination.pageSize = pageSize;
      this.getData(his.pagination.current, this.pagination.pageSize,this.filterType);
    },
    onShowSizeChange(current, pageSize) {
      this.pagination.pageSize = pageSize;
      this.getData(current, this.pagination.pageSize,this.filterType);
    },
    showDrawer() {
      console.log("!!!open!!!!!!!!!");
      this.createDrawerVisible = true
    },
    onClose() {
      this.createDrawerVisible = false
    },
    permissionInit() {
      if (
        this.$store.state.bookPermission.indexOf(
          this.Permission.Book_Create_Default
        ) > -1
      ) {
        this.bookCreate = true;
      }
      if (
        this.$store.state.partyPermission.indexOf(
          this.Permission.Book_Update_Default
        ) > -1
      ) {
        this.bookUpdate = true;
      }
      if (
        this.$store.state.partyPermission.indexOf(
          this.Permission.Book_Delete_Default
        ) > -1
      ) {
        this.bookDelete = true;
      }
    }
  },
  mounted() {
    this.getData(this.pagination.current, this.pagination.pageSize);
    this.permissionInit();
  }
};
</script>
<style>
.list-col {
  margin-right: 15px;

  line-height: 115px;
  text-align: center;
  float: left;
}
.list-col-text {
  margin-right: 15px;

  vertical-align: middle;
  text-align: center;

  display: table-cell;
  float: left;
}
.list-col.a {
  text-align: center;

  /* position: relative; */
  /* top: 50%;  */
  /*偏移*/
  /*
            margin-top: -40px;  */
}
.text-wrapper {
  white-space: pre-wrap;
}
.a {
  top: 50%; /*偏移*/
}
</style>
