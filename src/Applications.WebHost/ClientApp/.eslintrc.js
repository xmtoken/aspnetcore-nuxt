module.exports = {
  root: true,
  ignorePatterns: [
    //
    '/**/node_modules/**',
    '.eslintrc.*',
    '.prettierrc.*',
    '.stylelintrc.*',
  ],
  parserOptions: {
    parser: 'babel-eslint',
  },
  extends: [
    //
    'eslint:recommended',
    'plugin:vue/recommended',
    'plugin:prettier/recommended',
  ],
  plugins: ['vue'],
  rules: {
    'no-var': ['error'],

    'vue/attributes-order': ['error', { alphabetical: true }],

    'vue/component-tags-order': ['error'],

    'vue/html-self-closing': [
      'error',
      {
        html: {
          void: 'always',
        },
      },
    ],

    'vue/max-attributes-per-line': ['off'], // for prettier : https://github.com/prettier/prettier/pull/6644

    'vue/order-in-components': [
      'error',
      {
        order: [
          //
          'head', // for nuxt
          'layout', // for nuxt
          'auth', // for nuxt-auth
          'el',
          'name',
          'parent',
          'functional',
          'delimiters',
          'comments',
          'components',
          'directives',
          'filters',
          'extends',
          'mixins',
          'inheritAttrs',
          'model',
          'props',
          'propsData',
          'validate', // for nuxt
          'fetch', // for nuxt
          'asyncData', // for nuxt
          'data',
          'computed',
          'watch',
          'watchQuery', // for nuxt
          'beforeCreate',
          'created',
          'beforeMount',
          'mounted',
          'beforeUpdate',
          'updated',
          'activated',
          'deactivated',
          'beforeDestroy',
          'destroyed',
          'methods',
          'template',
          'render',
          'renderError',
        ],
      },
    ],
  },
};
