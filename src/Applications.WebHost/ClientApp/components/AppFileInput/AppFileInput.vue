<script lang="ts">
import '~/components/AppInput/AppInput.scss';
import { ValidationProvider } from 'vee-validate';
import { VueBuilder, VuePropHelper } from '~/core/vue';
import { Clearable, ClearableProps } from '~/mixins/clearable';
import { IconTabIndexable } from '~/mixins/icon-tab-indexable';
import { InputableProps } from '~/mixins/inputable';
import { RequiredMarkable } from '~/mixins/required-markable';
import { Slotable } from '~/mixins/slotable';
import { UIElementState } from '~/mixins/ui-element-state';
import { Validatable } from '~/mixins/validatable';

type ComponentProps = Record<string, any> & ClearableProps & InputableProps;

type ComponentRefs = {
  field: Element;
};

const Vue = VueBuilder.create() //
  .$attrs<ComponentProps>()
  .$refs<ComponentRefs>()
  .mixin(Clearable)
  .mixin(IconTabIndexable)
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
  computed: {
    props() {
      const defaults: ComponentProps = {
        //
      };
      const attrs: ComponentProps = {
        ...defaults,
        ...this.attrs,
      };
      const overrides: ComponentProps = {
        clearable: VuePropHelper.toBoolean(attrs.clearable) && !VuePropHelper.toBoolean(attrs.readonly),
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
        'required-marker': required && !VuePropHelper.toBoolean(this.disabledRequiredMarker),
        'v-input--tooltip-details': this.isEnabledTooltipMessage,
      };
    },
  },
});
</script>

<template>
  <validation-provider v-slot="{ errors, failed, required }" v-bind="validationProviderProps">
    <v-file-input ref="field" v-bind="props" :class="classes(required)" :error-messages="errors" v-on="$listeners">
      <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
      <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
        <slot v-bind="scope" :name="scopedSlotKey" />
      </template>
      <template v-if="failed && isEnabledTooltipMessage" #append-outer>
        <v-icon color="error" small>
          {{ validationErrorIcon }}
        </v-icon>
        <slot name="append-outer" />
      </template>
      <template v-if="isEnabledTooltipMessage" #message="scope">
        <v-tooltip :activator="$refs.field" :open-on-hover="false" top :value="focused || hovered">
          {{ scope.message }}
        </v-tooltip>
        <slot v-bind="scope" name="message" />
      </template>
    </v-file-input>
  </validation-provider>
</template>
