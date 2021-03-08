<template>
  <div>
    <h1>AppAutocomplete</h1>
    <h2>Mixins</h2>
    <h2>Props</h2>
    <v-sheet outlined rounded>
      <!-- prettier-ignore -->
      <v-data-table disable-pagination
                    disable-sort
                    :headers="[
                      { text: 'Name', value: 'name' },
                      { text: 'Type', value: 'type' },
                      { text: 'Default', value: 'default' },
                      { text: 'Description', value: 'description' }
                    ]"
                    hide-default-footer
                    :items="[
                      {
                        name: 'hideDetails',
                        type: 'string | boolean',
                        default: 'false',
                        description: '',
                      },
                      {
                        name: '',
                        type: '',
                        default: '',
                        description: '',
                      }]"
      />
    </v-sheet>
    <h2>Events</h2>
    <h2>Slots</h2>
  </div>
</template>
