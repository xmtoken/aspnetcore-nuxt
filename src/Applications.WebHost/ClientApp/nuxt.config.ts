import { NuxtConfig } from '@nuxt/types';

const config: NuxtConfig = {
  // typescript: {
  //   typeCheck: {
  //     eslint: {
  //       files: './**/*.{js,ts,vue}',
  //     },
  //   },
  // },
  build: {
    extend(config, { isDev }): void {
      if (isDev) {
        config.devtool = 'source-map';
      }
    },
  },
  buildModules: [
    //
    '@nuxt/typescript-build',
    '@nuxtjs/vuetify',
  ],
  components: true,
  css: ['@/assets/main.scss'],
  env: {},
  head: {
    titleTemplate(title) {
      return title ? `${title} - AspNetCoreNuxt` : 'AspNetCoreNuxt';
    },
    htmlAttrs: {
      lang: 'ja',
    },
    meta: [
      //
      { charset: 'utf-8' },
      { 'http-equiv': 'x-ua-compatible', content: 'ie=edge' },
      { name: 'description', content: '' },
      { name: 'format-detection', content: 'address=no,email=no,telephone=no' },
      { name: 'referrer', content: 'no-referrer' },
      { name: 'robots', content: 'none' },
      { name: 'viewport', content: 'width=device-width' },
    ],
    link: [
      //
      { rel: 'stylesheet', href: 'https://fonts.googleapis.com/css2?family=Noto+Sans+JP:wght@300;400;500&display=swap' },
    ],
  },
  mode: 'spa',
  modules: [
    //
    '@nuxtjs/auth',
    '@nuxtjs/axios',
  ],
  plugins: [
    //
    '~/plugins/axios',
    '~/plugins/injection',
    '~/plugins/vee-validate',
  ],
  router: {
    middleware: [
      //
      'versioning',
      'auth',
    ],
  },
  telemetry: false,
  auth: {
    cookie: false,
    localStorage: false,
    redirect: {
      callback: false,
      home: '/',
      login: '/sign-in',
      logout: '/sign-in',
    },
    strategies: {
      local: {
        autoFetchUser: false,
        tokenRequired: false,
        endpoints: {
          login: { url: '/account/sign-in', method: 'post' },
          logout: { url: '/account/sign-out', method: 'post' },
          user: { url: '/account', method: 'get', propertyName: false },
        },
      },
    },
  },
  axios: {
    baseURL: '/api',
  },
  vuetify: {
    defaultAssets: {
      font: false,
      icons: false,
    },
    icons: {
      iconfont: 'mdiSvg',
    },
    lang: {
      locales: {
        en: {
          dataIterator: {
            loadingText: '',
          },
          noDataText: '',
        },
      },
    },
  },
};

export default config;