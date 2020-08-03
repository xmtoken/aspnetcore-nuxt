import { Middleware } from '@nuxt/types';
import { AppVersion } from '~/types/api';

const LOCAL_STORAGE_KEY_API_VERSION = 'api-version';
const LOCAL_STORAGE_KEY_SPA_VERSION = 'spa-version';

const middleware: Middleware = async ({ $axios, route }) => {
  const data = await $axios.$get<AppVersion>('/versioning', { params: { v: Date.now() } });
  if (!data.apiVersion || !data.spaVersion) {
    return;
  }

  const apiVersion = localStorage.getItem(LOCAL_STORAGE_KEY_API_VERSION);
  const spaVersion = localStorage.getItem(LOCAL_STORAGE_KEY_SPA_VERSION);
  if (apiVersion === data.apiVersion && spaVersion === data.spaVersion) {
    return;
  }

  localStorage.setItem(LOCAL_STORAGE_KEY_API_VERSION, data.apiVersion);
  localStorage.setItem(LOCAL_STORAGE_KEY_SPA_VERSION, data.spaVersion);
  history.pushState(null, '', route.fullPath);
  location.reload(true);
};

export default middleware;
