import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import type { IAuthState } from "../../../utils";

const initialState: IAuthState = {
  userInfo: {
    userName: "",
    role: "",
    isProfileCompleted: false,
    userId: null,
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
        userInfo: {
          userName: string;
          role: string;
          isProfileCompleted: boolean;
          userId: number;
        };
        sessionId: number;
      }>
    ) => {
      state.userInfo = action.payload.userInfo;
      state.sessionId = action.payload.sessionId;
    },
    logoutUser: (state) => {
      state.userInfo = {
        userName: "",
        role: "",
        isProfileCompleted: false,
        userId: null,
      };
      state.sessionId = null;
    },
    setProfileComplete: (state) => {
      state.userInfo = { ...state.userInfo, isProfileCompleted: true };
    },
  },
});

export const { logoutUser, setSession, setProfileComplete } = authSlice.actions;
export const authReducer = authSlice.reducer;
