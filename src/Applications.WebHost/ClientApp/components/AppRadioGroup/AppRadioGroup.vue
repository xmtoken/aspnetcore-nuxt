<script lang="ts">
import { ValidationProvider } from 'vee-validate';
import { PropValidator } from 'vue/types/options';
import { VueBuilder, VuePropHelper } from '~/core/vue';
import { Inputable, InputableProps } from '~/mixins/inputable';
import { RequiredMarkable } from '~/mixins/required-markable';
import { Slotable } from '~/mixins/slotable';
import { UIElementState } from '~/mixins/ui-element-state';
import { Validatable } from '~/mixins/validatable';

type ComponentProps = Record<string, any> &
  InputableProps & {
    mandatory?: boolean;
    value?: any;
  };

const Vue = VueBuilder.create() //
  .$attrs<ComponentProps>()
  .mixin(Inputable)
  .mixin(RequiredMarkable)
  .mixin(Slotable)
  .mixin(UIElementState)
  .mixin(Validatable)
  .build();

export default Vue.extend({
  components: {
    ValidationProvider,
  },
  inheritAttrs: false,
  model: {
    event: 'change',
    prop: 'value',
  },
  props: {
    valueConverter: {
      default: undefined,
      type: Function,
    } as PropValidator<(val: any) => any>,
  },
  data() {
    return {
      created: false,
      value: null as any,
    };
  },
  computed: {
    props() {
      const defaults: ComponentProps = {
        mandatory: true,
      };
      const attrs: ComponentProps = {
        ...defaults,
        ...this.attrs,
      };
      const overrides: ComponentProps = {
        dense: VuePropHelper.toBoolean(attrs.dense) || this.denseX,
        hideDetails: attrs.hideDetails === 'auto' || attrs.hideDetails === 'tooltip' ? 'auto' : VuePropHelper.toBoolean(attrs.hideDetails),
      };
      delete attrs[this.$options.model!.prop!];
      return {
        ...attrs,
        ...overrides,
      };
    },
  },
  watch: {
    'attrs.value': {
      handler(val: any, _oldVal: any) {
        this.value = this.valueConverter ? this.valueConverter(val) : val;
        if (!this.created && this.value !== val) {
          this.$emit(this.$options.model!.event!, this.value);
        }
      },
      immediate: true,
    },
  },
  created() {
    this.created = true;
  },
  methods: {
    classes(required: boolean) {
      return {
        required,
        'required-marker': required && !this.disabledRequiredMarker,
        'v-input--dense-x': this.denseX,
        'v-input--tooltip-details': this.isEnabledTooltipMessage,
      };
    },
  },
});
</script>

<template>
  <validation-provider v-slot="{ errors, required }" v-bind="validationProviderProps">
    <v-radio-group v-bind="props" :class="classes(required)" :error-messages="errors" :value="value" v-on="$listeners">
      <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
      <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
        <slot v-bind="scope" :name="scopedSlotKey" />
      </template>
    </v-radio-group>
  </validation-provider>
</template>

<style lang="scss" scoped>
@import '~/components/AppInput/AppInput.scss';

.v-input--dense-x {
  padding-bottom: 1px;
  padding-top: 1px;
}
</style>
