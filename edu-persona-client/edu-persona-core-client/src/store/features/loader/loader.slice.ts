import { createSlice } from "@reduxjs/toolkit";

interface UiState {
  loading: boolean;
}

const initialState: UiState = {
  loading: false,
};

const loaderSlice = createSlice({
  name: "ui",
  initialState,
  reducers: {
    showLoader: (state) => {
      state.loading = true;
    },
    hideLoader: (state) => {
      state.loading = false;
    },
  },
});

export const { showLoader, hideLoader } = loaderSlice.actions;
export const loaderReducer = loaderSlice.reducer;
