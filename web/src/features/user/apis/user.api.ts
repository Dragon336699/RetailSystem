import { apiClient } from "../../../api/apiClient";
import type { UserDto } from "../types/user.type";

export const userApi = {
  async getAllCustomers() {
    const res = await apiClient.get<UserDto[]>("users/admin/customers");
    return res.data;
  }
}