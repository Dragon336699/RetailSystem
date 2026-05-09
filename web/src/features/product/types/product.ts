import type { Category } from "../../category/types/category.types";

export interface Product {
    id: string;
    productName: string;
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
    colorId: string;
    sizeId: string;
    stockQuantity: number;
}