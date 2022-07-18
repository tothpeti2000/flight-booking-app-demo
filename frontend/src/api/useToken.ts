import { useTokenStore } from "@/store/store";

const useToken = () => {
  const token = useTokenStore();

  const saveToken = (newToken: string) => {
    token.value = newToken;
  };

  const deleteToken = () => {
    token.value = null;
  };

  return {
    token,
    saveToken,
    deleteToken,
  };
};

export default useToken;
