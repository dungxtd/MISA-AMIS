<template>
  <div id="app">
    <Navbar />
    <div class="main">
      <Header />
      <Content />
    </div>
  </div>
</template>

<script>
import Navbar from "./components/Layout/Navbar";
import Header from "./components/Layout/Header";
import Content from "./components/Layout/Content";

export default {
  name: "App",
  components: {
    Navbar,
    Header,
    Content,
  },
};
</script>
<style>
@import "./styles/index.css";
</style>
