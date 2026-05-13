import { Navigate, Outlet } from "react-router-dom";
import { useUsers } from "../../hooks/users.hook";

export default function ProtectedRoute() {
    const { data: adminInfo, isLoading, isError } = useUsers();

    if (isLoading) {
        return <div>Loading...</div>;
    }

    if (isError || !adminInfo) {
        return <Navigate to="/login" replace/>;
    }

    return <Outlet />;
}