import { Table, type TableProps } from "antd";
import type { UserDto } from "../types/user.type";

type Props = {
  customers: UserDto[];
};

interface DataType {
  key: string;
  userName: string;
  fullName: string;
}

export default function CustomerList({ customers }: Props) {
  const dataSource: DataType[] = customers.map((c) => ({
    key: c.id,
    userName: c.userName,
    fullName: c.fullName,
  }));

  const columns: TableProps<DataType>["columns"] = [
    {
      title: "User Name",
      dataIndex: "userName",
      key: "userName",
      className: "font-bold",
    },
    {
      title: "Full Name",
      dataIndex: "fullName",
      key: "fullName",
      className: "font-bold",
    },
  ];

  return <Table columns={columns} dataSource={dataSource}></Table>;
}
