import useToken from "@/api/useToken";
import { createRouter, createWebHistory } from "vue-router";

import HomePage from "../pages/HomePage.vue";
const RegistrationPage = () => import("../pages/auth/RegistrationPage.vue");
const LoginPage = () => import("../pages/auth/LoginPage.vue");

const BookingProgress = () => import("../pages/booking/BookingProgress.vue");
const Start = () => import("../pages/booking/steps/Start.vue");
const Flights = () => import("../pages/booking/steps/Flights.vue");
const Passengers = () => import("../pages/booking/steps/Passengers.vue");
const Seats = () => import("../pages/booking/steps/Seats.vue");
const Confirmation = () => import("../pages/booking/steps/Confirmation.vue");

const NotFound = () => import("../pages/NotFound.vue");

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "home",
      component: HomePage,
    },
    {
      path: "/register",
      name: "register",
      component: RegistrationPage,
    },
    {
      path: "/login",
      name: "login",
      component: LoginPage,
    },
    {
      path: "/booking",
      name: "booking",
      component: BookingProgress,
      redirect: "/booking/start",
      meta: {
        requireAuth: true,
      },
      children: [
        {
          path: "start",
          name: "bookingStart",
          component: Start,
        },
        {
          path: "flights",
          name: "bookingFlights",
          component: Flights,
        },
        {
          path: "passengers",
          name: "passengers",
          component: Passengers,
        },
        {
          path: "seats",
          name: "seats",
          component: Seats,
        },
        {
          path: "confirm",
          name: "confirmation",
          component: Confirmation,
        },
      ],
    },
    {
      path: "/:pathMatch(.*)*/",
      name: "notFound",
      component: NotFound,
    },
  ],
});

router.beforeEach((to, from) => {
  const { token } = useToken();

  if (to.meta.requireAuth && !token.value) {
    return { path: "/login" };
  }

  if ((to.name === "login" || to.name === "register") && token.value) {
    return from.fullPath;
  }
});

export default router;
