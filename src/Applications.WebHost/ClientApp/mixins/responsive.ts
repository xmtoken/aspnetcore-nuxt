import FontFaceObserver from 'fontfaceobserver';
import Vue from 'vue';
import responsive from '~/extensions/responsive';

export default Vue.extend({
  created(): void {
    const font = new FontFaceObserver('Noto Sans JP');
    font.load().then(() => responsive());
  },
});
