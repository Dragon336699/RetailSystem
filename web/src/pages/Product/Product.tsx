import { PlusOutlined } from "@ant-design/icons";
import { Button } from "antd";
import FilterProduct from "../../features/product/components/FilterProduct";
import { useCategories } from "../../features/category/hooks/category.hook";
import { useState } from "react";
import { useSizes } from "../../features/size/hooks/size.hook";
import { useColors } from "../../features/color/hooks/color.hook";

export default function Product() {
  const [filters, setFilters] = useState({});

  const { data: categories } = useCategories();
  const { data: sizes } = useSizes();
  const { data: colors } = useColors();

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
      <FilterProduct
        filters={filters}
        setFilters={setFilters}
        categories={categories ?? []}
        sizes={sizes ?? []}
        colors={colors ?? []}
      />
    </>
  );
}
