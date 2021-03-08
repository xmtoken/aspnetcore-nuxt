<script lang="ts">
import './AppDataTableColumnResizer.scss';
import './AppDataTableColumnSticker.scss';
import './AppDataTableColumnVisualizer.scss';

import { mdiMenuDown } from '@mdi/js';
import { PropType } from 'vue';
import { dragscroll } from 'vue-dragscroll';
import { AppDataTableHeader, AppDataTableHeaderState } from './AppDataTable';
import AppDataTableColumnResizer from './AppDataTableColumnResizer';
import AppDataTableColumnSticker from './AppDataTableColumnSticker';
import AppDataTableColumnVisualizer from './AppDataTableColumnVisualizer';
import mixins from '~/extensions/mixins';
import { Slotable } from '~/mixins/slotable';

type FooterProps = {
  disableItemsPerPage: boolean;
  showFirstLastPage: boolean;
};

export default mixins(Slotable).extend({
  inheritAttrs: false,
  directives: {
    dragscroll,
  },
  props: {
    draggable: {
      default: false,
      type: Boolean,
    },
    fixedSelect: {
      default: false,
      type: Boolean,
    },
    fixedSelectWidth: {
      default: 56,
      type: Number,
    },
    footerProps: {
      default(): FooterProps {
        return {
          disableItemsPerPage: true,
          showFirstLastPage: true,
        };
      },
      type: Object as PropType<FooterProps>,
    },
    headers: {
      default() {
        return [];
      },
      type: Array as PropType<AppDataTableHeader[]>,
    },
    items: {
      default() {
        return [];
      },
      type: Array,
    },
    mobileBreakpoint: {
      default: 0,
      type: [String, Number],
    },
    outlined: {
      default: false,
      type: Boolean,
    },
    showSelect: {
      default: false,
      type: Boolean,
    },
    singleSelect: {
      default: false,
      type: Boolean,
    },
    value: {
      default() {
        return [];
      },
      type: Array,
    },
    localStrageKey: {
      default() {
        return '';
        // return `xxx-${this.$route.path}-${this._uid}`;
      },
      type: [String, Function],
    },
  },
  data() {
    return {
      icons: {
        mdiMenuDown,
      },
      internalHeaderWidths: this.headers.map(x => x.width),
      columnState: [] as AppDataTableHeaderState[],
      columnResizer: {} as AppDataTableColumnResizer,
      columnSticker: {} as AppDataTableColumnSticker,
      columnVisualizer: {} as AppDataTableColumnVisualizer,
    };
  },
  created(): void {
    this.columnState.push(
      ...this.headers.map<AppDataTableHeaderState>(x => ({
        fixed: x.fixed ?? null,
        resizable: x.resizable ?? true,
        visible: true,
        width: x.width ? parseInt(x.width.toString()) : null,
      }))
    );
    this.columnResizer = new AppDataTableColumnResizer(this.columnState);
    this.columnSticker = new AppDataTableColumnSticker(this.columnState);
    this.columnVisualizer = new AppDataTableColumnVisualizer(this.columnState);

    this.columnResizer.setResizedEventListener(() => this.setColumnFixedStyle());
  },
  mounted(): void {
    this.setInitialColumnVisibility();
    this.setColumnFixedStyle();
    this.setResize();
  },
  destroyed(): void {
    this.columnResizer.dispose();
  },
  computed: {
    classes(): object {
      return {
        'v-data-footer__select--disabled': this.footerProps.disableItemsPerPage,
        'v-data-table--draggable': this.draggable,
        'v-data-table--outlined': this.outlined,
      };
    },
    isAllSelected(): boolean {
      return this.items.length === this.value.length;
    },
    isIndeterminateSelected(): boolean {
      return this.items.length !== this.value.length && this.value.length > 0;
    },
  },
  watch: {
    items(): void {
      this.$nextTick(() => {
        this.setInitialColumnVisibility();
        this.setColumnFixedStyle();
      });
    },
  },
  methods: {
    onInputBulkCheckbox(): void {
      if (this.items.length === this.value.length) {
        this.value.splice(0, this.value.length);
        this.$el.querySelectorAll('tbody > tr').forEach(x => x.classList.remove('v-data-table__selected'));
      } else {
        this.value.splice(0, this.value.length, ...this.items);
        this.$el.querySelectorAll('tbody > tr').forEach(x => x.classList.add('v-data-table__selected'));
      }
    },
    onInputRowCheckbox(val: boolean, item: any): void {
      if (val) {
        if (this.singleSelect) {
          this.value.splice(0, this.value.length, item);
        } else {
          this.value.push(item);
        }
      } else {
        this.value.splice(this.value.indexOf(item), 1);
      }
      this.$nextTick(() => {
        this.$el.querySelectorAll('tbody tr td:first-child > .v-simple-checkbox.v-data-table__selected').forEach(x => {
          x!.closest('tr')!.classList.add('v-data-table__selected');
        });
        this.$el.querySelectorAll('tbody tr td:first-child > .v-simple-checkbox:not(.v-data-table__selected)').forEach(x => {
          x!.closest('tr')!.classList.remove('v-data-table__selected');
        });
      });
    },
    getRowCheckboxClass(item: any): string {
      return this.value.includes(item) ? 'v-data-table__selected' : '';
    },
    getRowCheckboxState(item: any): boolean {
      return this.value.includes(item);
    },

    setResize(): void {
      this.columnResizer.setResizer(this.$el as HTMLElement);
    },
    setColumnFixedStyle(): void {
      const selection = {
        fixed: this.fixedSelect,
        visible: this.showSelect,
        width: this.fixedSelectWidth,
      };
      this.columnSticker.setStickyClass(this.$el);
      this.columnSticker.setStickyPosition(this.$el, selection);
      this.columnSticker.setStickyToSelection(this.$el, selection);
    },
    getColumnVisibility(i: number): boolean {
      return this.columnState[i].visible;
    },
    setColumnVisibility(i: number): void {
      this.columnState[i].visible = !this.columnState[i].visible;
      this.columnVisualizer.setVisibility(this.$el, i);
      this.setColumnFixedStyle();
    },
    setInitialColumnVisibility(): void {
      // if (this.localStrageKey in localStorage) {
      //   let values: boolean[];
      //   try {
      //     values = JSON.parse(localStorage.getItem(this.localStrageKey)!);
      //   } catch {
      //     return;
      //   }
      //   if (Array.isArray(values) && values.length === this.internalHeaderVisibles.length) {
      //     values.forEach((value, index) => {
      //       if (typeof value === 'boolean' && value === false) {
      //         this.internalHeaderVisibles[index] = value;
      //         this.setColumnVisibility(index, value);
      //       }
      //     });
      //   }
      // }
    },

    onUpdateSortBy(): void {
      console.log('onUpdateSortBy');
      this.$nextTick(() => this.columnSticker.setStickyClass(this.$el));
    },
    onUpdateSortDesc(): void {
      console.log('onUpdateSortDesc');
      this.$nextTick(() => this.columnSticker.setStickyClass(this.$el));
    },
  },
});
</script>

<template>
  <v-data-table v-dragscroll.x="{ active: draggable, target: '.v-data-table__wrapper' }" v-bind="$attrs" :class="classes" :footer-props="footerProps" :headers="headers" :items="items" :mobile-breakpoint="mobileBreakpoint" :show-select="showSelect" :single-select="singleSelect" v-on="$listeners" @update:sort-by="onUpdateSortBy" @update:sort-desc="onUpdateSortDesc">
    <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
    <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
      <slot v-bind="scope" :name="scopedSlotKey" />
    </template>
    <template #header.data-table-select="{}">
      <app-scope v-slot>
        <v-simple-checkbox :indeterminate="isIndeterminateSelected" :value="isAllSelected" @input="onInputBulkCheckbox" />
      </app-scope>
    </template>
    <template #item.data-table-select="{ item }">
      <app-scope v-slot>
        <v-simple-checkbox :class="getRowCheckboxClass(item)" :value="getRowCheckboxState(item)" @input="onInputRowCheckbox(!getRowCheckboxState(item), item)" />
      </app-scope>
    </template>
    <template #top="{}">
      <app-menu max-height="calc(100vh - 24px)" offset-y>
        <template #activator="{ on }">
          <v-btn text v-on="on">
            Columns
            <v-icon>
              {{ icons.mdiMenuDown }}
            </v-icon>
          </v-btn>
        </template>
        <v-list dense>
          <v-list-item v-for="(header, i) in headers" :key="i">
            <v-list-item-content>
              {{ header.text }}
            </v-list-item-content>
            <v-list-item-action class="mb-0" style="margin-top: 3px;">
              <app-scope v-slot>
                <v-switch dense :input-value="getColumnVisibility(i)" @change="setColumnVisibility(i)" />
              </app-scope>
            </v-list-item-action>
          </v-list-item>
        </v-list>
      </app-menu>
    </template>
  </v-data-table>
</template>

<style lang="scss" scoped>
.v-data-table.v-data-footer__select--disabled ::v-deep {
  .v-data-footer__select {
    display: none;
  }
}

.v-data-table.v-data-table--draggable ::v-deep {
  .v-data-table__wrapper {
    cursor: move;

    thead {
      cursor: auto;
    }
  }
}

.v-data-table.v-data-table--outlined ::v-deep {
  border: 1px solid rgba(0, 0, 0, 0.12);
}
</style>
