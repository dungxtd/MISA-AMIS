<template>
  <div class="content">
    <div class="title">
      <div class="title-left">
        <div class="title-heading">Nhân viên</div>
      </div>
      <div class="title-right">
        <div class="button-add" @click="showPopupDetailParent()">
          <div class="add-text">Thêm mới nhân viên</div>
        </div>
      </div>
    </div>
    <div class="main-content">
      <div
        v-bind:class="{ popup_delete: isActDeletePu }"
        v-bind:style="{ left: pPopup[0] - 150 + 'px', top: pPopup[1] + 'px' }"
      ></div>
      <div class="header-list">
        <div class="header-left"></div>
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
              <td class="t-table">{{ employee.gender | formatGender }}</td>
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
                <div class="edit-text" @click="editClick(employee.employeeId)">
                  Sửa
                  <div class="dropdown">
                    <div
                      class="ic ic-max-16 edit-icon dropdown-toggle"
                      id="dropdownMenuButton"
                      data-toggle="dropdown"
                      aria-haspopup="true"
                      aria-expanded="false"
                    ></div>
                    <ul
                      class="dropdown-menu"
                      aria-labelledby="dropdownMenuButton"
                    >
                      <li class="dropdown-item" href="#">Action</li>
                      <li class="dropdown-item" href="#">Another action</li>
                      <li class="dropdown-item" href="#">
                        Something else here
                      </li>
                    </ul>
                  </div>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <div class="footer-list">
      <div class="left-footer">Tổng số : {{ count }} bản ghi</div>
      <div class="right-footer">
        <select v-model="pageSize">
          <option value="10">10 bản ghi trên một trang</option>
          <option value="20">20 bản ghi trên một trang</option>
          <option value="30">30 bản ghi trên một trang</option>
          <option value="40">40 bản ghi trên một trang</option>
        </select>
        <div class="direc-page" @click="toFPage()">Trước</div>
        <div
          class="nb-page"
          v-bind:class="{ crrPage: isFiPage }"
          @click="fiPage()"
        >
          {{ pageIndexTemp | formatIndexPagePre }}
        </div>
        <div
          class="nb-page"
          v-bind:class="{ crrPage: isScPage }"
          @click="sePage()"
        >
          {{ pageIndexTemp }}
        </div>
        <div
          class="nb-page"
          v-bind:class="{ crrPage: isThPage }"
          @click="thPage()"
        >
          {{ pageIndexTemp | formatIndexPageNext }}
        </div>
        <div class="direc-page" @click="toLPage()">Sau</div>
      </div>
    </div>
    <EmployeeDetail
      :isShow="isShowDetail"
      :employee="employee"
      @hideDetailPageParent="hideDetailPageParent"
    />
  </div>
</template>
<script>
import axios from "axios";
import EmployeeDetail from "./EmployeeDetail";
// import ClickOutside from 'vue-click-outside'

export default {
  name: "EmployeeList",
  components: {
    EmployeeDetail,
  },
  //   Tại dữ liệu khi mở page
  created() {
    //Load dữ liệu
    this.pageIndexTemp = this.pageIndex;
    this.loadData();
  },
  //   Các hàm
  methods: {
    //   Hàm load data
    loadData() {
      this.inputFilter = "";
      this.pageIndex = 1;
      this.pageIndexTemp = this.pageIndex + 1;
      this.getData();
      this.getCount();
    },
    // Hàm tìm kiếm và sắp xếp page
    async getData() {
      var aipUrl =
        "https://localhost:44368/api/v1/Employees/paging?pageIndex=" +
        this.pageIndex +
        "&pageSize=" +
        this.pageSize;
      if (this.inputFilter != "") aipUrl += "&filter=" + this.inputFilter;
      console.log(this.pageSize);
      await axios
        .get(aipUrl)
        .then((res) => {
          this.employees = res.data;
        })
        .catch((err) => {
          console.log(err);
        });
      // console.log(this.employees);
    },
    // Hàm đếm số lượng bản ghi
    async getCount() {
      var aipUrl =
        "https://localhost:44368/api/v1/Employees/count-paging?filter=" +
        this.inputFilter;
      await axios
        .get(aipUrl)
        .then((res) => {
          this.count = res.data[0];
        })
        .catch((err) => {
          console.log(err);
        });
      this.maxPage = (this.count / this.pageSize).toFixed();
    },
    // Hàm tìm kiếm và phân trang
    searchAndArrangePage() {
      this.pageIndex = 1;
      this.pageIndexTemp = this.pageIndex + 1;
      this.getData();
      this.getCount();
    },

    // Hàm chuyển trang của nút đầu tiên
    fiPage() {
      if (this.maxPage > 2) {
        if (this.pageIndex > 1 && this.pageIndex < this.maxPage) {
          if (this.pageIndexTemp > 2) {
            this.pageIndex--;
            this.pageIndexTemp = this.pageIndex;
          } else this.pageIndex--;
        }
        if (this.pageIndex == this.maxPage) {
          this.pageIndex = this.pageIndex - 2;
          this.pageIndexTemp = this.pageIndex;
        }
      } else {
        this.pageIndex = 1;
        this.pageIndexTemp = 2;
        this.isFiPage = true;
        this.isScPage = false;
        this.isThPage = false;
        this.getData();
      }
    },
    // Hàm chuyển trang của nút thứ 2
    sePage() {
      if (this.maxPage > 2) {
        if (this.pageIndex == 1) {
          this.pageIndex++;
        } else if (this.pageIndex == this.maxPage) {
          this.pageIndex--;
          this.pageIndexTemp = this.pageIndex;
        }
      } else {
        this.pageIndex = 2;
        this.pageIndexTemp = 2;
        this.isFiPage = false;
        this.isScPage = true;
        this.isThPage = false;
        this.getData();
      }
    },
    // Hàm chuyển trang của nút thứ 3
    thPage() {
      if (this.maxPage > 2) {
        if (this.pageIndex > 1 && this.pageIndex < this.maxPage) {
          this.pageIndex++;
          this.pageIndexTemp = this.pageIndex;
        } else if (this.pageIndex == 1) {
          this.pageIndex += 2;
          this.pageIndexTemp = this.pageIndex;
        }
        if (this.pageIndex == this.maxPage && this.isThPage == false) {
          this.pageIndexTemp--;
        }
      } else {
        this.pageIndex = 3;
        this.pageIndexTemp = 2;
        this.isFiPage = false;
        this.isScPage = false;
        this.isThPage = true;
        this.getData();
      }
    },
    // Hàm chuyển đến trang đầu
    toFPage() {
      this.pageIndex = 1;
      this.pageIndexTemp = 2;
    },
    /* Hàm chuyển đến cuối trang */
    toLPage() {
      this.pageIndex = this.maxPage;
      this.pageIndexTemp = this.maxPage - 1;
    },
    /* Hàm showPopupDelete */
    showPopupDelete(e) {
      this.isActDeletePu = true;
      this.pPopup[0] = e.pageX;
      this.pPopup[1] = e.pageY;
      alert(this.pPopup[0] + " " + this.pPopup[1]);
      // $popup_delete
    },
    clickOutside() {
      alert(111111111);
    },
    showPopupDetailParent() {
      this.formMode = true;
      this.employee = Object.create(null);
      this.isShowDetail = true;
    },
    hideDetailPageParent() {
      this.isShowDetail = false;
    },
    async editClick(employeeId) {
      // Lấy id của bản ghi được chọn
      this;

      //Gọi API lấy id thông tin nhân viên
      var apiUrl = "https://localhost:44368/api/v1/Employees/" + employeeId;
      await axios
        .get(apiUrl)
        .then((res) => {
          this.employee = res.data;
          console.log(this.employee);
        })
        .catch((err) => {
          console.log(err);
        });
      //Lấy trạng thái là form sửa
      this.formMode = false;
      //Hiển thị dialog
      this.isShowDetail = true;
    },
  },
  //
  //
  //   Theo dõi biến
  watch: {
    pageSize() {
      this.searchAndArrangePage();
    },
    pageIndex() {
      if (this.maxPage > 2) {
        if (this.pageIndex == 1) {
          this.isFiPage = true;
          this.isScPage = false;
          this.isThPage = false;
          this.getData();
        } else if (this.pageIndex == this.maxPage) {
          this.isFiPage = false;
          this.isScPage = false;
          this.isThPage = true;
          this.getData();
        } else {
          this.isFiPage = false;
          this.isScPage = true;
          this.isThPage = false;
          this.getData();
        }
      }
    },
  },

  // Data truyền vào
  data() {
    return {
      employees: [],
      inputFilter: "", // Từ khoá cần tìm kiếm
      pageSize: 10, // Số bản ghi yteen 1 trang
      pageIndex: 1, // Vị trí hiện tại cảu page
      pageIndexTemp: 0, //Biến để hiển thị số trang
      maxPage: 0, // Số số trang
      isFiPage: true, // Page 1 là page hiện tại
      isScPage: false, // Page 2 là page hiện tại
      isThPage: false, // Page 3 là page hiện tại
      count: 0, // Biến đếm số bản ghi
      isActDeletePu: false, //Biến hiển thị ô xoá
      pPopup: [0, 0], //
      isShowDetail: false, //biến ẩn hay hiện bảng detail
      employee: {}, //Biến chứa thông tin nhân viên hiển thị lên dialog
      formMode: null, //Biến chứa thông tin thêm hay sửa gửi thị lên dialog
    };
  },
};
</script>

<style scope>
.crrPage {
  background-color: #38393d;
  color: #fff;
}
.popup_delete {
  position: absolute;
  width: 100px;
  height: 100px;
  background: black;
  z-index: 999;
  top: 179px;
}
</style>
