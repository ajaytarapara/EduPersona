export interface IAuthState {
  sessionId: number | null;
  isAuthInitialized: boolean;
  userInfo: {
    userName: string | null;
    role: string | null;
    userId: number | null;
  };
}
