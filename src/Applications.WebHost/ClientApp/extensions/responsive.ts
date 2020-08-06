export function getResponsiveElements(): Element[] {
  // 高さを自動調整するエレメントを取得します。
  // v-data-table の様に内部エレメントに対して高さの補正を掛ける場合には以下で該当するエレメントを取得します。
  return Array.from(document.getElementsByClassName('responsive-100vh')).map(element => {
    if (element.classList.contains('v-data-table')) {
      return element.getElementsByClassName('v-data-table__wrapper')[0];
    }
    return element;
  });
}

export function setResponsiveMaxHeight(elements: Element[]): void {
  const buffer = 1;
  const appbar = document.getElementById('app-bar')!.clientHeight + 1;
  const used = document.getElementById('container')!.clientHeight - elements.map(x => x.clientHeight).reduce((x1, x2) => Math.max(x1, x2));
  for (const element of elements) {
    // calc メソッドを使うことでウィンドウの高さが変更されたときも自動で調整を行います。
    // ウィンドウの幅が変更されることにより他のエレメントの高さが変更になるケースは layouts/default で v-resize イベントをフックして処理します。
    (element as HTMLElement).style.maxHeight = `calc(100vh - ${buffer}px - ${appbar}px - ${used}px`;
  }
}
