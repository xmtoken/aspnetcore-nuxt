export default {
  build: {
    extend(config, { isDev }) {
      if (isDev) {
        config.devtool = 'source-map';
      }
    },
  },
  buildModules: [
    //
    '@nuxtjs/vuetify',
  ],
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
    '~/plugins/vee-validate',
  ],
  router: {
    middleware: ['auth'],
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
