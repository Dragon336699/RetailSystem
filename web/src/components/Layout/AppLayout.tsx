import { Outlet } from "react-router-dom";
import { Avatar, Layout, Menu, type MenuProps } from "antd";
import {
  BlockOutlined,
  DashboardOutlined,
  ProductOutlined,
} from "@ant-design/icons";
import { useState } from "react";

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

  const items: MenuItem[] = [
    getItem(
      <span className="text-left w-100 block">Dashboard</span>,
      "dashboard",
      <DashboardOutlined />,
    ),
    getItem(
      <span className="text-left w-100 block">Products</span>,
      "product",
      <ProductOutlined />,
    ),
    getItem(
      <span className="text-left w-100 block">Categories</span>,
      "categories",
      <BlockOutlined />,
    ),
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
          className={`overflow-hidden transition-all duration-200 px-6 ${
            collapsed ? "max-h-0 opacity-0 mt-0" : "max-h-20 opacity-100 mt-4"
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
          className="flex flex-row-reverse items-center"
          style={{
            padding: 0,
            background: "#ffffff",
            borderBottom: "1px solid #f0f0f0",
          }}
        >
          <Avatar size="large" style={{ marginRight: 20 }}>
            N
          </Avatar>
        </Header>
        <Content>
          <div style={{ padding: 24, minHeight: 360, background: "#fff" }}>
            <Outlet />
          </div>
        </Content>
      </Layout>
    </Layout>
  );
}
