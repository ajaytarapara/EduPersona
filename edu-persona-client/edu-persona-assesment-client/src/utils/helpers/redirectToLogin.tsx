import type { Store } from "@reduxjs/toolkit";
import { logoutUser } from "../../store/features";
import { ExternalAppRoute } from "../constants";
import { logout } from "../../api";

export const redirectToLogin = async (store: Store) => {
  const state = store?.getState();

  if (state.auth.userInfo.userId) await logout(state.auth?.userInfo?.userId);
  store.dispatch(logoutUser());
  // Hard redirect
  window.open(ExternalAppRoute.CoreLogin, "_self");
};
