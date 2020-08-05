<script lang="ts">
import { mdiMagnify } from '@mdi/js';
import HttpStatus from 'http-status-codes';
import { VueMaskDirective } from 'v-mask';
import mixins from '~/extensions/mixins';
import slotable from '~/mixins/slotable';
import { AddressModel } from '~/types/api';

export default mixins(slotable).extend({
  directives: {
    mask: VueMaskDirective,
  },
  inheritAttrs: false,
  props: {
    address: {
      default: undefined,
      type: String,
    },
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
      type: [Object, String],
    },
    contentStyle: {
      default: undefined,
      type: [Object, String],
    },
    dense: {
      default: false,
      type: Boolean,
    },
    disabled: {
      default: false,
      type: Boolean,
    },
    menuOffsetY: {
      default: false,
      type: Boolean,
    },
    readonly: {
      default: false,
      type: Boolean,
    },
    value: {
      default: undefined,
      type: String,
    },
  },
  data() {
    return {
      items: [] as AddressModel[],
      loading: false,
      menu: false,
      model: this.value as string | null,
      selected: [] as AddressModel[],
    };
  },
  computed: {
    appendIconInternal(): string | null {
      return this.disabled || this.readonly ? null : this.appendIcon;
    },
    listeners(): Record<string, Function | Function[]> {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      return listeners;
    },
    mask(): string {
      return '###-####';
    },
    menuNudgeBottom(): number {
      return this.menuOffsetY ? (this.dense ? 29 : 45) : 0;
    },
  },
  watch: {
    model(val: string | null): void {
      this.$emit('input', val);
    },
    value(val: string | null): void {
      this.model = val;
    },
  },
  methods: {
    emit(val: AddressModel): void {
      if (val.postalCode) {
        this.$emit('update:address', val.address);
      }
    },
    async search(): Promise<void> {
      try {
        this.loading = true;
        if (this.model) {
          const response = await this.$axios.get<AddressModel[]>(`/addresses/${this.model}`);
          switch (response.status) {
            case HttpStatus.OK:
              this.items.splice(0, this.items.length, ...response.data);
              break;
            case HttpStatus.NO_CONTENT:
              this.items.splice(0, this.items.length, { postalCode: null, address: '該当の郵便番号は見つかりませんでした。' });
              break;
          }
          this.selected = [];
          this.menu = true;
        }
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
      <app-text-field v-model="model" v-mask="mask" v-bind="$attrs" :append-icon="appendIconInternal" :append-icon-tabindex="appendIconTabindex" :class="contentClass" :dense="dense" :disabled="disabled" :loading="loading" :readonly="readonly" :style="contentStyle" v-on="listeners" @click:append="search">
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
