import type { Discount } from "@/interfaces/home/discount";
import type { OrderData } from "@/interfaces/home/orders";
import type { Partner } from "@/interfaces/home/partner";
import { useAPI } from "./useAPI";

export const getDiscounts = () => {
  return useAPI("/discount/offers").get().json<Discount[]>();
};

export const getPartners = () => {
  return useAPI("/partner").get().json<Partner[]>();
};

export const getOrders = () => {
  return useAPI("/ticketordering", {
    afterFetch(ctx) {
      ctx.data.orders = ctx.data.orders.map(postProcessOrderResponse);
      return ctx;
    },
  })
    .get()
    .json<OrderData>();
};

function postProcessOrderResponse(order: any) {
  const returnFlight = order.returnFlight
    ? {
        ...order.returnFlight,
        departure: new Date(order.returnFlight.departure),
      }
    : null;

  return {
    ...order,
    toFlight: {
      ...order.toFlight,
      departure: new Date(order.toFlight.departure),
    },
    returnFlight,
  };
}
