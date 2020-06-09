<script>
import AppTextField from './AppTextField';
import { mdiMagnify } from '@mdi/js';
import HttpStatus from 'http-status-codes';
import { VueMaskDirective } from 'v-mask';
export default {
  components: {
    AppTextField,
  },
  directives: {
    //
    mask: VueMaskDirective,
  },
  inheritAttrs: false,
  props: {
    appendIconTabindex: {
      default: -1,
      type: Number,
    },
    contentClass: {
      default: undefined,
      type: String,
    },
    contentStyle: {
      default: undefined,
      type: String,
    },
    mask: {
      default: '###-####',
      type: String,
    },
    value: {
      default: undefined,
      type: undefined,
    },
  },
  data() {
    return {
      icons: {
        mdiMagnify,
      },
      loading: false,
      menu: false,
      items: [],
      selected: null,
    };
  },
  computed: {
    model: {
      /** @returns {Any} */
      get() {
        return this.value;
      },
      /** @param {Any} val */
      set(val) {
        this.$emit('change', val);
      },
    },
  },
  methods: {
    async search() {
      if (!this.model) {
        return;
      }
      try {
        this.loading = true;
        const data = await this.$axios.$get('/addresses', { params: { code: this.model } });
        if (data.status === HttpStatus.OK) {
          if (data.results.length === 1) {
            this.onSelected(this.items[0]);
          } else {
            this.items.splice(0, this.items.length, ...data.results);
            this.selected = [];
            this.menu = false;
            this.$nextTick(() => (this.menu = true));
          }
        }
      } finally {
        this.loading = false;
      }
    },
    onSelected(val) {
      this.$emit('select', val);
    },
  },
};
</script>

<template>
  <div>
    <app-text-field ref="field" v-model="model" v-mask="mask" v-bind="$attrs" :append-icon="icons.mdiMagnify" :append-icon-tabindex="appendIconTabindex" :class="contentClass" :loading="loading" :style="contentStyle" v-on="$listeners" @click:append="search">
      <slot v-for="slot in Object.keys($slots)" :slot="slot" :name="slot" />
      <template v-for="slot in Object.keys($scopedSlots)" :slot="slot" slot-scope="scope">
        <slot v-bind="scope" :name="slot" />
      </template>
    </app-text-field>
    <v-menu :activator="$refs.field" offset-x :open-on-click="false" :value="menu">
      <v-list dense>
        <v-list-item-group v-model="selected">
          <v-list-item v-for="(item, i) in items" :key="i">
            <v-list-item-content @click="onSelected(item)">
              {{ item.address1 + item.address2 + item.address3 }}
            </v-list-item-content>
          </v-list-item>
        </v-list-item-group>
      </v-list>
    </v-menu>
  </div>
</template>
