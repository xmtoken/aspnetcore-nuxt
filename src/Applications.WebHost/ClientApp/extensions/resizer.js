export default Object.freeze({
  resize() {
    const limit = window.innerHeight;
    const container = document.getElementById('container').clientHeight;
    const elements = Array.from(document.getElementsByClassName('fluid-100vh')).map(element => {
      if (element.classList.contains('v-data-table')) {
        return element.getElementsByClassName('v-data-table__wrapper')[0];
      }
      return element;
    });
    if (elements.length > 0) {
      const used = container - elements.map(x => x.clientHeight).reduce((x1, x2) => Math.max(x1, x2));
      for (const element of elements) {
        element.style.maxHeight = limit - used + 'px';
      }
    }
  },
});
