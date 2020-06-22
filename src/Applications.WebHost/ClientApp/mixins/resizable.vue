<script>
import { Resizer } from '~/extensions';
export default {
  inheritAttrs: false,
  mounted() {
    this.$nextTick(() => Resizer.resize());
  },
};
</script>
