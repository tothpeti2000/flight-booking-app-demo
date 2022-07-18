import { useAPI } from "@/api/useAPI";
import type { LoginFormData } from "@/interfaces/loginFormData";
import type { RegistrationFormData } from "@/interfaces/registrationFormData";
import type { TokenResponse } from "@/interfaces/tokenResponse";
import router from "@/router";
import useToken from "./useToken";

const useUser = () => {
  const { deleteToken } = useToken();

  const register = (data: RegistrationFormData) => {
    return useAPI("/auth/register").post(data).json();
  };

  const login = (data: LoginFormData) => {
    return useAPI("/auth/login").post(data).json<TokenResponse>();
  };

  const logout = () => {
    deleteToken();
    router.push("/");
  };

  return {
    register,
    login,
    logout,
  };
};

export default useUser;
