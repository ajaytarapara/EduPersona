import type { Store } from "@reduxjs/toolkit";
import { logoutUser } from "../../store/features";
import { AppRoutes } from "../constants";
import { logout } from "../../api";

export const redirectToLogin = async (store: Store) => {
  const state = store?.getState();

  await logout(state.auth?.userInfo?.userId);
  store.dispatch(logoutUser());
  // Hard redirect
  window.location.replace(AppRoutes.Login);
};
