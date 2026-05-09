import { Badge, Pagination, Table, type TableProps } from "antd";
import type { Product } from "../types/product";
import { DeleteOutlined, EditOutlined } from "@ant-design/icons";

type ProductListProps = {
  products: Product[];
  page: number,
  pageSize: number,
  changePage: (page: number, pageSize: number) => void;
};

interface DataType {
  key: string;
  image: string;
  productName: string;
  category: string;
  price: number;
  stock: number;
  action: React.ReactNode;
}

export default function ProductList({ products, page, pageSize, changePage }: ProductListProps) {
  const dataSource: DataType[] = products.map((product) => ({
    key: product.id,
    image: product.productImages.find((img) => img.isThumbnail)?.imageUrl || "",
    productName: product.productName,
    category: product.categories[0].categoryName,
    price: product.price,
    stock: product.productVariants.reduce(
      (sum, variant) => sum + variant.stockQuantity,
      0,
    ),
    action: <div className="flex gap-4">
        <EditOutlined className="text-lg cursor-pointer"/>
        <DeleteOutlined className="text-lg cursor-pointer"/>
    </div>,
  }));

  const columns: TableProps<DataType>["columns"] = [
    {
      title: "Image",
      dataIndex: "image",
      key: "image",
      render: (text) => (
        <img src={text} alt="Product" style={{ width: 50, height: 50 }} />
      ),
    },
    {
      title: "Product Name",
      dataIndex: "productName",
      key: "productName",
      className: "font-bold",
    },
    {
      title: "Category",
      dataIndex: "category",
      key: "category",
      render: (text: string) => (
        <span className="bg-blue-100 text-blue-700 px-2.5 py-1 rounded-md text-xs font-semibold">
          {text.toUpperCase()}
        </span>
      ),
    },
    {
      title: "Price",
      dataIndex: "price",
      key: "price",
      render: (value: number) => (
        <span className="font-semibold">${value.toFixed(2)}</span>
      ),
    },
    {
      title: "Stock",
      dataIndex: "stock",
      key: "stock",
      render: (value: number) => (
        <span className="flex items-center gap-2">
          {value > 10 ? (
            <>
              <Badge status="success" />
              <span className="text-sm">{value} Units</span>
            </>
          ) : value > 0 ? (
            <>
              <Badge status="warning" />
              <span className="text-sm">{value} Units</span>
            </>
          ) : (
            <>
              <Badge status="error" />
              <span className="text-sm">Out of stock</span>
            </>
          )}
        </span>
      ),
    },
    {
      title: "Action",
      dataIndex: "action",
      key: "action",
    },
  ];
  return (
    <>
      <Table<DataType>
        dataSource={dataSource}
        columns={columns}
        pagination={{ placement: [] }}
      />
      <br />
      <Pagination
        current={page}
        pageSize={pageSize}
        align="end"
        onChange={(newPage, newPageSize) => {
            changePage(newPage, newPageSize);
        }}
      />
    </>
  );
}
