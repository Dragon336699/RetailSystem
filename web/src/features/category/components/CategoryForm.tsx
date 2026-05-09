import { Button, Form, Input } from "antd";
import type { Category } from "../types/category.types";
import { useEffect } from "react";

type Props<T> = {
  onSubmit: (data: T) => void;
  categoryData: Category | null;
};

export default function CategoryForm<T>({ onSubmit, categoryData }: Props<T>) {
  const [form] = Form.useForm();

  useEffect(() => {
    if (categoryData) {
      form.setFieldsValue(categoryData);
    } else {
      form.resetFields();
    }
  }, [categoryData, form]);

  const handleSubmit = (value: T) => {
    if (categoryData) {
      const updateData: T = {
        id: categoryData.id,
        ...value,
      };
      onSubmit(updateData);
    } else {
      onSubmit(value);
    }
  };

  return (
    <Form form={form} onFinish={handleSubmit} className="space-y-6">
      <div className="flex flex-col gap-2">
        <label className="text-sm font-medium text-gray-700">
          Category Name <span className="text-red-500">*</span>
        </label>

        <Form.Item
          name="categoryName"
          noStyle
          rules={[{ required: true, message: "Please input category name!" }]}
        >
          <Input
            placeholder="Enter category name"
            className="!rounded-xl !border-gray-300 focus:!border-blue-500 focus:!shadow-none h-10"
          />
        </Form.Item>
      </div>

      <div className="flex flex-col gap-2">
        <label className="text-sm font-medium text-gray-700">Description</label>

        <Form.Item name="description" noStyle>
          <Input.TextArea
            rows={5}
            placeholder="Enter category description..."
            className="!rounded-xl !border-gray-300 focus:!border-blue-500 focus:!shadow-none"
          />
        </Form.Item>
      </div>

      <Form.Item className="text-right">
        <Button type="primary" htmlType="submit">
          Save
        </Button>
      </Form.Item>
    </Form>
  );
}
