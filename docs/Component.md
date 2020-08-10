# Component

## app-alert-snackbar

`Props`

| Name  | Type              | Default   | Description          |
| ----- | ----------------- | --------- | -------------------- |
| color | string            | undefined | equals v-alert.color |
| icon  | string \| boolean | undefined | equals v-alert.icon  |
| type  | string            | undefined | equals v-alert.type  |

`Methods`

| Signature                                   | Description |
| ------------------------------------------- | ----------- |
| close()                                     | x           |
| setErrors(Record<string, string[]>, string) | x           |
| setMessage(string \| null)                  | x           |

## app-birthday-field

`Props`

| Name                 | Type             | Default     | Description                     |
| -------------------- | ---------------- | ----------- | ------------------------------- |
| append-icon          | string           | mdiCalendar | equals v-text-field.append-icon |
| append-icon-tabindex | number           | -1          | tabindex of append-icon         |
| content-class        | string \| object | undefined   | class of inner v-text-field     |
| content-style        | string \| object | undefined   | style of inner v-text-field     |
| dense                | boolean          | false       | equals v-text-field.dense       |
| disabled             | boolean          | false       | equals v-text-field.disabled    |
| menu-props           | object           | {}          | props of inner v-menu           |
| picker-offset-left   | boolean          | false       | x                               |
| picker-offset-y      | boolean          | false       | x                               |
| picker-props         | object           | {}          | props of inner v-date-picker    |
| readonly             | boolean          | false       | equals v-text-field.readonly    |
| value                | string           | undefined   | equals v-text-field.value       |

## app-checkbox

`Props`

| Name | Type | Default | Description |
| ---- | ---- | ------- | ----------- |

## app-combobox

`Props`

| Name | Type | Default | Description |
| ---- | ---- | ------- | ----------- |

## app-date-field

`Props`

| Name                 | Type             | Default     | Description |
| -------------------- | ---------------- | ----------- | ----------- |
| append-icon          | string           | mdiCalendar | x           |
| append-icon-tabindex | number           | -1          | x           |
| content-class        | string \| object | undefined   | x           |
| content-style        | string \| object | undefined   | x           |
| dense                | boolean          | false       | x           |
| disabled             | boolean          | false       | x           |
| menu-props           | object           | {}          | x           |
| picker-offset-left   | boolean          | false       | x           |
| picker-offset-y      | boolean          | false       | x           |
| picker-props         | object           | {}          | x           |
| readonly             | boolean          | false       | x           |
| value                | string           | undefined   | x           |

## app-date-picker

`Props`

| Name       | Type     | Default                             | Description |
| ---------- | -------- | ----------------------------------- | ----------- |
| day-format | function | val => dayjs(val).date().toString() | x           |
| locale     | string   | ja                                  | x           |

## app-file-input

`Props`

| Name | Type | Default | Description |
| ---- | ---- | ------- | ----------- |

## app-menu

`Props`

| Name                   | Type    | Default   | Description |
| ---------------------- | ------- | --------- | ----------- |
| close-on-content-click | boolean | false     | x           |
| value                  | boolean | undefined | x           |

`Slots`

| Name      | Props                               | Description |
| --------- | ----------------------------------- | ----------- |
| activator | { open: function, opened: boolean } | x           |
| default   | { close: function }                 | x           |

## app-notifications

`Props`

| Name          | Type           | Default | Description |
| ------------- | -------------- | ------- | ----------- |
| notifications | Notification[] | []      | x           |

## app-number-field

`Props`

| Name   | Type             | Default | Description |
| ------ | ---------------- | ------- | ----------- |
| format | string           | '0'     | x           |
| value  | string \| number | x       | x           |

## app-password-field

`Props`

| Name                 | Type   | Default | Description |
| -------------------- | ------ | ------- | ----------- |
| append-icon-tabindex | number | -1      | x           |

## app-postal-code-field

`Props`

| Name                 | Type             | Default    | Description |
| -------------------- | ---------------- | ---------- | ----------- |
| address              | string           | undefined  | sync        |
| append-icon          | string           | mdiMagnify | x           |
| append-icon-tabindex | number           | -1         | x           |
| content-class        | string \| object | undefined  | x           |
| content-style        | string \| object | undefined  | x           |
| dense                | boolean          | false      | x           |
| disabled             | boolean          | false      | x           |
| menu-offset-y        | boolean          | false      | x           |
| readonly             | boolean          | false      | x           |
| value                | string           | undefined  | x           |

`Events`

| Name           | Value  | Description |
| -------------- | ------ | ----------- |
| update:address | string | x           |

## app-radio-group

`Props`

| Name | Type | Default | Description |
| ---- | ---- | ------- | ----------- |

## app-select

`Props`

| Name       | Type                      | Default           | Description |
| ---------- | ------------------------- | ----------------- | ----------- |
| menu-props | string \| array \| object | { offsetY: true } | x           |

## app-switch

`Props`

| Name | Type | Default | Description |
| ---- | ---- | ------- | ----------- |

## app-textarea

`Props`

| Name | Type | Default | Description |
| ---- | ---- | ------- | ----------- |

## app-text-field

vuetify の v-text-field を vee-validate の validation-provider でラップした高次コンポーネントです。
v-text-field の Props、Slots、Events はすべてサポートされます。

`Props`

| Name                        | Type             | Default   | Description                                                     |
| --------------------------- | ---------------- | --------- | --------------------------------------------------------------- |
| append-icon-tabindex        | number           | 0         | append-icon の tabindex を指定します。                          |
| append-outer-icon-tabindex  | number           | 0         | append-outer-icon の tabindex を指定します。                    |
| clear-icon-tabindex         | number           | 0         | clear-icon の tabindex を指定します。                           |
| disabled-required-marker    | boolean          | false     | x                                                               |
| prepend-icon-tabindex       | number           | 0         | prepend-icon の tabindex を指定します。                         |
| prepend-inner-icon-tabindex | number           | 0         | prepend-inner-icon の tabindex を指定します。                   |
| vee-custom-messages         | object           | undefined | validation-provider の custom-messages プロパティを指定します。 |
| vee-disabled-rules          | boolean          | false     |                                                                 |
| vee-name                    | string           | undefined | validation-provider の name プロパティを指定します。            |
| vee-rules                   | string \| object | undefined | validation-provider の rules プロパティを指定します。           |
| vee-vid                     | string           | undefined | validation-provider の vid プロパティを指定します。             |

## app-time-field

`Props`

| Name                 | Type             | Default     | Description |
| -------------------- | ---------------- | ----------- | ----------- |
| append-icon          | string           | mdiCalendar | x           |
| append-icon-tabindex | number           | -1          | x           |
| content-class        | string \| object | undefined   | x           |
| content-style        | string \| object | undefined   | x           |
| dense                | boolean          | false       | x           |
| disabled             | boolean          | false       | x           |
| menu-props           | object           | {}          | x           |
| picker-offset-left   | boolean          | false       | x           |
| picker-offset-y      | boolean          | false       | x           |
| picker-props         | object           | {}          | x           |
| readonly             | boolean          | false       | x           |
| value                | string           | undefined   | x           |

## app-username-field

`Props`

| Name | Type   | Default | Description |
| ---- | ------ | ------- | ----------- |
| type | string | email   | x           |

## app-versioning

`Props`

| Name | Type | Default | Description |
| ---- | ---- | ------- | ----------- |
