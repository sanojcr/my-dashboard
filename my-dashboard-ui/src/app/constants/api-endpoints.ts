import { environment } from "../environments/environment";


export class ApiEndpoints {
  private static baseUrl = environment.apiBaseUrl;

  public static readonly LOGIN = `${this.baseUrl}/auth/login`;
  public static readonly REFRESH = `${this.baseUrl}/auth/refresh`;
  public static readonly REGISTER = `${this.baseUrl}/auth/register`;
  public static readonly HOME_CHECK_AUTH = `${this.baseUrl}/home/checkAuth`;
  public static readonly HOME_GET = `${this.baseUrl}/home/get`;
}
