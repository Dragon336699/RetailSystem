import { apiClient } from "../../../api/apiClient";
import type { Category, CreateCategoryRequest, UpdateCategoryRequest } from "../types/category.types";

export const categoryApi = {
  async getAllCategories() {
    const res = await apiClient.get<Category[]>("categories");
    return res.data;
  },

  async createCategory(categoryData: CreateCategoryRequest){
    const res = await apiClient.post<Category>("categories", categoryData);
    return res.data;
  },

  async updateCategory(categoryData: UpdateCategoryRequest) {
    const res = await apiClient.put<Category>("categories", categoryData);
    return res.data;
  },

  async deleteCategory(categoryId: string) {
    const res = await apiClient.delete(`categories/${categoryId}`);
    return res.data;
  },
};
