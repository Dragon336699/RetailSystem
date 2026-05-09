import { Popconfirm, type PopconfirmProps } from "antd";

type Props = {
  title: string;
  description: string;
  children: React.ReactNode;
  onConfirm: () => void;
};

export default function GenericPopConfirm({
  title,
  description,
  children,
  onConfirm,
}: Props) {
  const cancel: PopconfirmProps["onCancel"] = () => {};

  return (
    <Popconfirm
      title={title}
      description={description}
      onConfirm={onConfirm}
      onCancel={cancel}
      okText="Yes"
      cancelText="No"
    >
      {children}
    </Popconfirm>
  );
}
