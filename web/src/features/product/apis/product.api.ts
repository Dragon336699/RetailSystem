import type { UploadFile } from "antd";
import { apiClient } from "../../../api/apiClient";
import type {
  CreateProductRequest,
  Product,
  UpdateProductRequest,
} from "../types/product";

export const productApi = {
  async getAllProducts(skip?: number, take?: number) {
    const res = await apiClient.get<Product[]>("products", {
      params: { skip, take },
    });
    return res.data;
  },

  async createProduct(value: CreateProductRequest) {
    const formData = new FormData();

    formData.append("ProductName", value.productName);
    formData.append("Description", value.description ?? "");
    formData.append("Price", String(value.price));

    value.categoryIds.forEach((id) => {
      formData.append("CategoryIds", id);
    });

    value.sizesQuantity.forEach((x, i) => {
      formData.append(`SizesQuantity[${i}].SizeId`, x.sizeId);
      formData.append(`SizesQuantity[${i}].Quantity`, String(x.quantity));
    });

    value.productImages.forEach((file: UploadFile) => {
      const rawFile = file.originFileObj;

      if (rawFile) {
        formData.append("ProductImages", rawFile);
      }
    });

    const res = await apiClient.post("products", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
    return res.data;
  },

  async updateProduct(value: UpdateProductRequest) {
    const formData = new FormData();

    formData.append("Id", value.id);
    formData.append("ProductName", value.productName);
    formData.append("Description", value.description ?? "");
    formData.append("Price", String(value.price));
    if (value.thumbnailIndex !== undefined) {
      formData.append("ThumbnailIndex", String(value.thumbnailIndex));
    }
    if (value.thumbnailImageId) {
      formData.append("ThumbnailImageId", value.thumbnailImageId);
    }

    value.categoryIds.forEach((id) => {
      formData.append("CategoryIds", id);
    });

    value.sizesQuantity.forEach((x, i) => {
      if (x.id) {
        formData.append(`SizesQuantity[${i}].Id`, x.id);
      }
      formData.append(`SizesQuantity[${i}].SizeId`, x.sizeId);
      formData.append(`SizesQuantity[${i}].Quantity`, String(x.quantity));
    });

    value.removeImageIds.forEach((id) => {
      formData.append("RemoveImageIds", id);
    });
    if (value.productImages) {
      value.productImages.forEach((file: UploadFile) => {
        const rawFile = file.originFileObj;

        if (rawFile) {
          formData.append("ProductImages", rawFile);
        }
      });
    }

    const res = await apiClient.put<Product>("products", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
    return res.data;
  },

  async deleteProduct(productId: string) {
    const res = await apiClient.delete(`products/${productId}`);
    return res.data;
  },
};
