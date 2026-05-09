import { useQuery } from "@tanstack/react-query"
import { productApi } from "../apis/product.api"

export const useProducts = (page: number, pageSize: number) => {
    const skip = (page - 1) * pageSize;
    const take = pageSize;
    
    return useQuery({
        queryKey: ['products', page, pageSize],
        queryFn: () => productApi.getAllProducts(skip, take)
    })
}