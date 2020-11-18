import { DataTableHeader } from 'vuetify/types';

export type AppDataTableHeader = DataTableHeader & {
  fixed: 'left' | 'right' | null;
  resizable: boolean;
};

export type AppDataTableHeaderState = {
  fixed: 'left' | 'right' | null;
  resizable: boolean;
  visible: boolean;
  width: number | null;
};
