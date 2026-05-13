import { PlusOutlined } from "@ant-design/icons";
import { Button, message } from "antd";
import { useState } from "react";
import ProductList from "../../features/product/components/ProductList";
import { useProducts } from "../../features/product/hooks/product.hook";
import CreateProductModal from "../../features/product/components/CreateProductModal";
import { useCategories } from "../../features/category/hooks/category.hook";
import type {
  CreateProductRequest,
  Product,
  UpdateProductRequest,
} from "../../features/product/types/product";
import { productApi } from "../../features/product/apis/product.api";
import { useSizes } from "../../features/size/hooks/size.hook";
import { useQueryClient } from "@tanstack/react-query";
import UpdateProductModal from "../../features/product/components/UpdateProductModal";
import axios from "axios";

export default function ProductPage() {
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
  const [isUpdateModalOpen, setIsUpdateModalOpen] = useState(false);

  const [productNeedUpdate, setProductNeedUpdate] = useState<Product>();

  const { data: products } = useProducts(page, pageSize);
  const { data: categories } = useCategories();
  const { data: sizes } = useSizes();
  const queryClient = useQueryClient();

  const changePage = (page: number, pageSize: number) => {
    setPage(page);
    setPageSize(pageSize);
  };

  const handleOpenUpdateProductModal = (product: Product) => {
    setProductNeedUpdate(product);
    setIsUpdateModalOpen(true);
  };

  const handleCreateProduct = async (value: CreateProductRequest) => {
    try {
      const newProduct = await productApi.createProduct(value);
      setIsCreateModalOpen(false);

      queryClient.setQueryData<Product[]>(["products", page, pageSize], (old) => {
        if (!old) return [newProduct];
        return [...old, newProduct];
      });
    } catch (error: unknown) {
      if (axios.isAxiosError(error)) {
        const msg = error.response?.data?.message;
        message.error(msg || "Something went wrong");
        return;
      }

      message.error("Fail, please try again!");
    }
  };

  const handleUpdateProduct = async (value: UpdateProductRequest) => {
    try {
      const updatedProduct = await productApi.updateProduct(value);
      setIsUpdateModalOpen(false);

      queryClient.setQueryData<Product[]>(["products", page, pageSize], (old) => {
        if (!old) return;

        return old.map((p) =>
          p.id === value.id
            ? {
              ...p,
              productName: updatedProduct.productName,
              categories: updatedProduct.categories,
              price: updatedProduct.price,
              createdAt: updatedProduct.createdAt,
              updatedAt: updatedProduct.updatedAt,
              productImages: updatedProduct.productImages,
              productVariants: updatedProduct.productVariants,
            }
            : p,
        );
      });
    } catch (error: unknown) {
      if (axios.isAxiosError(error)) {
        const msg = error.response?.data?.message;
        message.error(msg || "Something went wrong");
        return;
      }

      message.error("Fail, please try again!");
    }
  };

  const handleDeleteProduct = async (productId: string) => {
    try {
      await productApi.deleteProduct(productId);

      queryClient.setQueryData<Product[]>(["products"], (old) =>
        old?.filter((p) => p.id !== productId)
      );
    } catch (error: unknown) {
      if (axios.isAxiosError(error)) {
        const msg = error.response?.data?.message;
        message.error(msg || "Something went wrong");
        return;
      }

      message.error("Fail, please try again!");
    }
  };

  return (
    <>
      <div className="product-header flex justify-between items-center mb-4">
        <div>
          <h2 className="text-left">Products</h2>
          <p className="text-left normal-text">
            Manage and track your luxury footwear inventory
          </p>
        </div>

        <Button onClick={() => setIsCreateModalOpen(true)}>
          <PlusOutlined /> Add Product
        </Button>
      </div>
      <br />
      <ProductList
        products={products ?? []}
        page={page}
        pageSize={pageSize}
        changePage={changePage}
        openUpdateProduct={handleOpenUpdateProductModal}
        onDeleteProduct={handleDeleteProduct}
      />

      <CreateProductModal
        open={isCreateModalOpen}
        onClose={() => setIsCreateModalOpen(false)}
        onCreate={handleCreateProduct}
        categories={categories ?? []}
        sizes={sizes ?? []}
      />

      <UpdateProductModal
        open={isUpdateModalOpen}
        onClose={() => setIsUpdateModalOpen(false)}
        onCreate={handleUpdateProduct}
        product={productNeedUpdate as Product}
        categories={categories ?? []}
        sizes={sizes ?? []}
      />
    </>
  );
}
