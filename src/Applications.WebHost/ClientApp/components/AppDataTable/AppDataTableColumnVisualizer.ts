import { AppDataTableHeaderState } from './AppDataTable';

export default class AppDataTableColumnVisualizer {
  private readonly state: AppDataTableHeaderState[];

  constructor(state: AppDataTableHeaderState[]) {
    this.state = state;
  }

  setVisibility(element: Element, i: number): void {
    const index = this.state.length - i;
    element.querySelectorAll(`th:nth-last-child(${index}), td:nth-last-child(${index})`).forEach(el => {
      if (this.state[i].visible) {
        el.classList.remove('invisible');
      } else {
        el.classList.add('invisible');
      }
    });
  }
}
