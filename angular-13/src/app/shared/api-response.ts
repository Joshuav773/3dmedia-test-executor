export interface ApiResponse<T> {
    isError: boolean;
    data: T[] | T;
    errorMsg: string;
}