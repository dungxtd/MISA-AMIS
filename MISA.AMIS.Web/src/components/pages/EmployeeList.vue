<template>
  <div class="content">
    <div class="title">
      <div class="title-left">
        <div class="title-heading">Nhân viên</div>
      </div>
      <div class="title-right">
        <!-- <div class="button-utilities">
            <div class="utilities-text">Tiện ích</div>
            <div class="ic ic-max-16 utilities-icon"></div>
          </div> -->
        <div class="button-add">
          <div class="add-text">Thêm mới nhân viên</div>
          <!-- <div class="add-line"></div>
            <div class="ic ic-max-16 add-icon"></div> -->
        </div>
      </div>
    </div>
    <!-- <div class="title-bottom">
        <div class="ic ic-max-16  bottom-icon"></div>
        <div class="bottom-text">Tất cả danh mục</div>
      </div> -->
    <div class="main-content">
      <div class="header-list">
        <div class="header-left">
          <!-- <div class="more-button">
              <div class="more-button-text">Thực hiện hàng loạt</div>
              <div class="ic ic-max-16 more-button-icon"></div>
            </div> -->
        </div>
        <div class="header-right">
          <div class="filter-input filter-employee-input">
            <input
              v-model="inputFilter"
              type="text"
              placeholder="Tìm theo mã, tên nhân viên"
              class="filter-input-label filter-employee-input-label"
            />
            <div
              v-on:click="searchAndArrangePage()"
              class="ic ic-max filter-input-icon"
            ></div>
          </div>
          <div v-on:click="loadData()" class="ic ic-max button-refresh"></div>
          <div class="ic ic-max button-excel"></div>
          <!-- <div class="ic ic-max button-setting"></div> -->
        </div>
      </div>
      <div class="list">
        <table>
          <thead>
            <tr>
              <th class="t-table check-box"><input type="checkbox" /></th>
              <th class="t-table">MÃ NHÂN VIÊN</th>
              <th class="t-table">TÊN NHÂN VIÊN</th>
              <th class="t-table">GIỚI TÍNH</th>
              <th class="t-table">NGÀY SINH</th>
              <th class="t-table">SỐ CMND</th>
              <th class="t-table">CHỨC DANH</th>
              <th class="t-table">TÊN ĐƠN VỊ</th>
              <th class="t-table">SỐ TÀI KHOẢN</th>
              <th class="t-table">TÊN NGÂN HÀNG</th>
              <th class="t-table">CHI NHÁNH TK NGÂN HÀNG</th>
              <th class="t-table">CHỨC NĂNG</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="employee in employees" :key="employee.employeeId">
              <td class="t-table check-box"><input type="checkbox" /></td>
              <td class="t-table">{{ employee.employeeCode }}</td>
              <td class="t-table">{{ employee.employeeName }}</td>
              <td class="t-table">{{ employee.gender }}</td>
              <td class="t-table">{{ employee.dateOfBirth | formatDate }}</td>
              <td class="t-table">{{ employee.identityNumber }}</td>
              <td class="t-table">{{ employee.employeePosition }}</td>
              <td class="t-table">
                {{ employee.departmentName }}
              </td>
              <td class="t-table">{{ employee.bankAccountNumber }}</td>
              <td class="t-table">{{ employee.bankName }}</td>
              <td class="t-table">{{ employee.bankBranchName }}</td>
              <td class="t-table wrap-edit-icon">
                <div class="edit-text">Sửa</div>
                <div class="ic ic-max-16 edit-icon"></div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <div class="footer-list">
      <div class="left-footer">Tổng số bản ghi</div>
      <div class="right-footer">
        <select v-model="npSelected">
          <option value="10">10 bản ghi trên một trang</option>
          <option value="20">20 bản ghi trên một trang</option>
          <option value="30">30 bản ghi trên một trang</option>
          <option value="40">40 bản ghi trên một trang</option>
        </select>
        <div class="direc-page">Trước</div>
        <div class="nb-page">{{ indexPage }}</div>
        <div class="nb-page" @click="indexPage++">{{ indexPage + 1 }}</div>
        <div class="nb-page" @click="indexPage += 2">
          {{ indexPage + 2 }}
        </div>
        <div class="direc-page">Sau</div>
      </div>
    </div>
  </div>
</template>
<script>
import axios from "axios";
export default {
  name: "EmployeeList",
  //   Tại dữ liệu khi mở page
  created() {
    //Load dữ liệu
    this.searchAndArrangePage();
    // console.log(this.employees);
  },
  //   Các hàm
  methods: {
    //   Hàm load data
    loadData() {
      this.inputFilter = "";
      this.searchAndArrangePage();
    },
    // Hàm tìm kiếm và sắp xếp page
    async searchAndArrangePage() {
      var aipUrl =
        "https://localhost:44368/api/v1/Employees/paging?pageIndex=" +
        this.indexPage +
        "&pageSize=" +
        this.npSelected;
      if (this.inputFilter != "") aipUrl += "&filter=" + this.inputFilter;
      console.log(this.npSelected);
      await axios
        .get(aipUrl)
        .then((res) => {
          this.employees = res.data;
        })
        .catch((err) => {
          console.log(err);
        });
      console.log(this.employees);
    },
  },
  //   Theo dõi biến
  watch: {
    npSelected() {
      this.searchAndArrangePage();
    },
    indexPage() {
      this.searchAndArrangePage();
    },
  },
  // Data truyền vào
  data() {
    return {
      employees: [],
      inputFilter: "",
      npSelected: 10,
      indexPage: 1,
    };
  },
};
</script>
<style></style>
