export interface ApiResponse<T> {
    error: boolean;
    data: T;
    errorMessage: string;
}