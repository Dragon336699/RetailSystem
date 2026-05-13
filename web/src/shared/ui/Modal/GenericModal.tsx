import { Modal } from "antd";
import type React from "react";

type Props = {
  open: boolean;
  title?: string;
  onClose: () => void;
  onOk?: () => void;
  children: React.ReactNode;
  footer: React.ReactNode[] | null;
  destroyClose?: boolean;
};

export function GenericModal({
  open,
  title,
  onClose,
  onOk,
  children,
  footer,
  destroyClose
}: Props) {
  return (
    <Modal
      destroyOnHidden={destroyClose ?? false}
      styles={{
        body: {
          maxHeight: "70vh",
          overflowY: "auto",
        },
      }}
      open={open}
      title={title}
      onCancel={onClose}
      onOk={onOk}
      footer={footer}
      width="80%"
      style={{ maxWidth: 1200 }}
    >
      {children}
    </Modal>
  );
}
