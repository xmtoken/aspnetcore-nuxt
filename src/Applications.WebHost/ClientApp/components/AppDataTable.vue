<script lang="ts">
import deepEqual from 'deep-equal';
import { PropType } from 'vue';
import { DataOptions } from 'vuetify/types';
import mixins from '~/extensions/mixins';
import slotable from '~/mixins/slotable';
import { Query } from '~/types/vue-router';

const QUERY_KEY_PAGING_INDEX = 'index';
const QUERY_KEY_SORTING = 'sort';
const QUERY_VAL_SORTING_DESC = 'desc';
const QUERY_VAL_SEPARATOR = ',';

type FooterProps = {
  disableItemsPerPage: boolean;
  showFirstLastPage: boolean;
};

type Options = {
  page: number;
  sortBy: string[];
  sortDesc: boolean[];
};

export default mixins(slotable).extend({
  inheritAttrs: false,
  props: {
    footerProps: {
      default(): FooterProps {
        return {
          disableItemsPerPage: true,
          showFirstLastPage: true,
        };
      },
      type: Object as PropType<FooterProps>,
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
        'v-data-footer__select--disabled': this.footerProps.disableItemsPerPage,
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
      const indexQueries = query[QUERY_KEY_PAGING_INDEX] ?? [];
      const indexQuery = Array.isArray(indexQueries) ? indexQueries[0] ?? '' : indexQueries;
      const index = Number.parseInt(indexQuery);
      const page = Number.isNaN(index) || index < 1 ? 1 : index;

      const sortQueries = query[QUERY_KEY_SORTING] ?? [];
      const sortQueriesArray = Array.isArray(sortQueries) ? sortQueries : [sortQueries];

      const sortBy = sortQueriesArray.map(x => x?.split(QUERY_VAL_SEPARATOR)[0] ?? '');
      const sortDesc = sortQueriesArray.map(x => x?.split(QUERY_VAL_SEPARATOR)[1]).map(x => x === QUERY_VAL_SORTING_DESC);

      return {
        page,
        sortBy,
        sortDesc,
      };
    },
    onUpdateOptions(options: DataOptions): void {
      const oldOptions = this.getOptionsFromQuery(this.$route.query);
      const newOptions = {
        page: options.page,
        sortBy: options.sortBy,
        sortDesc: options.sortDesc,
      };
      if (deepEqual(newOptions, oldOptions)) {
        return;
      }

      const sorting = options.sortBy.map((sortKey, i) => sortKey + (options.sortDesc[i] ? QUERY_VAL_SEPARATOR + QUERY_VAL_SORTING_DESC : ''));
      const newQuery = {
        ...this.$route.query,
        [QUERY_KEY_PAGING_INDEX]: options.page > 1 ? options.page.toString() : undefined,
        [QUERY_KEY_SORTING]: sorting.length > 0 ? sorting : undefined,
      };

      this.$router.push({ query: newQuery });
      this.$emit('route', newQuery);
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
.v-data-table.v-data-footer__select--disabled ::v-deep {
  .v-data-footer__select {
    display: none;
  }
}

.v-data-table.v-data-table--outlined ::v-deep {
  border: 1px solid rgba(0, 0, 0, 0.12);
}
</style>
