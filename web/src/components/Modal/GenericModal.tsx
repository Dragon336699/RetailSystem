import { Modal } from "antd";
import type React from "react";

type Props = {
  open: boolean;
  title?: string;
  onClose: () => void;
  onOk?: () => void;
  children: React.ReactNode;
  footer: React.ReactNode[] | null;
};

export function GenericModal({ open, title, onClose, onOk, children, footer }: Props) {
  return (
    <Modal open={open} title={title} onCancel={onClose} onOk={onOk} footer={footer}>
      {children}
    </Modal>
  );
}