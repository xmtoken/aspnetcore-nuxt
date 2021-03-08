import { Plugin } from '@nuxt/types';

const plugin: Plugin = ctx => {
  window.onunhandledrejection = (ev: PromiseRejectionEvent) => {
    console.log('onunhandledrejection', ev);
  };

  ctx.app.errorCaptured = (err, vm, info) => {
    console.log('err', err);
    console.log('vm', vm);
    console.log('info', info);
    return false;
  };
};

export default plugin;
