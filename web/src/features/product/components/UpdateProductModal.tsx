import { GenericModal } from "../../../shared/ui/Modal/GenericModal";
import type { Category } from "../../category/types/category.types";
import type { Size } from "../../size/types/size.types";
import type { Product, UpdateProductRequest } from "../types/product";
import UpdateProductForm from "./UpdateProductForm";

type Props = {
  open: boolean;
  onClose: () => void;
  onCreate: (value: UpdateProductRequest) => void;
  product: Product;
  categories: Category[];
  sizes: Size[];
};

export default function UpdateProductModal({ open, onClose, onCreate, product, categories, sizes }: Props) {
  return (
    <GenericModal open={open} onClose={onClose} footer={null} destroyClose={true}>
      <UpdateProductForm product={product} categories={categories} sizes={sizes} onSubmit={onCreate}></UpdateProductForm>
    </GenericModal>
  );
}
