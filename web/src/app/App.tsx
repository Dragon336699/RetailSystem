import { ConfigProvider, theme } from 'antd';
import './App.css'
import { RouterProvider } from 'react-router-dom';
import { router } from './routes/router';

function App() {

return (
    <ConfigProvider
      theme={{
        token: {
          colorPrimary: "#1677ff",
          borderRadius: 8,
          fontFamily: "inherit",
        },
        components: {
          Button: {
            borderRadius: 6,
            controlHeight: 40,
            fontWeight: 600,
          },
          Card: {
            borderRadiusLG: 12,
          },
        },
        algorithm: theme.defaultAlgorithm,
      }}
    >
      <RouterProvider router={router} />
    </ConfigProvider>
  );
}

export default App
