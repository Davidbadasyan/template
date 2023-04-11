/* eslint-disable no-param-reassign */
import JwtService from "@/core/services/JwtService";

const tokenInterceptor = async (config) => {
  const token = await JwtService.getToken();
  if(config && config.headers && config.headers.common){
    config.headers.common = config.headers.common || {};
    config.headers.common.authorization = `Bearer ${token}`;
    config.headers.common['correlation-id'] = JwtService.correlationId;
  }
  return config;
};

export default tokenInterceptor;
