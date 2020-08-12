# Component

## app-alert-snackbar

`Props`

| Name  | Type              | Default   | Description |
| ----- | ----------------- | --------- | ----------- |
| color | string            | undefined |             |
| icon  | string \| boolean | undefined |             |
| type  | string            | undefined |             |

`Methods`

| Signature                                        | Description |
| ------------------------------------------------ | ----------- |
| close() void                                     |             |
| setErrors(Record<string, string[]>, string) void |             |
| setMessage(string \| null) void                  |             |

## app-birthday-field

`Props`

| Name                 | Type             | Default                               | Description |
| -------------------- | ---------------- | ------------------------------------- | ----------- |
| append-icon          | string           | mdiCalendar                           |             |
| append-icon-tabindex | number           | -1                                    |             |
| content-class        | string \| object | undefined                             |             |
| content-style        | string \| object | undefined                             |             |
| dense                | boolean          | false                                 |             |
| disabled             | boolean          | false                                 |             |
| menu-props           | object           | { openOnClick: false }                |             |
| picker-offset-left   | boolean          | false                                 |             |
| picker-offset-y      | boolean          | false                                 |             |
| picker-props         | object           | { max: dayjs().format('YYYY-MM-DD') } |             |
| readonly             | boolean          | false                                 |             |
| value                | string           | undefined                             |             |

## app-checkbox

`Props`

| Name | Type | Default | Description |
| ---- | ---- | ------- | ----------- |

## app-combobox

`Props`

| Name | Type | Default | Description |
| ---- | ---- | ------- | ----------- |

## app-data-table

`Props`

| Name         | Type        | Default                                                | Description |
| ------------ | ----------- | ------------------------------------------------------ | ----------- |
| footer-props | object      | { disableItemsPerPage: true, showFirstLastPage: true } |             |
| options      | DataOptions | {}                                                     |             |
| outlined     | boolean     | false                                                  |             |

`Events`

| Name   | Value | Description |
| ------ | ----- | ----------- |
| route  | Query |             |

## app-date-field

`Props`

| Name                 | Type             | Default                | Description |
| -------------------- | ---------------- | ---------------------- | ----------- |
| append-icon          | string           | mdiCalendar            |             |
| append-icon-tabindex | number           | -1                     |             |
| content-class        | string \| object | undefined              |             |
| content-style        | string \| object | undefined              |             |
| dense                | boolean          | false                  |             |
| disabled             | boolean          | false                  |             |
| menu-props           | object           | { openOnClick: false } |             |
| picker-offset-left   | boolean          | false                  |             |
| picker-offset-y      | boolean          | false                  |             |
| picker-props         | object           | { type: string }       |             |
| readonly             | boolean          | false                  |             |
| value                | string           | undefined              |             |

## app-date-picker

`Props`

| Name       | Type     | Default                             | Description |
| ---------- | -------- | ----------------------------------- | ----------- |
| day-format | function | val => dayjs(val).date().toString() |             |
| locale     | string   | ja                                  |             |

## app-email-field

`Props`

| Name                 | Type   | Default   | Description |
| -------------------- | ------ | --------- | ----------- |
| append-icon-tabindex | number | -1        |             |
| type                 | string | email     |             |
| value                | string | undefined |             |

## app-file-input

`Props`

| Name | Type | Default | Description |
| ---- | ---- | ------- | ----------- |

## app-menu

`Props`

| Name                   | Type    | Default   | Description |
| ---------------------- | ------- | --------- | ----------- |
| close-on-content-click | boolean | false     |             |
| value                  | boolean | undefined |             |

`Slots`

| Name      | Props                               | Description |
| --------- | ----------------------------------- | ----------- |
| activator | { open: function, opened: boolean } |             |
| default   | { close: function }                 |             |

## app-navigation-drawer

`Props`

| Name | Type | Default | Description |
| ---- | ---- | ------- | ----------- |

## app-notifications

`Props`

| Name          | Type           | Default | Description |
| ------------- | -------------- | ------- | ----------- |
| notifications | Notification[] | []      |             |

## app-number-field

`Props`

| Name   | Type             | Default   | Description |
| ------ | ---------------- | --------- | ----------- |
| format | string           | '0'       |             |
| value  | string \| number | undefined |             |

## app-password-field

`Props`

| Name                 | Type   | Default | Description |
| -------------------- | ------ | ------- | ----------- |
| append-icon-tabindex | number | -1      |             |

## app-postal-code-field

`Props`

| Name                 | Type             | Default    | Description |
| -------------------- | ---------------- | ---------- | ----------- |
| address              | string           | undefined  | sync        |
| append-icon          | string           | mdiMagnify |             |
| append-icon-tabindex | number           | -1         |             |
| content-class        | string \| object | undefined  |             |
| content-style        | string \| object | undefined  |             |
| dense                | boolean          | false      |             |
| disabled             | boolean          | false      |             |
| menu-offset-y        | boolean          | false      |             |
| readonly             | boolean          | false      |             |
| value                | string           | undefined  |             |

`Events`

| Name           | Value  | Description |
| -------------- | ------ | ----------- |
| update:address | string |             |

## app-radio-group

`Props`

| Name | Type | Default | Description |
| ---- | ---- | ------- | ----------- |

## app-select

`Props`

| Name       | Type   | Default           | Description |
| ---------- | ------ | ----------------- | ----------- |
| menu-props | object | { offsetY: true } |             |

## app-switch

`Props`

| Name | Type | Default | Description |
| ---- | ---- | ------- | ----------- |

## app-textarea

`Props`

| Name | Type | Default | Description |
| ---- | ---- | ------- | ----------- |

## app-text-field

`Props`

| Name                        | Type             | Default   | Description |
| --------------------------- | ---------------- | --------- | ----------- |
| append-icon-tabindex        | number           | 0         |             |
| append-outer-icon-tabindex  | number           | 0         |             |
| clear-icon-tabindex         | number           | 0         |             |
| disabled-required-marker    | boolean          | false     |             |
| label                       | string           | undefined |             |
| prepend-icon-tabindex       | number           | 0         |             |
| prepend-inner-icon-tabindex | number           | 0         |             |
| vee-custom-messages         | object           | undefined |             |
| vee-disabled-rules          | boolean          | false     |             |
| vee-name                    | string           | undefined |             |
| vee-rules                   | string \| object | undefined |             |
| vee-vid                     | string           | undefined |             |

## app-time-field

`Props`

| Name                 | Type             | Default                              | Description |
| -------------------- | ---------------- | ------------------------------------ | ----------- |
| append-icon          | string           | mdiClockOutline                      |             |
| append-icon-tabindex | number           | -1                                   |             |
| content-class        | string \| object | undefined                            |             |
| content-style        | string \| object | undefined                            |             |
| dense                | boolean          | false                                |             |
| disabled             | boolean          | false                                |             |
| menu-props           | object           | { openOnClick: false }               |             |
| picker-offset-left   | boolean          | false                                |             |
| picker-offset-y      | boolean          | false                                |             |
| picker-props         | object           | { format: '24hr', useSeconds: false} |             |
| readonly             | boolean          | false                                |             |
| value                | string           | undefined                            |             |

## app-url-field

`Props`

| Name                 | Type   | Default   | Description |
| -------------------- | ------ | --------- | ----------- |
| append-icon-tabindex | number | -1        |             |
| type                 | string | url       |             |
| value                | string | undefined |             |

## app-username-field

`Props`

| Name | Type   | Default | Description |
| ---- | ------ | ------- | ----------- |
| type | string | email   |             |

## app-versioning

`Props`

| Name  | Type    | Default | Description |
| ----- | ------- | ------- | ----------- |
| value | boolean | false   |             |
