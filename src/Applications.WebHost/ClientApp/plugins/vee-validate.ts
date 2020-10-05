import { Plugin } from '@nuxt/types';
import { extend, setInteractionMode } from 'vee-validate';
import { confirmed, length, max, min, required } from 'vee-validate/dist/rules';
import { between, birthday, date, email, maxValue, minValue, password, postalCode, time, username } from './validation-rules';

const plugin: Plugin = () => {
  setInteractionMode('eager');
  extend('confirmed', { ...confirmed, message: '{_field_}が{target}と一致しません。' });
  extend('length', { ...length, message: '{_field_}は{length}文字で入力してください。' });
  extend('max', { ...max, message: '{_field_}は{length}文字以下で入力してください。' });
  extend('min', { ...min, message: '{_field_}は{length}文字以上で入力してください。' });
  extend('required', { ...required, message: '{_field_}を入力してください。' });
  extend('between', between);
  extend('birthday', birthday);
  extend('date', date);
  extend('email', email);
  extend('max_value', maxValue);
  extend('min_value', minValue);
  extend('password', password);
  extend('postal_code', postalCode);
  extend('time', time);
  extend('username', username);
};

export default plugin;
