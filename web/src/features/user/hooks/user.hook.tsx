import { useQuery } from "@tanstack/react-query"
import { userApi } from "../apis/user.api"

export const useCustomers = () => {
    return useQuery({
        queryKey: ['customers'],
        queryFn: userApi.getAllCustomers
    })
}