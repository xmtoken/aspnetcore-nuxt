const LOCAL_STORAGE_KEY_API_VERSION = 'api-version';
const LOCAL_STORAGE_KEY_SPA_VERSION = 'spa-version';
export default async function ({ $axios, route }) {
  const data = await $axios.$get('/versioning', { params: { v: Date.now() } });

  const apiVersion = localStorage.getItem(LOCAL_STORAGE_KEY_API_VERSION);
  const spaVersion = localStorage.getItem(LOCAL_STORAGE_KEY_SPA_VERSION);
  if (apiVersion === data.apiVersion && spaVersion === data.spaVersion) {
    return;
  }

  localStorage.setItem(LOCAL_STORAGE_KEY_API_VERSION, data.apiVersion);
  localStorage.setItem(LOCAL_STORAGE_KEY_SPA_VERSION, data.spaVersion);
  history.pushState(null, null, route.fullPath);
  location.reload(true);
}
