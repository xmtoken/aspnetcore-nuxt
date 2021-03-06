<script lang="ts">
import { mdiMagnify } from '@mdi/js';
import { StatusCodes } from 'http-status-codes';
import { PropType } from 'vue';
import { VueBuilder } from '~/core/vue';
import { Slotable } from '~/mixins/slotable';
import * as Messages from '~/resources/messages';
import { AddressModel } from '~/types/api';
import { Listeners } from '~/types/vue';

const Vue = VueBuilder.create() //
  .mixin(Slotable)
  .build();

export default Vue.extend({
  inheritAttrs: false,
  model: {
    event: 'input:value',
  },
  props: {
    appendIcon: {
      default: mdiMagnify,
      type: String,
    },
    appendIconTabindex: {
      default: -1,
      type: Number,
    },
    contentClass: {
      default: undefined,
      type: [String, Object] as PropType<string | object>,
    },
    contentStyle: {
      default: undefined,
      type: [String, Object] as PropType<string | object>,
    },
    dense: {
      default: false,
      type: Boolean,
    },
    disabled: {
      default: false,
      type: Boolean,
    },
    readonly: {
      default: false,
      type: Boolean,
    },
    value: {
      default: undefined,
      type: (null as any) as PropType<any>,
    },
  },
  data() {
    return {
      internalValue: this.value,
      items: [] as AddressModel[],
      loading: false,
      menu: false,
      selected: [] as AddressModel[],
    };
  },
  computed: {
    appendIconInternal(): string | null {
      return this.disabled || this.readonly ? null : this.appendIcon;
    },
    listeners(): Listeners {
      const listeners = { ...this.$listeners };
      delete listeners['input:value'];
      return listeners;
    },
    menuNudgeBottom(): number {
      return this.dense ? 29 : 45;
    },
  },
  watch: {
    internalValue(val: any): void {
      this.$emit('input:value', val);
    },
    value(val: any): void {
      this.internalValue = val;
    },
  },
  methods: {
    emit(val: AddressModel): void {
      if (val.postalCode) {
        this.$emit('select:address', val.address);
      }
    },
    async onBlur(): Promise<void> {
      if (!this.internalValue) {
        return;
      }
      try {
        this.loading = true;
        const response = await this.$axios.get<AddressModel[]>(`/addresses/${this.internalValue}`);
        if (response.status === StatusCodes.OK) {
          const code = response.data[0].postalCode!;
          this.internalValue = `${code.substr(0, 3)}-${code.substr(3)}`;
        }
      } finally {
        this.loading = false;
      }
    },
    async search(): Promise<void> {
      if (!this.internalValue) {
        return;
      }
      try {
        this.loading = true;
        const response = await this.$axios.get<AddressModel[]>(`/addresses/${this.internalValue}`);
        switch (response.status) {
          case StatusCodes.OK:
            this.items.splice(0, this.items.length, ...response.data);
            break;
          case StatusCodes.NO_CONTENT:
            this.items.splice(0, this.items.length, { address: Messages.NOT_FOUND_POSTAL_CODE } as AddressModel);
            break;
        }
        this.selected = [];
        this.menu = true;
      } finally {
        this.loading = false;
      }
    },
  },
});
</script>

<template>
  <v-menu v-model="menu" :nudge-bottom="menuNudgeBottom" :open-on-click="false">
    <template v-slot:activator="{}">
      <app-text-field v-model="internalValue" v-bind="$attrs" :append-icon="appendIconInternal" :append-icon-tabindex="appendIconTabindex" :class="contentClass" :dense="dense" :disabled="disabled" :loading="loading" :readonly="readonly" :style="contentStyle" v-on="listeners" @blur="onBlur" @click:append="search">
        <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
        <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
          <slot v-bind="scope" :name="scopedSlotKey" />
        </template>
      </app-text-field>
    </template>
    <v-list dense>
      <v-list-item-group v-model="selected">
        <v-list-item v-for="(item, i) in items" :key="i">
          <v-list-item-content @click="emit(item)">
            {{ item.address }}
          </v-list-item-content>
        </v-list-item>
      </v-list-item-group>
    </v-list>
  </v-menu>
</template>
