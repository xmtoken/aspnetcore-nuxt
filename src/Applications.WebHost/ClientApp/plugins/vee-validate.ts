import { Plugin } from '@nuxt/types';
import { extend, setInteractionMode } from 'vee-validate';
import { confirmed, length, max, min, required } from 'vee-validate/dist/rules';
import { between, birthday, date, email, maxValue, minValue, postalCode, time } from './validation-rules';
import * as MESSAGES from '~/resources/validation-rule-messages';

const plugin: Plugin = () => {
  setInteractionMode('eager');
  extend('between', { ...between, message: MESSAGES.BETWEEN });
  extend('birthday', { ...birthday, message: MESSAGES.BIRTHDAY });
  extend('confirmed', { ...confirmed, message: MESSAGES.CONFIRMED });
  extend('date', { ...date, message: MESSAGES.DATE });
  extend('email', { ...email, message: MESSAGES.EMAIL });
  extend('length', { ...length, message: MESSAGES.LENGTH });
  extend('max', { ...max, message: MESSAGES.MAX });
  extend('max_value', { ...maxValue, message: MESSAGES.MAX_VALUE });
  extend('min', { ...min, message: MESSAGES.MIN });
  extend('min_value', { ...minValue, message: MESSAGES.MIN_VALUE });
  extend('postal_code', { ...postalCode, message: MESSAGES.POSTAL_CODE });
  extend('required', { ...required, message: MESSAGES.REQUIRED });
  extend('time', { ...time, message: MESSAGES.TIME });
};

export default plugin;
