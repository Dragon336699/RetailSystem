import { apiClient } from "../../../shared/api/apiClient";
import type { UserDto } from "../types/user.type";

export const userApi = {
  async getAllCustomers() {
    const res = await apiClient.get<UserDto[]>("users/admin/customers", {
      withCredentials: true,
    });
    return res.data;
  },
  async getAdminInfo() {
    const res = await apiClient.get<UserDto>("users/admin/info", {
      withCredentials: true,
    });
    return res.data;
  },
};
