import axios, { AxiosInstance, AxiosRequestConfig } from 'axios';
import ENDPOINTS from '../constants/endpoints';
import { APP_CONFIG } from '../constants/appConfig';

const config: AxiosRequestConfig = {
  baseURL: APP_CONFIG.API_BASE_URL,
};

class RequestService {
  public axios: AxiosInstance;

  constructor() {
    this.axios = axios.create(config);
  }
}

export default new RequestService();
