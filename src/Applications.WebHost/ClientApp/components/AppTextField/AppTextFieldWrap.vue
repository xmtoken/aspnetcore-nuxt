<script lang="ts">
import '~/components/AppInput/AppInput.scss';
import { validate } from 'vee-validate';
import { PropType } from 'vue';
import { VueBuilder } from '~/core/vue';

const Vue = VueBuilder.create() //
  .build();

export default Vue.extend({
  inheritAttrs: false,
  inject: ['$_veeObserver'],
  data() {
    return {
      id: Date.now(),
      errors: [] as string[],
      focused: false,
      flags: {},
      inputattrs: {} as any,
    };
  },
  computed: {
    inputScopeProps(): object {
      return {
        attrs: {
          autofocus: true,
          errors: this.errors,
        },
        on: {
          blur: () => (this.focused = false),
          'update:error-messages': (errors: string[]) => (this.errors = errors),
          xxx: (attrs: any) => (this.inputattrs = attrs),
        },
      } as object;
    },
    labelScopeProps(): object {
      return {
        attrs: {
          ...this.inputattrs,
          errors: this.errors,
          tabindex: 0,
        },
        on: {
          focus: () => (this.focused = true),
        },
      } as object;
    },
  },
  created() {
    console.log('this.$_veeObserver', this.$_veeObserver);
    this.$_veeObserver.observe(this);
  },
  beforeDestroy() {
    this.$_veeObserver.unobserve(this.id);
  },
  methods: {
    validate() {
      //
      console.log('validate');
      this.errors = ['xxsasd', 'fajnjofnak'];
      return validate(null, 'required', {});
    },
    validateSilent() {
      //
      console.log('validateSilent');
      return validate(null, {}, {});
    },
  },
});
</script>

<template>
  <app-scope v-slot>
    <slot v-if="focused" v-bind="inputScopeProps" name="input" />
    <slot v-else v-bind="labelScopeProps" name="label" />
  </app-scope>
</template>
