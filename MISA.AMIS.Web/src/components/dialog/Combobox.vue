<template>
  <div>
    <div class="control-section">
      <div
        id="content"
        style="margin: 0px auto; width:300px; padding-top: 40px;"
      >
        <ejs-combobox
          id="country"
          ref="countryObj"
          :popupHeight="height"
          :filtering="onFiltering"
          :allowFiltering="allowFiltering"
          :noRecordsTemplate="nTemplate"
          :fields="fields"
          :dataSource="data"
          :placeholder="waterMark"
        ></ejs-combobox>
      </div>
    </div>
  </div>
</template>
<script>
import Vue from "vue";
import { ComboBoxPlugin } from "@syncfusion/ej2-vue-dropdowns";
import { ButtonPlugin } from "@syncfusion/ej2-vue-buttons";
import { Query } from "@syncfusion/ej2-data";
import * as data from "./dataSource.json";

Vue.use(ComboBoxPlugin);
Vue.use(ButtonPlugin);

var noTemplateVue = Vue.component("noRecordsTemplate", {
  template:
    '<div id="nodata"> No matched item, do you want to add it as new item in list? <ejs-button v-on:click.native="onclick">Add New Item</ejs-button></div>',
  data() {
    return {
      data: {},
      customValue: "",
      newItem: "",
    };
  },
  methods: {
    onclick: function() {
      // get the typed characters
      var x = document.getElementById("country").ej2_instances[0];
      this.customValue = x.element.value;
      // make new object based on typed characters
      this.newItem = { Name: this.customValue, Code: this.customValue };
      // new object added to data source.
      x.dataSource.push(this.newItem);
      // close the popup element.
      x.hidePopup();
      // pass new object to addItem method.
      x.addItem(this.newItem);
      // select the newly added item.
      x.value = this.customValue;
    },
  },
});
import func from "../../vue-temp/vue-editor-bridge";
export default Vue.extend({
  data: function() {
    return {
      data: data[(1, 2, 3, 4, 5, 6)],
      fields: { text: "Name", value: "Code" },
      height: "220px",
      waterMark: "Select a country",
      query: "",
      nTemplate: function(e) {
        return {
          template: noTemplateVue,
        };
      },
      allowFiltering: true,
    };
  },
  methods: {
    onFiltering: function(e) {
      this.query = new Query();
      // frame the query based on search string with filter type.
      this.query =
        e.text !== ""
          ? this.query.where("Name", "startswith", e.text, true)
          : this.query;
      // pass the filter data source, filter query to updateData method.
      e.updateData(this.data, this.query);
    },
  },
});
</script>
