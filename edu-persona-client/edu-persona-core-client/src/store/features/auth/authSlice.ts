import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import type { IAuthState } from "../../../utils";

const initialState: IAuthState = {
  userInfo: {
    userName: "",
    role: "",
    isProfileCompleted: false,
  },
  sessionId: null,
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    setSession: (
      state,
      action: PayloadAction<{
        userInfo: { userName: string; role: string, isProfileCompleted: boolean };
        sessionId: number;
      }>
    ) => {
      state.userInfo = action.payload.userInfo;
      state.sessionId = action.payload.sessionId;

    },
    logout: (state) => {
      state.userInfo = { userName: "", role: "", isProfileCompleted: false };
      state.sessionId = null;
    },
  },
});

export const { logout, setSession } = authSlice.actions;
export const authReducer = authSlice.reducer;
