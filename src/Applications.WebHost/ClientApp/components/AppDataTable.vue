<script>
const QUERY_KEY_PAGE_INDEX = 'index';
const QUERY_KEY_SORT_PROPERTY = 'sort';
const QUERY_KEY_SORT_DIRECTION = 'sort-direction';
const QUERY_VAL_SORT_ASCENDING = 'ascending';
const QUERY_VAL_SORT_DESCENDING = 'descending';
const QUERY_VAL_SEPARATOR = ',';
import { Slotable } from '~/mixins';
import deepEqual from 'deep-equal';
export default {
  mixins: [
    //
    Slotable,
  ],
  inheritAttrs: false,
  props: {
    footerProps: {
      default() {
        return {
          showFirstLastPage: true,
        };
      },
      type: Object,
    },
    options: {
      default() {
        return {};
      },
      type: Object,
    },
    outlined: {
      default: false,
      type: Boolean,
    },
  },
  data() {
    return {
      internalOptions: this.options,
    };
  },
  computed: {
    classes() {
      return {
        'v-data-table--outlined': this.outlined,
      };
    },
  },
  watch: {
    '$route.query'(newQuery, oldQuery) {
      const newOptions = this.getOptionsFromQuery(newQuery);
      const oldOptions = this.getOptionsFromQuery(oldQuery);
      if (deepEqual(newOptions, oldOptions)) {
        return;
      }
      this.internalOptions = {
        ...this.internalOptions,
        ...newOptions,
      };
    },
    internalOptions: {
      deep: true,
      handler(val) {
        this.$emit('update:options', val);
      },
    },
    options: {
      deep: true,
      handler(val) {
        this.internalOptions = val;
      },
    },
  },
  created() {
    this.internalOptions = {
      ...this.internalOptions,
      ...this.getOptionsFromQuery(this.$route.query),
    };
  },
  methods: {
    getOptionsFromQuery(query) {
      const index = Number.parseInt(query[QUERY_KEY_PAGE_INDEX]);
      const directions = query[QUERY_KEY_SORT_DIRECTION]?.split(QUERY_VAL_SEPARATOR) ?? [];
      const page = Number.isNaN(index) || index < 1 ? 1 : index;
      const sortBy = query[QUERY_KEY_SORT_PROPERTY]?.split(QUERY_VAL_SEPARATOR) ?? [];
      const sortDesc = directions
        .map(direction => {
          switch (direction) {
            case QUERY_VAL_SORT_ASCENDING:
              return false;
            case QUERY_VAL_SORT_DESCENDING:
              return true;
            default:
              return undefined;
          }
        })
        .filter(direction => direction !== undefined);
      return {
        page,
        sortBy,
        sortDesc,
      };
    },
    async onUpdateOptions(options) {
      const oldOptions = this.getOptionsFromQuery(this.$route.query);
      const newOptions = {
        page: options.page,
        sortBy: options.sortBy,
        sortDesc: options.sortDesc,
      };
      if (deepEqual(newOptions, oldOptions)) {
        return;
      }
      const directions = options.sortDesc.map(x => (x ? QUERY_VAL_SORT_DESCENDING : QUERY_VAL_SORT_ASCENDING));
      const newQuery = {
        ...this.$route.query,
        [QUERY_KEY_PAGE_INDEX]: options.page > 1 ? options.page : undefined,
        [QUERY_KEY_SORT_PROPERTY]: options.sortBy.length > 0 ? options.sortBy.join(QUERY_VAL_SEPARATOR) : undefined,
        [QUERY_KEY_SORT_DIRECTION]: directions.length > 0 ? directions.join(QUERY_VAL_SEPARATOR) : undefined,
      };
      await this.$router.push({ query: newQuery });
      this.$emit('routed', newQuery);
    },
  },
};
</script>

<template>
  <v-data-table v-bind="$attrs" :class="classes" :footer-props="footerProps" :options.sync="internalOptions" v-on="$listeners" @update:options="onUpdateOptions">
    <slot v-for="slot in slotKeys" :slot="slot" :name="slot" />
    <template v-for="slot in scopedSlotKeys" :slot="slot" slot-scope="scope">
      <slot v-bind="scope" :name="slot" />
    </template>
  </v-data-table>
</template>

<style lang="scss" scoped>
.v-data-table ::v-deep {
  .v-data-footer__select {
    display: none;
  }
}
</style>
