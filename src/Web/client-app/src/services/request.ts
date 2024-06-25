import axios, { AxiosInstance, AxiosRequestConfig } from 'axios';
import { APP_CONFIG } from '../constants/appConfig';

const config: AxiosRequestConfig = {
  baseURL: APP_CONFIG.API_BASE_URL,
  withCredentials: true,
};

class RequestService {
  public axios: AxiosInstance;

  constructor() {
    this.axios = axios.create(config);
  }
}

// eslint-disable-next-line import/no-anonymous-default-export
export default new RequestService();
