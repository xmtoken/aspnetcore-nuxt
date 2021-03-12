import { NuxtConfig } from '@nuxt/types';
import dotenv from 'dotenv';

dotenv.config({ path: `.env.${process.env.ENVIRONMENT}` });

const config: NuxtConfig = {
  // typescript: {
  //   typeCheck: {
  //     eslint: {
  //       files: './**/*.{js,ts,vue}',
  //     },
  //   },
  // },
  build: {
    extend(config, { isDev }) {
      if (isDev) {
        config.devtool = 'source-map';
      }
      config.module?.rules.push({ loader: 'ts-nameof-loader', test: /\.(ts|vue)$/ });
    },
  },
  buildModules: [
    '@nuxt/typescript-build',
    '@nuxtjs/dotenv',
    '@nuxtjs/vuetify',
  ],
  components: true,
  css: ['@/assets/main.scss'],
  head: {
    htmlAttrs: {
      lang: 'ja',
    },
    link: [
      { rel: 'stylesheet', href: 'https://fonts.googleapis.com/css2?family=Noto+Sans+JP:wght@300;400;500&display=swap' },
    ],
    meta: [
      { charset: 'utf-8' },
      { httpEquiv: 'x-ua-compatible', content: 'ie=edge' },
      { name: 'description', content: '' },
      { name: 'format-detection', content: 'address=no,email=no,telephone=no' },
      { name: 'referrer', content: 'no-referrer' },
      { name: 'robots', content: 'none' },
      { name: 'viewport', content: 'width=device-width' },
    ],
    titleTemplate(title) {
      return title ? `${title} - AspNetCoreNuxt` : process.env.ROUTER_BASE + 'AspNetCoreNuxt';
    },
  },
  modules: [
    '@nuxtjs/auth',
    '@nuxtjs/axios',
  ],
  plugins: [
    '~/plugins/axios',
    '~/plugins/dayjs',
    '~/plugins/error-handler',
    '~/plugins/string-format',
    '~/plugins/vee-validate',
  ],
  router: {
    base: process.env.ROUTER_BASE,
    middleware: [
      'versioning',
      'auth',
    ],
  },
  ssr: false,
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
        endpoints: {
          login: { url: '/account/sign-in', method: 'post' },
          logout: { url: '/account/sign-out', method: 'post' },
          user: { url: '/account', method: 'get', propertyName: false },
        },
        tokenRequired: false,
      },
    },
  },
  axios: {
    baseURL: `${process.env.ROUTER_BASE}/api`,
  },
  vuetify: {
    breakpoint: {
      mobileBreakpoint: 0,
    },
    defaultAssets: {
      font: false,
      icons: false,
    },
    icons: {
      iconfont: 'mdiSvg',
    },
    lang: {
      current: 'ja',
      locales: {
        ja: {
          dataIterator: {
            loadingText: '',
          },
          datePicker: {
            itemsSelected: '',
            nextMonthAriaLabel: '',
            nextYearAriaLabel: '',
            prevMonthAriaLabel: '',
            prevYearAriaLabel: '',
          },
          dataTable: {
            ariaLabel: {
              activateAscending: '',
              activateDescending: '',
              activateNone: '',
              sortAscending: '',
              sortDescending: '',
              sortNone: '',
            },
            sortBy: '',
          },
          noDataText: '',
        },
      },
    },
  },
};

export default config;
