import { Plugin } from '@nuxt/types';
import HttpStatus from 'http-status-codes';

const HTTP_HEADER_KEY_API_VERSION = 'x-api-version';
const HTTP_HEADER_KEY_SPA_VERSION = 'x-spa-version';
const LOCAL_STORAGE_KEY_API_VERSION = 'api-version';
const LOCAL_STORAGE_KEY_SPA_VERSION = 'spa-version';

const plugin: Plugin = context => {
  context.$axios.onRequest(config => {
    const apiVersion = localStorage.getItem(LOCAL_STORAGE_KEY_API_VERSION);
    if (apiVersion) {
      config.headers[HTTP_HEADER_KEY_API_VERSION] = apiVersion;
    }
    const spaVersion = localStorage.getItem(LOCAL_STORAGE_KEY_SPA_VERSION);
    if (spaVersion) {
      config.headers[HTTP_HEADER_KEY_SPA_VERSION] = spaVersion;
    }
  });

  context.$axios.onResponseError(error => {
    const status = error.response?.status;
    if (status === HttpStatus.BAD_REQUEST || status === HttpStatus.UNAUTHORIZED || status === HttpStatus.FORBIDDEN) {
      return Promise.resolve(error.response);
    }
    return Promise.reject(error);
  });

  context.$axios.onResponse(response => {
    if (response.status === HttpStatus.BAD_REQUEST) {
      const code = response.data?.error?.code;
      if (code === 'AmbiguousApiVersion' || code === 'ApiVersionUnspecified' || code === 'InvalidApiVersion' || code === 'UnsupportedApiVersion') {
        context.store.commit('versioning/updatable', true);
      }
    }
    return Promise.resolve(response);
  });

  context.$axios.onResponse(response => {
    if (response.status === HttpStatus.UNAUTHORIZED || response.status === HttpStatus.FORBIDDEN) {
      context.$auth.setUser(undefined);
      context.$auth.redirect('login');
    }
    return Promise.resolve(response);
  });
};

export default plugin;
