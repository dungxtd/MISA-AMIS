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
            <div class="ic ic-max filter-input-icon"></div>
          </div>
          <div @click="loadData()" class="ic ic-max button-refresh"></div>
          <div class="ic ic-max button-excel" @click="exportFile()"></div>
        </div>
      </div>
      <div class="list">
        <table>
          <thead>
            <tr>
              <th class="t-table check-box"><input type="checkbox" /></th>
              <th class="t-table" style="min-width: 100px">MÃ NHÂN VIÊN</th>
              <th class="t-table" style="min-width: 170px">TÊN NHÂN VIÊN</th>
              <th class="t-table" style="text-align: left; min-width: 70px">
                GIỚI TÍNH
              </th>
              <th class="t-table" style="text-align: center; min-width: 100px">
                NGÀY SINH
              </th>
              <th class="t-table" style="text-align: center; min-width: 100px">
                SỐ CMND
              </th>
              <th class="t-table" style="min-width: 100px">CHỨC DANH</th>
              <th class="t-table" style="min-width: 120px">TÊN ĐƠN VỊ</th>
              <th class="t-table" style="min-width: 100px">SỐ TÀI KHOẢN</th>
              <th class="t-table" style="min-width: 100px">TÊN NGÂN HÀNG</th>
              <th class="t-table" style="min-width: 100px">
                CHI NHÁNH TK NGÂN HÀNG
              </th>
              <th class="t-table" style="text-align: center; border-right: 0">
                CHỨC NĂNG
              </th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="employee in employees" :key="employee.employeeId">
              <td class="t-table check-box"><input type="checkbox" /></td>
              <td class="t-table">{{ employee.employeeCode }}</td>
              <td class="t-table">{{ employee.employeeName }}</td>
              <td class="t-table" style="text-align: left">
                {{ employee.gender | formatGender }}
              </td>
              <td class="t-table" style="text-align: center">
                {{ employee.dateOfBirth | formatDate }}
              </td>
              <td class="t-table" style="text-align: center">
                {{ employee.identityNumber }}
              </td>
              <td class="t-table">{{ employee.employeePosition }}</td>
              <td class="t-table">
                {{ employee.departmentName }}
              </td>
              <td class="t-table">{{ employee.bankAccountNumber }}</td>
              <td class="t-table">{{ employee.bankName }}</td>
              <td class="t-table">{{ employee.bankBranchName }}</td>
              <td style="border-right: 0">
                <MoreDialog
                  :idDelete="employee.employeeId"
                  :codeDelete="employee.employeeCode"
                  @editClick="editClick(employee.employeeId)"
                  @getData="getData"
                  @showStatusLog="showStatusLog"
                  @statusSuccess="statusSuccess"
                  @statusWarning="statusWarning"
                />
              </td>
            </tr>
          </tbody>
        </table>
        <div
          class="ms-pagination"
          style="bottom: unset; width: 2046px"
          :class="{ 'hide-more': !emptyData }"
        >
          <div class="w-full img-wrap pb-4">
            <img
              src="https://actappg2.misacdn.net/img/bg_report_nodata.76e50bd8.svg"
              class="nodata-img"
            />
            <br /><span class="nodata-text">Không có dữ liệu</span>
          </div>
          <th class="ms-out-right-white-30"></th>
          <th class="ms-out-right-grey-30"></th>
        </div>
      </div>
    </div>
    <div class="footer-list">
      <div class="left-footer" style="display: flex">
        Tổng số :
        <div style="font-weight: bold; margin: 0 4px">{{ count }}</div>
        bản ghi
      </div>
      <div class="right-footer">
        <div class="select-wrap">
          <select v-model="pageSize">
            <option value="10">10 bản ghi trên một trang</option>
            <option value="20">20 bản ghi trên một trang</option>
            <option value="30">30 bản ghi trên một trang</option>
            <option value="40">40 bản ghi trên một trang</option>
          </select>
        </div>
        <div class="direc-page" @click="toPPage()">Trước</div>
        <div
          class="nb-page"
          :class="{ 'hide-more': isFiPage || !(pageIndex > 2) }"
          @click="toFPage()"
        >
          1
        </div>
        <div
          class="nb-page"
          :class="{
            'hide-more': isFiPage || !(pageIndex > 3),
          }"
        >
          ...
        </div>
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
        <div
          class="nb-page"
          :class="{ 'hide-more': isThPage || !(pageIndex < maxPage - 2) }"
        >
          ...
        </div>
        <div
          class="nb-page"
          :class="{ 'hide-more': isThPage || !(pageIndex < maxPage - 1) }"
          @click="toLPage()"
        >
          {{ maxPage }}
        </div>
        <div class="direc-page" @click="toNPage()">Sau</div>
      </div>
    </div>
    <EmployeeDetail
      v-if="isShowDetail == true"
      :employee="employee"
      @hideDetailPageParent="hideDetailPageParent"
      :formMode="formMode"
      @showStatusLog="showStatusLog"
      @statusSuccess="statusSuccess"
      @statusWarning="statusWarning"
      :inputFocus="inputFocus"
    />
    <!-- :showStatusLog="showStatusLog"
      :mesStatus="mesStatus" -->
    <ErrorLog
      v-if="isShowStatusLog == true"
      @hideStatusLog="hideStatusLog"
      :mesStatus="mesStatus"
      :statusLog="statusLog"
    />
  </div>
</template>
<script>
import axios from "axios";
import EmployeeDetail from "./EmployeeDetail";
import MoreDialog from "../dialog/More";
import ErrorLog from "../dialog/ErrorLog";
export default {
  name: "EmployeeList",
  //CreatedBy: TDDUUNG
  //Date : 11/5/2021
  components: {
    EmployeeDetail,
    MoreDialog,
    ErrorLog,
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
    //CreatedBy: TDDUUNG
    //Date : 11/5/2021
    // Hàm tìm kiếm và sắp xếp page
    async getData() {
      var aipUrl =
        "https://localhost:44368/api/v1/Employees/paging?pageIndex=" +
        this.pageIndex +
        "&pageSize=" +
        this.pageSize;
      if (this.inputFilter != String.Empty)
        aipUrl += "&filter=" + this.inputFilter;
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
    //CreatedBy: TDDUUNG
    //Date : 11/5/2021
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

    //CreatedBy: TDDUUNG
    //Date : 11/5/2021
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
    //CreatedBy: TDDUUNG
    //Date : 11/5/2021
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
    toNPage() {
      if (this.pageIndex == 1) {
        this.pageIndex = 2;
        this.pageIndexTemp = 2;
      } else if (this.pageIndex == this.maxPage - 1) {
        this.pageIndex = this.maxPage;
        this.pageIndexTemp = this.maxPage - 1;
      } else if (this.pageIndex > 1 && this.pageIndex < this.maxPage - 1) {
        this.pageIndex++;
        this.pageIndexTemp++;
      }
    },
    toPPage() {
      if (this.pageIndex == 2) {
        this.pageIndex = 1;
        this.pageIndexTemp = 2;
      } else if (this.pageIndex == this.maxPage) {
        this.pageIndex = this.maxPage - 1;
        this.pageIndexTemp = this.maxPage - 1;
      } else if (this.pageIndex > 2 && this.pageIndex < this.maxPage) {
        this.pageIndex--;
        this.pageIndexTemp--;
      }
    },
    //Ham lay so ban ghi lon nhat va hien cua so them
    async showPopupDetailParent() {
      this.formMode = "add";
      this.employee = {};
      this.employee.employeeCode = "";
      var aipUrl = "https://localhost:44368/api/v1/Employees/getMaxCode";
      await axios
        .get(aipUrl)
        .then((res) => {
          var temp = res.data[0].split("-");
          this.employee.employeeCode =
            temp[0] + "-" + (parseInt(temp[1]) + 1).toString();
          this.employee.employeeCode;
          console.log(this.employee.employeeCode);
        })
        .catch((err) => {
          console.log(err);
        });
      this.isShowDetail = true;
      this.inputFocus = true;
    },
    //CreatedBy: TDDUUNG
    //Date : 11/5/2021
    // Ham an cua so dialog
    hideDetailPageParent() {
      this.isShowDetail = false;
      this.inputFocus = false;
      this.getData();
    },
    //Ham lay thong tin nhan vien dua len form sua
    async editClick(employeeId) {
      // Lấy id của bản ghi được chọn

      //Gọi API lấy id thông tin nhân viên
      var apiUrl = "https://localhost:44368/api/v1/Employees/" + employeeId;
      await axios
        .get(apiUrl)
        .then((res) => {
          this.employee = res.data;
          console.log(this.employee);
          this.employee.dateOfBirth = this.employee.dateOfBirth.substring(
            0,
            10
          );
          this.employee.identityDate = this.employee.identityDate.substring(
            0,
            10
          );
          console.log(this.employee);
        })
        .catch((err) => {
          console.log(err);
        });
      //Lấy trạng thái là form sửa
      this.formMode = "edit";
      //Hiển thị dialog
      this.isShowDetail = true;
      this.inputFocus = true;
    },
    //CreatedBy: TDDUUNG
    //Date : 11/5/2021
    //Ham xuat ra file excel
    async exportFile() {
      var apiUrl = "https://localhost:44368/api/v1/Employees/export";
      await axios({
        url: apiUrl,
        method: "GET",
        responseType: "blob",
      }).then((response) => {
        var fileURL = window.URL.createObjectURL(new Blob([response.data]));
        var fileLink = document.createElement("a");

        fileLink.href = fileURL;
        fileLink.setAttribute("download", "data.xlsx");
        document.body.appendChild(fileLink);

        fileLink.click();
      });
    },
    //CreatedBy: TDDUUNG
    //Date : 11/5/2021
    showStatusLog(sttMsg) {
      this.isShowStatusLog = true;
      this.mesStatus = sttMsg;
    },
    statusSuccess() {
      this.statusLog = true;
    },
    statusWarning() {
      this.statusLog = false;
    },
    hideStatusLog() {
      this.isShowStatusLog = false;
    },
  },
  //
  computed: {
    inputFilter: {
      get() {
        return this.debouncedInput;
      },
      set(val) {
        if (this.timeout) clearTimeout(this.timeout);
        this.timeout = setTimeout(() => {
          this.debouncedInput = val;
        }, 500);
      },
    },
  },
  //
  //   Theo dõi biến
  watch: {
    debouncedInput() {
      if (this.debouncedInput != String.Empty) {
        this.searchAndArrangePage();
      }
    },
    employees() {
      if (this.employees.length == 0) {
        this.emptyData = true;
      } else this.emptyData = false;
    },
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
      // inputFilter: "", // Từ khoá cần tìm kiếm
      pageSize: 10, // Số bản ghi yteen 1 trang
      pageIndex: 1, // Vị trí hiện tại cảu page
      pageIndexTemp: 0, //Biến để hiển thị số trang
      maxPage: 0, // Số số trang
      isFiPage: true, // Page 1 là page hiện tại
      isScPage: false, // Page 2 là page hiện tại
      isThPage: false, // Page 3 là page hiện tại
      count: 0, // Biến đếm số bản ghi
      isShowDetail: false, //biến ẩn hay hiện bảng detail
      employee: {}, //Biến chứa thông tin nhân viên hiển thị lên dialog
      formMode: "add", //Biến chứa thông tin thêm hay sửa gửi thị lên dialog
      isShowStatusLog: false, //Bien chua thong tin hien thi cua so loi
      mesStatus: "", ////Bien chua cau thong bao
      statusLog: false, //Biên chứa trạng thái thông báo
      emptyData: true, //Biến theo dõi data
      tranPage: false, //Biến theo dõi có focus vào ô hay ko
      inputFocus: false, //
      debouncedInput: "",
    };
  },
};
</script>

<style scope>
.crrPage {
  background-color: #ffffff;
  color: #38393d;
  border: 1px solid #38393d;
}
.popup_delete {
  position: absolute;
  width: 100px;
  height: 100px;
  background: black;
  z-index: 999;
  top: 179px;
}
.hide-more {
  display: none;
}
.nodata-img {
  position: sticky;
  margin: 50px 50px 30px;
  width: 132px;
  height: 74px;
  left: 45%;
}
.nodata-text {
  position: sticky;
  left: 44%;
  padding-left: 20px;
}
::-webkit-scrollbar {
  width: 5px;
  height: 7px;
}
input[type="checkbox"] {
  flex-shrink: 0;
  width: 18px;
  height: 18px;
  appearance: none;
  -webkit-appearance: none;
  background: white;
  outline: none;
  border: 1px solid #afafaf;
  border-radius: 2px;
  transition: 0.5s;
  position: relative;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
}
input:checked[type="checkbox"] {
  border: 1px solid #2ca01c;
  transform: rotate(90deg);
}

input[type="checkbox"]::before {
  content: "";
  position: absolute;
  width: 16px;
  height: 16px;
  background: url(../../assets/img/Sprites.svg);
  background-position: -1224px -360px;
  display: none;
}

input:checked[type="checkbox"]::before {
  display: block;
  transform: rotate(-90deg);
}
</style>
