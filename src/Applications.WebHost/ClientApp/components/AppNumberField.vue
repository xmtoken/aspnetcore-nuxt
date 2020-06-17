<script>
import AppTextField from './AppTextField';
import numbro from 'numbro';
export default {
  components: {
    AppTextField,
  },
  inheritAttrs: false,
  props: {
    format: {
      default: '0',
      type: String,
    },
    value: {
      default: undefined,
      type: undefined,
    },
  },
  data() {
    return {
      focused: false,
      model: this.toFormatted(this.value),
    };
  },
  computed: {
    /** @returns {String} */
    formattedText() {
      return this.toFormatted(this.model);
    },
    /** @returns {Object} */
    listeners() {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      return listeners;
    },
  },
  watch: {
    value(val) {
      const formatted = this.toFormatted(val);
      if (this.focused && this.formattedText === formatted) {
        return;
      }
      this.model = formatted;
    },
  },
  methods: {
    onBlue() {
      this.focused = false;
      this.model = this.formattedText;
    },
    onFocus() {
      this.focused = true;
    },
    onInput() {
      this.$emit('input', numbro.validate(this.formattedText, {}) ? numbro(this.formattedText).value() : this.model);
    },
    toFormatted(val) {
      return numbro.validate(val, {}) ? numbro(val).format(this.format) : val;
    },
  },
};
</script>

<template>
  <app-text-field v-model="model" v-bind="$attrs" v-on="listeners" @blur="onBlue" @focus="onFocus" @input="onInput">
    <slot v-for="slot in Object.keys($slots)" :slot="slot" :name="slot" />
    <template v-for="slot in Object.keys($scopedSlots)" :slot="slot" slot-scope="scope">
      <slot v-bind="scope" :name="slot" />
    </template>
  </app-text-field>
</template>

<style lang="scss" scoped>
.v-input ::v-deep {
  input {
    text-align: right;
  }
}
</style>
