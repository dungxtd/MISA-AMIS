<template>
  <div class="combobox">
    <div class="header-combobox" v-click-outside="clickOutSide">
      <input
        type="text"
        autocomplete="off"
        v-model="option"
        @input="filterOptions()"
        @focus="focusInput"
        @keyup.up="keyup"
        @keyup.down="keydown"
        @keyup.enter="enter"
        @keydown="keyout"
        v-on:blur="checkValue"
        :class="{ 'warning-combox': !isChecked }"
      />
      <div class="icon-combobox" @click="focusInput"></div>
    </div>
    <div v-if="filteredOptions && model" class="main-cbb">
      <ul class="wrap-option">
        <li
          class="option-combobox"
          v-for="(filteredOption, index) in filteredOptions"
          :key="index"
          @click="setOption(filteredOption)"
          :class="{ select: index == indexSelect }"
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
      indexSelect: -1,
      isChecked: true, //
    };
  },
  methods: {
    keyout() {
      this.model = true;
    },
    focusInput() {
      this.model = !this.model;
    },
    clickOutSide() {
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
      this.model = !this.model;
      this.filterOptions();
      this.checkValue();
    },

    keydown() {
      this.model = true;
      if (this.indexSelect < this.filteredOptions.length - 1)
        this.indexSelect++;
      else this.indexSelect = 0;
    },
    keyup() {
      if (this.indexSelect > 0) this.indexSelect--;
    },
    enter() {
      console.log(this.indexSelect);
      if (this.indexSelect >= 0)
        this.option = this.filteredOptions[this.indexSelect];
      this.model = false;
      this.filterOptions();
      this.indexSelect = -1;
    },
    checkValue() {
      this.isChecked = false;
      this.options.forEach((element) => {
        if (this.option == element) this.isChecked = true;
      });
    },
  },
  watch: {},
  mounted() {
    this.filterOptions();
  },
  computed: {},
};
</script>

<style scope>
.icon-combobox {
  width: 32px;
  background-color: transparent;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  background-position: -553px -354px !important;
  border: 1px solid #ebedf0;
  background: url(/img/Sprites.5f05e81f.svg) no-repeat;
}
input {
  border: none;
  font-size: 13px;
  padding: 5px 10px;
  padding-right: unset;
  text-overflow: ellipsis;
  background-color: transparent;
  border: 1px solid #ebedf0;
}
input:focus-visible {
  outline: none;
  border: 1px solid #2ca01c;
}
.warning-combox {
  border: 1px solid red !important;
}
.combobox {
  display: flex;
  flex-direction: column;
  align-items: center;
}
.header-combobox {
  display: flex;
  z-index: 10;
}
.wrap-option {
  width: 204px;
  background-color: rgb(255, 255, 255);
  cursor: pointer;
}
.main-cbb {
  z-index: 10;
}
.select {
  color: #35bf22;
  background-color: #ebedf0;
}
.option-combobox:hover {
  color: #35bf22;
  background-color: #ebedf0;
}
ul li {
  text-align: left !important;
  /* margin: auto 0; */
  padding: 5px 0px 5px 11px;
  list-style: none;
  /* display: inline-block; */
  /* width: 122%; */
  /* height: 94%; */
  /* background-color: #555; */
  /* position: relative; */
  /* top: 42%; */
  transform: translate(-10%, 0%);
  /* text-align: center; */
  border: 1px solid #ebedf0;
}
</style>
