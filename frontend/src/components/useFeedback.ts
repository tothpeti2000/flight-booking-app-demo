import { useToast } from "vue-toastification";

const useFeedback = () => {
  const toast = useToast();

  const showSuccess = (message: string) => toast.success(message);
  const showError = (message: string) => toast.error(message);

  return {
    showSuccess,
    showError,
  };
};

export default useFeedback;
