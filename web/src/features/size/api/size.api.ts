import { apiClient } from "../../../shared/api/apiClient";
import type { Size } from "../types/size.types";

export const sizeApi = {
    async getAllSizes() {
        const res = await apiClient.get<Size[]>('sizes');
        return res.data;
    }
}