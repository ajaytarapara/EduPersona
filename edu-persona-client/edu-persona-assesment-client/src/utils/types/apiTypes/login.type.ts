export interface ILoginPayload {
  email: string;
  password: string;
}

export interface ILoginResponse {
  sessionId: number;
}

export interface IValidateTokenResponse {
  userId: number;
  userName: string;
  role: string;
}
