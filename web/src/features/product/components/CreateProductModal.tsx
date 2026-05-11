import { GenericModal } from "../../../components/Modal/GenericModal";
import type { Category } from "../../category/types/category.types";
import type { Size } from "../../size/types/size.types";
import type { CreateProductRequest } from "../types/product";
import CreateProductForm from "./CreateProductForm";

type Props = {
  open: boolean;
  onClose: () => void;
  onCreate: (value: CreateProductRequest) => void;
  categories: Category[];
  sizes: Size[];
};

export default function CreateProductModal({ open, onClose, onCreate, categories, sizes }: Props) {
  return (
    <GenericModal open={open} onClose={onClose} footer={null}>
      <CreateProductForm categories={categories} sizes={sizes} onSubmit={onCreate}></CreateProductForm>
    </GenericModal>
  );
}
