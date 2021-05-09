<template>
  <div>
    <EmployeeList :key="componentKey" @reloadComponent="reloadComponent" />
  </div>
</template>

<script>
import EmployeeList from "../pages/EmployeeList";
export default {
  name: "Content",
  data() {
    return {
      componentKey: 0,
    };
  },
  components: {
    EmployeeList,
  },
  methods: {
    reloadComponent() {
      this.componentKey++;
    },
  },
};
</script>
<style></style>
