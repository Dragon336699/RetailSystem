import { apiClient } from "../../../api/apiClient";
import type { Color } from "../types/color.types";

export const colorApi = {
    async getAllColors() {
        const res = await apiClient.get<Color[]>('colors');
        return res.data;
    }
}