<template>
  <div id="more" v-click-outside="clickOutSide">
    <td class="t-table wrap-edit-icon non-border">
      <div class="edit-text" @click="editClick()">Sửa</div>
      <div class="ic ic-max edit-icon" @click="moreClick()"></div>
      <ul class="option" :class="{ 'hide-more': !isShowMore }">
        <li>Nhân bản</li>
        <li @click="deleteClick()">Xoá</li>
        <li>Ngưng sử dụng</li>
      </ul>
    </td>
    <ReportEdit
      v-if="isShowReportLog == true"
      @deleteEmployee="deleteEmployee"
      @hideReportLog="hideReportLog"
      :codeDelete="codeDeleteTemp"
    />
  </div>
</template>

<script>
import axios from "axios";
import Vue from "vue";
import ReportEdit from "../dialog/ReportEdit";
import vClickOutside from "v-click-outside";
Vue.use(vClickOutside);
export default {
  components: {
    ReportEdit,
  },
  name: "more_dialog",
  props: {
    idDelete: { type: String, default: "" },
    codeDelete: { type: String, default: "" },
  },

  methods: {
    clickOutSide() {
      this.isShowMore = false;
    },
    deleteClick() {
      this.codeDeleteTemp = this.codeDelete;
      this.isShowReportLog = true;
    },
    moreClick() {
      if (this.isShowMore == false) this.isShowMore = true;
      else this.isShowMore = false;
    },
    async deleteEmployee() {
      var aipUrl =
        "https://localhost:44368/api/v1/Employees?id=" + this.idDelete;
      await axios
        .delete(aipUrl)
        .then((res) => {
          console.log(res);
          if (res.status == 200) {
            this.isShowMore = false;
            this.$emit("statusSuccess");
            this.$emit("showStatusLog", "Xoá nhân viên thành công.");
            this.$emit("getData");
          }
        })
        .catch((err) => {
          console.log(err);
          this.$emit("statusWarning");
          this.$emit("showStatusLog", err.response.data.devMsg);
        });
    },
    hideReportLog() {
      this.isShowReportLog = false;
      this.isShowMore = false;
    },
    editClick() {
      this.$emit("editClick");
    },
  },
  data() {
    return {
      isShowMore: false, //biến hiển thị ô xoá
      isShowReportLog: false, //Bien hien thi report
      codeDeleteTemp: "", //Bien
    };
  },
};
</script>

<style scope>
.hide-more {
  display: none;
}
#more {
  font-weight: bold;
}
ul li {
  list-style: none;
  text-align: inherit;
  padding: 8px;
  box-sizing: border-box;
  outline: none;
  text-transform: none;
  text-decoration: none;
  font-weight: 100;
  cursor: pointer;
}
ul li:hover {
  background-color: #eee;
  color: #2cbc18;
}
.non-border {
  border: 0;
}
.edit-text {
  width: 40px;
  height: 30px;
  color: #0075c0;
  cursor: pointer;
  /* flex: 1; */
  text-align: center;
  /* display: flex; */
  /* justify-content: space-between; */
  /* margin-left: 20px; */
  margin: auto 0;
  padding-top: 8px;
}
</style>
