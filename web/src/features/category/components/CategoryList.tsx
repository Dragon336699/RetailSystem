import { Card } from "antd";
import type { Category } from "../types/category.types";
import { DeleteOutlined, EditOutlined, PlusOutlined } from "@ant-design/icons";
import GenericPopConfirm from "../../../shared/ui/Modal/GenericPopConfirm";

type Props = {
  categories: Category[];
  onOpenCreateModal: () => void;
  onOpenUpdateModal: (category: Category) => void;
  onDelete: (id: string) => void;
};

export default function CategoryList({
  categories,
  onOpenCreateModal,
  onOpenUpdateModal,
  onDelete,
}: Props) {
  const handleDelete = (categoryId: string) => {
    onDelete(categoryId);
  };
  return (
    <div className="flex flex-wrap gap-3">
      {categories.map((c) => (
        <Card
          key={c.id}
          className="w-55 shadow-sm hover:shadow-md transition rounded-xl"
        >
          <div className="flex items-center justify-between">
            <div className="text-left">
              <p className="font-medium text-gray-800 truncate">
                {c.categoryName}
              </p>
              <p className="font-small text-gray-600 truncate">
                {c?.description}
              </p>
            </div>

            <div className="flex gap-3">
              <span
                onClick={() => onOpenUpdateModal(c)}
                className="text-gray-500 text-lg cursor-pointer"
              >
                <EditOutlined />
              </span>

              <span className="text-red-500 text-lg cursor-pointer">
                <GenericPopConfirm
                  title="Delete"
                  description={`Are you sure to delete category "${c.categoryName}"?`}
                  onConfirm={() => handleDelete(c.id)}
                >
                  <DeleteOutlined />
                </GenericPopConfirm>
              </span>
            </div>
          </div>
        </Card>
      ))}
      <Card
        onClick={onOpenCreateModal}
        className="w-55 flex items-center justify-center border-dashed border-2 border-gray-300 bg-gray-50/60 hover:bg-gray-100/70 hover:border-blue-400 cursor-pointer transition rounded-xl shadow-sm"
      >
        <div className="flex items-center gap-2 text-gray-500">
          <PlusOutlined />
          <span className="font-medium text-sm">Add New</span>
        </div>
      </Card>
    </div>
  );
}
