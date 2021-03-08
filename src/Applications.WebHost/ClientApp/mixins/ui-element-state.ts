import { VueBuilder } from '~/core/vue';

const Vue = VueBuilder.create() //
  .build();

export const UIElementState = Vue.extend({
  data() {
    return {
      focused: false,
      hovered: false,
    };
  },
  mounted() {
    this.$el.querySelector('input')?.addEventListener('focus', () => {
      this.focused = true;
    });
    this.$el.querySelector('input')?.addEventListener('blur', () => {
      this.focused = false;
    });
    this.$el.addEventListener('mouseenter', () => {
      this.hovered = true;
    });
    this.$el.addEventListener('mouseleave', () => {
      this.hovered = false;
    });
  },
});
