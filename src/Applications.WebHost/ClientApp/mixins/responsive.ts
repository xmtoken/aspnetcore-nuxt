import FontFaceObserver from 'fontfaceobserver';
import Vue from 'vue';
import responsive from '~/extensions/responsive';

export default Vue.extend({
  created(): void {
    const font = new FontFaceObserver('Noto Sans JP');
    const fontTestString = [
      // ‰p‘å•¶Žš
      ...[...Array(26)].map((_, i) => String.fromCharCode(i + 65)),
      // ‰p¬•¶Žš
      ...[...Array(26)].map((_, i) => String.fromCharCode(i + 97)),
      // •½‰¼–¼
      ...[...Array(86)].map((_, i) => String.fromCharCode(i + 12353)),
      // •Ð‰¼–¼
      ...[...Array(101)].map((_, i) => String.fromCharCode(i + 12443)),
    ].join('');
    font.load(fontTestString).then(() => responsive());
  },
});
