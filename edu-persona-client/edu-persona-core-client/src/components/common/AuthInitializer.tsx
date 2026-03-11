import Cookies from "js-cookie";
import { useAppDispatch } from "../../store/hook";
import { useEffect } from "react";
import { logoutUser } from "../../store/features";

export const AuthInitializer = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const dispatch = useAppDispatch();
  const sessionId = Cookies.get("session_id");
  useEffect(() => {
    if (!sessionId) {
      dispatch(logoutUser());
    }
  }, [sessionId]);

  return <>{children}</>;
};
