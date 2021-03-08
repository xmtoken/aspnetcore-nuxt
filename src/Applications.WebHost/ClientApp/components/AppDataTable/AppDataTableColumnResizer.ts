import { AppDataTableHeaderState } from './AppDataTable';

export default class AppDataTableColumnResizer {
  private readonly state: AppDataTableHeaderState[];
  private readonly windowMousemoveEventListener: (event: MouseEvent) => any;
  private resizedEventListener: (() => any) | null;
  private resizeDividerElement: HTMLElement | null;
  private resizeWrapperElement: HTMLElement | null;

  constructor(state: AppDataTableHeaderState[]) {
    this.state = state;
    this.windowMousemoveEventListener = event => {
      if (this.resizeWrapperElement && this.resizeDividerElement) {
        const width = event.clientX - this.resizeWrapperElement.getBoundingClientRect().left + this.resizeWrapperElement.scrollLeft;
        this.resizeDividerElement.style.left = `${width}px`;
      }
    };
    window.addEventListener('mousemove', this.windowMousemoveEventListener);

    this.resizedEventListener = null;
    this.resizeDividerElement = null;
    this.resizeWrapperElement = null;
  }

  setResizer(element: HTMLElement): void {
    let thElement: HTMLElement | null;
    let index: number;
    let pageX: number;
    let width: number;
    let resizerleft: number;

    element.classList.add('v-data-table--resizable');

    // set Resizing Divider
    let divider = element.querySelector<HTMLElement>('.v-data-table__wrapper > .col-resizer-divider');
    if (!divider) {
      divider = document.createElement('div');
      divider.classList.add('col-resizer-divider');
      divider.classList.add('d-none');
      element.querySelector('.v-data-table__wrapper')!.appendChild(divider);
    }
    this.resizeDividerElement = divider!;

    this.resizeWrapperElement = element.querySelector('.v-data-table__wrapper');

    Array.from(element.querySelectorAll('th'))
      .reverse()
      .slice(0, this.state.length)
      .reverse()
      .forEach((x, i) => {
        if (!this.state[i].resizable || x.querySelector('.col-resizer') !== null) {
          return;
        }

        const resizer = document.createElement('div');
        resizer.classList.add('col-resizer');

        resizer.addEventListener('mousedown', event => {
          event.stopPropagation();

          // v-data-table--resizing
          element.classList.add('v-data-table--resizing');

          //
          thElement = (event.target as HTMLElement).parentElement;
          index = i;
          pageX = event.pageX;
          width = thElement!.clientWidth;

          let wid = thElement!.clientWidth;
          let ele = thElement;
          while (true) {
            if (!ele!.previousElementSibling) {
              break;
            }
            wid += ele!.previousElementSibling.clientWidth;
            ele = ele!.previousElementSibling as HTMLElement;
          }
          this.resizeDividerElement.style.left = wid + 'px';
          this.resizeDividerElement.classList.remove('d-none');

          resizerleft = wid;

          window.addEventListener(
            'mouseup',
            event => {
              // v-data-table--resizing
              element.classList.remove('v-data-table--resizing');

              if (thElement) {
                thElement.querySelector<HTMLElement>('.col-resizer')!.style.height = '100%';

                const newwidth = width + event.pageX - pageX;
                thElement!.style.maxWidth = `${newwidth}px`;
                thElement!.style.minWidth = `${newwidth}px`;
                thElement!.style.width = `${newwidth}px`;

                this.resizeDividerElement.classList.add('d-none');

                this.state[index].width = newwidth;

                this.resizedEventListener?.call(this);
              }
              thElement = null;
            },
            { once: true }
          );
        });

        x.appendChild(resizer);
      });
  }

  setResizedEventListener(listener: () => any): void {
    this.resizedEventListener = listener;
  }

  dispose(): void {
    if (this.windowMousemoveEventListener) {
      window.removeEventListener('mousemove', this.windowMousemoveEventListener);
    }
  }
}
