import { apiClient } from "../../../api/apiClient";
import type { Category } from "../types/category.types";

export const categoryApi = {
    async getAllCategories() {
        const res = await apiClient.get<Category[]>('categories');
        return res.data;
    }
}