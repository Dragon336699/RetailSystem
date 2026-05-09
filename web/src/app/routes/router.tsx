import { createBrowserRouter, Navigate } from "react-router-dom";
import Layout from "../../components/Layout/AppLayout";
import Product from "../../pages/Product/Product";
import Category from "../../pages/Category/Category";


export const router = createBrowserRouter([
    {
        path: "/",
        element: <Layout />,
        children: [
            {
                index: true,
                element: <Navigate to="/products" replace/>
            },
            {
                path: "products",
                element: <Product />
            },
            {
                path: "categories",
                element: <Category />
            }
        ]
    }
])