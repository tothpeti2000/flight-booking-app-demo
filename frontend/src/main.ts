import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";

import PrimeVue from "primevue/config";
import "primevue/resources/themes/saga-blue/theme.css";
import "primevue/resources/primevue.min.css";
import "primeicons/primeicons.css";
import "primeflex/primeflex.css";

import Toast, { POSITION, type PluginOptions } from "vue-toastification";
import "vue-toastification/dist/index.css";

const app = createApp(App);

app.use(router);
app.use(PrimeVue, { ripple: true });

const toastOptions: PluginOptions = {
  position: POSITION.BOTTOM_CENTER,
  timeout: 3000,
  transition: "Vue-Toastification__fade",
};

app.use(Toast, toastOptions);

app.mount("#app");
