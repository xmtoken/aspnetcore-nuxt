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
      this.setIconTabindex('append', val);
    },
    appendOuterIconTabindex(val: number): void {
      this.setIconTabindex('append-outer', val);
    },
    clearIconTabindex(val: number): void {
      this.setIconTabindex('clear', val);
    },
    prependIconTabindex(val: number): void {
      this.setIconTabindex('prepend', val);
    },
    prependInnerIconTabindex(val: number): void {
      this.setIconTabindex('prepend-inner', val);
    },
  },
  mounted(): void {
    if (this.appendIconTabindex) {
      this.setIconTabindex('append', this.appendIconTabindex);
    }
    if (this.appendOuterIconTabindex) {
      this.setIconTabindex('append-outer', this.appendOuterIconTabindex);
    }
    if (this.clearIconTabindex) {
      this.setIconTabindex('clear', this.clearIconTabindex);
    }
    if (this.prependIconTabindex) {
      this.setIconTabindex('prepend', this.prependIconTabindex);
    }
    if (this.prependInnerIconTabindex) {
      this.setIconTabindex('prepend-inner', this.prependInnerIconTabindex);
    }
  },
  methods: {
    setIconTabindex(suffix: string, val: number): void {
      const elements = this.$el.querySelectorAll(`.v-input__icon--${suffix} button.v-icon`);
      if (val) {
        elements.forEach(x => x.setAttribute('tabindex', val.toString()));
      } else {
        elements.forEach(x => x.removeAttribute('tabindex'));
      }
    },
  },
});
