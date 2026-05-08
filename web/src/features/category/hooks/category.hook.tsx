import { useQuery } from "@tanstack/react-query";
import { categoryApi } from "../apis/category.api";

export const useCategories = () => {
  return useQuery({
    queryKey: ["categories"],
    queryFn: categoryApi.getAllCategories,
  });
};