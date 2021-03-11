<script lang="ts">
import { mdiCalendar } from '@mdi/js';
import { PropType } from 'vue';
import { AppDatePickerProps } from '~/components/AppDatePicker/AppDatePicker.vue';
import { AppMenuProps } from '~/components/AppMenu/AppMenu.vue';
import { AppTextFieldProps } from '~/components/AppTextField/AppTextField.vue';
import { VueBuilder, VuePropHelper } from '~/core/vue';
import * as DateFormatter from '~/extensions/formatters/date-formatter';
import { Slotable } from '~/mixins/slotable';
import { Listeners } from '~/types/vue';

type ComponentProxyProps = Record<string, any> & //
  AppTextFieldProps;

export type AppTextDateFieldProps = ComponentProxyProps & {
  menuOffsetY?: boolean;
  menuProps?: AppMenuProps;
  pickerProps?: AppDatePickerProps;
};

const Vue = VueBuilder.create() //
  .$attrs<ComponentProxyProps>()
  .mixin(Slotable)
  .build();

export default Vue.extend({
  inheritAttrs: false,
  model: {
    event: 'input:value',
  },
  props: {
    menuOffsetY: {
      default: true,
      type: Boolean,
    },
    menuProps: {
      default(): AppMenuProps {
        return {};
      },
      type: Object as PropType<AppMenuProps>,
    },
    pickerProps: {
      default(): AppDatePickerProps {
        return {};
      },
      type: Object as PropType<AppDatePickerProps>,
    },
  },
  data() {
    return {
      // internalValue: this.value,
      menu: false,
    };
  },
  computed: {
    props() {
      const defaults: ComponentProxyProps = {
        appendIcon: mdiCalendar,
        appendIconTabindex: -1,
      };
      const attrs: ComponentProxyProps = {
        ...defaults,
        ...this.attrs,
      };
      const overrides: ComponentProxyProps = {
        //
      };
      return {
        ...attrs,
        ...overrides,
      };
    },
    menuPropsInternal() {
      const defaults: AppMenuProps = {
        minWidth: 'inherit',
        openOnClick: false,
      };
      const attrs: AppMenuProps = {
        ...defaults,
        ...this.menuProps,
      };
      const overrides: AppMenuProps = {
        disabled: VuePropHelper.toBoolean(this.props.disabled) || VuePropHelper.toBoolean(this.props.readonly),
        nudgeBottom: this.menuOffsetY ? (VuePropHelper.toBoolean(this.props.dense) || VuePropHelper.toBoolean(this.props.denseX) ? 27 : 45) : undefined,
      };
      return {
        ...attrs,
        ...overrides,
      };
    },

    // appendIconInternal(): string | null {
    //   return this.disabled || this.readonly ? null : this.appendIcon;
    // },
    // listeners(): Listeners {
    //   const listeners = { ...this.$listeners };
    //   delete listeners['input:value'];
    //   return listeners;
    // },
    // menuNudgeBottom(): number {
    //   return this.pickerOffsetY ? (this.dense ? 29 : 45) : 0;
    // },
    // menuNudgeLeft(): number {
    //   return this.pickerOffsetLeft ? 290 /* menu-width */ + 5 /* space */ : 0;
    // },
    // pickerValue: {
    //   get(): string | null {
    //     const format = this.pickerProps.type === 'month' ? 'YYYY-MM' : 'YYYY-MM-DD';
    //     return DateFormatter.isValid(this.internalValue) ? DateFormatter.format(this.internalValue, format) : null;
    //   },
    //   set(val: string | null): void {
    //     this.internalValue = val;
    //   },
    // },
  },
  // watch: {
  //   internalValue(val: any): void {
  //     this.$emit('input:value', val);
  //   },
  //   value(val: any): void {
  //     this.internalValue = val;
  //   },
  // },
  // methods: {
  //   onBlur(): void {
  //     if (this.pickerValue) {
  //       this.internalValue = this.pickerValue;
  //     }
  //   },
  //   onChange(): void {
  //     if (this.pickerValue) {
  //       this.internalValue = this.pickerValue;
  //     }
  //   },
  //   openPickerMenu(): void {
  //     this.menu = true;
  //   },
  // },
});
</script>

<template>
  <app-text-field ref="field" v-bind="props" v-on="$listeners" @click:append="menu = true">
    <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
    <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
      <slot v-bind="scope" :name="scopedSlotKey" />
    </template>
    <template v-slot:label>
      <app-menu v-model="menu" v-bind="menuPropsInternal" :activator="$refs.field">
        <template v-slot="{ close }">
          <app-date-picker v-bind="pickerProps" @change="close" />
        </template>
      </app-menu>
      <slot name="label">
        {{ menu }}
        <!-- {{ label }} -->
      </slot>
    </template>
  </app-text-field>
</template>
