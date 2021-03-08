<script lang="ts">
import { ValidationProvider } from 'vee-validate';
import { PropValidator } from 'vue/types/options';
import { VRadioGroup } from 'vuetify/src/components';
import { VueBuilder, VuePropHelper } from '~/core/vue';
import { InputableProps } from '~/mixins/inputable';
import { RequiredMarkable } from '~/mixins/required-markable';
import { Slotable } from '~/mixins/slotable';
import { UIElementState } from '~/mixins/ui-element-state';
import { Validatable } from '~/mixins/validatable';

type ComponentProps = Record<string, any> &
  InputableProps & {
    mandatory?: boolean;
    value?: any;
  };

type ComponentRefs = {
  field: InstanceType<typeof VRadioGroup>;
};

const Vue = VueBuilder.create() //
  .$attrs<ComponentProps>()
  .$refs<ComponentRefs>()
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
    'props.value': {
      handler(val: any, _oldVal: any) {
        this.value = this.valueConverter ? this.valueConverter(val) : val;
        if (val !== this.value) {
          this.$emit(this.$options.model!.event!, this.value);
        }
      },
      immediate: true,
    },
  },
  mounted() {
    const el = this.$el as HTMLElement;
    el.addEventListener('focusin', () => {
      this.$refs.field.isFocused = true;
    });
    el.addEventListener('focusout', ev => {
      if (this.$el.contains(ev.relatedTarget as Node)) {
        return;
      }
      this.$refs.field.isFocused = false;
    });
  },
  methods: {
    classes(required: boolean) {
      return {
        required,
        'required-marker': required && !VuePropHelper.toBoolean(this.disabledRequiredMarker),
        'v-input--tooltip-details': this.isEnabledTooltipMessage,
      };
    },
  },
});
</script>

<template>
  <validation-provider v-slot="{ errors, required }" v-bind="validationProviderProps">
    <v-radio-group ref="field" v-bind="props" :class="classes(required)" :error-messages="errors" :value="value" v-on="$listeners">
      <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
      <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
        <slot v-bind="scope" :name="scopedSlotKey" />
      </template>
    </v-radio-group>
  </validation-provider>
</template>

<style lang="scss" scoped>
.v-input--radio-group.v-input--is-focused ::v-deep {
  .v-radio:not(.v-radio--is-focused) {
    .v-input--selection-controls__ripple::before {
      background: initial !important;
      transform: initial !important;
    }
  }
}
</style>
