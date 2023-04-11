import axios from 'axios';
import tokenInterceptor from '@/core/services/interceptors/tokenInterceptor';
import authInterceptor from '@/core/services/interceptors/authInterceptor';
import errorInterceptor from '@/core/services/interceptors/errorInterceptor';

export default ({ url, version, useInterceptor }) => {
  const instance = axios.create({
    baseURL: `${url}/${version}`,
    headers: { Pragma: 'no-cache' },
  });
  instance.interceptors.request.use(tokenInterceptor);
  instance.interceptors.response.use((response) => response, authInterceptor);
  if (useInterceptor) {
    instance.interceptors.response.use((response) => response, errorInterceptor);
  }
  return instance;
};
