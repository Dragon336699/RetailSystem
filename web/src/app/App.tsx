import { ConfigProvider, message, theme } from 'antd';
import './App.css'
import { RouterProvider } from 'react-router-dom';
import { router } from './routes/router';

function App() {
const [messageApi, contextHolder] = message.useMessage();
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
      {contextHolder}
      <RouterProvider router={router} />
    </ConfigProvider>
  );
}

export default App
