import useFeedback from "@/components/useFeedback";
import { createFetch } from "@vueuse/core";
import useToken from "./useToken";

const { token } = useToken();
const { showError } = useFeedback();

export const useAPI = createFetch({
  baseUrl: import.meta.env.VITE_API_URL,
  options: {
    beforeFetch({ options }) {
      (options.headers as any).Authorization = `Bearer ${token.value}`;

      return { options };
    },
    immediate: false,
  },
  fetchOptions: {
    mode: "cors",
  },
});

export const handleResponse = (
  statusCode: number | null,
  data: any,
  handleSuccess?: () => void
) => {
  if (!statusCode) {
    showError("An error occurred while communicating with the server");
    return;
  }

  if (statusCode >= 400 && data?.detail) {
    showError(data.detail);
    return;
  }

  handleSuccess?.();
};
