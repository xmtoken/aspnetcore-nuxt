import { extend, setInteractionMode } from 'vee-validate';

setInteractionMode('eager');

// vee-validate rules

import { between } from 'vee-validate/dist/rules';
extend('between', { ...between, message: '{_field_}は{min}から{max}までの値を入力してください。' });

import { confirmed } from 'vee-validate/dist/rules';
extend('confirmed', { ...confirmed, message: '{_field_}が{target}と一致しません。' });

import { length } from 'vee-validate/dist/rules';
extend('length', { ...length, message: '{_field_}は{length}文字で入力してください。' });

import { max } from 'vee-validate/dist/rules';
extend('max', { ...max, message: '{_field_}は{length}文字以下で入力してください。' });

import { max_value } from 'vee-validate/dist/rules';
extend('max_value', { ...max_value, message: '{_field_}は{max}以下の値を入力してください。' });

import { min } from 'vee-validate/dist/rules';
extend('min', { ...min, message: '{_field_}は{length}文字以上で入力してください。' });

import { min_value } from 'vee-validate/dist/rules';
extend('min_value', { ...min_value, message: '{_field_}は{min}以上の値を入力してください。' });

import { required } from 'vee-validate/dist/rules';
extend('required', { ...required, message: '{_field_}を入力してください。' });

// custom-validate rules

import { birthday } from './validation-rules';
extend('birthday', birthday);

import { date } from './validation-rules';
extend('date', date);

import { email } from './validation-rules';
extend('email', email);

import { password } from './validation-rules';
extend('password', password);

import { postal_code } from './validation-rules';
extend('postal_code', postal_code);

import { time } from './validation-rules';
extend('time', time);

import { username } from './validation-rules';
extend('username', username);
