import { GenericModal } from "../../../components/Modal/GenericModal";
import type { Category, UpdateCategoryRequest } from "../types/category.types";
import CategoryForm from "./CategoryForm";

type Props = {
  open: boolean;
  onClose: () => void;
  onUpdate: (value: UpdateCategoryRequest) => void;
  category: Category | null;
};

export default function UpdateCategoryModal({ open, onClose, onUpdate, category }: Props) {

  return (
    <GenericModal open={open} onClose={onClose} footer={null}>
      <CategoryForm<UpdateCategoryRequest>
        categoryData={category}
        onSubmit={onUpdate}
      ></CategoryForm>
    </GenericModal>
  );
}
