import { createSlice, type PayloadAction } from "@reduxjs/toolkit";

interface AuthState {
  role: string | null;
  sessionId: string | null;
}

const initialState: AuthState = {
  role: "",
  sessionId: "",
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    setSession: (
      state,
      action: PayloadAction<{ role: string; sessionId: string }>
    ) => {
      state.role = action.payload.role;
      state.sessionId = action.payload.sessionId;
    },
    logout: (state) => {
      state.role = null;
      state.sessionId = null;
    },
  },
});

export const { logout } = authSlice.actions;
export const authReducer = authSlice.reducer;
