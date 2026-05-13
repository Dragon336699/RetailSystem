import { GenericModal } from "../../../shared/ui/Modal/GenericModal";
import type { CreateCategoryRequest } from "../types/category.types";
import CategoryForm from "./CategoryForm";

type Props = {
  open: boolean;
  onClose: () => void;
  onCreate: (value: CreateCategoryRequest) => void;
};

export default function CreateCategoryModal({ open, onClose, onCreate }: Props) {

  return (
    <GenericModal open={open} onClose={onClose} footer={null}>
      <CategoryForm
        categoryData={null}
        onSubmit={onCreate}
      ></CategoryForm>
    </GenericModal>
  );
}
