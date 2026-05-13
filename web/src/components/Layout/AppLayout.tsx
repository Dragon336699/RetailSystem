import { Link, Outlet, useNavigate } from "react-router-dom";
import { Avatar, Button, Layout, Menu, type MenuProps } from "antd";
import {
  BlockOutlined,
  LogoutOutlined,
  ProductOutlined,
  UserOutlined,
} from "@ant-design/icons";
import { useState } from "react";
import { loginApi } from "../../features/login/apis/login.api";

type MenuItem = Required<MenuProps>["items"][number];

function getItem(
  label: React.ReactNode,
  key: React.Key,
  icon?: React.ReactNode,
  children?: MenuItem[],
): MenuItem {
  return {
    key,
    icon,
    children,
    label,
  } as MenuItem;
}

export default function AppLayout() {
  const { Header, Content, Sider } = Layout;

  const [collapsed, setCollapsed] = useState(false);
  const navigate = useNavigate();

  const handleLogout = async () => {
    try {
      await loginApi.logout();
      navigate("/login", { replace: true });
    } catch (error: unknown) {
      if (error instanceof Error) {
        console.log(error.message);
      }
    }
  }

  const items: MenuItem[] = [
    getItem(
      <Link to="/products" className="text-left w-100 block">
        Products
      </Link>,
      "product",
      <ProductOutlined />,
    ),
    getItem(
      <Link to="/categories" className="text-left w-100 block">
        Categories
      </Link>,
      "categories",
      <BlockOutlined />,
    ),
    getItem(
      <Link to="/customers" className="text-left w-100 block">
        Customers
      </Link>,
      "customers",
      <UserOutlined />,
    )
  ];

  return (
    <Layout>
      <Sider
        width={280}
        collapsible
        collapsed={collapsed}
        onCollapse={(value) => setCollapsed(value)}
      >
        <div
          className={`overflow-hidden transition-all duration-200 px-6 ${collapsed ? "max-h-0 opacity-0 mt-0" : "max-h-20 opacity-100 mt-4"
            }`}
        >
          <h2 className="text-left mb-0">Retail System</h2>
          <p className="text-left text-sm text-gray-400">
            Luxury Footwear Admin
          </p>
        </div>
        <Menu
          theme="dark"
          defaultSelectedKeys={["1"]}
          mode="inline"
          items={items}
        />
      </Sider>
      <Layout className="flex flex-col">
        <Header
          className="flex flex-row-reverse items-center gap-3"
          style={{
            padding: 0,
            background: "#ffffff",
            borderBottom: "1px solid #f0f0f0",
          }}
        >
          <Button
            danger
            icon={<LogoutOutlined />}
            onClick={handleLogout}
          >
            Logout
          </Button>

          <Avatar size="large">
            N
          </Avatar>
        </Header>
        <Content>
          <div className="p-[24px] bg-[#fff] h-full">
            <Outlet />
          </div>
        </Content>
      </Layout>
    </Layout>
  );
}
