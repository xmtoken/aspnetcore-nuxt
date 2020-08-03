import { Plugin } from '@nuxt/types';
import { extend, setInteractionMode } from 'vee-validate';
import { between, confirmed, length, max, max_value as maxValue, min, min_value as minValue, required } from 'vee-validate/dist/rules';
import { birthday, date, email, password, postalCode, time, username } from './validation-rules';

const plugin: Plugin = () => {
  setInteractionMode('eager');
  extend('between', { ...between, message: '{_field_}は{min}から{max}までの値を入力してください。' });
  extend('confirmed', { ...confirmed, message: '{_field_}が{target}と一致しません。' });
  extend('length', { ...length, message: '{_field_}は{length}文字で入力してください。' });
  extend('max', { ...max, message: '{_field_}は{length}文字以下で入力してください。' });
  extend('max_value', { ...maxValue, message: '{_field_}は{max}以下の値を入力してください。' });
  extend('min', { ...min, message: '{_field_}は{length}文字以上で入力してください。' });
  extend('min_value', { ...minValue, message: '{_field_}は{min}以上の値を入力してください。' });
  extend('required', { ...required, message: '{_field_}を入力してください。' });
  extend('birthday', birthday);
  extend('date', date);
  extend('email', email);
  extend('password', password);
  extend('postal_code', postalCode);
  extend('time', time);
  extend('username', username);
};

export default plugin;
