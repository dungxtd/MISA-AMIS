<template>
  <div class="combobox" v-click-outside="clickOutSide">
    <div class="header-combobox">
      <input
        type="text"
        autocomplete="off"
        v-model="option"
        @input="filterOptions()"
        @focus="focusInput"
      />
      <button @click="model = !model">button</button>
    </div>
    <div v-if="filteredOptions && model" class="main-cbb">
      <ul class="wrap-option">
        <li
          class="option-combobox"
          v-for="(filteredOption, index) in filteredOptions"
          :key="index"
          @click="setOption(filteredOption)"
        >
          {{ filteredOption }}
        </li>
      </ul>
    </div>
  </div>
</template>
<script>
import Vue from "vue";
import vClickOutside from "v-click-outside";
Vue.use(vClickOutside);
export default {
  data() {
    return {
      options: ["Phòng Nhân sự", "Phòng Marketing", "Phòng Đào tạo"],
      option: "",
      model: false,
      filteredOptions: [],
    };
  },
  methods: {
    focusInput() {
      this.model = true;
    },

    clickOutSide() {
      console.log(1);
      this.model = false;
    },
    filterOptions() {
      if (this.option.length == 0) {
        this.filteredOptions = this.options;
      }

      this.filteredOptions = this.options.filter((options) => {
        return options.toLowerCase().startsWith(this.option.toLowerCase());
      });
    },
    setOption(option) {
      this.option = option;
      this.model = false;
    },
  },
  watch: {},
  mounted() {
    this.filterOptions();
  },
};
</script>

<style scope>
.combobox {
  display: flex;
  flex-direction: column;
  align-items: center;
}
.header-combobox {
  display: inline;
  z-index: 10;
}
.wrap-option {
  width: 183px;
  background-color: rgb(255, 255, 255);
  cursor: pointer;
}
.main-cbb {
  z-index: 10;
}
.option-combobox:hover {
  background-color: #1cb124;
}
ul li {
  margin: 0;
  padding: 0;
  list-style: none;
  /* display: inline-block; */
  width: 122%;
  height: 94%;
  /* background-color: #555; */
  position: relative;
  /* top: 42%; */
  transform: translate(-18%, 0%);
  text-align: center;
  height: 100%;
  border: 1px solid;
}
</style>
