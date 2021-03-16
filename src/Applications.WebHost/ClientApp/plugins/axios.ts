import { Plugin } from '@nuxt/types';
import { setupCache } from 'axios-cache-adapter';
import { StatusCodes } from 'http-status-codes';

const HTTP_HEADER_KEY_API_VERSION = 'x-api-version';
const HTTP_HEADER_KEY_SPA_VERSION = 'x-spa-version';
const LOCAL_STORAGE_KEY_API_VERSION = 'api-version';
const LOCAL_STORAGE_KEY_SPA_VERSION = 'spa-version';

const plugin: Plugin = ctx => {
  const cache = setupCache({
    exclude: {
      query: false,
    },
    maxAge: 1000 * 60 * 3,
  });
  ctx.$axios.defaults.adapter = cache.adapter;

  ctx.$axios.onRequest(config => {
    const apiVersion = localStorage.getItem(LOCAL_STORAGE_KEY_API_VERSION);
    if (apiVersion) {
      config.headers[HTTP_HEADER_KEY_API_VERSION] = apiVersion;
    }
    const spaVersion = localStorage.getItem(LOCAL_STORAGE_KEY_SPA_VERSION);
    if (spaVersion) {
      config.headers[HTTP_HEADER_KEY_SPA_VERSION] = spaVersion;
    }

    cache.config.maxAge = 0;
  });

  ctx.$axios.onResponseError(error => {
    if (ctx.$axios.isCancel(error)) {
      return Promise.resolve(error);
    }
    const status = error.response?.status;
    if (status === StatusCodes.BAD_REQUEST || status === StatusCodes.UNAUTHORIZED || status === StatusCodes.FORBIDDEN) {
      return Promise.resolve(error.response);
    }
    return Promise.reject(error);
  });

  ctx.$axios.onResponse(response => {
    if (response.status === StatusCodes.BAD_REQUEST) {
      const code = response.data?.error?.code;
      if (code === 'AmbiguousApiVersion' || code === 'ApiVersionUnspecified' || code === 'InvalidApiVersion' || code === 'UnsupportedApiVersion') {
        ctx.store.commit('versioning/updatablex', true);
      }
    }
    return Promise.resolve(response);
  });

  ctx.$axios.onResponse(response => {
    if (response.status === StatusCodes.UNAUTHORIZED || response.status === StatusCodes.FORBIDDEN) {
      ctx.$auth.setUser(undefined);
      ctx.$auth.redirect('login');
    }
    return Promise.resolve(response);
  });

  ctx.$axios.onResponse(async response => {
    if (response.status === StatusCodes.BAD_REQUEST) {
      if (response.request?.responseType === 'arraybuffer' && response.headers['content-type'].startsWith('application/problem+json')) {
        const content = await new Promise((resolve, reject) => {
          const reader = new FileReader();
          reader.addEventListener('abort', reject);
          reader.addEventListener('error', reject);
          reader.addEventListener('loadend', () => resolve(reader.result));
          reader.readAsText(new Blob([response.data]));
        });
        response.data = JSON.parse(content as string);
      }
    }
    return Promise.resolve(response);
  });
};

export default plugin;
