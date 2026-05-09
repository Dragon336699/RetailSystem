import { useState } from "react";
import CategoryList from "../../features/category/components/CategoryList";
import CreateCategoryModal from "../../features/category/components/CreateCategoryModal";
import { useCategories } from "../../features/category/hooks/category.hook";
import { categoryApi } from "../../features/category/apis/category.api";
import { useQueryClient } from "@tanstack/react-query";
import type {
  Category,
  CreateCategoryRequest,
  UpdateCategoryRequest,
} from "../../features/category/types/category.types";
import UpdateCategoryModal from "../../features/category/components/UpdateCategoryModal";

export default function Category() {
  const { data: categories } = useCategories();
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
  const [isUpdateModalOpen, setIsUpdateModalOpen] = useState(false);
  const [categoryUpdate, setCategoryUpdate] = useState<Category | null>(null);
  const queryClient = useQueryClient();

  const onOpenCreateModal = () => {
    setIsCreateModalOpen(true);
  };

  const onOpenUpdateModal = (value: Category) => {
    setIsUpdateModalOpen(true);
    setCategoryUpdate(value);
  };

  const handleDeleteCategory = async (categoryId: string) => {
    try {
      await categoryApi.deleteCategory(categoryId);

      queryClient.setQueryData<Category[]>(["categories"], (old) =>
        old?.filter((c) => c.id !== categoryId),
      );
    } catch (error: unknown) {
      if (error instanceof Error) {
        console.log(error.message);
      }
    }
  };

  const handleUpdateCategory = async (value: UpdateCategoryRequest) => {
    try {
      const updatedCategory = await categoryApi.updateCategory(value);
      setIsUpdateModalOpen(false);

      queryClient.setQueryData<Category[]>(["categories"], (old) =>
        old?.map((c) =>
          c.id === updatedCategory.id
            ? {
                ...c,
                categoryName: updatedCategory.categoryName,
                description: updatedCategory.description,
              }
            : c,
        ),
      );
    } catch (error: unknown) {
      if (error instanceof Error) {
        console.log(error.message);
      }
    }
  };

  const handleCreateCategory = async (value: CreateCategoryRequest) => {
    try {
      const newCategory =  await categoryApi.createCategory(value);
      setIsCreateModalOpen(false);

      queryClient.setQueryData<Category[]>(["categories"], (old) => {
        if (!old) return [newCategory];
        return [...old, newCategory]
      }
      
      );
    } catch (error: unknown) {
      if (error instanceof Error) {
        console.log(error.message);
      }
    }
  }; 

  return (
    <>
      <div className="flex justify-between items-center mb-4">
        <div>
          <h2 className="text-left">Categories</h2>
          <p className="text-left normal-text">Manage product categories</p>
        </div>
      </div>
      <br />
      <CategoryList
        onOpenCreateModal={onOpenCreateModal}
        onOpenUpdateModal={onOpenUpdateModal}
        categories={categories ?? []}
        onDelete={handleDeleteCategory}
      />

      <CreateCategoryModal
        open={isCreateModalOpen}
        onClose={() => setIsCreateModalOpen(false)}
        onCreate={handleCreateCategory}
      />

      <UpdateCategoryModal
        category={categoryUpdate}
        open={isUpdateModalOpen}
        onClose={() => setIsUpdateModalOpen(false)}
        onUpdate={handleUpdateCategory}
      />
    </>
  );
}
