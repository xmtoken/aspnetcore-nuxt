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
    mask: VueMaskDirective,
  },
  inheritAttrs: false,
  props: {
    appendIcon: {
      default: mdiMagnify,
      type: String,
    },
    appendIconTabindex: {
      default: -1,
      type: Number,
    },
    autocomplete: {
      default: 'postal-code',
      type: String,
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
    mask: {
      default: '###-####',
      type: String,
    },
    menuOffsetY: {
      default: false,
      type: Boolean,
    },
    value: {
      default: undefined,
      type: undefined,
    },
  },
  data() {
    return {
      items: [],
      loading: false,
      menu: false,
      model: this.value,
      selected: null,
    };
  },
  computed: {
    /** @returns {Object} */
    listeners() {
      const listeners = { ...this.$listeners };
      delete listeners.input;
      return listeners;
    },
    /** @returns {String} */
    menuClasses() {
      if (this.menuOffsetY) {
        return this.dense ? 'v-menu__content--offset-y-dense' : 'v-menu__content--offset-y';
      }
      return undefined;
    },
  },
  watch: {
    model(val) {
      this.$emit('input', val);
    },
    value(val) {
      this.model = val;
    },
  },
  methods: {
    emit(val) {
      this.$emit('search', val);
    },
    async search() {
      try {
        this.loading = true;
        if (this.model) {
          const response = await this.$axios.get('/addresses', { params: { code: this.model } });
          if (response.status === HttpStatus.OK && response.data.status === HttpStatus.OK) {
            if (response.data.results.length === 1) {
              this.emit(response.data.results[0]);
            } else {
              this.items.splice(0, this.items.length, ...response.data.results);
              this.selected = [];
              this.menu = true;
            }
            return;
          }
        }
        this.emit({});
      } finally {
        this.loading = false;
      }
    },
  },
};
</script>

<template>
  <v-menu v-model="menu" :content-class="menuClasses" :open-on-click="false">
    <template v-slot:activator="{}">
      <app-text-field v-model="model" v-mask="mask" v-bind="$attrs" :append-icon="appendIcon" :append-icon-tabindex="appendIconTabindex" :autocomplete="autocomplete" :class="contentClass" :dense="dense" :loading="loading" :style="contentStyle" v-on="listeners" @click:append="search">
        <slot v-for="slot in Object.keys($slots)" :slot="slot" :name="slot" />
        <template v-for="slot in Object.keys($scopedSlots)" :slot="slot" slot-scope="scope">
          <slot v-bind="scope" :name="slot" />
        </template>
      </app-text-field>
    </template>
    <v-list dense>
      <v-list-item-group v-model="selected">
        <v-list-item v-for="(item, i) in items" :key="i">
          <v-list-item-content @click="emit(item)">
            {{ item.address1 + item.address2 + item.address3 }}
          </v-list-item-content>
        </v-list-item>
      </v-list-item-group>
    </v-list>
  </v-menu>
</template>

<style lang="scss" scoped>
.v-menu__content--offset-y ::v-deep {
  margin-top: 45px;
}

.v-menu__content--offset-y-dense ::v-deep {
  margin-top: 29px;
}
</style>
