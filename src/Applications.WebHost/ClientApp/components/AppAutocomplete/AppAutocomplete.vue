<script lang="ts">
import { ValidationProvider } from 'vee-validate';
import { VueBuilder, VuePropHelper } from '~/core/vue';
import { Clearable, ClearableProps } from '~/mixins/clearable';
import { Iconable, IconableProps } from '~/mixins/iconable';
import { Inputable, InputableProps } from '~/mixins/inputable';
import { RequiredMarkable, RequiredMarkableProps } from '~/mixins/required-markable';
import { Slotable } from '~/mixins/slotable';
import { UIElementState } from '~/mixins/ui-element-state';
import { Validatable, ValidatableProxyProps } from '~/mixins/validatable';
import { ProxyProps } from '~/types/global';

type ComponentProxyProps = ProxyProps &
  ClearableProps &
  IconableProps &
  InputableProps &
  RequiredMarkableProps &
  ValidatableProxyProps & {
    //
  };

type ComponentProps = ComponentProxyProps & {
  //
};

export type AppAutocompleteProps = ComponentProps;

type ComponentRefs = {
  field: Element;
};

const Vue = VueBuilder.create() //
  .$attrs<ComponentProxyProps>()
  .$refs<ComponentRefs>()
  .mixin(Clearable)
  .mixin(Iconable)
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
  computed: {
    props() {
      const defaults: ComponentProxyProps = {
        //
      };
      const attrs: ComponentProxyProps & Record<string, any> = {
        ...defaults,
        ...this.attrs,
      };
      const overrides: ComponentProxyProps = {
        clearable: VuePropHelper.toBoolean(attrs.clearable) && !VuePropHelper.toBoolean(attrs.readonly),
        dense: VuePropHelper.toBoolean(attrs.dense) || this.denseX,
        hideDetails: attrs.hideDetails === 'auto' || attrs.hideDetails === 'tooltip' ? 'auto' : VuePropHelper.toBoolean(attrs.hideDetails),
      };
      return {
        ...attrs,
        ...overrides,
      };
    },
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
  <validation-provider v-slot="{ errors, failed, required }" v-bind="validationProviderProps">
    <v-autocomplete ref="field" v-bind="props" :class="classes(required)" :error-messages="errors" v-on="$listeners">
      <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
      <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
        <slot v-bind="scope" :name="scopedSlotKey" />
      </template>
      <!-- <template #append-outer>
        <v-icon v-if="failed && isEnabledTooltipMessage" color="error" small>
          {{ validationErrorIcon }}
        </v-icon>
        <slot name="append-outer" />
      </template> -->
      <template #message="scope">
        <v-tooltip v-if="isEnabledTooltipMessage" :activator="$refs.field" color="error" :open-on-hover="false" top :value="focused || hovered">
          {{ scope.message }}
        </v-tooltip>
        <slot v-bind="scope" name="message" />
      </template>
    </v-autocomplete>
  </validation-provider>
</template>

<style lang="scss" scoped>
@import '~/components/AppInput/AppInput.scss';
</style>
