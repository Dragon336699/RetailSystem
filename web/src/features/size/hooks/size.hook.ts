import { useQuery } from "@tanstack/react-query"
import { sizeApi } from "../api/size.api"

export const useSizes = () => {
    return useQuery({
        queryKey: ['sizes'],
        queryFn: sizeApi.getAllSizes
    })
}