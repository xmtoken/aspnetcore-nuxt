import { Plugin } from '@nuxt/types';
import HttpStatus from 'http-status-codes';

const plugin: Plugin = ({ $axios }, inject) => {
  $axios.get('/enumerations').then(response => {
    if (response.status === HttpStatus.OK) {
      inject('enum', response.data);
    }
  });
  $axios.get('/metadata').then(response => {
    if (response.status === HttpStatus.OK) {
      inject('metadata', response.data);
    }
  });
  // return Promise.all([
  //   $axios.get('/enumerations').then(response => {
  //     if (response.status === HttpStatus.OK) {
  //       inject('enum', response.data);
  //     }
  //   }),
  //   $axios.get('/metadata').then(response => {
  //     if (response.status === HttpStatus.OK) {
  //       inject('metadata', response.data);
  //     }
  //   }),
  // ]);
};

export default plugin;
