import type { UploadFile } from "antd";
import type { Category } from "../../category/types/category.types";

export interface Product {
  id: string;
  productName: string;
  description: string;
  price: number;
  createdAt: Date;
  updatedAt: Date;
  categories: Category[];
  productImages: ProductImage[];
  productVariants: ProductVariant[];
}

export interface ProductImage {
  id: string;
  imageUrl: string;
  isThumbnail: boolean;
}

export interface ProductVariant {
  id: string;
  productId: string;
  sizeId: string;
  stockQuantity: number;
}

export interface UploadProductSizeDto {
    id?: string;
    sizeId: string;
    quantity: number
}

export interface CreateProductRequest {
  productName: string;
  price: number;
  description: string | null;
  categoryIds: string[];
  productImages: UploadFile[];
  thumbnailIndex: number;
  sizesQuantity: UploadProductSizeDto[];
}

export interface UpdateProductRequest {
  id: string;
  productName: string;
  price: number;
  description: string | null;
  categoryIds: string[];
  removeImageIds: string[];
  productImages?: UploadFile[];
  thumbnailIndex?: number;
  thumbnailImageId?: string;
  sizesQuantity: UploadProductSizeDto[];
}