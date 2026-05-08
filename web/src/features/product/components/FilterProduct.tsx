import { FilterOutlined } from "@ant-design/icons";
import { Button, Card, Select } from "antd";
import type { Category } from "../../category/types/category.types";
import type { Size } from "../../size/types/size.types";
import type { Color } from "../../color/types/color.types";

type Filters = {
  category?: string;
  size?: string;
  color?: string;
};

type Props = {
  filters: Filters;
  setFilters: (filters: Filters) => void;
  categories: Category[];
  sizes: Size[];
  colors: Color[];
};

export default function FilterProduct({
  filters,
  setFilters,
  categories,
  sizes,
  colors,
}: Props) {
  return (
    <Card>
      <div style={{ display: "flex", gap: 12, width: "100%" }}>
        <Select
          placeholder="All Categories"
          style={{ flex: 1 }}
          options={categories.map((c) => ({
            label: c.categoryName,
            value: c.id,
          }))}
        />
        <Select
          placeholder="All Sizes"
          style={{ flex: 1 }}
          options={sizes.map((s) => ({
            label: s.sizeNumber,
            value: s.id,
          }))}
        />
        <Select
          placeholder="All Colors"
          style={{ flex: 1 }}
          options={colors.map((co) => ({
            label: co.colorName,
            value: co.id,
          }))}
        />

        <Button icon={<FilterOutlined />}>Clear Filters</Button>
      </div>
    </Card>
  );
}
