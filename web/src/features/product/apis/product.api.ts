import { apiClient } from "../../../api/apiClient";
import type { Product } from "../types/product";

export const productApi = {
  async getAllProducts(skip?: number, take?: number) {
    const res = await apiClient.get<Product[]>("products", {
      params: { skip, take },
    });
    return res.data;
  },
};
