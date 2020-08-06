import FontFaceObserver from 'fontfaceobserver';
import Vue from 'vue';
import responsive from '~/extensions/responsive';

export default Vue.extend({
  created(): void {
    const font = new FontFaceObserver('Noto Sans JP');
    const fontTestString = [
      // 英大文字
      ...[...Array(26)].map((_, i) => String.fromCharCode(i + 65)),
      // 英小文字
      ...[...Array(26)].map((_, i) => String.fromCharCode(i + 97)),
      // 平仮名
      ...[...Array(86)].map((_, i) => String.fromCharCode(i + 12353)),
      // 片仮名
      ...[...Array(101)].map((_, i) => String.fromCharCode(i + 12443)),
    ].join('');
    font.load(fontTestString).then(() => responsive());
  },
});
