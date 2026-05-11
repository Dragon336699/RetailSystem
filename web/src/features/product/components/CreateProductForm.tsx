import {
  Badge,
  Button,
  Form,
  Input,
  InputNumber,
  Select,
  Table,
  Upload,
  type UploadFile,
  type UploadProps,
} from "antd";
import { useState } from "react";
import { PlusOutlined, StarFilled } from "@ant-design/icons";

import type { Category } from "../../category/types/category.types";
import type { Size } from "../../size/types/size.types";
import type { ColumnsType } from "antd/es/table";
import type { CreateProductRequest } from "../types/product";

type Props = {
  onSubmit: (data: CreateProductRequest) => void;
  categories: Category[];
  sizes: Size[];
};

type DataType = {
  key: string;
  sizeId: string;
  sizeNumber: number;
};

export default function CreateProductForm({
  onSubmit,
  categories,
  sizes,
}: Props) {
  const [form] = Form.useForm<CreateProductRequest>();
  const [fileList, setFileList] = useState<UploadFile[]>([]);
  const [thumbnailIndex, setThumbnailIndex] = useState(0);

  const handleChange: UploadProps["onChange"] = ({ fileList }) => {
    setFileList(fileList);
  };

  const dataSource: DataType[] = sizes.map((s) => ({
    key: s.id,
    sizeId: s.id,
    sizeNumber: s.sizeNumber,
  }));

  const columns: ColumnsType<DataType> = [
    {
      title: "Size",
      dataIndex: "sizeNumber",
      key: "size",
    },
    {
      title: "Quantity",
      key: "quantity",
      render: (_, record, index) => (
        <>
          <Form.Item
            name={["sizesQuantity", index, "sizeId"]}
            initialValue={record.sizeId}
            hidden
          >
            <input />
          </Form.Item>

          <Form.Item
            name={["sizesQuantity", index, "quantity"]}
            initialValue={0}
            noStyle
          >
            <InputNumber min={0} className="w-full" />
          </Form.Item>
        </>
      ),
    },
  ];

  const itemRender = (
    originNode: React.ReactNode,
    _file: UploadFile,
    currFileList: UploadFile[],
  ) => {
    const index = currFileList.indexOf(_file);
    const isThumb = index === thumbnailIndex;

    return (
      <Badge
        count={isThumb ? <StarFilled style={{ color: "#1677ff" }} /> : ""}
      >
        <div
          onClick={() => setThumbnailIndex(index)}
          style={{
            border: isThumb ? "2px solid #1677ff" : undefined,
            borderRadius: 8,
            overflow: "hidden",
            cursor: "pointer",
            width: 104,
            height: 104,
          }}
        >
          {originNode}
        </div>
      </Badge>
    );
  };

  const handleSubmit = (value: CreateProductRequest) => {
    const sizesQuantity = value.sizesQuantity ?? [];

    const submitData: CreateProductRequest = {
      ...value,
      productImages: fileList,
      sizesQuantity: sizesQuantity.filter((i) => i.quantity > 0),
      thumbnailIndex: thumbnailIndex,
    };

    onSubmit(submitData);
  };

  return (
    <Form form={form} onFinish={handleSubmit}>
      <div className="flex gap-6">
        <div className="flex flex-col gap-4 w-2/3">
          {/* Product Name */}
          <div className="flex flex-col gap-2">
            <label className="text-sm font-medium text-gray-700">
              Product Name <span className="text-red-500">*</span>
            </label>

            <Form.Item
              name="productName"
              rules={[
                { required: true, message: "Please input product name!" },
              ]}
              noStyle
            >
              <Input placeholder="Enter product name" />
            </Form.Item>
          </div>

          {/* Description */}
          <div className="flex flex-col gap-2">
            <label className="text-sm font-medium text-gray-700">
              Description
            </label>

            <Form.Item name="description" noStyle>
              <Input.TextArea placeholder="Enter description" />
            </Form.Item>
          </div>

          {/* Categories */}
          <div className="flex flex-col gap-2">
            <label className="text-sm font-medium text-gray-700">
              Categories
            </label>

            <Form.Item
              name="categoryIds"
              rules={[
                {
                  required: true,
                  message: "Please select at least 1 category!",
                },
              ]}
            >
              <Select
                mode="multiple"
                allowClear
                placeholder="Select categories"
                options={categories.map((c) => ({
                  value: c.id,
                  label: c.categoryName,
                }))}
              />
            </Form.Item>
          </div>

          {/* Price + Color */}
          <div className="flex gap-3">
            <div className="flex flex-col gap-2">
              <label className="text-sm font-medium text-gray-700">
                Price <span className="text-red-500">*</span>
              </label>

              <Form.Item
                name="price"
                rules={[{ required: true, message: "Price is required" }]}
                noStyle
              >
                <InputNumber className="w-full" min={0} />
              </Form.Item>
            </div>
          </div>
        </div>

        <div className="w-1/3 flex flex-col gap-6">
          <div className="flex flex-col gap-2">
            <label className="text-sm font-medium text-gray-700">
              Images <span className="text-red-500">*</span>
            </label>

            <Form.Item
              name="productImages"
              valuePropName="fileList"
              getValueFromEvent={(e) => e?.fileList}
              rules={[{ required: true, message: "Please upload images" }]}
            >
              <Upload
                multiple
                listType="picture-card"
                beforeUpload={() => false}
                itemRender={itemRender}
                onChange={handleChange}
              >
                {fileList?.length >= 5 ? null : (
                  <div>
                    <PlusOutlined />
                    <div style={{ marginTop: 8 }}>Upload</div>
                  </div>
                )}
              </Upload>
            </Form.Item>
          </div>
        </div>
      </div>

      {/* STOCK */}
      <div className="flex flex-col gap-2 mt-6">
        <label className="text-sm font-medium text-gray-700">
          Stock by Size
        </label>

        <Table
          columns={columns}
          dataSource={dataSource}
          pagination={false}
          rowKey="key"
        />
      </div>

      {/* SUBMIT */}
      <div className="flex justify-end mt-6">
        <Button type="primary" htmlType="submit">
          Save Product
        </Button>
      </div>
    </Form>
  );
}
