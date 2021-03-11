import { VueBuilder } from '~/core/vue';

type IconableProxyProps = {
  appendIcon?: string;
  appendOuterIcon?: string;
  clearIcon?: string;
  prependIcon?: string;
  prependInnerIcon?: string;
};

export type IconableProps = IconableProxyProps & {
  appendIconTabindex?: number;
  appendOuterIconTabindex?: number;
  clearIconTabindex?: number;
  prependIconTabindex?: number;
  prependInnerIconTabindex?: number;
};

const Vue = VueBuilder.create() //
  .$attrs<IconableProxyProps>()
  .build();

export const Iconable = Vue.extend({
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
    appendIcon(_val: string, _oldVal: string) {
      setIconTabindex(this.$el, 'append', this.appendIconTabindex);
    },
    appendIconTabindex(val: number, _oldVal: number) {
      setIconTabindex(this.$el, 'append', val);
    },
    appendOuterIcon(_val: string, _oldVal: string) {
      setIconTabindex(this.$el, 'append-outer', this.appendOuterIconTabindex);
    },
    appendOuterIconTabindex(val: number, _oldVal: number) {
      setIconTabindex(this.$el, 'append-outer', val);
    },
    clearIcon(_val: string, _oldVal: string) {
      setIconTabindex(this.$el, 'clear', this.clearIconTabindex);
    },
    clearIconTabindex(val: number, _oldVal: number) {
      setIconTabindex(this.$el, 'clear', val);
    },
    prependIcon(_val: string, _oldVal: string) {
      setIconTabindex(this.$el, 'prepend', this.prependIconTabindex);
    },
    prependIconTabindex(val: number, _oldVal: number) {
      setIconTabindex(this.$el, 'prepend', val);
    },
    prependInnerIcon(_val: string, _oldVal: string) {
      setIconTabindex(this.$el, 'prepend-inner', this.prependInnerIconTabindex);
    },
    prependInnerIconTabindex(val: number, _oldVal: number) {
      setIconTabindex(this.$el, 'prepend-inner', val);
    },
  },
  mounted() {
    setIconTabindex(this.$el, 'append', this.appendIconTabindex);
    setIconTabindex(this.$el, 'append-outer', this.appendOuterIconTabindex);
    setIconTabindex(this.$el, 'clear', this.clearIconTabindex);
    setIconTabindex(this.$el, 'prepend', this.prependIconTabindex);
    setIconTabindex(this.$el, 'prepend-inner', this.prependInnerIconTabindex);
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
