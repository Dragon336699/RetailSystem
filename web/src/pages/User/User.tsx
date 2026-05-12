import CustomerList from "../../features/user/components/CustomerList";
import { useCustomers } from "../../features/user/hooks/user.hook";

export default function CustomerPage() {
  const { data: customers } = useCustomers();

  return <CustomerList customers={customers ?? []}></CustomerList>;
}
