import HttpStatus from 'http-status-codes';
export default function ({ $axios }, inject) {
  return Promise.all([
    $axios.get('/enumerations').then(response => {
      if (response.status === HttpStatus.OK) {
        inject('enum', response.data);
      }
    }),
    $axios.get('/metadata').then(response => {
      if (response.status === HttpStatus.OK) {
        inject('metadata', response.data);
      }
    }),
  ]);
}
