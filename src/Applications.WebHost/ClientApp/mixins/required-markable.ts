import Vue from 'vue';

export default Vue.extend({
  props: {
    disabledRequiredMarker: {
      default: false,
      type: Boolean,
    },
  },
});
