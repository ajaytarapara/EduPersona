import "./App.css";
import AppRoutes from "./routes/AppRoutes";
import { AuthInitializer } from "./components/common";
import { useAppDispatch, useAppSelector } from "./store/hook";
import { useEffect } from "react";
import { setSession } from "./store/features";
import { useLocation } from "react-router-dom";
import { getLoggedInUserInfo } from "./api";
import GlobalLoader from "./components/common/GlobalLoader";

function App() {
  const dispatch = useAppDispatch();
  const { userInfo } = useAppSelector((state) => state.auth);

  useEffect(() => {
    const initializeAuth = async () => {
      if (userInfo.userId === null) {
        const response = await getLoggedInUserInfo();

        dispatch(
          setSession({
            userInfo: {
              userName: response.data?.userName ?? "",
              role: response.data?.role ?? "",
              userId: response.data?.userId ?? 0,
            },
            sessionId: null,
          })
        );
      }
    };

    initializeAuth();
  }, [dispatch]);

  if (userInfo.userId === null) {
    return <GlobalLoader />;
  }

  return (
    <AuthInitializer>
      <AppRoutes />
    </AuthInitializer>
  );
}

export default App;
