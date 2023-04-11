import { useAuthStore } from "@/stores/auth";

const authInterceptor = (error) => {
  if (error.response && error.response.status === 401) {
    const authStore = useAuthStore();
    authStore.logout()
  }
  return Promise.reject(error);
};

export default authInterceptor;
