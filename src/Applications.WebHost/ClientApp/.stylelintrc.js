module.exports = {
  root: true,
  extends: [
    //
    'stylelint-config-standard',
    'stylelint-config-recommended-scss',
    'stylelint-prettier/recommended',
  ],
  plugins: [
    //
    'stylelint-scss',
    'stylelint-order',
  ],
  rules: {
    'order/properties-alphabetical-order': true,
    'selector-pseudo-element-no-unknown': [true, { ignorePseudoElements: ['v-deep'] }],
  },
};
