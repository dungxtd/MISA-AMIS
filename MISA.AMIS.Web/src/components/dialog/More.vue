<template>
<div id="more">
    <td class="t-table wrap-edit-icon non-border">
        <div class="edit-text" @click="editClick()">Sửa</div>
        <div class="ic ic-max edit-icon"  @click="moreClick()">
        </div>
            <ul class="option" :class="{ 'hide-more': !isShowMore }">
                <li>Nhân bản</li>
                <li @click="deleteEmployee()">Xoá</li>
                <li>Ngưng sử dụng</li>
            </ul>
    </td>
</div>
</template>

<script>
import axios from "axios"
export default {
name: "more_dialog",
props: {
    idDelete: { type: String, default: "" },
},
methods: {
    moreClick(){
        if( this.isShowMore == false )
            this.isShowMore = true;
        else this.isShowMore = false;
    },
    async deleteEmployee(){
        var aipUrl =
        "https://localhost:44368/api/v1/Employees?id=" + this.idDelete;
        await axios.delete(aipUrl)
        .then((res) => {
          if(res.status == 200){
              this.isShowMore = false
              this.$emit("loadData");
          }
        })
        .catch((err) => {
          console.log(err);
        });
    },
    editClick(){
        this.$emit("editClick")
    }
},
data() {
    return {
        isShowMore: false, //biến hiển thị ô xoá
    }
}
}
</script>

<style scope>
.hide-more {
  display: none;
}
ul li {
    list-style: none;
    text-align: inherit;
    padding: 8px;
    box-sizing: border-box;
    outline: none;
    text-transform: none;
    text-decoration: none;
}
ul li:hover{
    background-color: #eee;
    color: #2ca01c;
}
.non-border{
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