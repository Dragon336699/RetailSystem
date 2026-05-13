import { useQuery } from "@tanstack/react-query"
import { userApi } from "../../features/user/apis/user.api"

export const useUsers = () => {
    return useQuery({
        queryKey: ['adminInfo'],
        queryFn: userApi.getAdminInfo
    })
}