import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import type { IAuthState } from "../../../utils";

const initialState: IAuthState = {
  userInfo: {
    userName: "",
    role: "",
    userId: null,
  },
  sessionId: null,
  isAuthInitialized: false,
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    setSession: (
      state,
      action: PayloadAction<{
        userInfo: { userName: string; role: string; userId: number };
        sessionId: number | null;
      }>
    ) => {
      state.userInfo = action.payload.userInfo;
      state.sessionId = action.payload.sessionId;
      state.isAuthInitialized = true;
    },
    logoutUser: (state) => {
      state.userInfo = { userName: "", role: "", userId: null };
      state.sessionId = null;
      state.isAuthInitialized = false;
    },
  },
});

export const { logoutUser, setSession } = authSlice.actions;
export const authReducer = authSlice.reducer;
