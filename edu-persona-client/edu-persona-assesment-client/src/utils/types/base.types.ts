export interface IApiResponse<T> {
    success: boolean;
    statusCode: number;
    message: string;
    data: T | null;
    errors?: string[] | null;
  }