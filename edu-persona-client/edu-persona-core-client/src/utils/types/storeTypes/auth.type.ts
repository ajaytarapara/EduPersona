export interface IAuthState {
  sessionId: number | null;
  userInfo: {
    userName: string | null;
    role: string | null;
    isProfileCompleted: boolean;
  };
}
