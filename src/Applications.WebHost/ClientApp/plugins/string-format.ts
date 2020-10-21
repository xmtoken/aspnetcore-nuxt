import { Plugin } from '@nuxt/types';
import format from 'string-format';

const plugin: Plugin = () => {
  format.extend(String.prototype, {});
};

export default plugin;
