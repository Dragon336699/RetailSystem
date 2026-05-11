import { Badge, Pagination, Table, type TableProps } from "antd";
import type { Product } from "../types/product";
import { DeleteOutlined, EditOutlined } from "@ant-design/icons";
import GenericPopConfirm from "../../../components/Modal/GenericPopConfirm";

type ProductListProps = {
  products: Product[];
  page: number;
  pageSize: number;
  openUpdateProduct: (product: Product) => void;
  onDeleteProduct: (productId: string) => void;
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

export default function ProductList({
  products,
  page,
  pageSize,
  openUpdateProduct,
  onDeleteProduct,
  changePage,
}: ProductListProps) {
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
    createdAt: product.createdAt,
    updatedAt: product.updatedAt,
    action: (
      <div className="flex gap-4">
        <EditOutlined
          onClick={() => openUpdateProduct(product)}
          className="text-lg cursor-pointer"
        />
        <span className="text-red-500 text-lg cursor-pointer">
          <GenericPopConfirm
            title="Delete"
            description={`Are you sure to delete category "${product.productName}"?`}
            onConfirm={() => onDeleteProduct(product.id)}
          >
            <DeleteOutlined />
          </GenericPopConfirm>
        </span>
      </div>
    ),
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
      title: "Created At",
      dataIndex: "createdAt",
      key: "createdAt",
      render: (value) => {
        const date = new Date(value);
        return <span>{date.toLocaleString("vi-VN")}</span>;
      },
    },
    {
      title: "Updated At",
      dataIndex: "updatedAt",
      key: "updatedAt",
      render: (value) => {
        const date = new Date(value);
        return <span>{date.toLocaleString("vi-VN")}</span>;
      },
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
        rowKey="id"
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
