import { useQuery } from "@tanstack/react-query"
import { colorApi } from "../apis/color.api"

export const useColors = () => {
    return useQuery({
        queryKey: ['colors'],
        queryFn: colorApi.getAllColors
    })
}