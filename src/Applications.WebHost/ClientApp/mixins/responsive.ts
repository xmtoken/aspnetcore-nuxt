import FontFaceObserver from 'fontfaceobserver';
import Vue from 'vue';
import responsive from '~/extensions/responsive';

export default Vue.extend({
  created(): void {
    const font = new FontFaceObserver('Noto Sans JP');
    const fontTestString = [
      // �p�啶��
      ...[...Array(26)].map((_, i) => String.fromCharCode(i + 65)),
      // �p������
      ...[...Array(26)].map((_, i) => String.fromCharCode(i + 97)),
      // ������
      ...[...Array(86)].map((_, i) => String.fromCharCode(i + 12353)),
      // �Љ���
      ...[...Array(101)].map((_, i) => String.fromCharCode(i + 12443)),
    ].join('');
    font.load(fontTestString).then(() => responsive());
  },
});
