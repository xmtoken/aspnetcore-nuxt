import HttpStatus from 'http-status-codes';
const HTTP_HEADER_KEY_API_VERSION = 'x-api-version';
const HTTP_HEADER_KEY_SPA_VERSION = 'x-spa-version';
const LOCAL_STORAGE_KEY_API_VERSION = 'api-version';
const LOCAL_STORAGE_KEY_SPA_VERSION = 'spa-version';
export default function ({ $axios, app, store }) {
  $axios.onRequest(config => {
    const apiVersion = localStorage.getItem(LOCAL_STORAGE_KEY_API_VERSION);
    if (apiVersion) {
      config.headers[HTTP_HEADER_KEY_API_VERSION] = apiVersion;
    }
    const spaVersion = localStorage.getItem(LOCAL_STORAGE_KEY_SPA_VERSION);
    if (spaVersion) {
      config.headers[HTTP_HEADER_KEY_SPA_VERSION] = spaVersion;
    }
  });

  $axios.onResponseError(error => {
    const status = error.response?.status;
    if (status === HttpStatus.BAD_REQUEST || status === HttpStatus.UNAUTHORIZED || status === HttpStatus.FORBIDDEN) {
      return Promise.resolve(error.response);
    }
    return Promise.reject(error);
  });

  $axios.onResponse(response => {
    if (response.status === HttpStatus.BAD_REQUEST) {
      const code = response.data?.error?.code;
      if (code === 'AmbiguousApiVersion' || code === 'ApiVersionUnspecified' || code === 'InvalidApiVersion' || code === 'UnsupportedApiVersion') {
        store.commit('versioning/updatable', true);
      }
    }
    return Promise.resolve(response);
  });

  $axios.onResponse(async response => {
    if (response.status === HttpStatus.UNAUTHORIZED || response.status === HttpStatus.FORBIDDEN) {
      await app.$auth.reset();
    }
    return Promise.resolve(response);
  });
}
