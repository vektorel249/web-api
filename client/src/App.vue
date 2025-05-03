<template>
  <div>
    <a href="https://vite.dev" target="_blank">
      <img src="/vite.svg" class="logo" alt="Vite logo" />
    </a>
    <a href="https://vuejs.org/" target="_blank">
      <img src="./assets/vue.svg" class="logo vue" alt="Vue logo" />
    </a>

    <div>
      <h3>Kategoriler</h3>
      <button @click="load">Kategorileri Getir</button>
      <ul>
        <li v-for="c in categories" :key="c.categoryId">
          {{ c.categoryName }}
        </li>
      </ul>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      categories: [],
      hasError: false
    }
  },
  methods: {
    load() {
      this.hasError = false;
      const myHeaders = new Headers();
      myHeaders.append("Content-Type", "application/json");
      myHeaders.append("va-code", "123");

      const requestOptions = {
        method: "GET",
        headers: myHeaders,
      };
      // PROMISE
      fetch("https://localhost:12001/api/categories", requestOptions)
        .then((response) => response.json())
        .then((result) => this.categories = result)
        .catch((error) => this.hasError = true);
    }
  }
}
</script>
<style scoped>
.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
  transition: filter 300ms;
}

.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}

.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style>
