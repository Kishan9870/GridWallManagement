export class ResponseBase<T> {
  isSuccess: boolean;
  message: string;
  errors: string[];
  responseStatusCode: string;
  responseStatusCodeValue: number;
  result: T;

  constructor(
    isSuccess: boolean,
    message: string,
    errors: string[],
    responseStatusCode: string,
    responseStatusCodeValue: number,
    result: T
  ) {
    this.isSuccess = isSuccess;
    this.message = message;
    this.errors = errors;
    this.responseStatusCode = responseStatusCode;
    this.responseStatusCodeValue = responseStatusCodeValue;
    this.result = result;
  }
}
