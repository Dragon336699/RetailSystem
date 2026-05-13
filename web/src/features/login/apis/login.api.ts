import { apiClient } from "../../../shared/api/apiClient";
import type { LoginRequest } from "../types/login.type";

export const loginApi = {
  async login(data: LoginRequest) {
    await apiClient.post("users/login", data, { withCredentials: true });
  },
  async logout() {
    await apiClient.post("users/logout", { withCredentials: true });
  }
};