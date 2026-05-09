import { PlusOutlined } from "@ant-design/icons";
import { Button } from "antd";
import { useState } from "react";
import ProductList from "../../features/product/components/ProductList";
import { useProducts } from "../../features/product/hooks/product.hook";

export default function Product() {
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);

  const { data: products } = useProducts(page, pageSize);

  const changePage = (page: number, pageSize: number) => {
    setPage(page);
    setPageSize(pageSize);
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

        <Button>
          <PlusOutlined /> Add Product
        </Button>
      </div>
      <br />
      <ProductList products={products ?? []} page={page} pageSize={pageSize} changePage={changePage} />
    </>
  );
}
