import Vue from 'vue'
import App from './App.vue'
import moment from 'moment';
import vuetify from './plugins/vuetify'

Vue.config.productionTip = false
Vue.filter("formatDate", function (value) {
  if (value) {
    return moment(String(value)).format("MM/DD/YYYY");
  }
});
Vue.filter("formatIndexPagePre", function (value) {
  if (value) {
    value = value - 1;
    return value;
  }
});
Vue.filter("formatIndexPageNext", function (value) {
  if (value) {
    value = value + 1;
    return value;
  }
});
Vue.filter("formatGender", function (value) {
  if (value == 0) {
    return "Nam";
  }
  if (value == 1) {
    return "Nữ";
  }
  if (value == 2) {
    return "Khác";
  }
  else {
    console.log("sai định dạng giới tính");
    return null;
  }
});
Vue.directive('focus', {
  // Khi phần tử liên quan được thêm vào DOM...
  inserted: function (el) {
    // Ta gán focus vào phần tử đó
    el.focus()
  }
})
new Vue({
  vuetify,
  render: h => h(App)
}).$mount('#app')
