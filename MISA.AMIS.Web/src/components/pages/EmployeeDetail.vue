<template>
  <div id="EmployeeDetail" class="dialog" :class="{ 'dialog-hide': !isShow }">
    <div class="detail" title="Thông tin nhân viên">
      <div class="dialog-model"></div>
      <div class="dialog-content">
        <div class="dialog-header">
          <div class="left-header">
            Thông tin nhân viên
            <div class="customer">
              <input type="checkbox" />
              <div class="tit">Là khách hàng</div>
            </div>
            <div class="provider">
              <input type="checkbox" />
              <div class="tit">Là nhà cung cấp</div>
            </div>
          </div>
          <div class="right-header">
            <div class="ic ic-max icon-help"></div>
            <div class="ic ic-max icon-cancel" @click="hideDetailPage()"></div>
          </div>
        </div>
        <div class="dialog-body">
          <div class="flex first-inf">
            <div class="flex-1 left-f-inf">
              <div class="flex">
                <div class="flex flex-1">
                  <div>
                    <div class="label">Mã</div>
                    <input type="text" v-model="employee.employeeCode" />
                  </div>
                  <div class="flex-1">
                    <div class="label">Tên</div>
                    <input
                      type="text"
                      style="width: 83%"
                      v-model="employee.employeeName"
                    />
                  </div>
                </div>
                <div class="flex flex-1">
                  <div class="mgr10 flex-1">
                    <div class="label">
                      Ngày sinh
                    </div>
                    <input type="date" v-model="employee.dateOfBirth" />
                  </div>
                  <div class="flex-1">
                    <div class="label">Giới tính</div>
                    <div class="flex options">
                      <input type="radio" id="male" name="gender" value="0" />
                      <label for="male">Nam</label>
                      <input type="radio" id="female" name="gender" value="1" />
                      <label for="female">Nữ</label>
                      <input type="radio" id="other" name="gender" value="2" />
                      <label for="other">Khác</label>
                    </div>
                  </div>
                </div>
              </div>
              <div class="flex">
                <div class="flex-1">
                  <div class="label">Đơn vị</div>
                  <select type="text" />
                </div>
                <div class="flex flex-1">
                  <div class="mgr10 flex-1">
                    <div class="label">Số CMND</div>
                    <input type="text" v-model="employee.identityNumber" />
                  </div>
                  <div class="flex-1">
                    <div class="label">Ngày cấp</div>
                    <input type="date" v-model="employee.identityDate" />
                  </div>
                </div>
              </div>
              <div class="flex">
                <div class="flex-1">
                  <div class="label">Chức danh</div>
                  <input type="text" v-model="employee.employeePosition" />
                </div>
                <div class="flex-1 ">
                  <div class="label">Nơi cấp</div>
                  <input
                    type="text"
                    class="width95"
                    v-model="employee.identityPlace"
                  />
                </div>
              </div>
            </div>
          </div>
          <div class="second-inf">
            <div>
              <div class="label">Địa chỉ</div>
              <input type="text" class="width97" v-model="employee.address" />
            </div>
            <div class="flex">
              <div class="mgr10">
                <div class="label">ĐT di động</div>
                <input type="text" v-model="employee.phoneNumber" />
              </div>
              <div class="mgr10">
                <div class="label">ĐT cố định</div>
                <input type="text" v-model="employee.telephoneNumber" />
              </div>
              <div class="mgr10">
                <div class="label">Email</div>
                <input type="text" v-model="employee.email" />
              </div>
            </div>
            <div class="flex">
              <div class="mgr10">
                <div class="label">Tài khoản ngân hàng</div>
                <input type="text" v-model="employee.bankAccountNumber" />
              </div>
              <div class="mgr10">
                <div class="label">Tên ngân hàng</div>
                <input type="text" v-model="employee.bankName" />
              </div>
              <div class="mgr10">
                <div class="label">Chi nhánh</div>
                <input type="text" v-model="employee.bankBranchName" />
              </div>
            </div>
          </div>
        </div>
        <div class="dialog-footer"></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    isShow: { type: Boolean, default: false },
    employee: { type: Object, default: Object.create(null) },
    formModel: { type: Boolean, default: true },
  },
  methods: {
    hideDetailPage() {
      this.$emit("hideDetailPageParent");
    },
  },
};
</script>

<style scoped>
input[type="text"],
select[type="text"],
input[type="date"] {
  height: 24px;
  width: 90%;
  padding: 6px 6px;
  outline: none;
  font-size: 15px;
}
input[type="radio"] {
  margin: 5px;
}
label {
  margin-bottom: 0;
}
.dialog-hide {
  display: none;
}
</style>
