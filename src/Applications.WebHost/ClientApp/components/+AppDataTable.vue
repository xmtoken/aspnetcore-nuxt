<script lang="ts">
import deepEqual from 'deep-equal';
import { PropType } from 'vue';
import { DataOptions } from 'vuetify/types';
import mixins from '~/extensions/mixins';
import slotable from '~/mixins/slotable';
import { Query } from '~/types/vue-router';

const QUERY_KEY_PAGE_INDEX = 'index';
const QUERY_KEY_SORT_PROPERTY = 'sort';
const QUERY_VAL_SORT_ASCENDING = 'asc'; // also supports `ascending`, ignore case.
const QUERY_VAL_SORT_DESCENDING = 'desc'; // also supports `descending`, ignore case.
const QUERY_VAL_SEPARATOR = ',';

export default mixins(slotable).extend({
  inheritAttrs: false,
  props: {
    footerProps: {
      default(): object {
        return {
          showFirstLastPage: true,
        };
      },
      type: Object as PropType<object>,
    },
    options: {
      default(): DataOptions {
        return {} as DataOptions;
      },
      type: Object as PropType<DataOptions>,
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
    classes(): object {
      return {
        'v-data-table--outlined': this.outlined,
      };
    },
  },
  watch: {
    '$route.query'(newQuery: Query, oldQuery: Query): void {
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
      handler(val: DataOptions): void {
        this.$emit('update:options', val);
      },
    },
    options: {
      deep: true,
      handler(val: DataOptions): void {
        this.internalOptions = val;
      },
    },
  },
  created(): void {
    this.internalOptions = {
      ...this.internalOptions,
      ...this.getOptionsFromQuery(this.$route.query),
    };
  },
  methods: {
    getOptionsFromQuery(query: Query): Options {
      const indexQueries = query[QUERY_KEY_PAGE_INDEX] ?? [];
      const indexQuery = Array.isArray(indexQueries) ? indexQueries[0] ?? '' : indexQueries;
      const index = Number.parseInt(indexQuery);
      const page = Number.isNaN(index) || index < 1 ? 1 : index;

      const sortQueries = query[QUERY_KEY_SORT_PROPERTY] ?? [];
      const sortQueriesArray = Array.isArray(sortQueries) ? sortQueries : [sortQueries];

      const sortBy = sortQueriesArray.map(x => x?.split(QUERY_VAL_SEPARATOR)[0] ?? '');
      const sortDesc = sortQueriesArray.map(x => x?.split(QUERY_VAL_SEPARATOR)[1]).map(x => x === QUERY_VAL_SORT_DESCENDING);

      return {
        page,
        sortBy,
        sortDesc,
      };
    },
    async onUpdateOptions(options: DataOptions): Promise<void> {
      const oldOptions = this.getOptionsFromQuery(this.$route.query);
      const newOptions = {
        page: options.page,
        sortBy: options.sortBy,
        sortDesc: options.sortDesc,
      };
      if (deepEqual(newOptions, oldOptions)) {
        return;
      }

      const sort = options.sortBy.map((x, i) => x + (options.sortDesc[i] ? `,${QUERY_VAL_SORT_DESCENDING}` : ''));
      const newQuery = {
        ...this.$route.query,
        [QUERY_KEY_PAGE_INDEX]: options.page > 1 ? options.page.toString() : undefined,
        [QUERY_KEY_SORT_PROPERTY]: sort.length > 0 ? sort : undefined,
      };
      await this.$router.push({ query: newQuery });
      this.$emit('routed', newQuery);
    },
  },
});
</script>

<template>
  <v-data-table v-bind="$attrs" :class="classes" :footer-props="footerProps" :options.sync="internalOptions" v-on="$listeners" @update:options="onUpdateOptions">
    <slot v-for="slotKey in slotKeys" :slot="slotKey" :name="slotKey" />
    <template v-for="scopedSlotKey in scopedSlotKeys" :slot="scopedSlotKey" slot-scope="scope">
      <slot v-bind="scope" :name="scopedSlotKey" />
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
