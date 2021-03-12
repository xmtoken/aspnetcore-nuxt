import { Plugin } from '@nuxt/types';
import dayjs from 'dayjs';
import customParseFormat from 'dayjs/plugin/customParseFormat';
import isBetween from 'dayjs/plugin/isBetween';
import isSameOrAfter from 'dayjs/plugin/isSameOrAfter';
import isSameOrBefore from 'dayjs/plugin/isSameOrBefore';

require('dayjs/locale/ja');

const plugin: Plugin = () => {
  dayjs.extend(customParseFormat);
  dayjs.extend(isBetween);
  dayjs.extend(isSameOrAfter);
  dayjs.extend(isSameOrBefore);
  dayjs.locale('ja');
};

export default plugin;
