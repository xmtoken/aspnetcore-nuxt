import Vue from 'vue';

export default Vue.extend({
  props: {
    appendIconTabindex: {
      default: 0,
      type: Number,
    },
    appendOuterIconTabindex: {
      default: 0,
      type: Number,
    },
    clearIconTabindex: {
      default: 0,
      type: Number,
    },
    prependIconTabindex: {
      default: 0,
      type: Number,
    },
    prependInnerIconTabindex: {
      default: 0,
      type: Number,
    },
  },
  watch: {
    appendIconTabindex(val: number): void {
      setIconTabindex(this.$el, 'append', val);
    },
    appendOuterIconTabindex(val: number): void {
      setIconTabindex(this.$el, 'append-outer', val);
    },
    clearIconTabindex(val: number): void {
      setIconTabindex(this.$el, 'clear', val);
    },
    prependIconTabindex(val: number): void {
      setIconTabindex(this.$el, 'prepend', val);
    },
    prependInnerIconTabindex(val: number): void {
      setIconTabindex(this.$el, 'prepend-inner', val);
    },
  },
  mounted(): void {
    if (this.appendIconTabindex) {
      setIconTabindex(this.$el, 'append', this.appendIconTabindex);
    }
    if (this.appendOuterIconTabindex) {
      setIconTabindex(this.$el, 'append-outer', this.appendOuterIconTabindex);
    }
    if (this.clearIconTabindex) {
      setIconTabindex(this.$el, 'clear', this.clearIconTabindex);
    }
    if (this.prependIconTabindex) {
      setIconTabindex(this.$el, 'prepend', this.prependIconTabindex);
    }
    if (this.prependInnerIconTabindex) {
      setIconTabindex(this.$el, 'prepend-inner', this.prependInnerIconTabindex);
    }
  },
});

function setIconTabindex(el: Element, suffix: string, val: number) {
  const elements = el.querySelectorAll(`.v-input__icon--${suffix} button.v-icon`);
  if (val) {
    elements.forEach(x => x.setAttribute('tabindex', val.toString()));
  } else {
    elements.forEach(x => x.removeAttribute('tabindex'));
  }
}
