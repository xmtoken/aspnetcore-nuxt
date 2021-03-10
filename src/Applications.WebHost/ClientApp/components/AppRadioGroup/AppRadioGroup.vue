<script lang="ts">
import { ValidationProvider } from 'vee-validate';
import { PropValidator } from 'vue/types/options';
import { VueBuilder, VuePropHelper } from '~/core/vue';
import { Inputable, InputableProxyProps } from '~/mixins/inputable';
import { RequiredMarkable } from '~/mixins/required-markable';
import { Slotable } from '~/mixins/slotable';
import { UIElementState } from '~/mixins/ui-element-state';
import { Validatable, ValidatableProxyProps } from '~/mixins/validatable';

type ComponentProxyProps = Record<string, any> & //
  InputableProxyProps &
  ValidatableProxyProps & {
    mandatory?: boolean;
    value?: any;
  };

type ComponentRefs = {
  field: Element;
};

const Vue = VueBuilder.create() //
  .$attrs<ComponentProxyProps>()
  .$refs<ComponentRefs>()
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
    fitContent: {
      default: true,
      type: Boolean,
    },
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
      const defaults: ComponentProxyProps = {
        mandatory: true,
      };
      const attrs: ComponentProxyProps = {
        ...defaults,
        ...this.attrs,
      };
      const overrides: ComponentProxyProps = {
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
        'fit-content': this.fitContent,
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
  <validation-provider v-slot="{ errors, failed, required }" v-bind="validationProviderProps">
    <v-radio-group ref="field" v-bind="props" :class="classes(required)" :error-messages="errors" :value="value" v-on="$listeners">
      <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
      <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
        <slot v-bind="scope" :name="scopedSlotKey" />
      </template>
      <!-- <template #append>
        <v-icon v-if="failed && isEnabledTooltipMessage" color="error" small>
          {{ validationErrorIcon }}
        </v-icon>
        <slot name="append" />
      </template> -->
      <template #message="scope">
        <v-tooltip v-if="isEnabledTooltipMessage" :activator="$refs.field" color="error" :open-on-hover="false" top :value="focused || hovered">
          {{ scope.message }}
        </v-tooltip>
        <slot v-bind="scope" name="message" />
      </template>
    </v-radio-group>
  </validation-provider>
</template>

<style lang="scss" scoped>
@import '~/components/AppInput/AppInput.scss';

.fit-content {
  width: fit-content;
}

.v-input--dense-x {
  padding-bottom: 1px;
  padding-top: 1px;
}
</style>
