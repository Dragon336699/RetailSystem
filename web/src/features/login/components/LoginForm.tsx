import { Button, Form, Input } from "antd";
import { MailOutlined, LockOutlined } from "@ant-design/icons";
import type { LoginRequest } from "../types/login.type";

type Props = {
  onSubmit: (data: LoginRequest) => void;
};

export default function LoginForm({ onSubmit }: Props) {
  const [form] = Form.useForm();

  return (
    <Form form={form} onFinish={onSubmit} layout="vertical" requiredMark={false}>
      {/* Email */}
      <Form.Item
        label={<span className="text-xs font-medium text-gray-500 tracking-wide">Email</span>}
        name="userName"
        rules={[{ required: true, message: "Email is required" }]}
        className="mb-4"
      >
        <Input
          prefix={<MailOutlined className="text-gray-300 text-sm mr-1" />}
          placeholder="yatingzang0215@gmail.com"
          className="h-9 rounded border-gray-200 text-sm text-gray-600 placeholder:text-gray-300"
        />
      </Form.Item>

      {/* Password */}
      <Form.Item
        label={<span className="text-xs font-medium text-gray-500 tracking-wide">Password</span>}
        name="password"
        rules={[{ required: true, message: "Password is required" }]}
        className="mb-6"
      >
        <Input.Password
          prefix={<LockOutlined className="text-gray-300 text-sm mr-1" />}
          placeholder="••••••••"
          className="h-9 rounded border-gray-200 text-sm"
          visibilityToggle={false}
        />
      </Form.Item>

      {/* Login Button */}
      <Form.Item className="mb-3">
        <Button
          type="primary"
          htmlType="submit"
          className="w-full h-10 rounded font-semibold text-sm tracking-wide"
          style={{ backgroundColor: "#4F46E5", borderColor: "#4F46E5" }}
        >
          Login in
        </Button>
      </Form.Item>
    </Form>
  );
}
