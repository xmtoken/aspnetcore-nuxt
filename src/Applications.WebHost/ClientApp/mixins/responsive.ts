import FontFaceObserver from 'fontfaceobserver';
import Vue from 'vue';
import * as Responsive from '~/extensions/responsive';

export default Vue.extend({
  async mounted(): Promise<void> {
    const elements = Responsive.getResponsiveElements();
    try {
      // 高さを自動調整するエレメント以外で使用されている高さを計算するために mounted 時には一時的に非表示とします。
      for (const element of elements) {
        (element as HTMLElement).style.display = 'none';
      }

      // Font によってエレメントの高さが変わるため Font がアクティブなってから高さの計算を行います。
      const font = new FontFaceObserver('Noto Sans JP');
      const fontTestString = [
        ...[...Array(95)].map((_, i) => String.fromCharCode(i + 32)), // 英数字記号
        ...[...Array(86)].map((_, i) => String.fromCharCode(i + 12353)), // 平仮名
        ...[...Array(103)].map((_, i) => String.fromCharCode(i + 12441)), // 片仮名
      ].join('');
      await font.load(fontTestString).then(() => {
        Responsive.setResponsiveMaxHeight(elements);
      });
    } finally {
      for (const element of elements) {
        (element as HTMLElement).style.display = '';
      }
    }
  },
});
