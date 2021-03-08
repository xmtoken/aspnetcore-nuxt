import { sum } from 'lodash';
import { AppDataTableHeaderState } from './AppDataTable';

export default class AppDataTableColumnSticker {
  private readonly state: AppDataTableHeaderState[];

  constructor(state: AppDataTableHeaderState[]) {
    this.state = state;
  }

  setStickyClass(element: Element): void {
    this.state.forEach((header, i) => {
      const index = this.state.length - i;
      element.querySelectorAll(`th:nth-last-child(${index}), td:nth-last-child(${index})`).forEach(el => {
        if (header.fixed === null) {
          el.classList.remove('sticky');
        } else {
          el.classList.add('sticky');
        }
      });
    });
  }

  setStickyPosition(element: Element, selection: { fixed: boolean; visible: boolean; width: number }): void {
    this.state.forEach((header, i) => {
      if (header.fixed === 'left') {
        const widths = this.state.slice(0, i).map(x => (x.fixed === 'left' && x.visible ? parseInt((x.width ?? 0).toString()) : 0));
        const width = sum(widths) + (selection.visible && selection.fixed ? selection.width : 0);
        const index = this.state.length - i;
        element.querySelectorAll<HTMLElement>(`th:nth-last-child(${index}), td:nth-last-child(${index})`).forEach(el => {
          el.style.left = `${width}px`;
          el.style.right = '';
        });
      } else if (header.fixed === 'right') {
        const widths = this.state
          .slice()
          .reverse()
          .slice(0, this.state.length - i - 1)
          .map(x => (x.fixed === 'right' && x.visible ? parseInt((x.width ?? 0).toString()) : 0));
        const width = sum(widths);
        const index = this.state.length - i;
        element.querySelectorAll<HTMLElement>(`th:nth-last-child(${index}), td:nth-last-child(${index})`).forEach(el => {
          el.style.left = '';
          el.style.right = `${width}px`;
        });
      } else {
        const index = this.state.length - i;
        element.querySelectorAll<HTMLElement>(`th:nth-last-child(${index}), td:nth-last-child(${index})`).forEach(el => {
          el.style.left = '';
          el.style.right = '';
        });
      }
    });
  }

  setStickyToSelection(element: Element, selection: { fixed: boolean; visible: boolean; width: number }): void {
    if (selection.visible) {
      element.querySelectorAll<HTMLElement>('th:first-child, td:first-child').forEach(el => {
        if (selection.fixed) {
          el.classList.add('sticky');
          el.style.left = '0px';
        } else {
          el.classList.remove('sticky');
          el.style.left = '';
        }
        el.style.maxWidth = `${selection.width}px`;
      });
    }
  }
}
