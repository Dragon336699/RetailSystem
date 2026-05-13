import { createBrowserRouter, Navigate } from "react-router-dom";
import Layout from "../../shared/ui/Layout/AppLayout";
import Product from "../../pages/Product/Product";
import Category from "../../pages/Category/Category";
import CustomerPage from "../../pages/User/User";
import LoginPage from "../../pages/Login/Login";
import ProtectedRoute from "../../shared/ui/Route/ProtectedRoute";


export const router = createBrowserRouter([
    {
        path: '/',
        element: <ProtectedRoute />,
        children: [
            {
                element: <Layout />,
                children: [
                    {
                        index: true,
                        element: <Navigate to="/products" replace />
                    },
                    {
                        path: "products",
                        element: <Product />
                    },
                    {
                        path: "categories",
                        element: <Category />
                    },
                    {
                        path: "customers",
                        element: <CustomerPage />
                    }
                ]
            }
        ]
    },
    {
        path: 'login',
        element: <LoginPage />
    }
])